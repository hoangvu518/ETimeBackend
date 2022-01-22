
USE [TimePortal]
GO

/****** Object:  Table [hoangtest].[Test]    Script Date: 1/12/2022 3:22:23 PM ******/
DROP TABLE IF EXISTS [hoangtest].[Test]
GO

/****** Object:  Schema [hoangtest]    Script Date: 1/12/2022 3:22:23 PM ******/
DROP SCHEMA IF EXISTS [hoangtest]
GO

/****** Object:  Schema [hoangtest]    Script Date: 1/12/2022 3:22:23 PM ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'hoangtest')
EXEC sys.sp_executesql N'CREATE SCHEMA [hoangtest]'
GO

/****** Object:  Table [hoangtest].[Test]    Script Date: 1/12/2022 3:22:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[hoangtest].[Test]') AND type in (N'U'))
BEGIN
--The following statement was imported into the database project as a schema object and named hoangtest.Test.
--CREATE TABLE [hoangtest].[Test](
--	[Id] [int] NULL
--) ON [PRIMARY]

END
GO
