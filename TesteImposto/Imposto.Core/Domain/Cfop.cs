using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class Cfop
    {
        private List<RotaCfop> RotasCfop = new List<RotaCfop> {            
                new RotaCfop { EstadoOrigem = "SP", EstadoDestino = "RJ", Cfop = "6.000"},
                new RotaCfop { EstadoOrigem = "SP", EstadoDestino = "PE", Cfop = "6.001"},
                new RotaCfop { EstadoOrigem = "SP", EstadoDestino = "MG", Cfop = "6.002"},
                new RotaCfop { EstadoOrigem = "SP", EstadoDestino = "PB", Cfop = "6.003"},
                new RotaCfop { EstadoOrigem = "SP", EstadoDestino = "PR", Cfop = "6.004"},
                new RotaCfop { EstadoOrigem = "SP", EstadoDestino = "PI", Cfop = "6.005"},
                new RotaCfop { EstadoOrigem = "SP", EstadoDestino = "RO", Cfop = "6.006"},
                new RotaCfop { EstadoOrigem = "SP", EstadoDestino = "SE", Cfop = "6.007"},
                new RotaCfop { EstadoOrigem = "SP", EstadoDestino = "TO", Cfop = "6.008"},
                new RotaCfop { EstadoOrigem = "SP", EstadoDestino = "SE", Cfop = "6.009"},
                new RotaCfop { EstadoOrigem = "SP", EstadoDestino = "PA", Cfop = "6.010"},
                new RotaCfop { EstadoOrigem = "MG", EstadoDestino = "RJ", Cfop = "6.000"},
                new RotaCfop { EstadoOrigem = "MG", EstadoDestino = "PE", Cfop = "6.001"},
                new RotaCfop { EstadoOrigem = "MG", EstadoDestino = "MG", Cfop = "6.002"},
                new RotaCfop { EstadoOrigem = "MG", EstadoDestino = "PB", Cfop = "6.003"},
                new RotaCfop { EstadoOrigem = "MG", EstadoDestino = "PR", Cfop = "6.004"},
                new RotaCfop { EstadoOrigem = "MG", EstadoDestino = "PI", Cfop = "6.005"},
                new RotaCfop { EstadoOrigem = "MG", EstadoDestino = "RO", Cfop = "6.006"},
                new RotaCfop { EstadoOrigem = "MG", EstadoDestino = "SE", Cfop = "6.007"},
                new RotaCfop { EstadoOrigem = "MG", EstadoDestino = "TO", Cfop = "6.008"},
                new RotaCfop { EstadoOrigem = "MG", EstadoDestino = "SE", Cfop = "6.009"},
                new RotaCfop { EstadoOrigem = "MG", EstadoDestino = "PA", Cfop = "6.010"}
            };

        public string ObterCfopPorRota(string EstadoOrigem, string EstadoDestino)
        {
            string cfop = "6.000";
            var rotaCfop = RotasCfop.Find(s => s.EstadoOrigem == EstadoOrigem && s.EstadoDestino == EstadoDestino);
            if (rotaCfop != null)
                cfop = rotaCfop.Cfop;
            return cfop;
        }

    }

    class RotaCfop
    {
        public string EstadoOrigem {get; set;}
        public string EstadoDestino {get; set;}
        public string Cfop { get; set; }
    }
}
