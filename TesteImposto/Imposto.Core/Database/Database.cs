using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.DBase
{
    class Database
    {
        public Database()
        {

        }

        public static string DatConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ImpostoDB"].ConnectionString;
            }
        }


        //===[ DatSPReturnValue ]==========================================================

        public static Int32 DatSPReturnValue(string strStoredProc, DataTable dt)
        {
            return DatSPReturnValue(strStoredProc, dt.Rows[0]);
        }

        public static Int32 DatSPReturnValue(string strStoredProc, DataRow dr)
        {
            return DatSPReturnValue(strStoredProc, f_DataRowToArray(dr));
        }

        public static Int32 DatSPReturnValue(string strStoredProc)
        {
            return DatSPReturnValue(strStoredProc, new object[0, 0]);
        }

        public static Int32 DatSPReturnValue(string strStoredProc, object[,] objParam, string retorno = "Return")
        {
            SqlConnection cn = new SqlConnection(DatConnectionString);
            SqlCommand cmd = new SqlCommand(strStoredProc, cn);
            SqlParameter objSqlPar;
            Int32 intReturn = 0;

            try
            {
                cn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;
                objSqlPar = cmd.Parameters.Add(retorno, SqlDbType.Int);
                objSqlPar.Direction = ParameterDirection.ReturnValue;

                f_CmdParametros(cmd, objParam);

                var result = cmd.ExecuteScalar();

                intReturn = (Int32)cmd.Parameters[retorno].Value;
                cn.Close();
            }

            catch (Exception)
            {
                throw;
            }
            return intReturn;
        }


        //===[ DatSPReturnRS ]=============================================================

        public static DataTable DatSPReturnRS(string strStoredProc)
        {
            return DatSPReturnRS(strStoredProc, new object[0, 0]);
        }

        public static DataTable DatSPReturnRS(string strStoredProc, DataTable dt)
        {
            return DatSPReturnRS(strStoredProc, dt.Rows[0]);
        }

        public static DataTable DatSPReturnRS(string strStoredProc, DataRow dr)
        {
            return DatSPReturnRS(strStoredProc, f_DataRowToArray(dr));
        }

        public static DataTable DatSPReturnRS(string strStoredProc, object[,] objParam)
        {
            return DatSPReturnRS(strStoredProc, objParam, "Return");
        }

        public static DataTable DatSPReturnRS(string strStoredProc,
                                                          object[,] objParam,
                                                          string strNomeTabela)
        {
            SqlConnection cn = new SqlConnection(DatConnectionString);
            SqlCommand cmd = new SqlCommand(strStoredProc, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtReturn;

            try
            {
                cn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;

                f_CmdParametros(cmd, objParam);

                da = new SqlDataAdapter(cmd);

                dtReturn = new DataTable(strNomeTabela);

                da.Fill(dtReturn);
                da.Dispose();
                cn.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return dtReturn;
        }

        public static DataTable DatSqlReturnRS(string strSql)
        {
            return DatSqlReturnRS(strSql, "Return");
        }

        public static DataTable DatSqlReturnRS(string strSql, string strNomeTabela)
        {
            SqlConnection cn = new SqlConnection(DatConnectionString);
            SqlCommand cmd = new SqlCommand(strSql, cn);
            SqlDataAdapter da;
            DataTable dtReturn;

            try
            {
                cn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 300;

                da = new SqlDataAdapter(cmd);

                dtReturn = new DataTable(strNomeTabela);

                da.Fill(dtReturn);
                da.Dispose();
                cn.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return dtReturn;
        }


        //===[ Funções de apoio ]==========================================================

        private static object[,] f_DataRowToArray(DataRow dr)
        {
            object[,] objReturn = new object[dr.Table.Columns.Count, 2];
            int intCol = 0;

            foreach (DataColumn objCol in dr.Table.Columns)
            {
                objReturn[intCol, 0] = String.Concat("@", objCol.ColumnName);

                if (dr[objCol.ColumnName] is DBNull)
                    if (objCol.DataType.Name == "Byte[]")   // Timestamp
                        objReturn[intCol, 1] = null;
                    else
                    {
                        objReturn[intCol, 1] = System.DBNull.Value;
                    }
                else
                {
                    objReturn[intCol, 1] = dr[objCol.ColumnName];
                }

                intCol++;
            }

            return objReturn;
        }

        private static void f_CmdParametros(SqlCommand cmd, object[,] objPar)
        {
            object obj;

            for (int intI = 0; intI < objPar.GetLength(0); intI++)
            {

                obj = objPar[intI, 1];

                if (obj == null)
                    continue;

                if (obj.GetType().Name == "DateTime")
                {
                    obj = ((DateTime)obj).ToString("yyyyMMdd HH:mm:ss.fff");
                }

                // Qdo o campo no Sql é Money e esta vindo o valor System.DbNull,
                // dá um erro implicity conversion from data type nvarchar to money.            

                if (objPar[intI, 0].ToString() == "@vl_tipomoney" && obj == System.DBNull.Value)
                    cmd.Parameters.Add((string)objPar[intI, 0], SqlDbType.Money).Value = System.DBNull.Value;
                else
                    cmd.Parameters.AddWithValue((string)objPar[intI, 0], obj);
            }
        }
    }

}
