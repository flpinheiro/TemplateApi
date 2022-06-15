using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateApi.Domain.Models.Dal;

namespace TemplateApi.UniTest.Utils
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
            LastUpdate = DateTime.Now,
        };

        private static Person _person = new Person
        {
            BirthDay = "1968-12-21",
            Name = "Testered",
            SurName = "Test",
            Id = "Testered-Test",
            IsUpdated = false,
            LastUpdate = DateTime.Now,
        };

        public static IEnumerable<Person> People = new List<Person> { Person, _person };
    }
}
