USE [Leblue-Etl]
GO

/****** Object:  Table [dbo].[Cfg_ETL_Component]    Script Date: 05/20/2014 09:19:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cfg_ETL_Component](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[OpenRowSets] [nvarchar](100) NULL,
	[SqlCommand] [nvarchar](4000) NULL,
 CONSTRAINT [PK_Cfg_ETL_Component_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

