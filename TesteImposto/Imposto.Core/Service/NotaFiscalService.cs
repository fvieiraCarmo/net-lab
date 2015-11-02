using Imposto.Core.Data;
using Imposto.Core.Domain;
using Imposto.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        public string GerarNotaFiscal(Domain.Pedido pedido)
        {
            string errorMessage = "";
            //gera entidade de NF
            NotaFiscal notaFiscal = new NotaFiscal();
            notaFiscal.EmitirNotaFiscal(pedido);
            //serializa o objeto pra XML
            var xml = new Xml();
            var infra = new InfraStructure();
            var nomeArquivoXML = infra.GetXmlFilesDir() + "NF_" + notaFiscal.NumeroNotaFiscal + ".xml";
            string sGravaXML = xml.Serializar(notaFiscal, nomeArquivoXML);
            if (sGravaXML != "")
            {
                errorMessage += "\nNão foi possível gravar a Nota Fiscal em XML. Detalhes: " + sGravaXML;
                return errorMessage;
            }
            //grava a NF no banco
            NotaFiscalRepository nfRepository = new NotaFiscalRepository();
            string sGravaDB = nfRepository.IncluirNotaFiscalDB(notaFiscal);
            if (sGravaDB != "")
            {
                errorMessage += "\nNão foi possível gravar a Nota Fiscal no Banco de Dados. Detalhes: " + sGravaDB;
                return errorMessage;
            }

            return errorMessage;
        }
    }
}
