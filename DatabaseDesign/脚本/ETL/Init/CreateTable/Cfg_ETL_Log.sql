USE [Leblue-Etl]
GO

/****** Object:  Table [dbo].[Cfg_ETL_Log]    Script Date: 05/20/2014 09:21:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Cfg_ETL_Log](
	[Id] [int] NOT NULL,
	[ExecuteBeginTime] [datetime] NULL,
	[ComputerName] [varchar](100) NULL,
	[UserName] [varchar](100) NULL,
	[TaskName] [varchar](100) NULL,
	[EventName] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

