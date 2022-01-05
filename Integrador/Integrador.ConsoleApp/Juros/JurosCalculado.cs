using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Integrador.ConsoleApp.Juros
{
    public sealed class JurosCalculado : ValueObject
    {
        private JurosCalculado() { }
        public JurosCalculado(decimal valor)
        {
            Valor = valor;
        }

        public decimal Valor { get; }

        public static JurosCalculado CriarZero() => new JurosCalculado(0m);

        public static implicit operator decimal(JurosCalculado juros) => juros.Valor;
        public static decimal operator +(decimal valor1, JurosCalculado valor2) => valor1 + valor2.Valor;
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Valor;
        }
    }
}
