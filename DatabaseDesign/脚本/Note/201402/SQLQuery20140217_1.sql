select * from t_deploy_script;
select * from t_deploy_object;
select * from t_deploy_bridge;

select * from  t_deploy_error_log;
select * from  t_deploy_execution_log;
select * from t_deploy_script where id in(
select version_id from  t_deploy_status_log where status=0);

select * from sys.all_sql_modules where object_id=565577053;
select * from sys.all_columns where object_id=565577053;
select * from sys.column_type_usages where object_id=565577053;
select name,(select name from sys.systypes where xtype=system_type_id),max_length,is_nullable from sys.columns where object_id=565577053;
select * from sys.table_types;
select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='JSONHierarchy'
select * from sys.all_objects where type='TT';
select * from sys.systypes;

select * from sys.dm_audit_actions;
select * from sys.dm_db_file_space_usage;
select name from sys.all_objects where object_id in(
select object_id from sys.dm_db_index_usage_stats where database_id=11);
select * from sys.dm_os_memory_pools;

select name from [smartlife-120300].sys.tables where name not in (select name from sys.tables)

--------------------------------
select b.name  table_name,
a.name,a.system_type_id,a.max_length,a.is_nullable from sys.columns a right join sys.tables b on b.object_id=a.object_id
and b.object_id>0 order by table_name,name;


select count(b.name),b.object_id from sys.tables b where b.object_id>0 group by b.object_id
select distinct OBJECT_ID from sys.columns a where object_id>0;