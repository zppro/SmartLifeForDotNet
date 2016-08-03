--dbcc loginfo
--dbcc useroptions
select * from dbo.Oca_OldManBaseInfo;
select * from dbo.Oca_OldManConfigInfo;
select top 0 * from dbo.Oca_CC_Ext;
select * from dbo.Sys_DictionaryItem; 
delete  from dbo.Sys_DictionaryItem where DictionaryId=11013 and Itemid like '01%';

------1.
insert into dbo.t_deploy_script (version_id,update_script,recover_script,update_validate_script,recover_validate_script,type) 
select 300,'delete  from dbo.Sys_DictionaryItem where DictionaryId=11013 and Itemid like ''01%'';','select 1','select 1','select 1','D'
------2
 insert into dbo.t_deploy_object (database_name,user_name,table_name,object_type,change_type,version_id,function_id)
select DB_NAME() as database_name,user_name() as user_name,
'Sys_DictionaryItem' as table_name,
'Data' object_type,
'delete' as change_type,300 version_id,
 '104' as function_id;

select * from t_deploy_script where type='D';
select * from t_deploy_object where t;
select * from t_deploy_bridge where city='杭州西湖区' ;
select * from t_deploy_configure;
select * from t_deploy_function;
update t_deploy_object set function_id=102 where id=267;
update t_deploy_object set function_id=103 where id=268;
update t_deploy_object set function_id=103 where id=269;
update t_deploy_object set function_id=105 where id>269;
select 'insert into '+name +' select *  from '+name+';' from sys.tables;

insert into t_deploy_function (function_name,parent_id,function_class,function_id) values ('初始化',11,'部署',102);
insert into t_deploy_function (function_name,parent_id,function_class,function_id) values ('升级表结构',11,'部署',103);
insert into t_deploy_function (function_name,parent_id,function_class,function_id) values ('升级表数据',11,'部署',104);
insert into t_deploy_function (function_name,parent_id,function_class,function_id) values ('升级表数据28',11,'部署',105);
select distinct version_id ,version_id_int,function_class,function_name from v_deploy_version_for_function  
where 
--function_class='部署' and 
function_name='升级表数据'
order by version_id_int

select 3 version_id,b.definition  update_script,'drop function '+a.name  recover_script,LEN(b.definition) lengths,
'select count(c.name)  from sys.all_objects c where c.name='''+a.name+'''' update_validate_script,
'select count(d.name)  from sys.all_objects d where d.name='''+a.name+'''' recover_validate_script,'F' type
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id;

select count(c.name)  from sys.all_objects c where c.name='FUNC_Tol_GetPY'
select * from t_deploy_script;


