select * from sys.dm_xe_object_columns where object_name='page_split';

select * from sys.dm_xe_objects where object_type in ('TYPE','MAP');

select * from sys.dm_xe_map_values where name='wait_types';

select * from sys.dm_xe_objects where object_type='pred_compare' and name like 'greater_than_equal%'
select * from sys.dm_xe_objects where object_type='pred_source';

select * from sys.dm_xe_objects where object_type='action' and type_name='null';
select * from sys.dm_xe_objects where object_type='target';
delete from t_deploy_script where version_id=706;
select * from t_deploy_object where change_type='alter' and object_type='table';
select * from t_deploy_bridge;

select distinct version_id ,version_id_int from v_deploy_version_for_function  order by version_id_int
select * from t_deploy_function;
select * from t_deploy_script where version_id>857 and version_id<861 order by id;
delete  from t_deploy_script;
create table [dbo].Sys_TreeItem (TreeId char(5)  not null primary key,ItemId char(5)  not null primary key,Id int  not null ,Status tinyint  not null ,ItemCode varchar (100)  null ,ItemName nvarchar  null ,GenerateMode char(5)  not null ,GenerateContent nvarchar  not null ,PrefixId varchar (30)  null ,Description nvarchar  null ,OrderNo int  not null );
delete from t_deploy_object;
select * from t_deploy_status_log where version_id>978 and status=0;
select * from t_deploy_execution_log;
insert into t_deploy_object (database_name,user_name,table_name,object_type,change_type,version_id,function_id)
select  DB_NAME() as database_name,user_name() as user_name,
SUBSTRING(update_script,CHARINDEX(']',update_script)+2,(case CHARINDEX('ADD ',update_script) when 0 then CHARINDEX('(',update_script) else CHARINDEX('ADD ',update_script) end)-CHARINDEX(']',update_script)-2) as table_name,
'table' object_type,
SUBSTRING(update_script,0,CHARINDEX('table',update_script)) as change_type,
--cast(id as nvarchar(32))+'_'+cast(version_id as nvarchar(32)) 
version_id,
 '3' as function_id from dbo.t_deploy_script 
 where  id<81;
 
 select DB_NAME() as database_name,user_name() as user_name,
--SUBSTRING(update_script,CHARINDEX(']',update_script)+2,CHARINDEX('ADD',update_script)-CHARINDEX(']',update_script)-2) as table_name,
CHARINDEX(']',update_script),CHARINDEX('ADD',update_script),CHARINDEX(']',update_script),
'table' object_type,
SUBSTRING(update_script,0,CHARINDEX('table',update_script)) as change_type,
--cast(id as nvarchar(32))+'_'+cast(version_id as nvarchar(32)) 
version_id,
 '3' as function_id from dbo.t_deploy_script where   id>80;
 
 select update_script,CHARINDEX('(',update_script) a,CHARINDEX('ADD ',update_script) b,(case CHARINDEX('ADD ',update_script) when 0 then CHARINDEX('(',update_script) else CHARINDEX('ADD ',update_script) end) c from dbo.t_deploy_script;
 
 alter view [dbo].[v_deploy_version_for_function] as 
select ':'+cast(version_id as nvarchar(32))+':' version_id,a.version_id version_id_int,b.function_class,b.function_name from t_deploy_object a,t_deploy_function b
 where a.function_id=b.function_id  
 
 
  select constraint_schema, table_name,column_name from [smartlife-120300].INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
 where upper(constraint_name) like 'PK%'  and table_name='sys_treeitem' 
 
 EXEC sp_tables syscolumns, dbo
 
 select * from INFORMATION_SCHEMA.VIEWS;
 
 select a.ordinal_position,a.table_schema+'.'+a.table_name as tablename,a.column_name+' '+a.data_type+
 case a.DATA_TYPE when 'varchar' then ' ('+CAST (a.character_octet_length as nvarchar(32))+')'
when 'char' then '('+CAST (a.character_octet_length as nvarchar(32))+')' else '' end +
' ' +case a.is_nullable when 'NO' then ' not null' when 'YES' then ' null' else '' end +' '+case  when d.COLUMN_NAME IS NULL then '' 
else 'primary key' end +',' as columname
from (select * from [smartlife-120300].INFORMATION_SCHEMA.COLUMNS where table_name = 'Cer_Redirect') a left join (
  select constraint_schema, table_name,column_name from [smartlife-120300].INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
 where upper(constraint_name) like 'PK%' 
 )d
 on  a.TABLE_SCHEMA = d.constraint_schema
 and a.column_name = d.column_name and a.TABLE_NAME=d.TABLE_NAME
 
 create table [dbo].Sys_TreeItem (TreeId char(5)  not null  ,ItemId char(5)  not null  ,Id int  not null ,Status tinyint  not null ,ItemCode varchar (100)  null ,ItemName nvarchar  null ,GenerateMode char(5)  not null ,GenerateContent nvarchar  not null ,PrefixId varchar (30)  null ,Description nvarchar  null ,OrderNo int  not null ,CONSTRAINT  PK_Sys_TreeItem  PRIMARY KEY CLUSTERED  (ItemId, TreeId));
 
 
 select DATA_TYPE,replace('('+CAST (character_octet_length as nvarchar(32))+')','-1','max') ,character_octet_length from [smartlife-120300].INFORMATION_SCHEMA.COLUMNS where table_name ='Cer_DeployNode'
 
 select a.ordinal_position,a.table_schema+'.'+a.table_name as tablename,a.column_name+' '+a.data_type+
 case a.DATA_TYPE when 'varchar' then replace(' ('+CAST (a.character_octet_length as nvarchar(32))+')','-1','max')
when 'char' then '('+CAST (a.character_octet_length as nvarchar(32))+')' else '' end +
' ' +case a.is_nullable when 'NO' then ' not null' when 'YES' then ' null' else '' end +' '+case  when d.COLUMN_NAME IS NULL then '' 
else ' ' end +',' as columname
from (select * from [smartlife-120300].INFORMATION_SCHEMA.COLUMNS where table_name ='Cer_DeployNode') a left join (
  select constraint_schema, table_name,column_name from [smartlife-120300].INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
 where upper(constraint_name) like 'PK%' 
 )d
 on  a.TABLE_SCHEMA = d.constraint_schema
 and a.column_name = d.column_name and a.TABLE_NAME=d.TABLE_NAME