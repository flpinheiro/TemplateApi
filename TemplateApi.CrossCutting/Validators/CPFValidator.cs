using System.Text.RegularExpressions;
using TemplateApi.CrossCutting.Utils;

namespace TemplateApi.CrossCutting.Validators;

public static class CPFValidator
{
    public const int Size = 11;
    public static Regex CPFRegex { get; } = new Regex(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$");
    /// <summary>
    /// Formata um cpf para o padrão brasileito 
    /// </summary>
    /// <param name="cpf"></param>
    /// <returns></returns>
    public static string ToCPFFormat(this string cpf)
    {
        if (!CPFRegex.IsMatch(cpf)) throw new ArgumentException("Not a valid Cpf");
        return Convert.ToUInt64(cpf.OnlyNumber()).ToString(@"000\.000\.000\-00");
    }
    public static bool IsValidCpf(this string cpf)
    {
        if (string.IsNullOrEmpty(cpf) || !CPFRegex.IsMatch(cpf)) return false;
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        cpf = cpf.Trim().Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;

        for (int j = 0; j < 10; j++)
            if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                return false;

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        string digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cpf.EndsWith(digito);
    }

    public static string GerarCpf()
    {
        int soma = 0, resto = 0;
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        Random rnd = new Random();
        string semente = rnd.Next(100000000, 999999999).ToString();

        for (int i = 0; i < 9; i++)
            soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        semente = semente + resto;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        semente = semente + resto;
        return semente;
    }
}



