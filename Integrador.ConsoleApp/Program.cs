using System;

namespace Integrador.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gerador CNAB banco Eximia");

            string DiretorioArquivo = Extensions.StringExtensions.RecuperarDiretorioResouce();

            new ArquivoRemessa().GerarArquivoRemessa(DiretorioArquivo);

            Console.WriteLine("Fim Geração de Arquivo Remessa CNAB banco Eximia");
        }
    }
}