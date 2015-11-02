using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Imposto.Core.Helper
{
    class Xml
    {
        public string Serializar(object obj, string arquivoXML)
        {
            var errorMessage = "";
            try
            {
                var stream = new FileStream(arquivoXML, FileMode.Create);
                var serializador = new XmlSerializer(obj.GetType());
                serializador.Serialize(stream, obj);
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
            return errorMessage;
        }
    }
}
