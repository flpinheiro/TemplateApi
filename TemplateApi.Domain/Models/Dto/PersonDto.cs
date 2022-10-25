namespace TemplateApi.Domain.Models.Dto;

public record PersonDto(string Id, string Name, string SurName, string CPF, DateOnly BirthDay) : AddPersonDto(Name, SurName, CPF, BirthDay);
public record AddPersonDto(string Name, string SurName, string CPF, DateOnly BirthDay);
public record UpdatePersonDto(string? Name, string? SurName, DateOnly? BirthDay);

