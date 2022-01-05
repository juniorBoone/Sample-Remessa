using System.Collections.Generic;
using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;

namespace Integrador.ConsoleApp
{
    public sealed class Beneficiario : ValueObject
    {
        private Beneficiario() { }
        [JsonConstructor]
        public Beneficiario(string nome, string nomeSocial, DocumentoBase documento, EnderecoCompleto endereco)
        {
            Nome = nome;
            NomeSocial = nomeSocial;
            Endereco = endereco;
            Documento = documento;
        }

        public string Nome { get; }
        public string NomeSocial { get; }
        public DocumentoBase Documento { get;  }
        public EnderecoCompleto Endereco { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Nome;
            yield return NomeSocial;
            yield return Documento;
            yield return Endereco;
        }
    }
}