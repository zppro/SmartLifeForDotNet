use smartlife
go 
set statistics profile on
set statistics profile off
select * from t_deploy_bridge;

select * from t_deploy_script order by id;
select * from t_deploy_bridge;
select * from sys.dm_db_script_level;
select * from sys.configurations;
select * from t_deploy_tabletofunction;
select * from t_deploy_function ;
select * from dbo.t_deploy_error_log;
select * from dbo.t_deploy_execution_log;
select * from dbo.t_deploy_status_log;

create view v_deploy_version_for_function as 
select a.version_id,b.function_class,b.function_name from t_deploy_object a,t_deploy_function b
 where a.function_id=b.function_id  ;
 --order by a.version_id;
 dbcc ind (smartlife,t_deploy_function,1)
 dbcc traceon(3604)
 dbcc page (smartlife,1,90,1)
select version_id from v_deploy_version_for_function where function_name in ('电子钱包','数据更新') order by version_id;
--insert into and b.function_name in ('电子钱包','数据更新');
update dbo.t_deploy_object set version_id=b.version_id from dbo.t_deploy_object a,(
select 
SUBSTRING(update_script,CHARINDEX('table',update_script)+6,CHARINDEX('(',update_script)-CHARINDEX('table',update_script)-6) as table_name,
SUBSTRING(update_script,CHARINDEX('go',update_script)+3,CHARINDEX('table',update_script)-CHARINDEX('go',update_script)-3) as change_type,
--cast(id as nvarchar(32))+'_'+cast(version_id as nvarchar(32)) 
id version_id from dbo.t_deploy_script where version_id=3 and id<150) b where a.table_name=b.table_name and a.change_type=b.change_type;
update dbo.t_deploy_script set version_id=id where version_id=3;

alter table t_deploy_error_log add checkintime datetime not null default getdate();
alter table t_deploy_execution_log add checkintime datetime not null default getdate();
alter table t_deploy_status_log add checkintime datetime not null default getdate();


select distinct version_id ,version_id_int from v_deploy_version_for_function  order by version_id_int)