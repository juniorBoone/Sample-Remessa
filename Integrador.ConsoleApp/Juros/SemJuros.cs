using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Integrador.ConsoleApp.Juros
{
    public sealed class SemJuros : ValueObject, IJuros
    {
        public SemJuros() { }
        
        public Result<JurosCalculado> Calcular(decimal valorOriginal, int quantidadeDiasEmAtraso)
            => new JurosCalculado(0);


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return true;
        }
    }
}