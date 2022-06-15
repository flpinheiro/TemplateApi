using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateApi.Infra.Context;

namespace TemplateApi.UniTest.Utils
{
    internal static class MockedTemplateApicontextExtension
    {
        //Mock<TemplateApiContext>

        public static void SetSaveChanges(this Mock<TemplateApiContext> mock)
            => mock.Setup(x => x.SaveChanges()).Verifiable();

        public static void VerifySaveChanges(this Mock<TemplateApiContext> mock)
            => mock.Verify(x => x.SaveChanges(), Times.Once);

        public static void SetSaveChangesAsync(this Mock<TemplateApiContext> mock)
            => mock.Setup(x => x.SaveChangesAsync(default)).Verifiable();

        public static void VerifySaveChangesAsync(this Mock<TemplateApiContext> mock)
            => mock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }
}
