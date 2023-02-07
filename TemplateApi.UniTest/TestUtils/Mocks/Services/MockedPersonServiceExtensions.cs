using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using TemplateApi.CrossCutting.Models;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.Domain.Models.Queries;
using TemplateApi.UniTest.TestUtils.Fixtures;

namespace TemplateApi.UniTest.TestUtils.Mocks.Services
{
    internal static class MockedPersonServiceExtensions
    {
        #region Get
        public static void SetGetPeople(this Mock<IPersonService> mock)
        {
            mock.Setup(x => x.Get(It.IsAny<PersonQuery>())).ReturnsAsync(Fixture.PeopleDto).Verifiable();
            mock.Setup(x => x.Get(It.IsAny<PersonQuery>(), It.IsAny<Pagination>())).ReturnsAsync(Fixture.PeopleDto).Verifiable();
        }

        public static void VerifyGetAllPerson(this Mock<IPersonService> mock)
            => mock.Verify(x => x.Get(It.IsAny<PersonQuery>()), Times.Once);
        public static void VerifyGetAllPersonPaginated(this Mock<IPersonService> mock)
            => mock.Verify(x => x.Get(It.IsAny<PersonQuery>(), It.IsAny<Pagination>()), Times.Once);

        public static void SetGetPersonById(this Mock<IPersonService> mock)
            => mock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(Fixture.PersonDto).Verifiable();

        public static void SetGetPersonById(this Mock<IPersonService> mock, PersonDto person)
            => mock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(person).Verifiable();

        public static void VerifyGetPersonById(this Mock<IPersonService> mock)
            => mock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        #endregion

        #region Count
        public static void SetCount(this Mock<IPersonService> mock, PaginationResponse paginationResponse)
        {
            mock.Setup(x => x.Count(It.IsAny<PersonQuery>(), It.IsAny<Pagination>())).Returns(paginationResponse).Verifiable();
        }
        public static void SetCount(this Mock<IPersonService> mock) => mock.SetCount(Fixture.PaginationResponse);
        #endregion

        #region Add
        public static void SetAddPerson(this Mock<IPersonService> mock)
            => mock.Setup(x => x.Add(It.IsAny<PersonDto>())).ReturnsAsync(Fixture.Person.Id).Verifiable();
        public static void VerifyAddPerson(this Mock<IPersonService> mock)
            => mock.Verify(x => x.Add(It.IsAny<PersonDto>()), Times.Once);
        #endregion

        #region any
        public static void SetAnyAsync(this Mock<IPersonService> mock, bool any = true)
            => mock.Setup(x => x.Any(It.IsAny<Guid>())).ReturnsAsync(any).Verifiable();

        public static void VerifyAnyAsync(this Mock<IPersonService> mock)
            => mock.Verify(x => x.Any(It.IsAny<Guid>()), Times.Once);
        #endregion

        #region update
        public static void SetUpdatePerson(this Mock<IPersonService> mock)
          => mock.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<UpdatePersonDto>())).Verifiable();

        public static void VerifyUpdatePerson(this Mock<IPersonService> mock)
            => mock.Verify(x => x.Update(It.IsAny<Guid>(), It.IsAny<UpdatePersonDto>()), Times.Once);
        #endregion

        #region Delete
        public static void SetDeletePerson(this Mock<IPersonService> mock)
            => mock.Setup(x => x.Delete(It.IsAny<Guid>()));//.ReturnsAsync(Fixture.PersonDto).Verifiable();

        public static void VerifyDeletePerson(this Mock<IPersonService> mock)
            => mock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
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
