CREATE TABLE [dbo].[Request] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [RequestTitle]          VARCHAR (256) NOT NULL,
    [RequestDescription]    VARCHAR (256) NOT NULL,
    [RequestTypeId]         INT           NOT NULL,
    [IsApproved]            BIT           NOT NULL,
    [RequestedByEmployeeId] INT           NOT NULL,
    CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Request_Employee] FOREIGN KEY ([RequestedByEmployeeId]) REFERENCES [dbo].[Employee] ([Id]),
    CONSTRAINT [FK_Request_RequestType] FOREIGN KEY ([RequestTypeId]) REFERENCES [dbo].[RequestType] ([Id])
);

