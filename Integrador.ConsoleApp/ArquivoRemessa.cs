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
        public string NomeArquivo { get; private set; }
        public Beneficiario Beneficiario { get; private set; }
        public IEnumerable<Boleto> Boletos { get; private set; }
        public StreamWriter Arquivo { get; private set; }

        public void Gerar(Beneficiario beneficiario, IEnumerable<Boleto> boletos)
        {
            if (boletos == null || !boletos.Any())
            {
                throw new ArgumentException("Nenhum boleto informado");
            }

            Boletos = boletos;
            Beneficiario = beneficiario ?? throw new ArgumentException("Beneficiário não informado");

            NomeArquivo = StringExtensions.RecuperarDiretorioResouce();

            if (!Directory.Exists(NomeArquivo))
            {
                Directory.CreateDirectory(NomeArquivo);
            }

            NomeArquivo += Beneficiario.Codigo.FormatarTexto(5, '0');
            NomeArquivo += StringExtensions.RecuperarCodigoDoMes(DateTime.Now.Month);
            NomeArquivo += DateTime.Now.Day + ".CRM";

            if (File.Exists(NomeArquivo))
            {
                File.Delete(NomeArquivo);
            }
            Arquivo = File.CreateText(NomeArquivo);

            string strline = GerarHeader();
            if (string.IsNullOrWhiteSpace(strline))
            {
                throw new Exception("Registro HEADER obrigatório.");
            }
            Arquivo.WriteLine(strline);

            int sequencia = 1;
            foreach (Boleto boleto in boletos)
            { 
                strline = GerarDetalhe(boleto, sequencia);
                if (string.IsNullOrWhiteSpace(strline))
                    throw new Exception("Registro DETALHE obrigatório.");
                Arquivo.WriteLine(strline);
                sequencia = sequencia++;
            }

            strline = GerarTrailer();
            if (string.IsNullOrWhiteSpace(strline))
            {
                throw new Exception("Registro TRAILER obrigatório.");
            }
            Arquivo.WriteLine(strline);

            Arquivo.Close();
            Arquivo.Dispose();
        }
        public string GerarHeader()
        {
            StringBuilder build = new();                             // | Posição   | Tamanho | Descrição                            |
                                                                     // |-----------|---------|--------------------------------------|
            build.Append("0".FormatarTexto(1));                      // | 001 a 001 | 001     | Identificação do registro *header*   |
            build.Append("1".FormatarTexto(1));                      // | 002 a 002 | 001     | Identificação do arquivo de remessa  |
            build.Append("REMESSA".FormatarTexto(7));                // | 003 a 009 | 007     | Literal remessa                      |
            build.Append("01".FormatarTexto(2));                     // | 010 a 011 | 002     | Código do serviço de cobrança        |
            build.Append("COBRANCA".FormatarTexto(15));              // | 012 a 026 | 015     | Literal cobrança                     |
            build.Append(Beneficiario.Codigo.FormatarTexto(5, '0')); // | 027 a 031 | 005     | Código do beneficário                |
            build.Append(Beneficiario.CNPJ.FormatarTexto(14, '0'));  // | 032 a 045 | 014     | CNPF do beneficário                  |
            build.Append("".FormatarTexto(31));                      // | 046 a 076 | 031     | Filler                               |   
            build.Append(Beneficiario.Banco.FormatarTexto(3, '0'));  // | 077 a 079 | 003     | Número do banco                      |
            build.Append("EXIMIA".FormatarTexto(15));                // | 080 a 094 | 015     | BANCO                                |
            build.Append("".FormatarTexto(15));                      // | 095 a 102 | 008     | Filer                                |
            build.Append("1".FormatarTexto(7, '0'));                 // | 111 a 117 | 007     | Número da remessa                    |
            build.Append("".FormatarTexto(273));                     // | 118 a 390 | 273     | Filer                                |
            build.Append("2.00".FormatarTexto(4));                   // | 391 a 394 | 004     | Versão do sistema                    |
            build.Append("1".FormatarTexto(6, '0'));                 // | 395 a 400 | 006     | Número sequêncial do arquivo         |

            return build.ToString();
        }

        public static string GerarDetalhe(Boleto boleto, int sequencia)
        {
            StringBuilder build = new();

            build.Append("1".FormatarTexto(1));                                    // | 001 a 001 | 001     | Identificação do registro detalhe    | 
            build.Append("A".FormatarTexto(1));                                    // | 002 a 002 | 001     | Tipo de cobrança                     | 
            build.Append("A".FormatarTexto(1));                                    // | 003 a 003 | 001     | Tipo de carteira                     | 
            build.Append("A".FormatarTexto(1));                                    // | 004 a 004 | 001     | Tipo de impressão                    | 
            build.Append("".FormatarTexto(12));                                    // | 005 a 016 | 012     | Filer                                | 
            build.Append("A".FormatarTexto(1));                                    // | 017 a 017 | 001     | Tipo de moeda                        | 
            build.Append("A".FormatarTexto(1));                                    // | 018 a 018 | 001     | Tipo de Desconto                     | 
            build.Append("A".FormatarTexto(1));                                    // | 019 a 019 | 001     | Tipo de Juros                        | 
            build.Append("".FormatarTexto(28));                                    // | 020 a 047 | 028     | Filer                                | 
            build.Append(("AA2" + boleto.NossoNumero).FormatarTexto(9));           // | 048 a 056 | 009     | Nosso Numero                         | 
            build.Append("".FormatarTexto(6));                                     // | 057 a 062 | 006     | Filer                                | 
            build.Append(DateTime.Now.ToString("yyyyMMdd").FormatarTexto(8));      // | 063 a 070 | 008     | Data instrução                       | 
            build.Append("".FormatarTexto(1));                                     // | 071 a 071 | 001     | Vazio                                | 
            build.Append("N".FormatarTexto(1));                                    // | 072 a 072 | 001     | Postagem                             | 
            build.Append("".FormatarTexto(1));                                     // | 073 a 073 | 001     | Filer                                | 
            build.Append("B".FormatarTexto(1));                                    // | 074 a 074 | 001     | Emissão boleto                       | 
            build.Append("".FormatarTexto(2));                                     // | 075 a 076 | 002     | Vazio                                | 
            build.Append("".FormatarTexto(2));                                     // | 077 a 078 | 002     | Vazio                                | 
            build.Append("".FormatarTexto(4));                                     // | 079 a 082 | 004     | Filer                                | 
            build.Append(boleto.Desconto.Valor.ToString().FormatarTexto(10, '0')); // | 083 a 092 | 010     | Valor de desconto                    | 
            build.Append("".FormatarTexto(4, '0'));                                // | 093 a 096 | 004     | % multa pagamento em atraso          | 
            build.Append("".FormatarTexto(12));                                    // | 097 a 108 | 012     | Filer                                | 
            build.Append("01".FormatarTexto(2));                                   // | 109 a 110 | 002     | Instrução                            | 
            build.Append(boleto.NumeroDocumento.ToString().FormatarTexto(10));     // | 111 a 120 | 010     | Seu número                           | 
            build.Append(boleto.Vencimento.ToString("ddMMyy").FormatarTexto(6));   // | 121 a 126 | 006     | Data de vencimento                   | 
            build.Append(boleto.Valor.ToString().FormatarTexto(13, '0'));          // | 127 a 139 | 013     | Valor                                | 
            build.Append("".FormatarTexto(9));                                     // | 140 a 148 | 009     | Filer                                | 
            build.Append("O".FormatarTexto(1));                                    // | 149 a 149 | 001     | Espécie                              | 
            build.Append("S".FormatarTexto(1));                                    // | 150 a 150 | 001     | Aceite                               | 
            build.Append(DateTime.Now.ToString("ddMMyy").FormatarTexto(6));        // | 151 a 156 | 006     | Data de emissão                      | 
            build.Append("".FormatarTexto(2, '0'));                                // | 157 a 158 | 002     | Protesto                             | 
            build.Append("".FormatarTexto(2, '0'));                                // | 159 a 160 | 002     | Numero de dias protesto              | 
            build.Append(boleto.Juros.Calcular(boleto.Desconto.Valor, 1)           
                                     .Value.Valor.ToString().FormatarTexto(12));   // | 161 a 173 | 012     | Valor de juros por dia de atraso     | 
            build.Append(boleto.Desconto.ValidoAte                                 
                                     .ToString("ddMMyy").FormatarTexto(6, '0'));   // | 174 a 179 | 006     | Data limite de desconto              | 
            build.Append("".FormatarTexto(13, '0'));                               // | 180 a 192 | 013     | Zeros                                | 
            build.Append("".FormatarTexto(13));                                    // | 193 a 205 | 013     | Filer                                | 
            build.Append("".FormatarTexto(13, '0'));                               // | 206 a 218 | 013     | Zeros                                | 
            build.Append(boleto.Pagador.Documento.Tipo ==                          
                         ETipoDocumento.CPF ? "0" : "2".FormatarTexto(1));         // | 219 a 219 | 001     | Tipo de pessoa do pagador PF ou PJ   | 
            build.Append("".FormatarTexto(1));                                     // | 220 a 220 | 001     | Filer                                | 
            build.Append(boleto.Pagador.Documento.Valor.FormatarTexto(14, '0'));   // | 221 a 234 | 014     | CNPJ ou CPF do pagador               | 
            build.Append(boleto.Pagador.Nome.FormatarTexto(40));                   // | 235 a 274 | 040     | Nome do pagador                      | 
            build.Append(boleto.Pagador.Endereco.Rua.FormatarTexto(40));           // | 275 a 314 | 040     | Endereço do pagador                  | 
            build.Append("".FormatarTexto(5, '0'));                                // | 315 a 319 | 005     | Zeros                                | 
            build.Append("".FormatarTexto(6));                                     // | 320 a 235 | 006     | Filer                                | 
            build.Append("".FormatarTexto(1, '0'));                                // | 326 a 326 | 001     | Filer                                | 
            build.Append(boleto.Pagador.Endereco.Cep.FormatarTexto(8, '0'));       // | 327 a 224 | 008     | CEP do pagador                       | 
            build.Append("".FormatarTexto(5, '0'));                                // | 335 a 339 | 005     | Zeros                                | 
            build.Append("".FormatarTexto(14, '0'));                               // | 340 a 353 | 014     | Zeros                                | 
            build.Append("".FormatarTexto(41));                                    // | 354 a 394 | 041     | Deixar em branco                     | 
            build.Append(sequencia.ToString().FormatarTexto(6, '0'));              // | 395 a 400 | 006     | Numero sequêncial do registro        | 

            return build.ToString();
        }

        public string GerarTrailer()
        {
            StringBuilder build = new StringBuilder();          // | Posição   | Tamanho | Descrição                            |
            build.AppendLine();                                 // |-----------|---------|--------------------------------------|
            build.Append("9".FormatarTexto(1));                 // | 001 a 001 | 001     | Identificação do registro *trailer*  |
            build.Append("1".FormatarTexto(1));                 // | 002 a 002 | 001     | Identificação do arquivo de mressa   |
            build.Append(Beneficiario.Banco.FormatarTexto(3));  // | 003 a 005 | 003     | Numero Eximia                        |
            build.Append(Beneficiario.Codigo.FormatarTexto(5)); // | 006 a 010 | 005     | Codigo do beneficiario               |
            build.Append("".FormatarTexto(383));                // | 011 a 394 | 384     | Filer                                |
            build.Append("1".FormatarTexto(6, '0'));            // | 395 a 400 | 006     | Numero sequencial do registro        |  

            return build.ToString();
        }

    }
}
