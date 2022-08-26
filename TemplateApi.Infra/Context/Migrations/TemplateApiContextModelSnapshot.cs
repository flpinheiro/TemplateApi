﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TemplateApi.Infra.Context;

#nullable disable

namespace TemplateApi.Infra.Context.Migrations
{
    [ExcludeFromCodeCoverage]
    [DbContext(typeof(TemplateApiContext))]
    partial class TemplateApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TemplateApi.Domain.Models.Dal.Person", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("char(36)")
                        .HasColumnName("id")
                        .IsFixedLength();

                    b.Property<string>("BirthDay")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .HasColumnName("birth_day")
                        .IsFixedLength();

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("char(11)")
                        .HasColumnName("cpf")
                        .IsFixedLength();

                    b.Property<bool>("IsUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_updated");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2")
                        .HasColumnName("last_update_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("name");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("surname");

                    b.HasKey("Id");

                    b.HasIndex("CPF")
                        .IsUnique()
                        .HasDatabaseName("uk_person_cpf");

                    b.ToTable("Person", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
