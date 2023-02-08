using TemplateApi.CrossCutting.Utils;

namespace TemplateApi.UniTest.CrossCutting.Utils
{
    public class StringConverterTest
    {
        [Fact]
        public void RemoveAscent_Test()
        {
            var test = "áéíóú";
            var expected = "aeiou";

            var actual = test.RemoveAccents();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("test", "Test")]
        [InlineData("   test   ", "Test")]
        [InlineData("test test", "Test Test")]
        [InlineData("test     test", "Test Test")]
        [InlineData("of", "of")]
        [InlineData("test of test", "Test of Test")]
        [InlineData("test        of          test", "Test of Test")]
        [InlineData("felipe luís pinheiro", "Felipe Luís Pinheiro")]
        [InlineData("123456", "123456")]
        public void FirstToUpperTest(string test, string expected)
        {
            var actual = test.FirstCharToUpper();
            Assert.Equal(expected, actual);
        }

    }
}
