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
        {
        
        }
    }
}
