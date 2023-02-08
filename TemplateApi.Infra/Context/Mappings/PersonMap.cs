using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateApi.CrossCutting.Utils;
using TemplateApi.CrossCutting.Validators;
using TemplateApi.Domain.Models.Constants;
using TemplateApi.Domain.Models.Dal;

namespace TemplateApi.Infra.Context.Mappings;

internal class PersonMap : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Person");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(PersonContants.NameSize).HasColumnName("name").IsUnicode();
        builder.Property(x => x.SurName).IsRequired().HasMaxLength(PersonContants.SurnameSize).HasColumnName("surname").IsUnicode();
        builder.Property(x => x.BirthDay).IsRequired().HasColumnName("birth_day").IsUnicode(false).IsFixedLength().HasMaxLength(PersonContants.BirthdaySize);
        builder.Property(x => x.CPF).IsRequired().HasColumnName("cpf").IsUnicode(false).IsFixedLength().HasMaxLength(CPFValidator.Size);
        builder.Property(x => x.Email).IsRequired().HasColumnName("email").IsUnicode(true).HasMaxLength(PersonContants.EmailSize);
        builder.Property(x => x.LastUpdate).IsRequired().HasColumnName("last_update_date");
        builder.Property(x => x.IsUpdated).IsRequired().HasColumnName("is_updated").HasDefaultValue(false);


        builder.HasIndex(x => x.CPF).IsUnique().HasDatabaseName("uk_person_cpf");

#if DEBUG
        builder.HasData(PersonSeed.GetPersonListSeed());
#endif
    }
}

internal static class PersonSeed
{
    internal static IEnumerable<Person> GetPersonListSeed()
    {
        var list = new List<Person>();
        for (int i = 0; i < 1000; i++)
        {
            var (name, surname, email) = RandomPersonGenerator.GetPerson();
            var person = new Person(
                Id: Guid.NewGuid(),
                Name: name,
                SurName: surname,
                BirthDay: RandomDateTime.NextDateOnly().ToShortDateString(),
                CPF: CPFValidator.GerarCpf(),
                Email: email)
            {
                LastUpdate = DateTime.Now,
            };

            list.Add(person);
        }
        return list;
    }
}
