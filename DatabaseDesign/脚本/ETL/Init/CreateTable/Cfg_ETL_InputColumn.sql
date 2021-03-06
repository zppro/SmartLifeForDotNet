USE [Leblue-Etl]
GO

/****** Object:  Table [dbo].[Cfg_ETL_InputColumn]    Script Date: 05/20/2014 09:21:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Cfg_ETL_InputColumn](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](400) NULL,
	[Precision] [int] NULL,
	[Scale] [int] NULL,
	[Length] [int] NULL,
	[DataType] [varchar](100) NULL,
	[CodePage] [int] NULL,
	[MappedColumnId] [int] NULL,
	[externalMetadataColumnId] [int] NULL,
	[usageType] [int] NULL,
	[lineageId] [int] NULL,
 CONSTRAINT [PK_Cfg_ETL_InputColumn_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

