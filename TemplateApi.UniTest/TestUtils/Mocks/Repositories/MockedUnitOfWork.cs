using AutoMapper;
using Moq;
using Serilog;
using System.Threading.Tasks;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Infra;
using TemplateApi.UniTest.Utils.Mocks;

namespace TemplateApi.UniTest.TestUtils.Mocks.Repositories
{
    internal class MockedUnitOfWork : IUnitOfWork
    {
        public readonly Mock<IPersonRepository> MockPersonRepository;
        private readonly Mock<ILogger> MockLogger;
        private readonly IUnitOfWork _unitOfWork;

        public MockedUnitOfWork()
        {
            var context = MockedTemplateApicontextExtension.CreateTempleateApiContextInMemory();
            MockPersonRepository = new Mock<IPersonRepository>();
            MockLogger = new Mock<ILogger>();

            MockLogger.SetLogger();

            _unitOfWork = new UnitOfWork(context,  MockLogger.Object);

        }
        public IPersonRepository PersonRepository => MockPersonRepository.Object;


        public ILogger Logger => MockLogger.Object;

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public void RollBack()
        {
            _unitOfWork.RollBack();
        }

        public void Save()
        {
            _unitOfWork.Save();
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.SaveAsync();
        }
    }
}
