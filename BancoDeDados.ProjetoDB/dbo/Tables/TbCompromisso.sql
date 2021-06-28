CREATE TABLE [dbo].[TBCompromisso] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [Assunto]         VARCHAR (50) NULL,
    [Local]           VARCHAR (50) NULL,
    [DataCompromisso] DATETIME     NULL,
    [HoraInicio]      TIME (7)     NULL,
    [HoraTermino]     TIME (7)     NULL,
    [Id_Contato]      INT          NULL,
    CONSTRAINT [PK_TBCompromisso] PRIMARY KEY CLUSTERED ([Id] ASC)
);





