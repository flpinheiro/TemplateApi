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
using Xunit;

namespace TemplateApi.UniTest.Api.Controllers
{
    public class PersonControllerTest
    {
        private readonly Mock<IPersonService> _service;
        private readonly PersonDto _personDto;
        private readonly IList<PersonDto> _people;
        private readonly PersonController _controller;

        public PersonControllerTest()
        {
            _service = new Mock<IPersonService>();
            _controller = new PersonController(_service.Object);
            _personDto = new PersonDto()
            {
                BirthDay = new DateOnly(1985, 5, 1),
                Name = "test",
                SurName = "tested",
                Id = "test_id"
            };
            _people = new List<PersonDto>() { _personDto, new PersonDto() };


            _service.Setup(x => x.GetAllPerson()).ReturnsAsync(_people).Verifiable();
            _service.Setup(x => x.GetPersonById(It.IsAny<string>())).ReturnsAsync(_personDto).Verifiable();
            _service.Setup(x => x.AddPerson(It.IsAny<PersonDto>())).ReturnsAsync(_personDto).Verifiable();
            _service.Setup(x => x.UpdatePerson(It.IsAny<string>(), It.IsAny<PersonDto>())).Verifiable();
            _service.Setup(x => x.DeletePerson(It.IsAny<string>())).ReturnsAsync(_personDto).Verifiable();
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
            Assert.Equal(_people, model);
        }

        [Fact]
        public async Task GetPersonById_ShouldReturnOnePerson()
        {
            var result = await _controller.GetPerson("");

            var okObject = result.Result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okObject?.StatusCode);
            var model = okObject?.Value;
            Assert.IsAssignableFrom<PersonDto>(model);
            Assert.Equal(_personDto, model);
        }

        [Fact]
        public async Task GetPersonByName_ShouldReturnPersonList()
        {
            var returnList = _people.Where(a => a.Name != null && a.Name.Equals(_personDto.Name)).ToList();
            _service.Setup(x => x.GetPersonByName(It.IsAny<string>())).ReturnsAsync(returnList).Verifiable();
            var result = await _controller.GetPersonByName("");

            var okObject = result.Result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okObject?.StatusCode);
            var model = okObject?.Value;
            Assert.IsAssignableFrom<IEnumerable<PersonDto>>(model);
            Assert.Equal(returnList, model);
        }

        [Fact]
        public async void AddPerson_ShouldReturn201()
        {
            var result = await _controller.Create(_personDto);

            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult?.StatusCode);
            var model = createdAtActionResult?.Value;
            Assert.IsAssignableFrom<PersonDto>(model);
            Assert.Equal(_personDto, model);
        }



        [Fact]
        public async void UpdatePerson_ShouldReturn200()
        {
            var result = await _controller.Edit("", _personDto);

            var okObject = result.Result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okObject?.StatusCode);
            var model = okObject?.Value;
            Assert.IsAssignableFrom<PersonDto>(model);
            Assert.Equal(_personDto, model);
        }

        [Fact]
        public async void DeletePerson_ShouldReturn200()
        {
            var result = await _controller.Delete("");

            var okObject = result.Result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okObject?.StatusCode);
            var model = okObject?.Value;
            Assert.IsAssignableFrom<PersonDto>(model);
            Assert.Equal(_personDto, model);
        }
    }
}
