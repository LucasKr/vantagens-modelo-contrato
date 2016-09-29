using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialUmCore
{
    public class Clt : ModeloContrato
    {
        public Clt(decimal salarioBruto, ushort horasTrabalhadas) : base(ETipoContrato.CLT, salarioBruto, horasTrabalhadas)
        {
        }

        public override decimal GetValorFerias()
        { 
            decimal umTercoDoBruto = SalarioBruto + (SalarioBruto / 3);
            return ((umTercoDoBruto - GetValorDescontos()) / 12);
        }

        public override decimal GetValorDecimo()
        {
            return ((SalarioBruto - GetValorDescontos()) / 12);
        }

        public override void SetPercentualImposto(EImpostos imposto, decimal percentual)
        {
            if (imposto.Equals(EImpostos.ISS | EImpostos.IRPJ | EImpostos.DAS | EImpostos.MensalidadeCooperativa))
                return;

            ImpostosCalculados.Add(imposto, percentual);
        }
    }
}
