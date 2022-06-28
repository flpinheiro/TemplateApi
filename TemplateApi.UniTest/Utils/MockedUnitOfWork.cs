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

        public MockedUnitOfWork()
        {
            MockPersonRepository = new Mock<IPersonRepository>();
            MockLogger = new Mock<ILogger>();

            if (_mapper == null) _mapper = MockedIMapperExtensions.CreateIMapper();

            MockLogger.SetLogger();

        }
        public IPersonRepository PersonRepository => MockPersonRepository.Object;

        private IMapper _mapper;
        public IMapper Mapper => _mapper;

        public ILogger Logger => MockLogger.Object;

        public void Dispose()
        {

        }

        public void RollBack()
        {

        }

        public void Save()
        {

        }

        public async Task SaveAsync()
        {

        }
    }
}
