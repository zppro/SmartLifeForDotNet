USE [Leblue-Etl]
GO

/****** Object:  Table [dbo].[Cfg_ETL_Variable]    Script Date: 05/20/2014 09:25:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Cfg_ETL_Variable](
	[EvaluateAsExpression] [bit] NOT NULL,
	[Description] [varchar](max) NULL,
	[Expression] [varchar](100) NULL,
	[Name] [nvarchar](100) NULL,
	[Namespace] [varchar](50) NULL,
	[RaiseChangedEvent] [bit] NOT NULL,
	[Scope] [varchar](100) NULL,
	[Value] [varchar](100) NULL,
	[ValueType] [varchar](10) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Cfg_ETL_Variable] ADD  DEFAULT ((1)) FOR [EvaluateAsExpression]
GO

ALTER TABLE [dbo].[Cfg_ETL_Variable] ADD  DEFAULT ((1)) FOR [RaiseChangedEvent]
GO

