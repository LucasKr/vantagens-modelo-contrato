using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcialUmCore;
using System.IO;

namespace ParcialUmProgram
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StreamWriter file = new StreamWriter("C:\\College\\teste.txt");
            ModeloContrato clt = new Clt(1000, 160);
            clt.SetPercentualImposto(EImpostos.INSS, (decimal) 0.11);
            clt.SetPercentualImposto(EImpostos.IRPF, (decimal) 0.25);
            clt.SetValorBeneficio(EBeneficios.AssistMedica, 500m);
            clt.SetValorBeneficio(EBeneficios.Educacao, 400m);
            clt.SetValorBeneficio(EBeneficios.SeguroVida, 80m);
            clt.SetValorBeneficio(EBeneficios.ValeRefeicao, 320m);
            clt.SetValorBeneficio(EBeneficios.Ferias, 0m);
            clt.SetValorBeneficio(EBeneficios.DecimoTerceiro, 0m);

            WriteModeloContrato(file, clt);


            ModeloContrato mei = new Mei(2100, 160);
            mei.SetPercentualImposto(EImpostos.ISS, (decimal)0.02);
            mei.SetPercentualImposto(EImpostos.IRPJ, (decimal)0.12);
            mei.SetPercentualImposto(EImpostos.DAS, 49.90m);
            mei.SetValorBeneficio(EBeneficios.ValeRefeicao, 320m);
            WriteModeloContrato(file, mei);

            ModeloContrato cooperado = new Cooperado(2700, 160);
            cooperado.SetPercentualImposto(EImpostos.INSS, (decimal)0.11);
            cooperado.SetPercentualImposto(EImpostos.IRPF, (decimal)0.17);
            cooperado.SetPercentualImposto(EImpostos.MensalidadeCooperativa, (decimal)0.05);
            cooperado.SetValorBeneficio(EBeneficios.AssistMedica, 200m);

            WriteModeloContrato(file, cooperado);

            file.WriteLine("------------------------------------------------------------");
            file.Close();
        }

        private static void WriteModeloContrato(StreamWriter file, ModeloContrato modelo)
        {
            file.WriteLine("------------------------------------------------------------");
            file.WriteLine("Modelo de contrato: {0} ", modelo.TipoContrato);
            file.WriteLine("");
            file.WriteLine(" Impostos: ");
            foreach(var i in modelo.ImpostosCalculados)
            {
                decimal valorPercentual = i.Value * 100;

                if (EImpostos.DAS.Equals(i.Key))
                    valorPercentual = Math.Round(((i.Value / modelo.SalarioBruto) * 100), 2); 

                file.WriteLine(" {0}: {1}% - {2:C} ", i.Key, valorPercentual, modelo.CalculaValorDesconto(i.Key));
            }
            file.WriteLine(" Total impostos: {0}%", Math.Round(((modelo.CalculaValorDescontos() / modelo.SalarioBruto) * 100), 2));
            file.WriteLine(" Total impostos: {0:C}", modelo.CalculaValorDescontos());
            file.WriteLine("");
            file.WriteLine(" Beneficios: ");

            foreach(var i in modelo.BeneficiosCalulados)
            { 
                file.WriteLine(" {0}: {1:C} ", i.Key, modelo.CalculaValorBeneficio(i.Key));
            }
            file.WriteLine(" Total Beneficios: {0:C} ", modelo.CalculaValorAdicional());
            file.WriteLine("");
            file.WriteLine(" Salário Bruto: {0:C} Salário Liquido: {1:C} Salário Liquido c/ Beneficios: {2:C}", modelo.SalarioBruto, modelo.CalculaSalarioLiquido(), modelo.CalculaSalarioLiquidoComBeneficios());
        }
    }
}
