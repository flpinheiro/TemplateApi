using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.IO;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.UniTest.Utils
{
    internal static class MockedPersonServiceExtensions
    {
        //Mock<IPersonService>
        public static void SetGetAllPeron(this Mock<IPersonService> mock)
            => mock.Setup(x => x.GetAllPerson()).ReturnsAsync(Fixture.PeopleDto).Verifiable();

        public static void SetGetAllPeron(this Mock<IPersonService> mock, IEnumerable<PersonDto> people)
            => mock.Setup(x => x.GetAllPerson()).ReturnsAsync(people).Verifiable();

        public static void VerifyGetAllPerson(this Mock<IPersonService> mock)
            => mock.Verify(x => x.GetAllPerson(), Times.Once);

        public static void SetGetPersonById(this Mock<IPersonService> mock)
            => mock.Setup(x => x.GetPersonById(It.IsAny<string>())).ReturnsAsync(Fixture.PersonDto).Verifiable();

        public static void SetGetPersonById(this Mock<IPersonService> mock, PersonDto person)
            => mock.Setup(x => x.GetPersonById(It.IsAny<string>())).ReturnsAsync(person).Verifiable();

        public static void VerifyGetPersonById(this Mock<IPersonService> mock)
            => mock.Verify(x => x.GetPersonById(It.IsAny<string>()), Times.Once);

        public static void SetGetPersonByName(this Mock<IPersonService> mock)
    => mock.Setup(x => x.GetPersonByName(It.IsAny<string>())).ReturnsAsync(Fixture.PeopleDto).Verifiable();

        public static void SetGetPersonByName(this Mock<IPersonService> mock, IEnumerable<PersonDto> people)
            => mock.Setup(x => x.GetPersonByName(It.IsAny<string>())).ReturnsAsync(people).Verifiable();

        public static void VerifyGetPersonByName(this Mock<IPersonService> mock)
            => mock.Verify(x => x.GetPersonByName(It.IsAny<string>()), Times.Once);

        public static void SetAddPerson(this Mock<IPersonService> mock)
            => mock.Setup(x => x.AddPerson(It.IsAny<PersonDto>())).ReturnsAsync(Fixture.PersonDto).Verifiable();
        public static void VerifyAddPerson(this Mock<IPersonService> mock)
            => mock.Verify(x => x.AddPerson(It.IsAny<PersonDto>()), Times.Once);

        public static void SetUpdatePerson(this Mock<IPersonService> mock)
            => mock.Setup(x => x.UpdatePerson(It.IsAny<string>(), It.IsAny<PersonDto>())).Verifiable();

        public static void VerifyUpdatePerson(this Mock<IPersonService> mock)
            => mock.Verify(x => x.UpdatePerson(It.IsAny<string>(), It.IsAny<PersonDto>()), Times.Once);

        public static void SetDeletePerson(this Mock<IPersonService> mock)
            => mock.Setup(x => x.DeletePerson(It.IsAny<string>())).ReturnsAsync(Fixture.PersonDto).Verifiable();

        public static void VerifyDeletePerson(this Mock<IPersonService> mock)
            => mock.Verify(x => x.DeletePerson(It.IsAny<string>()), Times.Once);

        public static void SetExportToExcel(this Mock<IPersonService> mock)
            => mock.Setup(x => x.ExportToExcel(It.IsAny<IEnumerable<PersonDto>>()))
            .Returns(new FileStreamResult(new MemoryStream(), "text/csv")).Verifiable();

        public static void VerifyExportToExcel(this Mock<IPersonService> mock)
            => mock.Verify(x => x.ExportToExcel(It.IsAny<IEnumerable<PersonDto>>()), Times.Once);
    }
}
