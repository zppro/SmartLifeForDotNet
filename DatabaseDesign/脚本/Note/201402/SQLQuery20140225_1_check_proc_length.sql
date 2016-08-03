select * from sys.dm_db_script_level;
select * from sys.configurations;

select * from t_deploy_bridge;

declare @update_type nvarchar(50)
declare @type nvarchar(max)
declare @source_db_name nvarchar(50)
declare @str_select nvarchar(max)
set @type='''AF'',''TF'',''FN'''
set @source_db_name='Smartlife-120300'
set @update_type='function'

  set @str_select= 'select top 1 @str=b.definition,@str_drop=''drop '+@update_type+' ''+a.name+'';'' 
  from ['+@source_db_name+'].sys.all_objects a,['+@source_db_name+'].sys.all_sql_modules b  
  where a.type in ('+@type+') and a.object_id>0 and a.object_id=b.object_id   and a.name=@id' 
  print @str_select
  
  
  IF (NOT OBJECT_ID(N'dbo.t_deploy_object_version_history', 'U') IS NULL)
BEGIN
  DROP TABLE dbo.t_deploy_object_version_history
END

IF (OBJECT_ID(N'dbo.t_deploy_object_version_history', 'U') IS NULL)
BEGIN
   PRINT ''
   PRINT 'Creating table t_deploy_object_version_history ...'
create table t_deploy_object_version_history
(
database_name           nvarchar(20)    not null,
user_name               nvarchar(30)    not null,
Before_definition	Nvarchar(max)	Not null,
After_definition        Nvarchar(max)	Not null,
Before_length	        Int	        Not null,
After_length		Int	        Not null,
Before_modify_date	Datetime	Not null,
After_modify_date	Datetime	Not null,
Version_id		Int	        null,
comments		Nvarchar(4000)	null
)
END
go

alter table t_deploy_object_version_history add type char(1) default('F');
alter table t_deploy_object_version_history add Object_name	        Varchar(60)	Not null;
alter table t_deploy_script   add Table_name	Varchar(60)	   null;
--alter table t_deploy_script   drop column "t_deploy_script.object_name"	Varchar(60)	   null;
alter table t_deploy_script   add object_name	Varchar(60)	   null;
--alter table t_deploy_script alter column Table_name  to object_name;
sp_rename 't_deploy_script.object_name','object_name'

select e.name,e.lengths,f.lengths
from (
select a.name,LEN(b.definition) lengths
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) e,
 (
select a.name,LEN(b.definition) lengths
 from remote1.[Smartlife-120399].sys.all_objects a,remote1.[Smartlife-120399].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) f
where  e.name=f.name and e.lengths<>f.lengths

---FUNC_Tol_GetWB	9066	9557

select e.name,e.lengths--,f.lengths
from (
select a.name,LEN(b.definition) lengths
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id) e,
 (
select a.name,LEN(b.definition) lengths
 from [remote1.Smartlife-120399].sys.all_objects a,[remote1.Smartlife-120399].sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id) f
where  e.name=f.name and e.lengths<>f.lengths

select name from [smartlife-120301].sys.tables where name not in (select name from sys.tables );
select name from [smartlife-120300].sys.tables order by name desc ;
exec sp_tables @table_owner='dbo'
select update_script from t_deploy_script where object_name='Pub_User' order by id;

update t_deploy_script   set  OBJECT_NAME=b.table_name 
from t_deploy_script,  (select table_name,version_id from t_deploy_object where object_type='table') b 
where t_deploy_script.version_id=b.version_id 
;

select name from [Smartlife-120301].sys.tables where name not in (select name from sys.tables );

select * from t_deploy_object_column_detail;

select e.name,e.lengths
from (
select a.name,LEN(b.definition) lengths
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('V') and a.object_id>0 and a.object_id=b.object_id) e
where e.name not in (
select a.name
 from [Smartlife-120300].sys.all_objects a,[Smartlife-120300].sys.all_sql_modules b 
where a.type in ('V') and a.object_id>0 and a.object_id=b.object_id
);


select e.name,e.lengths
from (
select a.name,LEN(b.definition) lengths
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) e
where e.name not in (
select a.name
 from [Smartlife-120300].sys.all_objects a,[Smartlife-120300].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id
);

exec SP_dba_repl_function @source_db_name='Smartlife-120300',@update_type='function';
go

------------------------
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter Function [dbo].[FUNC_Tol_GetObjectNames]
(
@source_db_name nvarchar(50)='Smartlife-120300',
@update_type  nvarchar(40)='function'
)
RETURNS 
@object TABLE (
		name VARCHAR(50) 
		)
AS
  BEGIN
  declare @type nvarchar(max)
  declare @str_sql nvarchar(max)
if (@update_type='function')
 begin
 set @type='''AF'',''TF'',''FN'''
 end
 
 if (@update_type='procedure')
 begin
 set @type='''P'''
 end
 
 if (@update_type='View')
 begin
 set @type='''V'''
 end
  set @str_sql='INSERT @object (name) select e.name id
from (
select a.name,LEN(b.definition) lengths
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('+@type+') and a.object_id>0 and a.object_id=b.object_id) e,
 (
select a.name,LEN(b.definition) lengths
 from ['+@source_db_name+'].sys.all_objects a,['+@source_db_name+'].sys.all_sql_modules b 
where a.type in ('+@type+') and a.object_id>0 and a.object_id=b.object_id) f
where  e.name=f.name and e.lengths<>f.lengths;'
exec sp_executesql @str_sql
--INSERT @object (name) VALUES(@thirdlongitude, @thirdlatitude)
RETURN 
    END 
    
    
    select dbo.FUNC_Tol_GetObjectNames('Smartlife-120300','View')