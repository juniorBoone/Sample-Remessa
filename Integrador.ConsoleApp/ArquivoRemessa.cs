using Integrador.ConsoleApp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ConsoleApp
{
    public class ArquivoRemessa
    {
        private StreamWriter arquivoRemessa;
        private string cnpjBeneficiario;
        private int sequencial = 1;

        public ArquivoRemessa(Stream stream)
        {
            arquivoRemessa = new StreamWriter(stream, Encoding.GetEncoding("ISO-8859-1"));
        }

        public void Gerar(IEnumerable<Boleto> boletos)
        {
            Header();
            foreach (Boleto boleto in boletos)
            {
                Detalhe(boleto);
            }
            arquivoRemessa.Close();
            arquivoRemessa.Dispose();
            arquivoRemessa = null;    
        }
        private void Header()
        {
            arquivoRemessa.WriteLine(
                "0" +   //  | 001 a 001 - Identificação do registro* header*
                "1" +        //| 002 a 002 | 001 | Identificação do arquivo de remessa | valor deve ser 1 |
                "REMESSA" +        //| 003 a 009 | 007 | Literal remessa | "REMESSA" |
                "01" +                 //| 010 a 011 | 002 | Código do serviço de cobrança | fixo "01" |
                "COBRANCA" +                //| 012 a 026 | 015 | Literal cobrança | "COBRANCA" |
                "".PadRight(5) +                           //| 027 a 031 | 005 | Código do beneficário |                                      |
                cnpjBeneficiario +                                             //| 032 a 045 | 014 | CNPF do beneficário | Alinhado com zeros a esquerda |
                "".PadRight(31) +                                                             //| 046 a 076 | 031 | Filler | Deixar em branco                     |
                "987" +                                                          //| 077 a 079 | 003 | Número do banco | 987 |
                "EXIMIA" +                                                            //| 080 a 094 | 015 | BANCO | Literal "EXIMIA" |
                "".PadRight(8) +                                                                     //| 095 a 102 | 008 | Filer | Deixar em branco                     |
                "0000001" +                                                                     //| 111 a 117 | 007 | Número da remessa                   | Numero do ultimo arquivo de remessa |
                "".PadRight(273) +                                        // | 118 a 390 | Filer | Deixar em branco                     |
                "2.00".PadRight(4) +                                      // | 391 a 394 | Versão do sistema | "2.00" |
                StringExtensions.ToPadLeftZeros(sequencial.ToString(), 6) // | 395 a 400 | Número sequêncial do arquivo | Alinhado a direta e zeros à esquerda |
            );
        }

        private void Detalhe(Boleto boleto)
        {
            arquivoRemessa.WriteLine(boleto.Beneficiario.Nome);
        }

    }
}
