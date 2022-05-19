# Infraestructure - Entity Framework 6 - Code First

## packages intalled

* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.Tools
* Microsoft.EntityFrameworkCore.Design
* Microsoft.EntityFrameworkCore.Relational
* Microsoft.EntityFrameworkCore.SqlServer - according to the database used

### Entity Framework 6 Providers

The Entity Framework is now being developed under an open-source license and EF6 and above will not be shipped as part of the .NET Framework. This has many advantages but also requires that EF providers be rebuilt against the EF6 assemblies. This means that EF providers for EF5 and below will not work with EF6 until they have been rebuilt.

[Entity Framework 6 Providers](https://docs.microsoft.com/en-us/ef/ef6/fundamentals/providers/)

* [Microsoft SQL Server provider](https://www.nuget.org/packages/EntityFramework)

* [Microsoft SQL Server Compact Edition provider](https://nuget.org/packages/EntityFramework.SqlServerCompact)
* [Devart dotConnect Data Providers](https://www.devart.com/dotconnect/)
* [CData Software providers](https://www.cdata.com/ado/)
* [Firebird provider](https://www.nuget.org/packages/EntityFramework.Firebird/)
* [Visual Fox Pro provider](https://www.nuget.org/packages/VFPEntityFrameworkProvider2/)
* [MySQL Connector/NET for Entity Framework](https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework60.html)
* [PostgreSQL](https://www.nuget.org/packages/EntityFramework6.Npgsql/)
* [Oracle](https://www.nuget.org/packages/Oracle.ManagedDataAccess.EntityFramework/)
* [SQLite](https://www.nuget.org/packages/System.Data.SQLite/)

## Migration

* [Entity Framework Tools](https://docs.microsoft.com/en-us/ef/core/cli/powershell)

### Migration Commands used

* Get-Help about_EntityFrameworkCore
* Add-Migration MigrationName -Context TemplateApiContext -Output Context/Migrations
* Update-Database -Context TemplateApiContext
* Remove-Migration -Context TemplateApiContext
* Script-DbContext -Context TemplateApiContext -Output  Scripts/InitialMIgration.sql

* Drop-Database -Context TemplateApiContext


        Generates a SQL script from the DbContext. Bypasses any migrations.

* Script-Migration

        Generates a SQL script that applies all of the changes from one selected migration to another selected migration.

For database first use

* Scaffold-DbContext

For the production database is recommended to generate the migration script, so it can be revised manualy by an expert to avoid unecessary waste of ressorce from the database.

Entity Framework is very good at the moment of development of the system, especialy because can produce and modificate the database extremely quickly, but can be a little slow when compared with another ORM framework like Dapper.

So if your system start to behave s