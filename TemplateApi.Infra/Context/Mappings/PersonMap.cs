using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateApi.Domain.Models.Dal;

namespace TemplateApi.Infra.Context.Mappings
{
    internal class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");

            builder.HasKey(x => x.Id);

            builder.Property(x=> x.Id).HasColumnName("id").IsUnicode(false).IsFixedLength().HasMaxLength(70);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(250).HasColumnName("name").IsUnicode();
            builder.Property(x => x.SurName).IsRequired().HasMaxLength(250).HasColumnName("sur_name").IsUnicode();
            builder.Property(x => x.BirthDay).IsRequired().HasColumnName("birth_day");
        }
    }
}
