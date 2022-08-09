using System;
using TemplateApi.CrossCutting.Validators;

namespace TemplateApi.UniTest.CrossCutting
{
    public class CNPJValidatorTest
    {
        [Theory]
        [InlineData("19.353.248/0001-64", true)]
        [InlineData("99.532.115/0001-94", true)]
        [InlineData("67513448000180", true)]
        [InlineData("61138278000114", true)]
        [InlineData("test", false)]
        [InlineData("123456789", false)]
        [InlineData("12345678910", false)]
        [InlineData(null, false)]

        public void IsValid_should_check_if_is_a_valid_cnpj(string CNPJ, bool isValid)
        {
            var result = CNPJValidator.IsValidCNPJ(CNPJ);
            Assert.Equal(isValid, result);
        }


        [Theory]
        [InlineData("19353248000164", "19.353.248/0001-64")]
        [InlineData("43980138000107", "43.980.138/0001-07")]
        public void ToCNPJFormat_should_return_Formated_CNPJ(string CNPJ, string formated)
        {
            var result = CNPJValidator.ToCNPJFormat(CNPJ);
            Assert.Equal(formated, result);
        }


        [Theory]
        [InlineData("344416890")]
        [InlineData("3919939107400")]
        [InlineData("teste")]
        public void ToCNPJFormat_should_throw_ArgumentException(string invalidCNPJ)
        {
            Assert.Throws<ArgumentException>(() => CNPJValidator.ToCNPJFormat(invalidCNPJ));
        }
    }
}
