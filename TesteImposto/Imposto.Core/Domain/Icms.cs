using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class Icms
    {
        public double BaseIcms { get; set; }
        public double AliquotaIcms { get; set; }
        public double ValorIcms { get; set; }
        public string TipoIcms { get; set; }

        public void CalcularIcms(double valorProduto, string estadoOrigem, string estadoDestino, string cFop)
        {
            if (estadoOrigem == estadoDestino)
            {
                this.TipoIcms = "60";
                this.AliquotaIcms = 0.18;
            }
            else
            {
                this.TipoIcms = "10";
                this.AliquotaIcms = 0.17;
            }
            if (cFop == "6.009")
            {
                this.BaseIcms = valorProduto * 0.90; //redução de base
            }
            else
            {
                this.BaseIcms = valorProduto;
            }
            this.ValorIcms = this.BaseIcms * this.AliquotaIcms;
        }
    }
}
