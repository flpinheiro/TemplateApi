using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace TemplateApi.CrossCutting.Utils;

public static class StringConverter
{

    /// <summary>
    /// Formata um cep para o padrão brasileito 
    /// </summary>
    /// <param name="cep"></param>
    /// <returns></returns>
    public static string FormatCep(this string cep)
    {
        var cepFormat = cep.OnlyNumber();
        return cepFormat.Length == 8 ? Convert.ToUInt64(cepFormat).ToString(@"00\.000\-000") : cepFormat;
    }

    /// <summary>
    /// Formata um numero de telefone para o padrão brasileito
    /// </summary>
    /// <param name="ddd"></param>
    /// <param name="mainNumber"></param>
    /// <returns></returns>
    public static string FormatPhoneNumber(string ddd, string mainNumber)
    {
        var dddFormat = ddd.OnlyNumber();
        var mainNumberFormat = mainNumber.OnlyNumber();
        var ret = string.Empty;
        if (dddFormat.Length != 0) ret += $"({dddFormat})";
        ret += mainNumberFormat.Length switch
        {
            8 => Convert.ToUInt64(mainNumberFormat).ToString(@"0000\-0000"),
            9 => Convert.ToUInt64(mainNumberFormat).ToString(@"00000\-0000"),
            _ => mainNumberFormat
        };

        return ret;
    }

    /// <summary>
    /// Formata uma string no padrão yyyMMdd para <see cref="DateTime"/>
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static DateTime? FormatDate(this string date)
    {
        var dateFormat = date.OnlyNumber();
        if (dateFormat.Length != 8) return null;

        try
        {
            return DateTime.ParseExact(date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //throw;
            return null;
        }
    }

    /// <summary>
    /// Formata uma string numeria para padrão de dinheiro. Deconsidera centavos. 
    /// </summary>
    /// <param name="capitalSocial"></param>
    /// <returns></returns>
    public static string FormatMoney(this string capitalSocial)
        => $"R$ {Convert.ToDecimal(capitalSocial.OnlyNumber()):#.00}";

    /// <summary>
    /// remove todos os caracteres não numerico da string
    /// </summary>
    /// <param name="str">string</param>
    /// <returns></returns>
    public static string OnlyNumber(this string str)
        => Regex.Replace(str, @"[^\d]", string.Empty);

    /// <summary>
    /// remove all white space from a string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string RemoveAllWhiteSpace(this string str)
        => Regex.Replace(str, @"\s+", "");

    /// <summary>
    /// Removedor de acentos
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string RemoveAccents(this string text)
    {
        var sbReturn = new StringBuilder();
        var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
        foreach (char letter in arrayText)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                sbReturn.Append(letter);
        }
        return sbReturn.ToString();
    }

    /// <summary>
    /// Make a string to a first letter to upper case, useful for own name. 
    /// </summary>
    /// <param name="text">string to be transformed</param>
    /// <returns>string with the first letter upper case</returns>
    public static string FirstCharToUpper(this string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return text;
        var collection = text.Trim() // remove white space after and before the string
            .ToLowerInvariant() // all to lower case
            .Split(" ") // create a collection o "words"
            .Where(a=> !string.IsNullOrWhiteSpace(a)); // remove all extra white space inside.
        var result = new List<string>();
        foreach (var item in collection)
        {
            // only to upper case if there are more than 2 lettes on the "word"
            if (item.Length > 2)
                result.Add($"{char.ToUpper(item[0])}{item[1..]}");
            else 
                result.Add(item);
        }

        return string.Join(" ", result);
    }

}



