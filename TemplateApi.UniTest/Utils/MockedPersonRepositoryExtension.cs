using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Models.Dal;

namespace TemplateApi.UniTest.Utils
{
    internal static class MockedPersonRepositoryExtension
    {
        public static void SetGetAllAsync(this Mock<IPersonRepository> mock)
            => mock.Setup(x => x.GetAllAsync()).ReturnsAsync(Fixture.People).Verifiable();

        public static void SetGetByIdAsync(this Mock<IPersonRepository> mock)
            => mock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(Fixture.Person).Verifiable();

        public static void SetGetByNameAsync(this Mock<IPersonRepository> mock)
            => mock.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(Fixture.People).Verifiable();

        public static void SetAnyAsync(this Mock<IPersonRepository> mock, bool any = true)
            => mock.Setup(x => x.AnyAsync(It.IsAny<string>())).ReturnsAsync(any).Verifiable();

        public static void SetAdd(this Mock<IPersonRepository> mock)
            => mock.Setup(x => x.Add(It.IsAny<Person>())).Returns(Fixture.Person?.Id ?? "null-Id").Verifiable();

        public static void SetAddThrowException(this Mock<IPersonRepository> mock)
    => mock.Setup(x => x.Add(It.IsAny<Person>())).Throws<Exception>().Verifiable();

        public static void SetUpdate(this Mock<IPersonRepository> mock)
            => mock.Setup(x => x.Update(It.IsAny<Person>())).Verifiable();

        public static void SetDelete(this Mock<IPersonRepository> mock)
            => mock.Setup(x => x.Delete(It.IsAny<Person>())).Verifiable();
    }
}
