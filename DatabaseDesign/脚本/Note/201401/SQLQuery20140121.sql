select * from sys.dm_db_index_physical_stats(DB_ID(N'smartlife'),object_id(N'dbo.t_deploy_function'),null,null,null);

select DB_ID(N'smartlife'),object_id(N'dbo.Oca_ServiceVoice')

select * from sys.dm_os_waiting_tasks;

select * from sys.dm_xe_objects;
select * from sys.dm_os_workers ;
select max(checkintime) from dbo.t_deploy_error_log where  node_id=7 and version_id=91;
select * from dbo.t_deploy_error_log;
update dbo.t_deploy_error_log set content=content+' 1' where node_id=7 and version_id=91 and
checkintime=(select max(checkintime) from dbo.t_deploy_error_log where  node_id=7 and version_id=91);

select * from t_deploy_configure;
alter table t_deploy_configure add version_id_to int not null default 999999;
insert into t_deploy_configure (policy_name,node_id,version_id,value) values ('部署策略',5,1,11);
insert into t_deploy_configure (policy_name,node_id,version_id,value) values ('部署策略',6,1,11);
insert into t_deploy_configure (policy_name,node_id,version_id,value) values ('部署策略',7,1,11);
insert into t_deploy_configure (policy_name,node_id,version_id,value) values ('部署策略',8,1,11);
insert into t_deploy_configure (policy_name,node_id,version_id,value) values ('部署策略',9,1,11);
insert into t_deploy_configure (policy_name,node_id,version_id,value) values ('部署策略',10,1,11);
insert into t_deploy_configure (policy_name,node_id,version_id,value) values ('部署策略',11,1,11);
insert into t_deploy_configure (policy_name,node_id,version_id,value) values ('容错策略',12,1,11);
update t_deploy_configure set policy_name='容错策略' where id>47;