using System;
using System.Collections.Generic;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.UniTest.Utils.Fixtures
{
    internal static class Fixture
    {
        public static Person Person = new Person
        {
            BirthDay = "1985-05-01",
            Name = "Tester",
            SurName = "Tested",
            Id = "Tester-Tested",
            IsUpdated = false,
            LastUpdate = DateTime.UtcNow,
            CPF = "87246002022",
        };

        private static Person _person = new Person
        {
            BirthDay = "1968-12-21",
            Name = "Testered",
            SurName = "Test",
            Id = "Testered-Test",
            IsUpdated = false,
            LastUpdate = DateTime.UtcNow,
            CPF = "59143964095",
        };

        public static IEnumerable<Person> People = new List<Person> { Person, _person };

        public static PersonDto PersonDto = new PersonDto()
        {
            BirthDay = new DateOnly(1985, 5, 1),
            Name = "test",
            SurName = "tested",
            Id = "test_id",
            CPF = "59143964095",
        };

        public static AddPersonDto AddPersonDto = new AddPersonDto
        {
            BirthDay = new DateOnly(1985, 5, 1),
            Name = "test",
            SurName = "tested",
            CPF = "59143964095",
        };

        public static IEnumerable<PersonDto> PeopleDto = new List<PersonDto> { PersonDto };
    }
}
