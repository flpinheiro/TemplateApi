using AutoMapper;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Domain.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>()
                .ForMember(x => x.BirthDay, y => y.MapFrom(src => DateOnly.Parse(src.BirthDay)))
                .ReverseMap()
                .ForMember(x => x.BirthDay, y => y.MapFrom(src => src.BirthDay.ToShortDateString()));
        }
    }
}
