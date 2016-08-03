select dbo.FUNC_Tol_GetObjectNames('Smartlife-120300','function')

select * from t_deploy_object_column_detail;
select update_script from t_deploy_script order by id
-- WHERE type='T'
select * from t_deploy_function;
select table_name from t_deploy_object where object_type='table' and change_type='not';

insert into dbo.t_deploy_object (database_name,user_name,table_name,object_type,change_type,version_id,function_id)
select DB_NAME() as database_name,user_name() as user_name,
'Sys_Application' table_name,'table' object_type,'not' change_type,
11 version_id,'103' as function_id ;

insert into dbo.t_deploy_object (database_name,user_name,table_name,object_type,change_type,version_id,function_id)
select DB_NAME() as database_name,user_name() as user_name,
'Sys_Behavior' table_name,'table' object_type,'not' change_type,
11 version_id,'103' as function_id ;

insert into dbo.t_deploy_object (database_name,user_name,table_name,object_type,change_type,version_id,function_id)
select DB_NAME() as database_name,user_name() as user_name,
'Sys_Dictionary' table_name,'table' object_type,'not' change_type,
11 version_id,'103' as function_id ;

insert into dbo.t_deploy_object (database_name,user_name,table_name,object_type,change_type,version_id,function_id)
select DB_NAME() as database_name,user_name() as user_name,
'Sys_Platform' table_name,'table' object_type,'not' change_type,
11 version_id,'103' as function_id ;


insert into dbo.t_deploy_object (database_name,user_name,table_name,object_type,change_type,version_id,function_id)
select DB_NAME() as database_name,user_name() as user_name,
'Sys_Server' table_name,'table' object_type,'not' change_type,
11 version_id,'103' as function_id ;

select COUNT(*) from Oca_OldManBaseInfo;
select distinct version_id ,version_id_int 
from v_deploy_version_for_function 
--where function_name='$(function_name)' 
order by version_id_int;
select * from v_deploy_version_for_function where function_name='升级表数据'

  IF (OBJECT_ID(N'dbo.Pub_ReminderObject', 'U') IS NULL)  BEGIN     PRINT ''     PRINT 'Creating table Pub_ReminderObject ...'  
  create table [dbo].Pub_ReminderObject (Id int  not null  ,CheckInTime datetime  not null ,ObjectType varchar(255)  null ,ObjectKey varchar(50)  null ,ReminderId int  not null ,ResponseAppType char(5)  null ,ResponseFlag tinyint  not null ,CONSTRAINT  PK_Pub_ReminderObject  PRIMARY KEY CLUSTERED  (Id)); 
   END    
    go
初始化
升级表结构
升级表数据
升级表数据28


insert into t_deploy_script (version_id,object_name,update_script,recover_script,update_validate_script,recover_validate_script,type)
select 3 version_id,a.name object_name,' declare @str nvarchar(max)
select top 1 @str=b.definition
from [$(source_db_name)].sys.all_objects a,[$(source_db_name)].sys.all_sql_modules b 
where a.type in (''P'') and a.object_id>0 and a.object_id=b.object_id 
and a.name='''+a.name+''' and  len(b.definition)>40;
exec sp_executesql @str;'  update_script,'drop procedure '+a.name+';'  recover_script,
'select count(c.name)  from sys.all_objects c where c.name='''+a.name+'''' update_validate_script,
'select count(d.name)  from sys.all_objects d where d.name='''+a.name+'''' recover_validate_script,'P' type
from [Smartlife-120300].sys.all_objects a,[Smartlife-120300].sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>40;

select * from t_deploy_bridge where city='杭州上城区' and version_id=1 and database_name is not null;

insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州上城区','192.168.1.109',Getdate(),1,11,'smartlife-120301');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州上城区','192.168.1.109',Getdate(),1,11,'smartlife-120302');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州上城区','192.168.1.109',Getdate(),1,11,'smartlife-120303');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州上城区','192.168.1.109',Getdate(),1,11,'smartlife-120304');

insert into dbo.t_deploy_object_column_detail (database_name,user_name,table_name,column_name,system_type_id,max_length,
is_null,system_type_id_old,max_length_old,
is_null_old,object_type,change_type,version_id,function_id)
select  DB_NAME() as database_name,user_name() as user_name,
m.table_name,m.column_name,m.system_type_id,m.max_length,
m.is_nullable,
n.system_type_id system_type_id_old,n.max_length max_length_old,
n.is_nullable is_nullable_old,'column' object_type,'alter' change_type,3 version_id,2 function_id
from (
select b.name  table_name,
a.name column_name,a.system_type_id,a.max_length,a.is_nullable from [smartlife-120301].sys.columns a right join [smartlife-120301].sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) m,(
select b.name+a.name column_name
,a.system_type_id,a.max_length,a.is_nullable 
from sys.columns a right join sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) n
where m.table_name+m.column_name  =n.column_name 
 
and m.table_name+m.column_name not in (
select e.table_name+e.column_name pri_key
from (
select b.name  table_name,
a.name column_name,a.system_type_id,a.max_length,a.is_nullable from [smartlife-120301].sys.columns a right join [smartlife-120301].sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) e inner join
(
select b.name  table_name,
a.name column_name,a.system_type_id,a.max_length,a.is_nullable from sys.columns a right join sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) f on
 e.table_name=f.table_name and e.column_name=f.column_name and e.system_type_id=f.system_type_id and e.max_length=f.max_length
 and e.is_nullable=f.is_nullable
);