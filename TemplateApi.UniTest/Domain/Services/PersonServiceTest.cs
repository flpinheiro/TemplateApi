#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using System;
using System.Linq;
using TemplateApi.CrossCutting.Exceptions;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Services;
using TemplateApi.UniTest.TestUtils.Fixtures;
using TemplateApi.UniTest.TestUtils.Mocks.Repositories;

namespace TemplateApi.UniTest.Domain.Services
{
    public class PersonServiceTest
    {
        private readonly MockedUnitOfWork _unitOfWork;

        private readonly IPersonService _service;

        public PersonServiceTest()
        {
            _unitOfWork = new MockedUnitOfWork();

            _service = new PersonService(_unitOfWork);
        }

        [Fact]
        public void Should_PersonConstructor_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new PersonService(null));
        }

        [Fact]
        public async void Should_GetPersonById_return_Person()
        {
            _unitOfWork.MockPersonRepository.SetGetByIdAsync();

            var person = await _service.Get(Guid.Empty);

            Assert.NotNull(person);
            Assert.Equal(Fixture.Person.Name, person?.Name);
            Assert.Equal(Fixture.Person.SurName, person?.Surname);
            Assert.Equal(Fixture.Person.Id, person?.Id);

            _unitOfWork.MockPersonRepository.VerifyGetByIdAsync();
        }

        [Fact]
        public async void Should_GetAllPerson_Return_People_list()
        {
            _unitOfWork.MockPersonRepository.SetGetAllAsync();

            var people = await _service.Get(Fixture.QueryDto);

            Assert.NotNull(people);
            Assert.Equal(people.Count(), Fixture.People.Count());
        }

        [Fact]
        public async void Should_GetAllPersonPaginated_Return_People_list()
        {
            _unitOfWork.MockPersonRepository.SetGetAllAsync();

            var people = await _service.Get(Fixture.QueryDto, Fixture.Pagination);

            Assert.NotNull(people);
            Assert.Equal(people.Count(), Fixture.People.Count());
        }

        [Fact]
        public async void Should_Add_return_ok()
        {
            _unitOfWork.MockPersonRepository.SetAdd();

            var id = await _service.Add(Fixture.AddPersonDto);

            Assert.Equal(Fixture.Person.Id, id);
            _unitOfWork.MockPersonRepository.VerifyAdd();
        }

        [Fact]
        public async void Should_Update_return_ok()
        {
            _unitOfWork.MockPersonRepository.SetUpdate();
            _unitOfWork.MockPersonRepository.SetGetByIdAsync();

            await _service.Update(Guid.Empty, Fixture.UpdatePersonDto);

            _unitOfWork.MockPersonRepository.VerifyUpdate();
            _unitOfWork.MockPersonRepository.VerifyGetByIdAsync();

        }

        [Fact]
        public async void Should_Update_throw_exception()
        {
            _unitOfWork.MockPersonRepository.SetGetByIdAsync();
            _unitOfWork.MockPersonRepository.SetUpdateThrowException<Exception>();
            await Assert.ThrowsAsync<Exception>(async () => await _service.Update(Guid.Empty, Fixture.UpdatePersonDto));
            _unitOfWork.MockPersonRepository.VerifyUpdate();
            _unitOfWork.MockPersonRepository.VerifyGetByIdAsync();
        }

        [Fact]
        public async void Should_Update_throw_PersonNotFoundexception()
        {
            _unitOfWork.MockPersonRepository.SetGetByIdAsync(null);
            await Assert.ThrowsAsync<PersonNotFoundException>(async () => await _service.Update(Guid.Empty, Fixture.UpdatePersonDto));
            _unitOfWork.MockPersonRepository.VerifyGetByIdAsync();
        }

        [Fact]
        public async void Should_Delete_return_ok()
        {

            _unitOfWork.MockPersonRepository.SetDelete();
            _unitOfWork.MockPersonRepository.SetGetByIdAsync();

            await _service.Delete(Guid.Empty);

            _unitOfWork.MockPersonRepository.VerifyDelete();

            _unitOfWork.MockPersonRepository.VerifyGetByIdAsync();
        }

        [Fact]
        public async void Should_Delete_throw_exception()
        {

            _unitOfWork.MockPersonRepository.SetGetByIdAsync();
            _unitOfWork.MockPersonRepository.SetDeleteThrowException<Exception>();
            await Assert.ThrowsAsync<Exception>(async () => await _service.Delete(Guid.Empty));
            _unitOfWork.MockPersonRepository.VerifyDelete();
            _unitOfWork.MockPersonRepository.VerifyGetByIdAsync();
        }

        [Fact]
        public async void Should_Delete_throw_PersonNotfoundexception()
        {

            _unitOfWork.MockPersonRepository.SetGetByIdAsync(null);
            await Assert.ThrowsAsync<PersonNotFoundException>(async () => await _service.Delete(Guid.Empty));
            _unitOfWork.MockPersonRepository.VerifyGetByIdAsync();
        }

        [Fact]
        public void Should_ExportToExcel_produces_file()
        {
            var file = _service.ExportToExcel(Fixture.PeopleDto);
            Assert.NotNull(file);
        }

        [Fact]
        public void Should_Count_return_Ok()
        {
            _unitOfWork.MockPersonRepository.SetCount();
            var countAll = _service.Count(Fixture.QueryDto, Fixture.Pagination);
            Assert.Equal(Fixture.PaginationResponse, countAll);
        }

    }
}
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
