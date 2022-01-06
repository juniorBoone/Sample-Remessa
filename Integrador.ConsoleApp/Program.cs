using System;
using System.IO;

namespace Integrador.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gerador CNAB banco Eximia");
            var repositorio = new RepositorioBoletos();
            var boletos = repositorio.RecuperarTodos();
            

            string nomeArquivo = "C:\\Users\\junio\\Documents\\Remessa\\arq01.txt";

            using (var fileStream = new FileStream(nomeArquivo, FileMode.Create))
            {
                var arquivoRemessa = new ArquivoRemessa(fileStream);
                arquivoRemessa.Gerar(boletos);
            }
            
        }
    }
}