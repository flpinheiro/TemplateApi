using System;
using System.Linq;
using TemplateApi.CrossCutting.Exceptions;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.Domain.Services;
using TemplateApi.UniTest.Utils;
using Xunit;

namespace TemplateApi.UniTest.Api.Services
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

            var person = await _service.GetPersonById("");

            Assert.NotNull(person);
            Assert.Equal(Fixture.Person.Name, person.Name);
            Assert.Equal(Fixture.Person.SurName, person.SurName);
            Assert.Equal(Fixture.Person.Id, person.Id);

            _unitOfWork.MockPersonRepository.VerifyGetByIdAsync();
        }

        [Fact]
        public async void Should_GetAllPerson_Return_People_list()
        {
            _unitOfWork.MockPersonRepository.SetGetAllAsync();

            var people = await _service.GetAllPerson();

            Assert.NotNull(people);
            Assert.Equal(people.Count(), Fixture.People.Count());
            _unitOfWork.MockPersonRepository.VerifyGetAllAsync();
        }

        [Fact]
        public async void Should_GetByNameAsync_Return_People_list()
        {
            _unitOfWork.MockPersonRepository.SetGetByNameAsync();

            var people = await _service.GetPersonByName("");

            Assert.NotNull(people);
            Assert.Equal(people.Count(), Fixture.People.Count());
            _unitOfWork.MockPersonRepository.VerifyGetByNameAsync();
        }

        [Fact]
        public async void Should_Add_return_ok()
        {
            _unitOfWork.MockPersonRepository.SetAdd();

            var person = await _service.AddPerson(new Domain.Models.Dto.PersonDto());

            Assert.NotNull(person);
            Assert.Equal(Fixture.Person.Id, person.Id);
            _unitOfWork.MockPersonRepository.VerifyAdd();
        }

        [Fact]
        public async void Should_Add_throw_exception_stringName()
        {
            _unitOfWork.MockPersonRepository.SetAdd();
            await Assert.ThrowsAsync<Exception>(async () => await _service.AddPerson(new PersonDto { Name = "string" }));
            _unitOfWork.MockPersonRepository.VerifyAdd();
        }

        [Fact]
        public async void Should_Add_throw_exception()
        {
            _unitOfWork.MockPersonRepository.SetAddThrowException<Exception>();
            await Assert.ThrowsAsync<Exception>(async () => await _service.AddPerson(new PersonDto()));
            _unitOfWork.MockPersonRepository.VerifyAdd();
        }

        [Fact]
        public async void Should_Update_return_ok()
        {
            _unitOfWork.MockPersonRepository.SetAnyAsync();
            _unitOfWork.MockPersonRepository.SetUpdate();

            await _service.UpdatePerson("", new Domain.Models.Dto.PersonDto());

            _unitOfWork.MockPersonRepository.VerifyUpdate();
            _unitOfWork.MockPersonRepository.VerifyAnyAsync();

        }

        [Fact]
        public async void Should_Update_throw_exception()
        {
            _unitOfWork.MockPersonRepository.SetAnyAsync();
            _unitOfWork.MockPersonRepository.SetUpdateThrowException<Exception>();
            await Assert.ThrowsAsync<Exception>(async () => await _service.UpdatePerson("", new PersonDto()));
            _unitOfWork.MockPersonRepository.VerifyUpdate();
            _unitOfWork.MockPersonRepository.VerifyAnyAsync();
        }

        [Fact]
        public async void Should_Update_throw_PersonNotFoundexception()
        {
            _unitOfWork.MockPersonRepository.SetAnyAsync(false);
            await Assert.ThrowsAsync<PersonNotFoundException>(async () => await _service.UpdatePerson("", new PersonDto()));
            _unitOfWork.MockPersonRepository.VerifyAnyAsync();
        }


        [Fact]
        public async void Should_Delete_return_ok()
        {

            _unitOfWork.MockPersonRepository.SetDelete();
            _unitOfWork.MockPersonRepository.SetGetByIdAsync();

            await _service.DeletePerson("");

            _unitOfWork.MockPersonRepository.VerifyDelete();

            _unitOfWork.MockPersonRepository.VerifyGetByIdAsync();
        }

        [Fact]
        public async void Should_Delete_throw_exception()
        {

            _unitOfWork.MockPersonRepository.SetGetByIdAsync();
            _unitOfWork.MockPersonRepository.SetDeleteThrowException<Exception>();
            await Assert.ThrowsAsync<Exception>(async () => await _service.DeletePerson(""));
            _unitOfWork.MockPersonRepository.VerifyDelete();
            _unitOfWork.MockPersonRepository.VerifyGetByIdAsync();
        }

        [Fact]
        public async void Should_Delete_throw_PersonNotfoundexception()
        {

            _unitOfWork.MockPersonRepository.SetGetByIdAsync(null);
            await Assert.ThrowsAsync<PersonNotFoundException>(async () => await _service.DeletePerson(""));
            _unitOfWork.MockPersonRepository.VerifyGetByIdAsync();
        }

    }
}
