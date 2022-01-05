using CSharpFunctionalExtensions;

namespace Integrador.ConsoleApp.Juros
{
    public interface IJuros
    {
        Result<JurosCalculado> Calcular(decimal valorOriginal, int quantidadeDiasEmAtraso);
    }
}
