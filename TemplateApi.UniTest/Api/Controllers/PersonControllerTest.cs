#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateApi.Controllers;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.Domain.Models.Validators;
using TemplateApi.UniTest.TestUtils.Fixtures;
using TemplateApi.UniTest.TestUtils.Mocks.Services;

namespace TemplateApi.UniTest.Api.Controllers
{
    public class PersonControllerTest
    {
        private readonly Mock<IPersonService> mock;
        private readonly PersonController _controller;

        public PersonControllerTest()
        {
            var validator = new PersonValidation();
            mock = new Mock<IPersonService>();
            _controller = new PersonController(mock.Object, validator);

            mock.SetGetPeople();
            mock.SetGetPersonById();
            mock.SetAddPerson();
            mock.SetUpdatePerson();
            mock.SetDeletePerson();
            mock.SetExportToExcel();
            mock.SetCount();
            mock.SetAnyAsync();
        }

        [Fact]
        public void Constructor_shouldReturnException()
        {
            Assert.Throws<ArgumentNullException>(() => new PersonController(null, null));
        }

        [Fact]
        public async Task GetAllTest_shouldReturnPersonList()
        {
            var result = await _controller.GetPeople(Fixture.QueryDto, Fixture.Pagination);

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
        public async void AddPerson_ShouldReturn201()
        {
            var result = await _controller.Create(Fixture.PersonDto);

            var createdAtActionResult = result.Result as CreatedResult;
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
        public async void UpdatePerson_ShouldReturn204()
        {
            var result = await _controller.Edit("", Fixture.UpdatePersonDto);
            var noContentResult = result as NoContentResult;
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult?.StatusCode);
            mock.VerifyUpdatePerson();
        }

        //[Fact]
        //public async void UpdatePerson_ShouldReturn400()
        //{
        //    var result = await _controller.Edit("", Fixture.UpdatePersonDto);
        //    var badResponseResult = result as BadRequestObjectResult;
        //    Assert.NotNull(badResponseResult);
        //    Assert.Equal(400, badResponseResult?.StatusCode);
        //    var values = badResponseResult?.Value as IEnumerable<object>;
        //    Assert.NotEmpty(values);
        //}

        [Fact]
        public async void DeletePerson_ShouldReturn200()
        {
            var result = await _controller.Delete("");

            var okObject = result as NoContentResult;
            Assert.Equal(StatusCodes.Status204NoContent, okObject?.StatusCode);
            mock.VerifyDeletePerson();
        }

        [Fact]
        public async void ExportToExcelAll_ShouldReturnFile()
        {
            var file = await _controller.ExportoExcelAll(Fixture.QueryDto);

            Assert.NotNull(file);
            mock.VerifyGetAllPerson();
            mock.VerifyExportToExcel();
        }

        [Fact]
        public void Count_Should_ReturnOk()
        {
            var resultAll = _controller.CountPeople(Fixture.QueryDto, Fixture.Pagination);

            var okObjectResultAll = resultAll.Result as OkObjectResult;
            Assert.NotNull(okObjectResultAll);
            var countAll = okObjectResultAll?.Value;
            Assert.Equal(Fixture.PaginationResponse, countAll);
        }
    }
}
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
