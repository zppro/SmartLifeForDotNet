USE [Leblue-Etl]
GO

/****** Object:  Table [dbo].[Cfg_ETL_TaskStep]    Script Date: 05/20/2014 09:23:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Cfg_ETL_TaskStep](
	[Id] [int] NOT NULL,
	[StepId] [int] NOT NULL,
	[StepName] [nvarchar](100) NOT NULL,
	[StepType] [nvarchar](10) NOT NULL,
	[SuccessProc] [varchar](100) NULL,
	[FailProc] [varchar](100) NULL,
	[TaskId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

