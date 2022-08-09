using System;
using TemplateApi.CrossCutting.Validators;

namespace TemplateApi.UniTest.CrossCutting
{
    public class CPFValidatorTest
    {
        [Theory]
        [InlineData("34441689004", true)]
        [InlineData("39199391074", true)]
        [InlineData("714.651.820-55", true)]
        [InlineData("245.757.080-89", true)]
        [InlineData("test", false)]
        [InlineData("123456789", false)]
        [InlineData("12345678910", false)]
        [InlineData(null, false)]

        public void IsValid_should_check_if_is_a_valid_cpf(string cpf, bool isValid)
        {
            var result = CPFValidator.IsValidCpf(cpf);
            Assert.Equal(isValid, result);
        }


        [Theory]
        [InlineData("34441689004", "344.416.890-04")]
        [InlineData("39199391074", "391.993.910-74")]
        [InlineData("71465182055", "714.651.820-55")]
        [InlineData("24575708089", "245.757.080-89")]
        public void ToCPFFormat_should_return_Formated_CPF(string cpf, string formated)
        {
            var result = CPFValidator.ToCPFFormat(cpf);
            Assert.Equal(formated, result);
        }


        [Theory]
        [InlineData("344416890")]
        [InlineData("3919939107400")]
        [InlineData("teste")]
        public void ToCPFFormat_should_throw_ArgumentException(string invalidCpf)
        {
            Assert.Throws<ArgumentException>(() => CPFValidator.ToCPFFormat(invalidCpf));
        }
    }
}
