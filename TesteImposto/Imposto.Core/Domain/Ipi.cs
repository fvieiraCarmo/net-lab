using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class Ipi
    {
        public double BaseIpi { get; set; }
        public double AliquotaIpi { get; set; }
        public double ValorIpi { get; set;  }

        public void CalcularIpi(double valorProduto)
        {
            this.BaseIpi = valorProduto;
            this.AliquotaIpi = 0.10;
            this.ValorIpi = this.BaseIpi * this.AliquotaIpi;
        }
    }
}
