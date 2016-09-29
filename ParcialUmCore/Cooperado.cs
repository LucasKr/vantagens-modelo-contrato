using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialUmCore
{
    public class Cooperado : ModeloContrato
    {
        public Cooperado(decimal salarioBruto, ushort horasTrabalhadas)
            : base(ETipoContrato.Cooperado, salarioBruto, horasTrabalhadas)
        {
        }

        public override void SetPercentualImposto(EImpostos imposto, decimal percentual)
        { 
            if (imposto.Equals(EImpostos.ISS | EImpostos.IRPJ | EImpostos.DAS))
                return;

            ImpostosCalculados.Add(imposto, percentual);
        }
         
    }
}
