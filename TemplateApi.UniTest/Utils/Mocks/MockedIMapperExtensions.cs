using AutoMapper;
using TemplateApi.Domain.Configurations;

namespace TemplateApi.UniTest.Utils.Mocks;
internal static class MockedIMapperExtensions
{
    public static IMapper CreateIMapper()
    {

        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        return mappingConfig.CreateMapper();

    }

}

