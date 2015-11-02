using Imposto.Core.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Imposto.Core.Domain;

namespace TesteImposto
{
    public partial class  FormImposto : Form
    {
        private Pedido pedido = new Pedido();

        public FormImposto()
        {
            InitializeComponent();
            iniciar();
        }

        private void iniciar()
        {
            //combos
            var estados = new EstadosBrasil().ObterEstados();
            cboEstadoOrigem.DataSource = new BindingSource(estados, null);
            cboEstadoDestino.DataSource = new BindingSource(estados, null);
            
            limpar();           
        }

        private void limpar()
        {
            textBoxNomeCliente.Text = "";
            cboEstadoOrigem.SelectedIndex = 0;
            cboEstadoDestino.SelectedIndex = 0;
            dataGridViewPedidos.DataSource = null;
            dataGridViewPedidos.AutoGenerateColumns = true;
            dataGridViewPedidos.DataSource = GetTablePedidos();
            textBoxNomeCliente.Focus();
            this.pedido = new Pedido();
        }

        private object GetTablePedidos()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do Produto", typeof(string)));
            table.Columns.Add(new DataColumn("Código do Produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor do Produto", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));
            
            return table;
        }

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {
            if (!validarForm()) return;
            
            NotaFiscalService service = new NotaFiscalService();
            pedido.EstadoOrigem = cboEstadoOrigem.SelectedValue.ToString();
            pedido.EstadoDestino = cboEstadoDestino.SelectedValue.ToString();
            pedido.NomeCliente = textBoxNomeCliente.Text;

            DataTable table = (DataTable)dataGridViewPedidos.DataSource;

            pedido.ItensDoPedido.Clear();
            foreach (DataRow row in table.Rows)
            {
                pedido.ItensDoPedido.Add(
                    new PedidoItem()
                    {
                        Brinde = Convert.ToBoolean(row["Brinde"].GetType().Name.Equals("DBNull") ? false : row["Brinde"]),
                        CodigoProduto =  row["Código do produto"].ToString(),
                        NomeProduto = row["Nome do produto"].ToString(),
                        ValorItemPedido = Convert.ToDouble(row["Valor do Produto"].ToString())            
                    });
            }
            string sGeraNota = "";
            sGeraNota = service.GerarNotaFiscal(pedido);
            if (sGeraNota != "")
                MessageBox.Show("Erro ao gerar a Nota Fiscal. Detalhes:\n\n" + sGeraNota);
            else
            {
                MessageBox.Show("A Nota Fiscal foi gerada com sucesso.");
                limpar();
            }
        }

        private void dataGridViewPedidos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                MessageBox.Show("Favor informar um valor numérico");
            }
        }

        private bool validarForm()
        {
            if (textBoxNomeCliente.Text.Trim() == "")
            {
                MessageBox.Show("Favor informar o Nome do Cliente");
                textBoxNomeCliente.Focus();
                return false;
            }
            //valida a grid
            if (dataGridViewPedidos.Rows.Count == 1)
            {
                MessageBox.Show("Favor informar ao menos um Item/Produto.");
                return false;
            }
            for (int i = 0; i < dataGridViewPedidos.Rows.Count - 1; i++ )
            {
                var row = dataGridViewPedidos.Rows[i];
                for (int j = 0; j <= 2; j++)
                {
                    var cel = row.Cells[j];
                    if (cel.Value == null || cel.Value.ToString().Trim() == "")
                    {
                        MessageBox.Show("Favor informar o " + cel.OwningColumn.Name);
                        dataGridViewPedidos.ClearSelection();
                        cel.Selected = true;
                        dataGridViewPedidos.CurrentCell = cel;
                        dataGridViewPedidos.BeginEdit(true);
                        return false;
                    }
                }
            }
                
            return true;
        }

    }
}
