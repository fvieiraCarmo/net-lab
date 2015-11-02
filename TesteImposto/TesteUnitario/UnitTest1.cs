using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Imposto.Core.Domain;
using System.Collections.Generic;
using Imposto.Core.Service;

namespace TesteUnitario
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Icms()
        {
            double valorProduto = 1000.00;
            string ufOrigem = "SP";
            string ufDestino = "RJ";
            string cfop = "6.000";

            Icms icms = new Icms();
            icms.CalcularIcms(valorProduto, ufOrigem, ufDestino, cfop);            
        }

        [TestMethod]
        public void Test_Ipi()
        {
            double valorProduto = 500; 
            Ipi ipi = new Ipi();
            ipi.CalcularIpi(valorProduto);
        }

        [TestMethod]
        public void Test_Cfop()
        {
            string ufOrigem = "SP";
            string ufDestino = "RO";
            
            Cfop cfop = new Cfop();
            string sCfop = cfop.ObterCfopPorRota(ufOrigem, ufDestino);
        }

        [TestMethod]
        public void Test_GeraItemNotaFiscal()
        {
            NotaFiscal nf = new NotaFiscal();
            nf.EstadoDestino = "SP";
            PedidoItem pedidoItem = new PedidoItem();
            pedidoItem.ValorItemPedido = 1000;
            pedidoItem.Brinde = true;
            NotaFiscalItem nfItem = nf.GerarItemNotaFiscal(pedidoItem);
        }

        [TestMethod]
        public void Test_GravarNotaFiscal()
        {
            Pedido pedido = new Pedido();
            pedido.NomeCliente = "FABIO VIEIRA";
            pedido.EstadoOrigem = "SP";
            pedido.EstadoDestino = "MG";
            pedido.ItensDoPedido = new List<PedidoItem>
                {
                    new PedidoItem { NomeProduto = "BERMUDA",
                                     CodigoProduto = "BM3490",
                                     ValorItemPedido = 70.00,
                                     Brinde = false
                                   }
                };
            NotaFiscalService nfService = new NotaFiscalService();
            string msgErro = nfService.GerarNotaFiscal(pedido);
        }
    }
}
