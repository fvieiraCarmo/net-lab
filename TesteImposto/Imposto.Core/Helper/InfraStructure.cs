using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Imposto.Core.Helper
{
    class InfraStructure
    {
        public string GetXmlFilesDir()
        {
            return ConfigurationManager.AppSettings["XmlFilesDir"].ToString();
        }
    }
}
