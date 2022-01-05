using System.Collections.Generic;
using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;

namespace Integrador.ConsoleApp
{
    public sealed class EnderecoCompleto : ValueObject
    {
        private EnderecoCompleto() { }

        [JsonConstructor]
        public EnderecoCompleto(string rua, string numero, string complemento, string bairro, string cidade, string cep, string uF, string pais)
        {
            Rua = rua;
            Numero = numero;
            Complemento = string.IsNullOrWhiteSpace(complemento) ? string.Empty : complemento;
            Bairro = bairro;
            Cidade = cidade;
            Cep = cep;
            UF = uF;
            Pais = pais;
        }

        public string Rua { get; }
        public string Numero { get;  }
        public string Complemento { get; }
        public string Bairro { get; }
        public string Cidade { get;  }
        public string Cep { get;  }
        public string UF { get; }
        public string Pais { get; }

        public static EnderecoCompleto CriarVazio()
            => new EnderecoCompleto("", "","","","","","","");


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Rua;
            yield return Numero;
            yield return Complemento;
            yield return Bairro;
            yield return Cidade;
            yield return Cep;
            yield return UF;
            yield return Pais;
        }
    }
}