USE [Leblue-Etl]
GO

/****** Object:  Table [dbo].[Cfg_ETL_TranslateRule]    Script Date: 05/20/2014 09:23:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Cfg_ETL_TranslateRule](
	[sourcetablename] [varchar](100) NULL,
	[sourcecolumnname] [varchar](100) NULL,
	[desttablename] [varchar](100) NULL,
	[destcolumnname] [varchar](100) NULL,
	[oldvalue] [varchar](100) NULL,
	[newvalue] [varchar](100) NULL,
	[mapExpression] [varchar](100) NULL,
	[type] [int] NULL,
	[typedest] [varchar](100) NULL,
	[brokerTable] [varchar](100) NULL,
	[brokerColumn] [varchar](100) NULL,
	[brokerTargetColumn] [varchar](100) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

