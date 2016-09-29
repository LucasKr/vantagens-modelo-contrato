using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialUmCore
{
    public class Mei : ModeloContrato
    {
        public Mei(decimal salarioBruto, ushort horasTrabalhadas) 
            : base(ETipoContrato.MEI, salarioBruto, horasTrabalhadas)
        {
        }
        
        public override void SetPercentualImposto(EImpostos imposto, decimal percentual)
        {
            if (imposto.Equals(EImpostos.INSS | EImpostos.IRPF | EImpostos.MensalidadeCooperativa))
                return;

            ImpostosCalculados.Add(imposto, percentual);
        }

        public override decimal GetValorDesconto(EImpostos imposto)
        {
            decimal percentual;

            if (EImpostos.DAS.Equals(imposto))
                return ImpostosCalculados.TryGetValue(imposto, out percentual) ? percentual : 0;

            return base.GetValorDesconto(imposto);
        }

    }
}
