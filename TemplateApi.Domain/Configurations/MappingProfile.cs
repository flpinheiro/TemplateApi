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
            //DateOnly birthday;
            //CreateMap<AddPersonDto, PersonDto>();
            //CreateMap<Person, PersonDto>()
            //    .ForMember(x => x.BirthDay, y => y.MapFrom(src => !string.IsNullOrEmpty(src.BirthDay) && DateOnly.TryParse(src.BirthDay, out birthday) ? birthday : new DateOnly()))
            //    .ForMember(x => x.CPF, y => y.MapFrom(src => !string.IsNullOrEmpty(src.CPF) ? CPFValidator.ToCPFFormat(src.CPF) : string.Empty))
            //    .ReverseMap()
            //    .ForMember(x => x.BirthDay, y => y.MapFrom(src => src.BirthDay.ToShortDateString()))
            //    .ForMember(x => x.CPF, y => y.MapFrom(src => !string.IsNullOrEmpty(src.CPF) ? src.CPF.OnlyNumber() : string.Empty));

            CreateMap<Person, PersonDto>()
                .ForCtorParam(nameof(PersonDto.Id), opt=> opt.MapFrom(src => src.Id))
                .ForCtorParam(nameof(PersonDto.Name), opt => opt.MapFrom(src => src.Name))
                .ForCtorParam(nameof(PersonDto.SurName), opt => opt.MapFrom(src => src.SurName))
                .ForCtorParam(nameof(PersonDto.CPF), opt => opt.MapFrom(src => CPFValidator.ToCPFFormat(src.CPF)))
                .ForCtorParam(nameof(PersonDto.BirthDay), opt => opt.MapFrom(src => DateOnly.Parse(src.BirthDay)));

            CreateMap<AddPersonDto, Person>()
                .ForCtorParam(nameof(PersonDto.Id), opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForCtorParam(nameof(PersonDto.Name), opt => opt.MapFrom(src => src.Name))
                .ForCtorParam(nameof(PersonDto.SurName), opt => opt.MapFrom(src => src.SurName))
                .ForCtorParam(nameof(PersonDto.CPF), opt => opt.MapFrom(src => src.CPF.OnlyNumber()))
                .ForCtorParam(nameof(PersonDto.BirthDay), opt => opt.MapFrom(src => src.BirthDay.ToShortDateString()));
        }
    }
}
