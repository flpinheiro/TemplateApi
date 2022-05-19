CREATE TABLE [Person] (
    [id] char(36) NOT NULL,
    [name] nvarchar(250) NOT NULL,
    [sur_name] nvarchar(250) NOT NULL,
    [birth_day] char(10) NOT NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY ([id])
);
GO


