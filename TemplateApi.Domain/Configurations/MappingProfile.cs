using AutoMapper;
using TemplateApi.CrossCutting.Utils;
using TemplateApi.CrossCutting.Validators;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Domain.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            DateOnly birthday;
            CreateMap<AddPersonDto, PersonDto>();
            CreateMap<Person, PersonDto>()
                .ForMember(x => x.BirthDay, y => y.MapFrom(src => !string.IsNullOrEmpty(src.BirthDay) && DateOnly.TryParse(src.BirthDay, out birthday) ? birthday : new DateOnly()))
                .ForMember(x => x.CPF, y => y.MapFrom(src => !string.IsNullOrEmpty(src.CPF) ? CPFValidator.ToCPFFormat(src.CPF) : string.Empty))
                .ReverseMap()
                .ForMember(x => x.BirthDay, y => y.MapFrom(src => src.BirthDay.ToShortDateString()))
                .ForMember(x => x.CPF, y => y.MapFrom(src => !string.IsNullOrEmpty(src.CPF) ? src.CPF.OnlyNumber() : string.Empty));
        }
    }
}
