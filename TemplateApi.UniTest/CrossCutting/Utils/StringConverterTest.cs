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
        [InlineData("test", "Test")]
        [InlineData("test test", "Test Test")]
        public void FirstToUpperTest(string test, string expected)
        {
            var actual = test.FirstCharToUpper();
            Assert.Equal(expected, actual);
        }

    }
}
