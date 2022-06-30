using Moq;
using Serilog;
using System;
using System.Linq;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Infra;
using TemplateApi.Infra.Context;
using TemplateApi.UniTest.Utils;
using Xunit;

namespace TemplateApi.UniTest.Infra.Repositories
{
    public class PersonRepositoryTest : IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private TemplateApiContext _context;
        private Mock<ILogger>? _mockLogger;
        private AutoMapper.IMapper? _mapper;
        public PersonRepositoryTest()
        {
            _context = MockedTemplateApicontextExtension.CreateTempleateApiContextInMemory();

            _mockLogger = new Mock<ILogger>();
            _mockLogger.SetLogger();
            _mapper = MockedIMapperExtensions.CreateIMapper();
            _unitOfWork = new UnitOfWork(_context, _mapper, _mockLogger.Object);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _unitOfWork.Dispose();
        }

        [Fact]
        public void UnitOfWorkConstructorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new UnitOfWork(null, null, null));
            Assert.Throws<ArgumentNullException>(() => new UnitOfWork(_context, null, null));
            Assert.Throws<ArgumentNullException>(() => new UnitOfWork(_context, _mapper, null));
            Assert.NotNull(_unitOfWork.Mapper);
        }

        [Fact]
        public async void GetAllAsyncTest()
        {
            var people = await _unitOfWork.PersonRepository.GetAllAsync();

            Assert.NotNull(people);
            Assert.Equal(Fixture.People.Count(), people.Count());
        }

        [Fact]
        public async void GetByIdAsyncTest()
        {
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(Fixture.Person?.Id ?? "");
            Assert.NotNull(person);
            if (person != null && Fixture.Person != null)
            {
                Assert.Equal(Fixture.Person.Id, person.Id);
                Assert.Equal(Fixture.Person.Name, person.Name);
                Assert.Equal(Fixture.Person.SurName, person.SurName);
            }
        }

        [Fact]
        public async void GetPersonByNameTest()
        {
            var name = Fixture.Person.Name;
            var people = await _unitOfWork.PersonRepository.GetByNameAsync(name);
            Assert.NotNull(people);
            //foreach (var item in people)
            //{
            //    var names = new string[] { item.Name, item.SurName };
            //    Assert.Contains(name, names);
            //}
        }

        [Fact]
        public async void AnyAsyncTest()
        {
            var result = await _unitOfWork.PersonRepository.AnyAsync(Fixture.Person?.Id ?? "");
            Assert.True(result);
        }

        [Fact]
        public async void AddTest()
        {
            var person = new Person()
            {
                Id = "Add",
                BirthDay = (new DateOnly()).ToShortDateString(),
                Name = "Add-name",
                SurName = "Add-surname"
            };
            _unitOfWork.PersonRepository.Add(person);
            _unitOfWork.Save();
            var result = await _unitOfWork.PersonRepository.GetByIdAsync(person.Id);
            var people = await _unitOfWork.PersonRepository.GetAllAsync();
            Assert.NotNull(result);
            Assert.False(person.IsUpdated);
            Assert.Equal(Fixture.People.Count() + 1, people.Count());
        }

        [Fact]
        public async void UpdateTest()
        {
            var person = Fixture.Person;
            if (person == null) return;
            person.Name = "update";
            person.SurName = "update";

            _unitOfWork.PersonRepository.Update(person);
            _unitOfWork.Save();
            var people = await _unitOfWork.PersonRepository.GetAllAsync();
            Assert.NotNull(person);
            Assert.True(person.IsUpdated);
            Assert.Equal(Fixture.People.Count(), people.Count());
        }

        [Fact]
        public async void DeleteTest()
        {
            _unitOfWork.PersonRepository.Delete(Fixture.Person);
            await _unitOfWork.SaveAsync();

            var people = await _unitOfWork.PersonRepository.GetAllAsync();
            Assert.Equal(Fixture.People.Count() - 1, people.Count());
        }
    }
}
