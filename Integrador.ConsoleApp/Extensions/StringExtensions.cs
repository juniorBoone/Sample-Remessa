using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Integrador.ConsoleApp.Extensions
{
    public static class StringExtensions
    {
        public static string ToPadLeftZeros(this string value, int tamanho)
            => value.PadLeft(tamanho, '0');

        public static bool IsNumeric(this string value)
        {
            long number = 0;
            return long.TryParse(value, out number);
        }
        
        public static string ToBase64(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        
        public static string ClearText(this string value, char[] removeValues)
            => ClearText(value, string.Join("|", removeValues.Select(c => $"[{c}]")));

        public static string ClearText(this string value, string[] removeValues)
            => ClearText(value, string.Join("|", removeValues.Select(c => $"[{c}]")));

        internal static string ClearText(string value, string removeRegexp) 
            => new Regex(removeRegexp).Replace(value, "");

        public static string TruncateRight(this string value, int length)
        {
            if ((value.Length - length) <= 0)
                return value;

            return value.Substring(value.Length - length);
        }

        public static string TruncateLeft(this string value, int length)
        {
            if ((value.Length - length) <= 0)
                return value;

            return value.Substring(0, length);
        }

        public static string RemoveAccentsAndSpecialChars(this string text)
        {
            var sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);

            return Regex.Replace(sbReturn.ToString(), @"[^a-zA-Z|^0-9|^ ]", string.Empty);
        }

        public static string ToMd5Hash(this string valor)
        {
            using (var md5Hash = MD5.Create())
            {
                var builder = new StringBuilder();
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(valor));
                for (int i = 0; i < data.Length; i++)
                    builder.Append(data[i].ToString("X2"));
                return builder.ToString();
            }
        }

        public static string RecuperarDiretorioResouce()
        {
            string path = Directory.GetCurrentDirectory();
            path = path.Remove(path.LastIndexOf("\\"));
            path = path.Remove(path.LastIndexOf("\\"));
            path = path.Remove(path.LastIndexOf("\\") + 1);
            path += @"resource\";

            return path;
        }

        public static string RecuperarCodigoDoMes(in int month)
        {
            return month switch
            {
                1 => "1",
                2 => "2",
                3 => "3",
                4 => "4",
                5 => "5",
                6 => "6",
                7 => "7",
                8 => "8",
                9 => "9",
                10 => "0",
                11 => "N",
                12 => "D",
                _ => ""
            };
        }

        public static string FormatarTexto(this string valor, int tamanho, char padChar = ' ')
        {
            if (valor.Length > tamanho)
                valor = valor[..tamanho];

            return valor.RemoveAccentsAndSpecialChars().PadLeft(tamanho, padChar);
        }
    }
}