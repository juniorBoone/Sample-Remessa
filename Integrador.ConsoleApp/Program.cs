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
            
            // TODO : Gerar arquivo conforme CNAB
            foreach (var boleto in boletos)
            {
                Console.WriteLine($"{boleto.NossoNumero}|{boleto.Valor}|{boleto.Beneficiario.Nome}");
                
            }
        }
    }
}