using System;
using System.Collections.Generic;
using TemplateApi.CrossCutting.Models;
using TemplateApi.CrossCutting.Utils;
using TemplateApi.CrossCutting.Validators;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.Domain.Models.Queries;

namespace TemplateApi.UniTest.TestUtils.Fixtures
{
    internal static class Fixture
    {
        public static Guid TestGuid = Guid.NewGuid();
        public static Person Person
        {
            get
            {
                var name = "test-name";
                var surname = "test-surname";
                var email = $"{name.RemoveAccents()}.{surname.RemoveAccents()}@test.com";
                return new(TestGuid, name, surname, RandomDateTime.NextDateOnly().ToShortDateString(), CPFValidator.GerarCpf(), email);
            }

        }
        public static IEnumerable<Person> People = new List<Person> { Person };
        public static PersonDto PersonDto = new(Person.Id, Person.Name, Person.SurName, Person.CPF, DateOnly.Parse(Person.BirthDay), Person.Email);
        public static AddPersonDto AddPersonDto = new(Person.Name, Person.SurName, Person.CPF, DateOnly.Parse(Person.BirthDay), Person.Email);
        public static UpdatePersonDto UpdatePersonDto = new(Person.Name, Person.SurName, DateOnly.Parse(Person.BirthDay), Person.Email);
        public static IEnumerable<PersonDto> PeopleDto = new List<PersonDto> { PersonDto };
        public static PaginationResponse PaginationResponse = new(1, 10);
        public static PersonQuery QueryDto = new();
        public static Pagination Pagination = new();
    }
}
