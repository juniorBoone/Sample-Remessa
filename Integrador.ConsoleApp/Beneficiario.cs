using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Integrador.ConsoleApp
{
    public sealed class Beneficiario : ValueObject
    {
        public Beneficiario(string banco, string agencia, string codigo, string cnpj)
        {
            Banco = banco;
            Agencia = agencia;
            Codigo = codigo;
            CNPJ = cnpj; 
        }

        public string Banco { get; }
        public string Agencia { get; }
        public string Codigo { get; }
        public string CNPJ { get; }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Banco;
            yield return Agencia;
            yield return Codigo;
        }
    }
}