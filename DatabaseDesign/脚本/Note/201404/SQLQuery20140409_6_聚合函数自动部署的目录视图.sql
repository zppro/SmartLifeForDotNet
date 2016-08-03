select * from sys.all_sql_modules;

select * from sys.all_parameters
where object_id in (select object_id from sys.all_objects where name='sp_etl_column');


select * from sys.all_parameters
where object_id in (select object_id from sys.assembly_modules where assembly_class='JoinStrEx');------参数

select * from sys.assemblies;----------程序集
select *,LEN(content) from sys.assembly_files;  -----生成脚本的查询视图
select * from sys.assembly_modules;  ---聚合函数名称
select * from sys.assembly_references;  
select * from sys.assembly_types;


