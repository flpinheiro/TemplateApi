using AutoMapper;
using TemplateApi.CrossCutting.Utils;
using TemplateApi.CrossCutting.Validators;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Domain.Configurations;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>()
            .ForCtorParam(nameof(PersonDto.Id), opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(PersonDto.Name), opt => opt.MapFrom(src => src.Name))
            .ForCtorParam(nameof(PersonDto.Surname), opt => opt.MapFrom(src => src.SurName))
            .ForCtorParam(nameof(PersonDto.CPF), opt => opt.MapFrom(src => CPFValidator.ToCPFFormat(src.CPF)))
            .ForCtorParam(nameof(PersonDto.Birthday), opt => opt.MapFrom(src => DateOnly.Parse(src.BirthDay)))
            .ForCtorParam(nameof(PersonDto.Email), opt => opt.MapFrom(src => src.Email));

        CreateMap<AddPersonDto, Person>()
            .ForCtorParam(nameof(PersonDto.Id), opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForCtorParam(nameof(PersonDto.Name), opt => opt.MapFrom(src => src.Name))
            .ForCtorParam(nameof(PersonDto.Surname), opt => opt.MapFrom(src => src.Surname))
            .ForCtorParam(nameof(PersonDto.CPF), opt => opt.MapFrom(src => src.CPF.OnlyNumber()))
            .ForCtorParam(nameof(PersonDto.Birthday), opt => opt.MapFrom(src => src.Birthday.ToShortDateString()))
            .ForCtorParam(nameof(PersonDto.Email), opt => opt.MapFrom(src => src.Email.ToLower()));
    }
}

public static class MappingExtensions
{
    private static IMapper? _mapper;
    private static IMapper Mapper
    {
        get
        {
            if (_mapper != null) return _mapper;
            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            }).CreateMapper();

            return _mapper;
        }
    }

    public static PersonDto ToDto(this Person person) => Mapper.Map<PersonDto>(person);
    public static IEnumerable<PersonDto> ToDto(this IEnumerable<Person> people) => Mapper.Map<IEnumerable<PersonDto>>(people);
    public static Person ToModel(this AddPersonDto addPersonDto) => Mapper.Map<Person>(addPersonDto);
}
