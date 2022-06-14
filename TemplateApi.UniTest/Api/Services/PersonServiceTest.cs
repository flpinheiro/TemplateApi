using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dal;
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
            
            _unitOfWork.MockPersonRepository.Verify(x=> x.GetByIdAsync(""));
        }

        [Fact]
        public async void Should_GetAllPerson_Return_People_list()
        {
            _unitOfWork.MockPersonRepository.SetGetAllAsync();

            var people = await _service.GetAllPerson();

            Assert.NotNull(people);
            Assert.Collection(people, item => {
                Assert.Contains(Fixture.Person.Name, item.Name);
                Assert.Contains(Fixture.Person.SurName, item.SurName);
                Assert.Contains(Fixture.Person.Id, item.Id);
            });
        }

        [Fact]
        public async void Should_GetByNameAsync_Return_People_list()
        {
            _unitOfWork.MockPersonRepository.SetGetByNameAsync();

            var people = await _service.GetPersonByName("");

            Assert.NotNull(people);
            Assert.Collection(people, item => {
                Assert.Contains(Fixture.Person.Name, item.Name);
                Assert.Contains(Fixture.Person.SurName, item.SurName);
                Assert.Contains(Fixture.Person.Id, item.Id);
            });
        }

        [Fact]
        public async void Should_AnyAsync_return_true()
        {
            _unitOfWork.MockPersonRepository.SetAnyAsync();
            _unitOfWork.MockPersonRepository.SetAdd();

            var person = await _service.AddPerson(new Domain.Models.Dto.PersonDto());

            Assert.NotNull(person);
            Assert.Equal(Fixture.Person.Id, person.Id);
        }

    }
}
