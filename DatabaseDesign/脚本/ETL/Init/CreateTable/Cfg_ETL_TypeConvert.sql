USE [Leblue-Etl]
GO

/****** Object:  Table [dbo].[Cfg_ETL_TypeConvert]    Script Date: 05/20/2014 09:24:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Cfg_ETL_TypeConvert](
	[SourceType] [varchar](400) NULL,
	[DestinationType] [varchar](400) NULL,
	[IsImplicitConversion] [bit] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

