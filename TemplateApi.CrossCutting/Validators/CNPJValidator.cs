using System.Text.RegularExpressions;
using TemplateApi.CrossCutting.Utils;

namespace TemplateApi.CrossCutting.Validators
{
    public static class CNPJValidator
    {
        public const string CNPJFormatRegex = @"/^\d{2}\.?\d{3}\.?\d{3}\/?\d{4}\-?\d{2}$/gm";
        public static Regex CNPJRegex { get; } = new Regex(CNPJFormatRegex);
        public static string Format(string cnpj)
        {
            if (!Regex.IsMatch(cnpj, CNPJFormatRegex)) throw new ArgumentException("that is not a cnpj");
            return Convert.ToUInt64(cnpj.OnlyNumber()).ToString(@"00\.000\.000\/000\-00");
        }
        public static bool IsValid(string cnpj)
        {
            if (!Regex.IsMatch(cnpj, CNPJFormatRegex)) return false;
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }
}
