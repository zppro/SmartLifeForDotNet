USE [Leblue-Etl]
GO

/****** Object:  Table [dbo].[Cfg_ETL_OutputTable]    Script Date: 05/20/2014 09:22:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Cfg_ETL_OutputTable](
	[Id] [int] NOT NULL,
	[TableId] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[TaskId] [int] NOT NULL,
	[LineageId] [int] NOT NULL,
	[ExternalMetadataTableId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

