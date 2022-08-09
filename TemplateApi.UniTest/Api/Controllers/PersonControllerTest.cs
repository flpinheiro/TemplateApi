using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateApi.Controllers;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.UniTest.Utils;

namespace TemplateApi.UniTest.Api.Controllers
{
    public class PersonControllerTest
    {
        private readonly Mock<IPersonService> mock;
        private readonly PersonController _controller;

        public PersonControllerTest()
        {
            mock = new Mock<IPersonService>();
            _controller = new PersonController(mock.Object);

            mock.SetGetAllPeron();
            mock.SetGetPersonById();
            mock.SetAddPerson();
            mock.SetUpdatePerson();
            mock.SetDeletePerson();
        }

        [Fact]
        public void Constructor_shouldReturnException()
        {
            Assert.Throws<ArgumentNullException>(() => new PersonController(null));
        }

        [Fact]
        public async Task GetAllTest_shouldReturnPersonList()
        {
            var result = await _controller.GetAll();

            var okObject = result.Result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okObject?.StatusCode);
            var model = okObject?.Value;
            Assert.IsAssignableFrom<IEnumerable<PersonDto>>(model);
            Assert.Equal(Fixture.PeopleDto, model);
            mock.VerifyGetAllPerson();
        }

        [Fact]
        public async Task GetPersonById_ShouldReturnOnePerson()
        {
            var result = await _controller.GetPerson("");

            var okObject = result.Result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okObject?.StatusCode);
            var model = okObject?.Value;
            Assert.IsAssignableFrom<PersonDto>(model);
            Assert.Equal(Fixture.PersonDto, model);
            mock.VerifyGetPersonById();
        }

        [Fact]
        public async Task GetPersonByName_ShouldReturnPersonList()
        {
            var returnList = Fixture.PeopleDto.Where(a => a.Name != null && a.Name.Equals(Fixture.PersonDto.Name)).ToList();
            mock.SetGetPersonByName(returnList);
            var result = await _controller.GetPersonByName("");

            var okObject = result.Result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okObject?.StatusCode);
            var model = okObject?.Value;
            Assert.IsAssignableFrom<IEnumerable<PersonDto>>(model);
            Assert.Equal(returnList, model);
            mock.VerifyGetPersonByName();
        }

        [Fact]
        public async void AddPerson_ShouldReturn201()
        {
            var result = await _controller.Create(Fixture.PersonDto);

            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult?.StatusCode);
            var model = createdAtActionResult?.Value;
            Assert.IsAssignableFrom<PersonDto>(model);
            Assert.Equal(Fixture.PersonDto, model);
            mock.VerifyAddPerson();
        }



        [Fact]
        public async void UpdatePerson_ShouldReturn200()
        {
            var result = await _controller.Edit("", Fixture.PersonDto);

            var okObject = result.Result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okObject?.StatusCode);
            var model = okObject?.Value;
            Assert.IsAssignableFrom<PersonDto>(model);
            Assert.Equal(Fixture.PersonDto, model);
            mock.VerifyUpdatePerson();
        }

        [Fact]
        public async void DeletePerson_ShouldReturn200()
        {
            var result = await _controller.Delete("");

            var okObject = result.Result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okObject?.StatusCode);
            var model = okObject?.Value;
            Assert.IsAssignableFrom<PersonDto>(model);
            Assert.Equal(Fixture.PersonDto, model);
            mock.VerifyDeletePerson();
        }
    }
}
