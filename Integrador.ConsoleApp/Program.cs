using System;

namespace Integrador.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gerador CNAB banco Eximia");
            var repositorio = new RepositorioBoletos();
            var boletos = repositorio.RecuperarTodos();
            var beneficiario = repositorio.RecuperarBeneficiario();

            new ArquivoRemessa().Gerar(beneficiario, boletos);
        }
    }
}