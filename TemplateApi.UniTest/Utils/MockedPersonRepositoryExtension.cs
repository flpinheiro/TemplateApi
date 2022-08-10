using Moq;
using System;
using TemplateApi.CrossCutting.Models;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Models.Dal;

namespace TemplateApi.UniTest.Utils
{
    internal static class MockedPersonRepositoryExtension
    {
        #region get
        public static void SetGetAllAsync(this Mock<IPersonRepository> mock)
        {
            mock.Setup(x => x.GetAllAsync(It.IsAny<Pagination>())).ReturnsAsync(Fixture.People).Verifiable();

            mock.Setup(x => x.GetAllAsync()).ReturnsAsync(Fixture.People).Verifiable();
        }
        public static void VerifyGetAllAsync(this Mock<IPersonRepository> mock)
            => mock.Verify(x => x.GetAllAsync(), Times.Once);
        public static void VerifyGetAllAsyncPaginated(this Mock<IPersonRepository> mock)
            => mock.Verify(x => x.GetAllAsync(It.IsAny<Pagination>()), Times.Once);

        public static void SetGetByIdAsync(this Mock<IPersonRepository> mock)
    => mock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(Fixture.Person).Verifiable();

        public static void SetGetByIdAsync(this Mock<IPersonRepository> mock, Person? person)
            => mock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(person).Verifiable();

        public static void VerifyGetByIdAsync(this Mock<IPersonRepository> mock)
            => mock.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);

        public static void SetGetByNameAsync(this Mock<IPersonRepository> mock)
        {
            mock.Setup(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<Pagination>())).ReturnsAsync(Fixture.People).Verifiable();

            mock.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(Fixture.People).Verifiable();
        }
        public static void VerifyGetByNameAsync(this Mock<IPersonRepository> mock)
            => mock.Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Once);

        public static void VerifyGetByNamePaginatedAsync(this Mock<IPersonRepository> mock)
            => mock.Verify(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<Pagination>()), Times.Once);
        #endregion

        #region any
        public static void SetAnyAsync(this Mock<IPersonRepository> mock, bool any = true)
    => mock.Setup(x => x.AnyAsync(It.IsAny<string>())).ReturnsAsync(any).Verifiable();

        public static void VerifyAnyAsync(this Mock<IPersonRepository> mock)
            => mock.Verify(x => x.AnyAsync(It.IsAny<string>()), Times.Once);
        #endregion

        #region add
        public static void SetAdd(this Mock<IPersonRepository> mock)
    => mock.Setup(x => x.Add(It.IsAny<Person>())).Returns(Fixture.Person?.Id ?? "null-Id").Verifiable();

        public static void VerifyAdd(this Mock<IPersonRepository> mock)
            => mock.Verify(x => x.Add(It.IsAny<Person>()), Times.Once);

        public static void SetAddThrowException<TException>(this Mock<IPersonRepository> mock) where TException : Exception, new()
            => mock.Setup(x => x.Add(It.IsAny<Person>())).Throws<TException>().Verifiable();
        #endregion

        #region update
        public static void SetUpdate(this Mock<IPersonRepository> mock)
    => mock.Setup(x => x.Update(It.IsAny<Person>())).Verifiable();

        public static void VerifyUpdate(this Mock<IPersonRepository> mock)
            => mock.Verify(x => x.Update(It.IsAny<Person>()), Times.Once);

        public static void SetUpdateThrowException<TException>(this Mock<IPersonRepository> mock) where TException : Exception, new()
            => mock.Setup(x => x.Update(It.IsAny<Person>())).Throws<TException>().Verifiable();
        #endregion

        #region delete
        public static void SetDelete(this Mock<IPersonRepository> mock)
            => mock.Setup(x => x.Delete(It.IsAny<Person>())).Verifiable();

        public static void VerifyDelete(this Mock<IPersonRepository> mock)
            => mock.Verify(x => x.Delete(It.IsAny<Person>()), Times.Once);

        public static void SetDeleteThrowException<TException>(this Mock<IPersonRepository> mock) where TException : Exception, new()
            => mock.Setup(x => x.Delete(It.IsAny<Person>())).Throws<TException>().Verifiable();
        #endregion
    }
}
