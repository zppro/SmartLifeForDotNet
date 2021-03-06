USE [Leblue-Etl]
GO

/****** Object:  Table [dbo].[Cfg_ETL_ConnectManager]    Script Date: 05/20/2014 09:19:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cfg_ETL_ConnectManager](
	[Name] [nvarchar](100) NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](400) NULL,
	[ConnectString] [nvarchar](max) NOT NULL,
	[Abbr] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Cfg_ETL_ConnectManager_Id] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

