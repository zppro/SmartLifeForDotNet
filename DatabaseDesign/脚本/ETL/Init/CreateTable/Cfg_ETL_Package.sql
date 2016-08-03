USE [Leblue-Etl]
GO

/****** Object:  Table [dbo].[Cfg_ETL_Package]    Script Date: 05/20/2014 09:22:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cfg_ETL_Package](
	[CreatorName] [nvarchar](100) NULL,
	[CreatorComputerName] [nvarchar](100) NULL,
	[CreationDate] [datetime] NULL,
	[PackageType] [int] NULL,
	[EnableConfig] [bit] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

