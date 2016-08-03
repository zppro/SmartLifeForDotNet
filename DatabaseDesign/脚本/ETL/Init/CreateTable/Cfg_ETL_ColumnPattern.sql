USE [Leblue-Etl]
GO

/****** Object:  Table [dbo].[Cfg_ETL_ColumnPattern]    Script Date: 05/20/2014 09:18:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Cfg_ETL_ColumnPattern](
	[Id] [int] NOT NULL,
	[TableName] [varchar](100) NOT NULL,
	[ColumnName] [varchar](100) NOT NULL,
	[RegularExpression] [varchar](100) NOT NULL,
	[Ratio] [float] NOT NULL,
	[StatisticsTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Cfg_ETL_ColumnPattern] ADD  DEFAULT ((0)) FOR [Ratio]
GO

ALTER TABLE [dbo].[Cfg_ETL_ColumnPattern] ADD  DEFAULT (getdate()) FOR [StatisticsTime]
GO

