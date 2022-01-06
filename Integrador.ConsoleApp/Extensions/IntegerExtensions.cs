namespace Integrador.ConsoleApp.Extensions
{
    public static class IntegerExtensions
    {
        public static string ToPadLeftZeros(this int value, int quantity) =>
            value.ToString().PadLeft(quantity, '0');
        
        public static string ToPadRightZeros(this int value, int quantity) =>
            value.ToString().PadRight(quantity, '0');

        public static bool Between(this int value, int start, int end) =>
            value >= start && value <= end;

        public static string AddCheckDigit(this int value)
        {
            var sum = 0;
            var number = value.ToString();
            for (int i = number.Length - 1, multiplier = 2; i >= 0; i--)
            {
                sum += (int)char.GetNumericValue(number[i]) * multiplier;
                if (++multiplier == 8) multiplier = 2;
            }
            var digit = (11 - (sum % 11)).ToString();

            if (digit == "11") digit = "0";
            else if (digit == "10") digit = "0";

            return number + digit;
        }
    }
}