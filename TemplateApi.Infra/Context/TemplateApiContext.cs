using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Infra.Context.Mappings;

namespace TemplateApi.Infra.Context
{
    public class TemplateApiContext: DbContext
    {
        public DbSet<Person>? People { get; set; }

        public TemplateApiContext(DbContextOptions<TemplateApiContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
