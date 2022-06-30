using System.Text.RegularExpressions;

namespace TemplateApi.CrossCutting.Utils
{
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

    }
}



