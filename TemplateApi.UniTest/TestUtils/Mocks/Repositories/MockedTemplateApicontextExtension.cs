using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moq;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Infra.Context;
using TemplateApi.UniTest.TestUtils.Fixtures;

namespace TemplateApi.UniTest.TestUtils.Mocks.Repositories
{
    internal static class MockedTemplateApicontextExtension
    {
        public const string InMemoryConnectionString = "DataSource=:memory:";

        public static TemplateApiContext CreateTempleateApiContextInMemory()
        {
            var _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();

            var option = new DbContextOptionsBuilder<TemplateApiContext>()
                .UseSqlite(_connection)
                .Options;

            var context = new MockedTemplateApiContext(option);
            context.Database.EnsureCreated();
            return context;
        }

        public static void Seed(this TemplateApiContext context)
        {
            context.People.AddRange(Fixture.People);
            context.SaveChanges();
        }

        public static void SetSaveChanges(this Mock<TemplateApiContext> mock)
            => mock.Setup(x => x.SaveChanges()).Verifiable();

        public static void VerifySaveChanges(this Mock<TemplateApiContext> mock)
            => mock.Verify(x => x.SaveChanges(), Times.Once);

        public static void SetSaveChangesAsync(this Mock<TemplateApiContext> mock)
            => mock.Setup(x => x.SaveChangesAsync(default)).Verifiable();

        public static void VerifySaveChangesAsync(this Mock<TemplateApiContext> mock)
            => mock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    internal class MockedTemplateApiContext : TemplateApiContext
    {
        public MockedTemplateApiContext(DbContextOptions<TemplateApiContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PersonSeed());
        }

        private class PersonSeed : IEntityTypeConfiguration<Person>
        {
            public void Configure(EntityTypeBuilder<Person> builder)
            {
                builder.HasData(Fixture.People);
            }
        }
    }


}
