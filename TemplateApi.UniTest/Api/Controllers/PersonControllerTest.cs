#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateApi.Controllers;
using TemplateApi.CrossCutting.Models;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.UniTest.Utils.Fixtures;
using TemplateApi.UniTest.Utils.Mocks.Services;

namespace TemplateApi.UniTest.Api.Controllers
{
    public class PersonControllerTest
    {
        private readonly Mock<IPersonService> mock;
        private readonly PersonController _controller;

        public PersonControllerTest()
        {
            var validator = new PersonDtoValidation();
            mock = new Mock<IPersonService>();
            _controller = new PersonController(mock.Object, validator);

            mock.SetGetAllPeron();
            mock.SetGetPersonById();
            mock.SetAddPerson();
            mock.SetUpdatePerson();
            mock.SetDeletePerson();
            mock.SetExportToExcel();
            mock.SetCount();
        }

        [Fact]
        public void Constructor_shouldReturnException()
        {
            Assert.Throws<ArgumentNullException>(() => new PersonController(null, null));
        }

        [Fact]
        public async Task GetAllTest_shouldReturnPersonList()
        {
            var result = await _controller.GetAll(new Pagination());

            var okObject = result.Result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okObject?.StatusCode);
            var model = okObject?.Value;
            Assert.IsAssignableFrom<IEnumerable<PersonDto>>(model);
            Assert.Equal(Fixture.PeopleDto, model);
            mock.VerifyGetAllPersonPaginated();
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
            var result = await _controller.GetPersonByName("", new Pagination());

            var okObject = result.Result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okObject?.StatusCode);
            var model = okObject?.Value;
            Assert.IsAssignableFrom<IEnumerable<PersonDto>>(model);
            Assert.Equal(returnList, model);
            mock.VerifyGetPersonByNamePaginated();
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
        public async void AddPerson_ShouldReturn400()
        {
            var person = new PersonDto()
            {
                Name = null,
                SurName = null,
                // BirthDay = null,
                CPF = null,
            };
            var result = await _controller.Create(person);
            var badResponseResult = result.Result as BadRequestObjectResult;
            Assert.NotNull(badResponseResult);
            Assert.Equal(400, badResponseResult?.StatusCode);
            var values = badResponseResult?.Value as IEnumerable<object>;
            Assert.NotEmpty(values);
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
        public async void UpdatePerson_ShouldReturn400()
        {
            var person = new PersonDto()
            {
                Name = null,
                SurName = null,
                // BirthDay = null,
                CPF = null,
            };

            var result = await _controller.Edit("", person);
            var badResponseResult = result.Result as BadRequestObjectResult;
            Assert.NotNull(badResponseResult);
            Assert.Equal(400, badResponseResult?.StatusCode);
            var values = badResponseResult?.Value as IEnumerable<object>;
            Assert.NotEmpty(values);
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

        [Fact]
        public async void ExportToExcelAll_ShouldReturnFile()
        {
            var file = await _controller.ExporttoExcelAll();

            Assert.NotNull(file);
            mock.VerifyGetAllPerson();
            mock.VerifyExportToExcel();
        }

        [Fact]
        public async void ExportToExcelByName_ShouldReturnFile()
        {
            var file = await _controller.ExportoExcelByName("test");

            Assert.NotNull(file);
            mock.VerifyGetPersonByName();
            mock.VerifyExportToExcel();
        }

        [Fact]
        public void Count_Should_ReturnOk()
        {
            var resultAll = _controller.CountAll(new Pagination());

            var okObjectResultAll = resultAll.Result as OkObjectResult;
            Assert.NotNull(okObjectResultAll);
            var countAll = okObjectResultAll?.Value;
            Assert.Equal(Fixture.PaginationResponse, countAll);

            var resultByName = _controller.CountPersonByName("", new Pagination());

            var okObjectResultByName = resultByName.Result as OkObjectResult;
            Assert.NotNull(okObjectResultByName);
            var countByName = okObjectResultByName?.Value;
            Assert.Equal(Fixture.PaginationResponse, countByName);
        }
    }
}
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
