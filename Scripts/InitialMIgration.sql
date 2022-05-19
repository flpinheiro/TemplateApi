CREATE TABLE [Person] (
    [id] char(70) NOT NULL,
    [name] nvarchar(250) NOT NULL,
    [sur_name] nvarchar(250) NOT NULL,
    [birth_day] datetime2 NOT NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY ([id])
);
GO


