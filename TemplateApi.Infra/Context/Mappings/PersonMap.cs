using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateApi.CrossCutting.Validators;
using TemplateApi.Domain.Models.Dal;

namespace TemplateApi.Infra.Context.Mappings
{
    internal class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").IsUnicode(false).IsFixedLength().HasMaxLength(36);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(250).HasColumnName("name").IsUnicode();
            builder.Property(x => x.SurName).IsRequired().HasMaxLength(250).HasColumnName("surname").IsUnicode();
            builder.Property(x => x.BirthDay).IsRequired().HasColumnName("birth_day").IsUnicode(false).IsFixedLength().HasMaxLength(10);
            builder.Property(x => x.CPF).IsRequired().HasColumnName("cpf").IsUnicode(false).IsFixedLength().HasMaxLength(CPFValidator.Size);
            builder.Property(x => x.LastUpdate).IsRequired().HasColumnName("last_update_date");
            builder.Property(x => x.IsUpdated).IsRequired().HasColumnName("is_updated").HasDefaultValue(false);

            builder.HasIndex(x => x.CPF).IsUnique().HasDatabaseName("uk_person_cpf");
        }
    }
}
