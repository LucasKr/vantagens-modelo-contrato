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

        public override decimal CalculaValorFerias()
        { 
            decimal umTercoDoBruto = SalarioBruto + (SalarioBruto / 3);
            return ((umTercoDoBruto - CalculaValorDescontos()) / 12);
        }

        public override decimal CalculaValorDecimo()
        {
            return ((SalarioBruto - CalculaValorDescontos()) / 12);
        }

        public override void SetPercentualImposto(EImpostos imposto, decimal percentual)
        {
            if (imposto.Equals(EImpostos.ISS | EImpostos.IRPJ | EImpostos.DAS | EImpostos.MensalidadeCooperativa))
                return;

            ImpostosCalculados.Add(imposto, percentual);
        }
    }
}
