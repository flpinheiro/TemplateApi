CREATE TABLE [Person] (
    [id] char(36) NOT NULL,
    [name] nvarchar(250) NOT NULL,
    [surname] nvarchar(250) NOT NULL,
    [birth_day] char(10) NOT NULL,
    [cpf] char(11) NOT NULL,
    [last_update_date] datetime2 NOT NULL,
    [is_updated] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_Person] PRIMARY KEY ([id])
);
GO


CREATE UNIQUE INDEX [uk_person_cpf] ON [Person] ([cpf]);
GO


