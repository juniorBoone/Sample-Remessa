using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Integrador.ConsoleApp.Juros
{
    public sealed class PercentualPorDia : ValueObject, IJuros
    {
        public PercentualPorDia(decimal valor)
        {
            Valor = valor;
        }
        public decimal Valor { get; }

        public Result<JurosCalculado> Calcular(decimal valorOriginal, int quantidadeDiasEmAtraso)
        {
            if (Valor <= 0) return Result.Failure<JurosCalculado>("Valor deve ser maior que zero");
            if (quantidadeDiasEmAtraso <= 0) return new JurosCalculado(0);
            return new JurosCalculado( Math.Round((valorOriginal * (Valor / 100M)) * quantidadeDiasEmAtraso, 2));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Valor;
        }
    }
}
