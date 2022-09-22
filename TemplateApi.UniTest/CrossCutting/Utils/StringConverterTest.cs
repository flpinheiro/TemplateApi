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

    }
}
