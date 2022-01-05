using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Integrador.ConsoleApp
{
    public sealed class Cedente : ValueObject
    {
        public Cedente(string banco, string agencia, string codigo)
        {
            Banco = banco;
            Agencia = agencia;
            Codigo = codigo;
        }

        public string Banco { get; }
        public string Agencia { get; }
        public string Codigo { get; }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Banco;
            yield return Agencia;
            yield return Codigo;
        }
    }
}