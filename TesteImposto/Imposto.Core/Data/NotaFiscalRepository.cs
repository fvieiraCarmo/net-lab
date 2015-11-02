using Imposto.Core.Domain;
using Imposto.Core.DBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository
    {
        public string IncluirNotaFiscalDB(NotaFiscal notaFiscal)
        {
            var errorMessage = "";
            try
            {
                int newID = 0;
                newID = Database.DatSPReturnValue("P_NOTA_FISCAL",
                    new object[,] {{"@pId", 0},
                                    {"@pNumeroNotaFiscal", notaFiscal.NumeroNotaFiscal},
                                    {"@pSerie", notaFiscal.Serie},
                                    {"@pNomeCliente", notaFiscal.NomeCliente},
                                    {"@pEstadoDestino", notaFiscal.EstadoDestino},
                                    {"@pEstadoOrigem", notaFiscal.EstadoOrigem}
                       }, "@pId");
                
                //insere os itens da nota (produtos)
                foreach (var item in notaFiscal.ItensDaNotaFiscal)
                {
                    Database.DatSPReturnRS("P_NOTA_FISCAL_ITEM",
                       new object[,] {{"@pId", 0},
                                      {"@pIdNotaFiscal", newID},
                                      {"@pCfop", item.Cfop},
                                      {"@pTipoIcms", item.TipoIcms},
                                      {"@pBaseIcms", item.BaseIcms},
                                      {"@pAliquotaIcms", item.AliquotaIcms},
                                      {"@pValorIcms", item.ValorIcms},
                                      {"@pNomeProduto", item.NomeProduto},
                                      {"@pCodigoProduto", item.CodigoProduto},
                                      {"@pBaseIpi", item.BaseIpi},
                                      {"@pAliquotaIpi", item.AliquotaIpi},
                                      {"@pValorIpi", item.ValorIpi},
                                      {"@pTaxaDesconto", item.TaxaDesconto}
                       });
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
            return errorMessage;
        }
    }
}
