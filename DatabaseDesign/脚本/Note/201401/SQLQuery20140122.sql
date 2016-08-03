select * from sys.dm_xe_packages

select * from sys.dm_xe_objects where name='page_split';
select * from sys.dm_xe_object_columns where object_name='page_split';

select * from sys.databases;
select * from sys.messages where message_id=102;
select * from t_deploy_script;
select * from t_deploy_bridge;
select * from sys.all_objects a,sys.all_columns b 
where (a.name=N't_deploy_configure') and a.object_id=b.object_id and (b.name=N'version_id_to')

select * from sys.all_objects a,sys.all_columns b 
where (a.name=N't_deploy_status_log') and a.object_id=b.object_id and (b.name=N'checkintime')
print 'creating table [schema].[source_table]'

select OBJECT_ID(N'[dbo].[t_deploy_function]','U')

SELECT name FROM master.dbo.sysdatabases
                WHERE (name = N'mysdb')
                
use smartlife
go
IF (OBJECT_ID(N'dbo.t_deploy_errordefine', 'U') IS NULL)
BEGIN
   PRINT ''
   PRINT 'Creating table t_deploy_errordefine ...'
create table dbo.t_deploy_errordefine
(
id         int identity(1,1)  not null,
error_id   int                not null,
describe   nvarchar(256)      not null
)
END
go
      
      
      insert into dbo.t_deploy_object
select  DB_NAME() as database_name,user_name() as user_name,
SUBSTRING(update_script,CHARINDEX(']',update_script)+2,CHARINDEX('ADD',update_script)-CHARINDEX(']',update_script)-2) as table_name,
'table' object_type,
SUBSTRING(update_script,0,CHARINDEX('table',update_script)) as change_type,
--cast(id as nvarchar(32))+'_'+cast(version_id as nvarchar(32)) 
version_id,
 '3' as function_id from dbo.t_deploy_script
go
/*
 where version_id=3 and id<150;
 go
 insert into dbo.t_deploy_object
select DB_NAME() as database_name,user_name() as user_name,
SUBSTRING(update_script,CHARINDEX(']',update_script)+2,CHARINDEX('ADD',update_script)-CHARINDEX(']',update_script)-2) as table_name,
'table' object_type,
SUBSTRING(update_script,0,CHARINDEX('table',update_script)) as change_type,
--cast(id as nvarchar(32))+'_'+cast(version_id as nvarchar(32)) 
id version_id,
 '3' as function_id from dbo.t_deploy_script where version_id=3 and id>309;
go
*/
         select * from t_deploy_tabletofunction;   
         select * from dbo.t_deploy_bridge;
         select * from dbo.t_deploy_configure;
         select  * from t_deploy_errordefine; 
         select * from t_deploy_function;
         delete from t_deploy_function where id>136;
         select * from dbo.t_deploy_tabletofunction;
         select * from dbo.t_deploy_execution_log;
         select * from dbo.t_deploy_object;
         select * from dbo.t_deploy_orderformat;
         select * from dbo.t_deploy_script ORDER BY ID;
         DELETE FROM dbo.t_deploy_script WHERE ID<7
         
         select a.ordinal_position,a.table_schema+'.'+a.table_name as tablename,a.column_name+' '+a.data_type+
 case a.DATA_TYPE when 'varchar' then ' ('+CAST (a.character_octet_length as nvarchar(32))+')'
when 'char' then '('+CAST (a.character_octet_length as nvarchar(32))+')' else '' end +
' ' +case a.is_nullable when 'NO' then ' not null' when 'YES' then ' null' else '' end +' '+case  when d.COLUMN_NAME IS NULL then '' 
else 'primary key' end +',' as columname
from (select * from [smartlife-120300].INFORMATION_SCHEMA.COLUMNS where table_name ='Cer_Redirect') a left join (
  select constraint_schema, table_name,column_name from [smartlife-120300].INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
 where upper(constraint_name) like 'PK%' 
 )d
 on  a.TABLE_SCHEMA = d.constraint_schema
 and a.column_name = d.column_name and a.TABLE_NAME=d.TABLE_NAME
         
         select name from [smartlife-120300].sys.tables order by name desc
         
         
         insert into dbo.t_deploy_object
select  DB_NAME() as database_name,user_name() as user_name,
--SUBSTRING(update_script,CHARINDEX(']',update_script)+2,CHARINDEX('ADD',update_script)-CHARINDEX(']',update_script)-2) as table_name,
'table' object_type,
SUBSTRING(update_script,0,CHARINDEX('table',update_script)) as change_type,
--cast(id as nvarchar(32))+'_'+cast(version_id as nvarchar(32)) 
version_id,
 '3' as function_id from dbo.t_deploy_script
go



insert into dbo.t_deploy_object
select  DB_NAME() as database_name,user_name() as user_name,
SUBSTRING(update_script,CHARINDEX(']',update_script)+2,CHARINDEX('(',update_script)-CHARINDEX(']',update_script)-2) as table_name,
'table' object_type,
SUBSTRING(update_script,0,CHARINDEX('table',update_script)) as change_type,
--cast(id as nvarchar(32))+'_'+cast(version_id as nvarchar(32)) 
version_id,
 '3' as function_id from dbo.t_deploy_script where version_id=3 and id<81;
go

 insert into dbo.t_deploy_object
select DB_NAME() as database_name,user_name() as user_name,
SUBSTRING(update_script,CHARINDEX(']',update_script)+2,CHARINDEX('ADD',update_script)-CHARINDEX(']',update_script)-2) as table_name,
'table' object_type,
SUBSTRING(update_script,0,CHARINDEX('table',update_script)) as change_type,
--cast(id as nvarchar(32))+'_'+cast(version_id as nvarchar(32)) 
id version_id,
 '3' as function_id from dbo.t_deploy_script where version_id=3 and id>80;
go
