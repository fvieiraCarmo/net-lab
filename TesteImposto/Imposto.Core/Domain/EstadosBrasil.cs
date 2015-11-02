using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class EstadosBrasil
    {
        public EstadosBrasil() { }

        public Dictionary<string, string> ObterEstados()
        {
            return new Dictionary<string, string>
               {
                    {"AC", "Acre"},
                    {"AL", "Alagoas"},
                    {"AP", "Amapá"},
                    {"AM", "Amazonas"},
                    {"BA", "Bahia"},
                    {"CE", "Ceará"},
                    {"DF", "Distrito Federal"},
                    {"ES", "Espírito Santo"},
                    {"GO", "Goiás"},
                    {"MA", "Maranhão"},
                    {"MT", "Mato Grosso"},
                    {"MS", "Mato Grosso do Sul"},
                    {"MG", "Minas Gerais"},
                    {"PA", "Pará"},
                    {"PB", "Paraíba"},
                    {"PR", "Paraná"},
                    {"PE", "Pernambuco"},
                    {"PI", "Piauí"},
                    {"RJ", "Rio de Janeiro"},
                    {"RN", "Rio Grande do Norte"},
                    {"RS", "Rio Grande do Sul"},
                    {"RO", "Rondônia"},
                    {"RR", "Roraima"},
                    {"SC", "Santa Catarina"},
                    {"SP", "São Paulo"},
                    {"SE", "Sergipe"},
                    {"TO", "Tocantins"},
               };
        }
    }
}
