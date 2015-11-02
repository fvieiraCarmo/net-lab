using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class NotaFiscal
    {
        public int Id { get; set; }
        public int NumeroNotaFiscal { get; set; }
        public int Serie { get; set; }
        public string NomeCliente { get; set; }

        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }

        public List<NotaFiscalItem> ItensDaNotaFiscal { get; set; }

        public NotaFiscal()
        {
            ItensDaNotaFiscal = new List<NotaFiscalItem>();
        }

        public void EmitirNotaFiscal(Pedido pedido)
        {
            //Dados básicos da NF
            this.NumeroNotaFiscal = new Random().Next(Int32.MaxValue);
            this.Serie = new Random().Next(Int32.MaxValue);
            this.NomeCliente = pedido.NomeCliente;
            this.EstadoDestino = pedido.EstadoDestino;
            this.EstadoOrigem = pedido.EstadoOrigem;

            //Produtos da NF
            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = GerarItemNotaFiscal(itemPedido);
                this.ItensDaNotaFiscal.Add(notaFiscalItem);
            }    
        }


        public NotaFiscalItem GerarItemNotaFiscal(PedidoItem itemPedido)
        {
            NotaFiscalItem notaFiscalItem = new NotaFiscalItem();

            notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
            notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;
            //Desconto
            notaFiscalItem.TaxaDesconto = ObterTaxaDesconto();
            double vlDesconto = itemPedido.ValorItemPedido * (notaFiscalItem.TaxaDesconto / 100.0D);

            itemPedido.ValorItemPedido = itemPedido.ValorItemPedido - vlDesconto;

            //Cfop
            notaFiscalItem.Cfop = new Cfop().ObterCfopPorRota(this.EstadoOrigem, this.EstadoDestino);
            //Icms
            CalcularIcms(notaFiscalItem, itemPedido.ValorItemPedido);
            //Ipi
            CalcularIpi(notaFiscalItem, itemPedido.ValorItemPedido);
            //Brinde
            if (itemPedido.Brinde)
            {
                notaFiscalItem.TipoIcms = "60";
                notaFiscalItem.AliquotaIcms = 0.18;
                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                notaFiscalItem.AliquotaIpi = 0;
                notaFiscalItem.ValorIpi = 0;
            }

            return notaFiscalItem;
        }
        
        public double ObterTaxaDesconto()
        {
            double taxaDesconto = 0;
            //Taxa de desconto de 10% para região Sudeste
            if (this.EstadoDestino == "SP" || this.EstadoDestino == "RJ" || this.EstadoDestino == "MG" || this.EstadoDestino == "ES")
            {
                taxaDesconto = 10;
            }
            return taxaDesconto;
        }

        public void CalcularIcms(NotaFiscalItem nfItem, double valorItemPedido)
        {
            Icms icms = new Icms();
            icms.CalcularIcms(valorItemPedido, this.EstadoOrigem, this.EstadoDestino, nfItem.Cfop);
            nfItem.BaseIcms = icms.BaseIcms;
            nfItem.AliquotaIcms = icms.AliquotaIcms;
            nfItem.ValorIcms = icms.ValorIcms;
            nfItem.TipoIcms = icms.TipoIcms;
        }

        public void CalcularIpi(NotaFiscalItem nfItem, double valorItemPedido)
        {
            Ipi ipi = new Ipi();
            ipi.CalcularIpi(valorItemPedido);
            nfItem.BaseIpi = ipi.BaseIpi;
            nfItem.AliquotaIpi = ipi.AliquotaIpi;
            nfItem.ValorIpi = ipi.ValorIpi;
        }
    }
}
