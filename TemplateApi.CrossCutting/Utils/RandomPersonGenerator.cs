﻿using System.Text.Json;

namespace TemplateApi.CrossCutting.Utils
{
    public static class RandomPersonGenerator
    {
        private static PersonList? personList = null;
        private static PersonList People
        {
            get
            {
                if (personList != null) return personList;
                string startupPath = Directory.GetCurrentDirectory();
                var file = Path.Combine(startupPath, "names.json");

                using StreamReader r = new(file);
                string json = r.ReadToEnd();
                personList = JsonSerializer.Deserialize<PersonList>(json) ?? new PersonList();
                return personList;
            }
        }
        private static readonly Random random = new();

        private static string GetName() => People.Names[random.Next(People.Names.Count)].FirstCharToUpper();
        private static string GetSurname() => People.Surnames[random.Next(People.Surnames.Count)].FirstCharToUpper();

        public static (string name, string surname, string email) GetPerson()
        {
            var name = GetName();
            var surname = GetSurname();
            var email = $"{name.RemoveAccents()}.{surname.RemoveAccents()}@template.com".ToLowerInvariant();
            return (name, surname, email);
        }

        private class PersonList
        {
            public List<string> Names { get; set; } = new List<string>(1000);
            public List<string> Surnames { get; set; } = new List<string>(1000);
        }
    }

}
