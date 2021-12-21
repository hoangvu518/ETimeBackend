CREATE TABLE [dbo].[RequestType] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (256) NOT NULL,
    CONSTRAINT [PK_RequestType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

