select * from sys.dm_xe_object_columns where object_name='page_split';

select * from sys.dm_xe_objects where object_type in ('TYPE','MAP');

select * from sys.dm_xe_map_values where name='wait_types';

select * from sys.dm_xe_objects where object_type='pred_compare' and name like 'greater_than_equal%'
select * from sys.dm_xe_objects where object_type='pred_source';

select * from sys.dm_xe_objects where object_type='action' and type_name='null';
select * from sys.dm_xe_objects where object_type='target';
delete from t_deploy_script where version_id<3;
select * from t_deploy_object where change_type='alter' and object_type='table';
select * from t_deploy_bridge;

select distinct version_id ,version_id_int from v_deploy_version_for_function  order by version_id_int
select * from t_deploy_function;
select * from t_deploy_script;
delete from t_deploy_object;
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