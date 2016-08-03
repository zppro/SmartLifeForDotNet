select * from t_deploy_script where type='F'
delete from dbo.t_deploy_script where type='F'
select * from t_deploy_object;
delete  from t_deploy_object;
insert into t_deploy_script (version_id,update_script,recover_script,update_validate_script,recover_validate_script,type)
select 3 version_id,b.definition  update_script,'drop function '+a.name  recover_script,
'select count(c.name)  from sys.all_objects c where c.name='''+a.name+'''' update_validate_script,
'select count(d.name)  from sys.all_objects d where d.name='''+a.name+'''' recover_validate_script,'F' type
 from [smartlife-120300].sys.all_objects a,[smartlife-120300].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id;

select * from t_deploy_object where  object_type='Function')

alter table t_deploy_script alter column update_script nvarchar(max) not null;

select DB_NAME() as database_name,user_name() as user_name,
replace(replace(SUBSTRING(update_script, (case CHARINDEX('.',update_script) when 0 then CHARINDEX('unction',update_script)+7 else  CHARINDEX('.',update_script) end )+1,CHARINDEX('(',update_script)-CHARINDEX('.',update_script)-1),']',''),'[','') as table_name,
'Function' object_type,
SUBSTRING(update_script,CHARINDEX('FUNCTION',update_script)-7,6) as change_type,
--cast(id as nvarchar(32))+'_'+cast(version_id as nvarchar(32)) 
version_id,
 '3' as function_id
 from dbo.t_deploy_script where TYPE='F'
 
 select GETDATE() where cast(DATENAME(HH,GETDATE()) as int)> 22 or cast(DATENAME(HH,GETDATE()) as int)<7