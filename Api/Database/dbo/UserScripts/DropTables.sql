IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Request]') AND type in (N'U'))
ALTER TABLE [dbo].[Request] DROP CONSTRAINT IF EXISTS [FK_Request_RequestType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Request]') AND type in (N'U'))
ALTER TABLE [dbo].[Request] DROP CONSTRAINT IF EXISTS [FK_Request_Employee]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
ALTER TABLE [dbo].[Employee] DROP CONSTRAINT IF EXISTS [FK_Employee_Employee]
GO
/****** Object:  Table [dbo].[RequestType]    Script Date: 12/21/2021 12:09:29 PM ******/
DROP TABLE IF EXISTS [dbo].[RequestType]
GO
/****** Object:  Table [dbo].[Request]    Script Date: 12/21/2021 12:09:29 PM ******/
DROP TABLE IF EXISTS [dbo].[Request]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 12/21/2021 12:09:29 PM ******/
DROP TABLE IF EXISTS [dbo].[Employee]
GO
