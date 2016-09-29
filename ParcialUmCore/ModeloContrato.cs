using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParcialUmCore
{
    public enum EImpostos { INSS, IRPF, IRPJ, ISS, DAS, MensalidadeCooperativa };
    public enum EBeneficios { DecimoTerceiro,  Ferias, AssistMedica, ValeRefeicao, SeguroVida, Educacao };
    public enum ETipoContrato { CLT, MEI, Cooperado };

    public abstract class ModeloContrato
    {
        
        public decimal SalarioBruto { get; set; }
        public ushort HorasTrabalhadas { get; set; }

        public ETipoContrato TipoContrato { get; set; }

        public Dictionary<EBeneficios, decimal> BeneficiosCalulados { get; set; }
        public Dictionary<EImpostos, decimal> ImpostosCalculados { get; set; }

        public ModeloContrato(ETipoContrato tipoContrato, decimal salarioBruto, ushort horasTrabalhadas)
        {
            this.SalarioBruto = salarioBruto;
            this.HorasTrabalhadas = horasTrabalhadas;
            this.TipoContrato = tipoContrato; 
            this.BeneficiosCalulados = new Dictionary<EBeneficios, decimal>();
            this.ImpostosCalculados = new Dictionary<EImpostos, decimal>();
        }

        public virtual void SetValorBeneficio(EBeneficios eBeneficio, decimal value)
        {
            BeneficiosCalulados.Add(eBeneficio, value);
        }

        public virtual decimal CalculaValorFerias()
        {
            return 0;
        }

        public virtual decimal CalculaValorDecimo()
        {
            return 0;
        }

        public abstract void SetPercentualImposto(EImpostos imposto, decimal percentual);

        public virtual decimal CalculaValorDescontos()
        { 
            decimal result = 0;
            foreach(var item in ImpostosCalculados)
            {
                result += CalculaValorDesconto(item.Key);
            }
            return result;
        }

        public virtual decimal CalculaValorDesconto(EImpostos imposto)
        {
            decimal percentual = 0; 
            return ImpostosCalculados.TryGetValue(imposto, out percentual) ? SalarioBruto * percentual : 0;
        }


        public virtual decimal CalculaValorAdicional()
        {
            decimal result = 0m;
            foreach(var item in BeneficiosCalulados)
            {
                result += CalculaValorBeneficio(item.Key);
            }
            return result;
        }


        public virtual decimal CalculaValorBeneficio(EBeneficios beneficio)
        {
            if (EBeneficios.Ferias.Equals(beneficio))
                return CalculaValorFerias();
            else if (EBeneficios.DecimoTerceiro.Equals(beneficio))
                return CalculaValorDecimo();
             
            decimal valorBeneficio = 0;
            return BeneficiosCalulados.TryGetValue(beneficio, out valorBeneficio) ? valorBeneficio : 0;
        }

        public virtual decimal CalculaValorHora()
        {
            return SalarioBruto / HorasTrabalhadas;
        }

        public virtual decimal CalculaSalarioLiquido()
        {
            return SalarioBruto - CalculaValorDescontos();
        }

        public virtual decimal CalculaSalarioLiquidoComBeneficios()
        {
            return CalculaSalarioLiquido() + CalculaValorAdicional();
        }
    }
}
