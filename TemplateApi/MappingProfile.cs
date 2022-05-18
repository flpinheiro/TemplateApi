using AutoMapper;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
        }
    }
}
