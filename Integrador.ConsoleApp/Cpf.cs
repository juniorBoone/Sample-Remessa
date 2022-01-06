using System;
using System.Text;
using CSharpFunctionalExtensions;

namespace Integrador.ConsoleApp
{
    public sealed class Cpf : DocumentoBase
    {
        internal Cpf(string valor) : base(ETipoDocumento.CPF, valor) { }

        public override string Formatado => ToString();

        public static Result<Cpf> ValidarECriar(string valor)
        {
            var cpf = new Cpf(valor);
            if (cpf.Validar() is var resultado && resultado.IsFailure)
                return Result.Failure<Cpf>(resultado.Error);
            return cpf;
        }

        public static Cpf Recuperar(string valor) => Criar(valor);

        public static Cpf Criar(string valor)
        {
            if (ValidarECriar(valor) is var cpf && cpf.IsFailure)
                throw new CpfInvalidoException(cpf.Error);
            return cpf.Value;
        }

        public static implicit operator Cpf(string valor) => Criar(valor);

        public Result Validar()
        {
            if (!ValidarSequencia(Valor))
                return Result.Failure( "Valor inválido para CPF");
            if (!ValidarDigitos(Valor))
                return Result.Failure( "Valor inválido para CPF");
            return Result.Success();
        }

        public override string ToString() => string.Format(new CpfFormatter(), "{0}", this);

        #region Métodos de validação
        
        private static bool ValidarSequencia(string value)
        {
            var isEqualSequence = true;
            for (int i = 1; i < value.Length && isEqualSequence; i++)
                if (value[i] != value[0])
                    isEqualSequence = false;

            return !(isEqualSequence || value == "12345678909");
        }

        private static bool ValidarDigitos(string value)
        {
            var tempCpf = value.Substring(0, 9);
            var d1 = ObterModulo11(tempCpf);
            var d2 = ObterModulo11($"{tempCpf}{d1}");
            return value.EndsWith($"{d1}{d2}");
        }

        private static string ObterModulo11(string value)
        {
            var sum = 0;
            for (int i = value.Length - 1, multiplier = 2; i >= 0; i--)
            {
                sum += (int)char.GetNumericValue(value[i]) * multiplier;
                ++multiplier;
            }
            var mod = (sum % 11);
            if (mod == 0 || mod == 1) return "0";
            return (11 - mod).ToString();
        }
        #endregion
    }

    internal sealed class CpfFormatter : ICustomFormatter, IFormatProvider
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg is Cpf)
                return new StringBuilder((arg as Cpf).Valor).Insert(3, '.').Insert(7, '.').Insert(11, '-').ToString();
            else if (arg is string)
                return new StringBuilder(arg as string).Insert(3, '.').Insert(7, '.').Insert(11, '-').ToString();
            else
                throw new FormatException($"Não é possível formatar objetos de tipos diferentes de CPF ou string");
        }
        public object GetFormat(Type formatType)
            => (formatType == typeof(ICustomFormatter)) ? this : null;
    }

    public sealed class CpfInvalidoException : Exception
    {
        public CpfInvalidoException() { }

        public CpfInvalidoException(string message) : base(message) { }

        public CpfInvalidoException(string message, Exception inner) : base(message, inner) { }
    }
}