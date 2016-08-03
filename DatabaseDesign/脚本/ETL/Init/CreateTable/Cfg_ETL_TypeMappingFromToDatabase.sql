USE [Leblue-Etl]
GO

/****** Object:  Table [dbo].[Cfg_ETL_TypeMappingFromToDatabase]    Script Date: 05/20/2014 09:25:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Cfg_ETL_TypeMappingFromToDatabase](
	[FromToDatabaseId] [int] NULL,
	[SourceType] [varchar](400) NULL,
	[MinSourceVersion] [varchar](10) NULL,
	[MaxSourceVersion] [varchar](10) NULL,
	[DestinationType] [varchar](400) NULL,
	[MinDestinationVersion] [varchar](10) NULL,
	[MaxDestinatoinVersion] [varchar](10) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

