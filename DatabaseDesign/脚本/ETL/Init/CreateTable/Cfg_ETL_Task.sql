USE [Leblue-Etl]
GO

/****** Object:  Table [dbo].[Cfg_ETL_Task]    Script Date: 05/20/2014 09:23:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cfg_ETL_Task](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Description] [nvarchar](4000) NULL,
	[StartStepId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Owner] [int] NULL,
	[Remark] [nvarchar](400) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModiifed] [datetime] NULL,
	[LastRunDate] [datetime] NULL,
	[LastRunTime] [datetime] NULL,
	[LastRunOutcome] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Cfg_ETL_Task] ADD  DEFAULT ((1)) FOR [Enabled]
GO

ALTER TABLE [dbo].[Cfg_ETL_Task] ADD  DEFAULT ((1)) FOR [StartStepId]
GO

ALTER TABLE [dbo].[Cfg_ETL_Task] ADD  DEFAULT ((1)) FOR [CategoryId]
GO

ALTER TABLE [dbo].[Cfg_ETL_Task] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO

