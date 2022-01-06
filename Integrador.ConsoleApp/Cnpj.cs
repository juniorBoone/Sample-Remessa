using System;
using System.Text;
using CSharpFunctionalExtensions;

namespace Integrador.ConsoleApp
{
    public sealed class Cnpj : DocumentoBase
    {
        internal Cnpj(string valor) : base(ETipoDocumento.CNPJ, valor) { }

        public override string Formatado => ToString();

        public static Result<Cnpj> ValidarECriar(string valor)
        {
            var cnpj = new Cnpj(valor);
            if (cnpj.Validar() is var resultado && resultado.IsFailure)
                return Result.Failure<Cnpj>(resultado.Error);
            return cnpj;
        }

        public static Cnpj Recuperar(string valor) => Criar(valor);

        public static Cnpj Criar(string valor)
        {
            if (ValidarECriar(valor) is var cnpj && cnpj.IsFailure)
                throw new CnpjInvalidoException(cnpj.Error);
            return cnpj.Value;
        }

        public static implicit operator Cnpj(string valor) => Criar(valor);

        public Result Validar()
        {
            if (ValidarSeDigitosSaoIdenticos(Valor))
                return Result.Failure( "Valor inválido para CNPJ");
            if (!ValidarDigitos(Valor))
                return Result.Failure("Valor inválido para CNPJ");
            return Result.Success();
        }

        public override string ToString()
            => string.Format(new CnpjFormatter(), "{0}", this);

        #region Métodos de validação
        
        private static bool ValidarSeDigitosSaoIdenticos(string valor)
        {
            var isEqualSequence = true;
            for (int i = 1; i < valor.Length && isEqualSequence; i++)
                if (valor[i] != valor[0])
                    isEqualSequence = false;

            return isEqualSequence;
        }

        private static bool ValidarDigitos(string valor)
        {
            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            var soma1 = 0;
            var soma2 = 0;
            for (var i = 0; i < 12; i++)
            {
                var d = ObterDigito(i, valor);
                soma1 += d * multiplicador1[i];
                soma2 += d * multiplicador2[i];
            }

            var resto = (soma1 % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var d1 = resto;
            soma2 += resto * multiplicador2[12];

            resto = (soma2 % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var d2 = resto;

            return valor.EndsWith($"{d1}{d2}");
        }

        private static int ObterDigito(int posicao, string valor)
        {
            int count = 0;
            for (int i = 0; i < valor.Length; i++)
            {
                if (char.IsDigit(valor[i]))
                {
                    if (count == posicao)
                    {
                        return valor[i] - '0';
                    }
                    count++;
                }
            }

            return 0;
        }
        #endregion
    }

    internal sealed class CnpjFormatter : ICustomFormatter, IFormatProvider
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg is Cnpj)
                return new StringBuilder((arg as Cnpj).Valor).Insert(2, '.').Insert(6, '.').Insert(10, '/').Insert(15, '-').ToString();
            else if (arg is string)
                return new StringBuilder(arg as string).Insert(2, '.').Insert(6, '.').Insert(10, '/').Insert(15, '-').ToString();
            else
                throw new FormatException($"Não é possível formatar objetos de tipos diferentes de CPF ou string");
        }
        public object GetFormat(Type formatType)
            => (formatType == typeof(ICustomFormatter)) ? this : null;
    }

    public sealed class CnpjInvalidoException : Exception
    {
        public CnpjInvalidoException() { }

        public CnpjInvalidoException(string message) : base(message) { }

        public CnpjInvalidoException(string message, Exception inner) : base(message, inner) { }
    }
}