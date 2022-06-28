using System;
using System.Linq;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Infra.Context;
using TemplateApi.Infra.Repositories;
using TemplateApi.UniTest.Utils;
using Xunit;

namespace TemplateApi.UniTest.Infra.Repositories
{
    public class PersonRepositoryTest: IDisposable
    {
        private IPersonRepository _repository;
        private TemplateApiContext _context;
        public PersonRepositoryTest()
        {
            _context = MockedTemplateApicontextExtension.CreateTempleateApiContextInMemory();
            _context.Database.EnsureCreated();
            _context.Seed();
            _repository = new PersonRepository(_context);

        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async void GetAllAsyncTest()
        {
            var people = await _repository.GetAllAsync();

            Assert.NotNull(people);
            Assert.Equal(Fixture.People.Count(),people.Count());
            Assert.Equal(Fixture.People,people);
        }

        [Fact]
        public async void GetByIdAsyncTest()
        {
            var person = await _repository.GetByIdAsync(Fixture.Person?.Id ??"");
            Assert.NotNull(person);
            Assert.Equal(Fixture.Person, person);
        }
    }
}
