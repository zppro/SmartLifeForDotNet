select sum(usecounts*size_in_bytes)/1024/1024,sum(size_in_bytes)/1024/1024 from sys.dm_exec_cached_plans
select * from sys.dm_exec_cached_plans
select * from sys.system_internals_allocation_units 

SELECT usecounts, text, dbid, objectid FROM
   sys.dm_exec_cached_plans cp
   CROSS APPLY sys.dm_exec_sql_text(cp.plan_handle)
WHERE objtype= 'Proc';

SELECT CASE when dbid = 32767 
            then 'Resource' 
            else DB_NAME(dbid) end [DB_NAME], 
       OBJECT_SCHEMA_NAME(objectid,dbid) AS [SCHEMA_NAME], 
       OBJECT_NAME(objectid,dbid)AS [OBJECT_NAME], 
       SUM(usecounts) AS [Use_Count], 
       SUM(total_logical_reads) AS [total_logical_reads],
       SUM(total_logical_reads) / SUM(usecounts) * 1.0 AS [avg_logical_reads],
       dbid, 
       objectid  
FROM sys.dm_exec_cached_plans cp
   CROSS APPLY sys.dm_exec_sql_text(cp.plan_handle)
   JOIN 
   (SELECT SUM(total_logical_reads) AS [total_logical_reads],
           plan_handle  
      FROM sys.dm_exec_query_stats  
      GROUP BY plan_handle) qs
    ON cp.plan_handle = qs.plan_handle 
WHERE objtype= 'Proc'
group by dbid,objectid

--

dbcc showcontig ('Oca_OldmanBaseinfo')
dbcc tracestatus
dbcc proccache
dbcc help('traceon')
dbcc help('?')
select a.* from 
 sys.all_objects a
where a.type in ('AF','TF','FN') and a.object_id>0 and a.create_date=a.modify_date

create table person
(last_name nvarchar(10),
family_name nvarchar(10),
birthday    datetime,
gender    tinyint
)

create table t_deploy_script_long4000
(update_script nvarchar(max) null,
ojbect_name    varchar(50)   null,
type           varchar(20)   null
)

insert into t_deploy_bridge  select * from t_deploy_bridge;

insert into t_deploy_script (version_id,update_script,recover_script,update_validate_script,recover_validate_script,type)
select 3 version_id,
 'insert into '+name +' select *  from '+name+';' update_script,
'delete from '+name+';' recover_script,
'select COUNT(*) from '+name+';' update_validate_script,
'select COUNT(*) from '+name+';' recover_validate_script,'D' type
 from sys.tables where name not like 't_deploy%';
 
 select LEN(update_script) from t_deploy_script;
 update dbo.t_deploy_script set version_id=id where version_id=3 ;
 select COUNT(*) from Sys_MenuBehavior;
 
 insert into dbo.t_deploy_object (database_name,user_name,table_name,object_type,change_type,version_id,function_id)
select DB_NAME() as database_name,user_name() as user_name,
SUBSTRING(recover_script,CHARINDEX('from',recover_script)+5,len(recover_script)-CHARINDEX('from',recover_script)-5) as table_name,
'data' object_type,
'insert' change_type,
--cast(id as nvarchar(32))+'_'+cast(version_id as nvarchar(32)) 
version_id,
 '103' as function_id from dbo.t_deploy_script where TYPE='D';
 
 



select e.name,e.lengths,f.lengths
from (
select a.name,LEN(b.definition) lengths
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) e,
 (
select a.name,LEN(b.definition) lengths
 from [Smartlife-120399].sys.all_objects a,[Smartlife-120399].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) f
where  e.name=f.name and e.lengths<>f.lengths

select b.definition,a.name
from [smartlife-120300].sys.all_objects a,[smartlife-120300].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>4000;

FUNC_Tol_GetWB
FUNC_Tol_ParseJSON

insert into t_deploy_script (version_id,update_script,recover_script,update_validate_script,recover_validate_script,type)
select 3 version_id,' declare @str nvarchar(max)
select top 1 @str=b.definition
from [smartlife-120300].sys.all_objects a,[smartlife-120300].sys.all_sql_modules b 
where a.type in (''AF'',''TF'',''FN'') and a.object_id>0 and a.object_id=b.object_id 
and a.name='''+a.name+''' and  len(b.definition)>4000;
exec sp_executesql @str;'  update_script,'drop function '+a.name+';'  recover_script,
'select count(c.name)  from sys.all_objects c where c.name='''+a.name+'''' update_validate_script,
'select count(d.name)  from sys.all_objects d where d.name='''+a.name+'''' recover_validate_script,'F' type
from [smartlife-120300].sys.all_objects a,[smartlife-120300].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>4000;

 declare @str nvarchar(max)
 select top 1 @str=b.definition  from [smartlife-120300].sys.all_objects a,[smartlife-120300].sys.all_sql_modules b   where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id   and a.name='FUNC_Tol_GetWB' and  len(b.definition)>4000;  
 exec sp_executesql @str;
 
 
 insert into t_deploy_script (version_id,update_script,recover_script,update_validate_script,recover_validate_script,type)
select 3 version_id,b.definition  update_script,'drop function '+a.name  recover_script,
'select count(c.name)  from sys.all_objects c where c.name='''+a.name+'''' update_validate_script,
'select count(d.name)  from sys.all_objects d where d.name='''+a.name+'''' recover_validate_script,'F' type
 from [$(source_db_name)].sys.all_objects a,[$(source_db_name)].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)<4000;


select e.name,e.lengths,f.lengths,e.create_date,e.modify_date
from (
select a.name,LEN(b.definition) lengths,a.create_date,a.modify_date
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) e,
 (
select a.name,LEN(b.definition) lengths
 from [Smartlife-120300].sys.all_objects a,[Smartlife-120300].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) f
where  e.name=f.name and e.lengths<>f.lengths

--------------------================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter   PROCEDURE [dbo].[SP_dba_repl_function]
@source_db_name nvarchar(50)='Smartlife-120300'
 AS
 BEGIN
SET NOCOUNT ON
 
DECLARE  @id nvarchar(50)
declare @str nvarchar(max)
declare @str_drop nvarchar(max)

DECLARE id_cursor CURSOR FOR 
select e.name id
from (
select a.name,LEN(b.definition) lengths
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) e,
 (
select a.name,LEN(b.definition) lengths
 from [Smartlife-120300].sys.all_objects a,[Smartlife-120300].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) f
where  e.name=f.name and e.lengths<>f.lengths;
 
OPEN id_cursor
 
FETCH NEXT FROM id_cursor 
INTO  @id
 
WHILE @@FETCH_STATUS = 0
 BEGIN
    
 select top 1 @str=b.definition,@str_drop='drop function '+a.name+';'  from [Smartlife-120300].sys.all_objects a,[Smartlife-120300].sys.all_sql_modules b  
  where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id   and a.name=@id 
  --and  len(b.definition)>4000;  
  exec sp_executesql @str_drop;
  exec sp_executesql @str;
   --print @str
     FETCH NEXT FROM id_cursor 
    INTO  @id
 END 
CLOSE id_cursor
 DEALLOCATE id_cursor
 END
 
 exec [dbo].[SP_dba_repl_function] 'smartlife'
 