using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Integrador.ConsoleApp
{
    public sealed class DescontoAntecipacao : ValueObject
    {
        public DescontoAntecipacao(DateTime validoAte, decimal valor)
        {
            ValidoAte = validoAte;
            Valor = valor;
        }

        public DateTime ValidoAte { get; }
        public decimal Valor { get; }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ValidoAte;
            yield return Valor;
        }
    }
}