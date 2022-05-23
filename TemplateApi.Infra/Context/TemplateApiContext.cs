using Microsoft.EntityFrameworkCore;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Infra.Context.Mappings;

namespace TemplateApi.Infra.Context
{
    public class TemplateApiContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public TemplateApiContext(DbContextOptions<TemplateApiContext> options) : base(options)
        {
            People = Set<Person>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
