using AutoMapper;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateApi.Domain.Configurations;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Infra;
using TemplateApi.Infra.Context;

namespace TemplateApi.UniTest.Utils
{
    internal class MockedUnitOfWork : IUnitOfWork
    {
        public readonly Mock<IPersonRepository> MockPersonRepository;
        private readonly Mock<ILogger> MockLogger;

        private readonly IUnitOfWork _unitOfWork;
        public MockedUnitOfWork()
        {
            MockPersonRepository = new Mock<IPersonRepository>();
            MockLogger = new Mock<ILogger>();


            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            MockLogger.Setup(x => x.Debug(It.IsAny<string>())).Verifiable();
            MockLogger.Setup(x => x.Error(It.IsAny<string>())).Verifiable();
            MockLogger.Setup(x => x.Fatal(It.IsAny<string>())).Verifiable();
            MockLogger.Setup(x => x.Information(It.IsAny<string>())).Verifiable();
            MockLogger.Setup(x => x.Warning(It.IsAny<string>())).Verifiable();

            var context = new Mock<TemplateApiContext>();
//            context.Setup(x=> x.Database.BeginTransaction()).Returns()

            _unitOfWork = new UnitOfWork(context.Object, _mapper, Logger);
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
