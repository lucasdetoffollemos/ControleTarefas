CREATE TABLE [dbo].[TBContato] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [nome]     VARCHAR (50) NULL,
    [email]    VARCHAR (50) NULL,
    [telefone] INT          NULL,
    [empresa]  VARCHAR (50) NULL,
    [cargo]    VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

