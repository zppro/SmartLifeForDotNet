USE [Leblue-Etl]
GO

/****** Object:  Table [dbo].[Cfg_ETL_ColumnStatistics]    Script Date: 05/20/2014 09:18:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Cfg_ETL_ColumnStatistics](
	[Id] [int] NOT NULL,
	[TableName] [varchar](100) NOT NULL,
	[ColumnName] [varchar](100) NOT NULL,
	[MaxValue] [varchar](100) NOT NULL,
	[MinValue] [varchar](100) NOT NULL,
	[MeanValue] [varchar](100) NOT NULL,
	[StdevValue] [varchar](100) NOT NULL,
	[StatisticsTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Cfg_ETL_ColumnStatistics] ADD  DEFAULT ((0)) FOR [MaxValue]
GO

ALTER TABLE [dbo].[Cfg_ETL_ColumnStatistics] ADD  DEFAULT ((0)) FOR [MinValue]
GO

ALTER TABLE [dbo].[Cfg_ETL_ColumnStatistics] ADD  DEFAULT ((0)) FOR [MeanValue]
GO

ALTER TABLE [dbo].[Cfg_ETL_ColumnStatistics] ADD  DEFAULT ((0)) FOR [StdevValue]
GO

ALTER TABLE [dbo].[Cfg_ETL_ColumnStatistics] ADD  DEFAULT (getdate()) FOR [StatisticsTime]
GO

