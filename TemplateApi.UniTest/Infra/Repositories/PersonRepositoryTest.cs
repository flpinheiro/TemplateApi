﻿#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
using Moq;
using Serilog;
using System;
using TemplateApi.CrossCutting.Enums;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Queries;
using TemplateApi.Infra;
using TemplateApi.Infra.Context;
using TemplateApi.UniTest.TestUtils.Fixtures;
using TemplateApi.UniTest.TestUtils.Mocks;
using TemplateApi.UniTest.TestUtils.Mocks.Repositories;

namespace TemplateApi.UniTest.Infra.Repositories
{
    public sealed class PersonRepositoryTest : IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TemplateApiContext _context;
        private readonly Mock<ILogger>? _mockLogger;
        public PersonRepositoryTest()
        {
            _context = MockedTemplateApicontextExtension.CreateTempleateApiContextInMemory();

            _mockLogger = new Mock<ILogger>();
            _mockLogger.SetLogger();
            _unitOfWork = new UnitOfWork(_context, _mockLogger.Object);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _unitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void UnitOfWorkConstructorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new TemplateApiContext(null));
            Assert.Throws<ArgumentNullException>(() => new UnitOfWork(null, null));
            Assert.Throws<ArgumentNullException>(() => new UnitOfWork(_context, null));
        }

        [Fact]
        public async void GetAllAsyncTest()
        {
            var people = await _unitOfWork.PersonRepository.GetPeopleAsync(Fixture.QueryDto);

            Assert.NotNull(people);
            Assert.NotEmpty(people);
        }


        [Fact]
        public void CountAllPaginatedTest()
        {
            var count = _unitOfWork.PersonRepository.CountPeople(Fixture.QueryDto, Fixture.Pagination);

            Assert.NotNull(count);
            Assert.InRange(count.Total, 1, 1010);
            Assert.InRange(count.Pages, 1, 110);
        }

        [Fact]
        public async void GetByIdAsyncTest()
        {
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(Fixture.Person?.Id ?? Guid.Empty);
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
            var name = Fixture.Person.Name ?? string.Empty;
            var query = new PersonQuery { Name = name };
            var people = await _unitOfWork.PersonRepository.GetPeopleAsync(query);
            Assert.NotNull(people);
        }
        [Fact]
        public async void GetPersonByNamePaginatedTest()
        {
            var name = Fixture.Person.Name ?? string.Empty;
            var query = new PersonQuery { Name = name, Cpf = "34441689004" };
            var people = await _unitOfWork.PersonRepository.GetPeoplePaginatedAsync(query, Fixture.Pagination);
            Assert.NotNull(people);
        }
        //[Fact]
        //public void CountPersonByNamePaginatedTest()
        //{
        //    var name = Fixture.Person.Name ?? string.Empty;
        //    var count = _unitOfWork.PersonRepository.CountByName(name, new Pagination());
        //    Assert.NotNull(count);
        //    Assert.InRange(count.Total, 1, 10);
        //    Assert.InRange(count.Pages, 1, 10);
        //}
        [Theory]
        [InlineData(SortAsEnum.Asc, PersonEnum.Name)]
        [InlineData(SortAsEnum.Desc, PersonEnum.Name)]
        [InlineData(SortAsEnum.Asc, PersonEnum.Surname)]
        [InlineData(SortAsEnum.Desc, PersonEnum.Surname)]
        [InlineData(SortAsEnum.Asc, PersonEnum.Cpf)]
        [InlineData(SortAsEnum.Desc, PersonEnum.Cpf)]
        [InlineData(SortAsEnum.Asc, PersonEnum.Birthday)]
        [InlineData(SortAsEnum.Desc, PersonEnum.Birthday)]
        public async void GetPersonSortedTest(SortAsEnum sortAs, PersonEnum sortBy)
        {
            var query = new PersonQuery
            {
                SortAs = sortAs,
                SortBy = sortBy,
            };
            var people = await _unitOfWork.PersonRepository.GetPeoplePaginatedAsync(query, Fixture.Pagination);
            Assert.NotNull(people);
        }
        [Fact]
        public async void AnyAsyncTest()
        {
            var result = await _unitOfWork.PersonRepository.AnyAsync(Fixture.Person?.Id ?? Guid.Empty);
            Assert.True(result);
        }

        [Fact]
        public async void AddTest()
        {
            var person = new Person(Guid.NewGuid(), "Add-name", "Add-surname", (new DateOnly()).ToShortDateString(), "34024804090", "test@test.com");
            //{
            //    Id = "Add",
            //    BirthDay = (new DateOnly()).ToShortDateString(),
            //    Name = "Add-name",
            //    SurName = "Add-surname",
            //    CPF = "34024804090",
            //};
            _unitOfWork.PersonRepository.Add(person);
            _unitOfWork.Save();
            var result = await _unitOfWork.PersonRepository.GetByIdAsync(person.Id);
            //var people = await _unitOfWork.PersonRepository.GetAllAsync();
            Assert.NotNull(result);
            Assert.False(person.IsUpdated);
            //Assert.Equal(Fixture.People.Count() + 1, people.Count());
        }

        [Fact]
        public void UpdateTest()
        {
            var person = Fixture.Person;
            if (person == null) return;
            person.Name = "update";
            person.SurName = "update";

            _unitOfWork.PersonRepository.Update(person);
            _unitOfWork.Save();
            //var people = await _unitOfWork.PersonRepository.GetAllAsync();
            Assert.NotNull(person);
            Assert.True(person.IsUpdated);
            //Assert.Equal(Fixture.People.Count(), people.Count());
        }

        [Fact]
        public async void DeleteTest()
        {
            _unitOfWork.PersonRepository.Delete(Fixture.Person);
            await _unitOfWork.SaveAsync();

            //var people = await _unitOfWork.PersonRepository.GetAllAsync();
            //Assert.Equal(Fixture.People.Count() - 1, people.Count());

        }
    }
}
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
