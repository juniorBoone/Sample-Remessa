using System.Collections.Generic;
using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;

namespace Integrador.ConsoleApp
{
    public enum ETipoDocumento
    {
        CPF,
        CNPJ
    }
    
    public class DocumentoBase : ValueObject
    {
        internal DocumentoBase(ETipoDocumento tipo, string valor)
        {
            Tipo = tipo;
            Valor = RemoverMascara(valor);
        }

        public ETipoDocumento Tipo { get; protected set; }
        public string Valor { get; protected set; }
        [JsonIgnore]
        public virtual string Formatado => Formatar();

        protected virtual string Formatar()
            => Valor.ToString();

        protected string RemoverMascara(string value)
            => (value != null) ? value.Replace(".", "").Replace("-", "").Replace("/", "").Trim() : "";

        public static DocumentoBase Criar(string tipo, string numero)
        {
            if (tipo == ETipoDocumento.CNPJ.ToString())
                return new Cnpj(numero);
            return new Cpf(numero);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Tipo;
            yield return Valor;
        }
    }
}