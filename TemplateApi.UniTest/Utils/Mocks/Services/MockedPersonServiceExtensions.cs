﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.IO;
using TemplateApi.CrossCutting.Models;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.UniTest.Utils.Fixtures;

namespace TemplateApi.UniTest.Utils.Mocks.Services
{
    internal static class MockedPersonServiceExtensions
    {
        //Mock<IPersonService>
        #region Get
        //public static void SetGetAllPeron(this Mock<IPersonService> mock)
        //{
        //    mock.Setup(x => x.GetAllPerson()).ReturnsAsync(Fixture.PeopleDto).Verifiable();
        //    mock.Setup(x => x.GetAllPerson(It.IsAny<Pagination>())).ReturnsAsync(Fixture.PeopleDto).Verifiable();
        //}

        //public static void SetGetAllPeron(this Mock<IPersonService> mock, IEnumerable<PersonDto> people)
        //{
        //    mock.Setup(x => x.GetAllPerson()).ReturnsAsync(people).Verifiable();
        //    mock.Setup(x => x.GetAllPerson(It.IsAny<Pagination>())).ReturnsAsync(people).Verifiable();
        //}

        //public static void VerifyGetAllPerson(this Mock<IPersonService> mock)
        //    => mock.Verify(x => x.GetAllPerson(), Times.Once);
        //public static void VerifyGetAllPersonPaginated(this Mock<IPersonService> mock)
        //    => mock.Verify(x => x.GetAllPerson(It.IsAny<Pagination>()), Times.Once);


        public static void SetGetPersonById(this Mock<IPersonService> mock)
            => mock.Setup(x => x.GetPersonById(It.IsAny<string>())).ReturnsAsync(Fixture.PersonDto).Verifiable();

        public static void SetGetPersonById(this Mock<IPersonService> mock, PersonDto person)
            => mock.Setup(x => x.GetPersonById(It.IsAny<string>())).ReturnsAsync(person).Verifiable();

        public static void VerifyGetPersonById(this Mock<IPersonService> mock)
            => mock.Verify(x => x.GetPersonById(It.IsAny<string>()), Times.Once);

        //public static void SetGetPersonByName(this Mock<IPersonService> mock)
        //{
        //    mock.Setup(x => x.GetPersonByName(It.IsAny<string>())).ReturnsAsync(Fixture.PeopleDto).Verifiable();
        //    mock.Setup(x => x.GetPersonByName(It.IsAny<string>(), It.IsAny<Pagination>())).ReturnsAsync(Fixture.PeopleDto).Verifiable();

        //}
        //public static void SetGetPersonByName(this Mock<IPersonService> mock, IEnumerable<PersonDto> people)
        //{
        //    mock.Setup(x => x.GetPersonByName(It.IsAny<string>())).ReturnsAsync(people).Verifiable();
        //    mock.Setup(x => x.GetPersonByName(It.IsAny<string>(), It.IsAny<Pagination>())).ReturnsAsync(people).Verifiable();
        //}
        //public static void VerifyGetPersonByName(this Mock<IPersonService> mock)
        //    => mock.Verify(x => x.GetPersonByName(It.IsAny<string>()), Times.Once);

        //public static void VerifyGetPersonByNamePaginated(this Mock<IPersonService> mock)
        //    => mock.Verify(x => x.GetPersonByName(It.IsAny<string>(), It.IsAny<Pagination>()), Times.Once);
        #endregion

        #region Count
        //public static void SetCount(this Mock<IPersonService> mock, PaginationResponse paginationResponse)
        //{
        //    mock.Setup(x => x.CountAllPerson(It.IsAny<Pagination>())).Returns(paginationResponse).Verifiable();
        //    mock.Setup(x => x.CountPersonByName(It.IsAny<string>(),It.IsAny<Pagination>())).Returns(paginationResponse).Verifiable();
        //}
        //public static void SetCount(this Mock<IPersonService> mock) => SetCount(mock, Fixture.PaginationResponse);
        #endregion

        #region Add
        public static void SetAddPerson(this Mock<IPersonService> mock)
            => mock.Setup(x => x.AddPerson(It.IsAny<PersonDto>())).ReturnsAsync(Fixture.PersonDto).Verifiable();
        public static void VerifyAddPerson(this Mock<IPersonService> mock)
            => mock.Verify(x => x.AddPerson(It.IsAny<PersonDto>()), Times.Once);
        #endregion

        #region update
        public static void SetUpdatePerson(this Mock<IPersonService> mock)
          => mock.Setup(x => x.UpdatePerson(It.IsAny<string>(), It.IsAny<PersonDto>())).Verifiable();

        public static void VerifyUpdatePerson(this Mock<IPersonService> mock)
            => mock.Verify(x => x.UpdatePerson(It.IsAny<string>(), It.IsAny<PersonDto>()), Times.Once);
        #endregion

        #region Delete
        public static void SetDeletePerson(this Mock<IPersonService> mock)
            => mock.Setup(x => x.DeletePerson(It.IsAny<string>())).ReturnsAsync(Fixture.PersonDto).Verifiable();

        public static void VerifyDeletePerson(this Mock<IPersonService> mock)
            => mock.Verify(x => x.DeletePerson(It.IsAny<string>()), Times.Once);
        #endregion

        #region ExportToExcel
        public static void SetExportToExcel(this Mock<IPersonService> mock)
    => mock.Setup(x => x.ExportToExcel(It.IsAny<IEnumerable<PersonDto>>()))
    .Returns(new FileStreamResult(new MemoryStream(), "text/csv")).Verifiable();

        public static void VerifyExportToExcel(this Mock<IPersonService> mock)
            => mock.Verify(x => x.ExportToExcel(It.IsAny<IEnumerable<PersonDto>>()), Times.Once);
        #endregion    
    }
}