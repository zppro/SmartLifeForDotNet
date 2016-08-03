USE [SmartLife-120300]
GO

/****** Object:  Table [dbo].[temp_test]    Script Date: 04/15/2014 15:36:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[temp_test2](
	[id] [int] NULL,
	[inputcode] [varchar](100) NULL,
	[inputcode2] [varchar](100) NULL,
	[name] [varchar](100) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[temp_test] ADD  DEFAULT ([dbo].[FUNC_Tol_GetWB]('')) FOR [inputcode]
GO

ALTER TABLE [dbo].[temp_test] ADD  DEFAULT ([dbo].[FUNC_Tol_GetPY]('Îå')) FOR [inputcode2]
GO
set timing on

set statistics time on
set statistics profile on
insert into temp_test(id,name)
select object_id,name from sys.all_objects;

insert into temp_test2(id,name)
select object_id,name from sys.all_objects;

select COUNT(*) from temp_test;
select COUNT(*) from temp_test2;

select CallNo from ' + @Source +'.dbo.Oca_OldManConfigInfo 
union 
select CallNo2 as CallNo from ' + @Source +'.dbo.Oca_OldManConfigInfo 
union 
select CallNo3 as CallNo from ' + @Source +'.dbo.Oca_OldManConfigInfo