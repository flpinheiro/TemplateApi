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

            if (_mapper == null) _mapper = MockedIMapperExtensions.CreateIMapper();

            MockLogger.SetLogger();

            _unitOfWork = new UnitOfWork(context, _mapper, MockLogger.Object);

        }
        public IPersonRepository PersonRepository => MockPersonRepository.Object;

        private IMapper _mapper;
        public IMapper Mapper => _mapper;

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
