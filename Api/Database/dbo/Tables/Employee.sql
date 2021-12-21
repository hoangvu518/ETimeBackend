CREATE TABLE [dbo].[Employee] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (50)    NOT NULL,
    [LastName]  VARCHAR (50)    NOT NULL,
    [ManagerId] INT             NULL,
    [Salary]    DECIMAL (18, 2) NULL,
    [Email]     VARCHAR (200)   NOT NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employee_Employee] FOREIGN KEY ([ManagerId]) REFERENCES [dbo].[Employee] ([Id])
);

