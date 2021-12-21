SET IDENTITY_INSERT [dbo].[Employee] ON 
GO
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [ManagerId], [Salary], [Email]) VALUES (1, N'Hoang', N'Pham', NULL, CAST(100000.00 AS Decimal(18, 2)), N'hoang@gmail.com')
GO
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [ManagerId], [Salary], [Email]) VALUES (2, N'Tam', N'La', 1, CAST(50000.00 AS Decimal(18, 2)), N'tam@gmail.com')
GO
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [ManagerId], [Salary], [Email]) VALUES (3, N'Khanh', N'Nguyen', 1, CAST(20000.00 AS Decimal(18, 2)), N'khanh@yahoo.com')
GO
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [ManagerId], [Salary], [Email]) VALUES (4, N'Crystal', N'Tan', 2, CAST(10000.00 AS Decimal(18, 2)), N'tan@crypto.com')
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[RequestType] ON 
GO
INSERT [dbo].[RequestType] ([Id], [Name]) VALUES (1, N'Sick')
GO
INSERT [dbo].[RequestType] ([Id], [Name]) VALUES (2, N'Vacation')
GO
INSERT [dbo].[RequestType] ([Id], [Name]) VALUES (3, N'P20')
GO
INSERT [dbo].[RequestType] ([Id], [Name]) VALUES (4, N'PDD')
GO
SET IDENTITY_INSERT [dbo].[RequestType] OFF
GO
SET IDENTITY_INSERT [dbo].[Request] ON 
GO
INSERT [dbo].[Request] ([Id], [RequestTitle], [RequestDescription], [RequestTypeId], [IsApproved], [RequestedByEmployeeId]) VALUES (1, N'Hoang is OOO', N'Doctor Appointment', 1, 0, 1)
GO
INSERT [dbo].[Request] ([Id], [RequestTitle], [RequestDescription], [RequestTypeId], [IsApproved], [RequestedByEmployeeId]) VALUES (2, N'Tam is OOO', N'Dental Appointment', 1, 0, 2)
GO
INSERT [dbo].[Request] ([Id], [RequestTitle], [RequestDescription], [RequestTypeId], [IsApproved], [RequestedByEmployeeId]) VALUES (3, N'Tam is OOO Again', N'Vacation', 2, 0, 2)
GO
SET IDENTITY_INSERT [dbo].[Request] OFF
GO
