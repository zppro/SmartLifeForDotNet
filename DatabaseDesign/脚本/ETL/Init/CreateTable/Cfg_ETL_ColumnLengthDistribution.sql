USE [Leblue-Etl]
GO

/****** Object:  Table [dbo].[Cfg_ETL_ColumnLengthDistribution]    Script Date: 05/20/2014 09:16:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Cfg_ETL_ColumnLengthDistribution](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [varchar](100) NOT NULL,
	[ColumnName] [varchar](100) NOT NULL,
	[LengthValue] [varchar](100) NULL,
	[CountNum] [int] NOT NULL,
	[MaxLengthValue] [int] NOT NULL,
	[MinLengthValue] [int] NOT NULL,
	[StatisticsTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Cfg_ETL_ColumnLengthDistribution] ADD  DEFAULT ((0)) FOR [CountNum]
GO

ALTER TABLE [dbo].[Cfg_ETL_ColumnLengthDistribution] ADD  DEFAULT ((0)) FOR [MaxLengthValue]
GO

ALTER TABLE [dbo].[Cfg_ETL_ColumnLengthDistribution] ADD  DEFAULT ((0)) FOR [MinLengthValue]
GO

ALTER TABLE [dbo].[Cfg_ETL_ColumnLengthDistribution] ADD  DEFAULT (getdate()) FOR [StatisticsTime]
GO

