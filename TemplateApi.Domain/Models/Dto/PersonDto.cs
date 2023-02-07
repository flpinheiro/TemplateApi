namespace TemplateApi.Domain.Models.Dto;

public record PersonDto(Guid Id, string Name, string Surname, string CPF, DateOnly Birthday, string Email) : AddPersonDto(Name, Surname, CPF, Birthday, Email);
public record AddPersonDto(string Name, string Surname, string CPF, DateOnly Birthday, string Email);
public record UpdatePersonDto(string? Name, string? Surname, DateOnly? Birthday, string Email);

