select * from dbo.IPCC_CallRecord_201401

select * from dbo.callrecord;
select * from sys.extended_properties;

select name from remote_dbo.[smartlife-120300].sys.tables where name not in (select name from sys.tables )

select  DB_NAME() as database_name,user_name() as user_name,
e.table_name,e.column_name,e.system_type_id,e.max_length,
e.is_nullable,'column' object_type,'add' change_type,3 version_id,2 function_id
from (
select b.name  table_name,
a.name column_name,a.system_type_id,a.max_length,a.is_nullable from remote_dbo.[smartlife-120300].sys.columns a 
right join remote_dbo.[smartlife-120300].sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) e 
where e.table_name+e.column_name not in
(
select b.name+a.name column_name
--,a.system_type_id,a.max_length,a.is_nullable 
from sys.columns a right join sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) 

-----------------------
select  DB_NAME() as database_name,user_name() as user_name,
m.table_name,m.column_name,m.system_type_id,m.max_length,
m.is_nullable,n.system_type_id system_type_id_old,n.max_length max_length_old,n.is_nullable is_null_old,
'column' object_type,'alter' change_type,3 version_id,2 function_id
from (
select b.name  table_name,
a.name column_name,a.system_type_id,a.max_length,a.is_nullable from remote_dbo.[smartlife-120300].sys.columns a 
right join remote_dbo.[smartlife-120300].sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) m,(
select b.name+a.name column_name
,a.system_type_id,a.max_length,a.is_nullable 
from sys.columns a right join sys.tables b on b.object_id=a.object_id
and b.object_id>0 )  n
where m.table_name+m.column_name =n.column_name
and m.table_name+m.column_name not in (
select e.table_name+e.column_name pri_key
from (
select b.name  table_name,
a.name column_name,a.system_type_id,a.max_length,a.is_nullable 
from remote_dbo.[smartlife-120300].sys.columns a right join remote_dbo.[smartlife-120300].sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) e inner join
(
select b.name  table_name,
a.name column_name,a.system_type_id,a.max_length,a.is_nullable from sys.columns a right join sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) f on
 e.table_name=f.table_name and e.column_name=f.column_name and e.system_type_id=f.system_type_id and e.max_length=f.max_length
 and e.is_nullable=f.is_nullable
);

--------------------------------------------------

select e.name,e.lengths,f.lengths
from (
select a.name,LEN(b.definition) lengths
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) e,
 (
select a.name,LEN(b.definition) lengths
 from remote_dbo.[smartlife-120300].sys.all_objects a,remote_dbo.[smartlife-120300].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) f
where  e.name=f.name and e.lengths<>f.lengths
;

---------------------检查存储过程的定义的文本长度

select e.name,e.lengths,f.lengths
from (
select a.name,LEN(b.definition) lengths
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id) e,
 (
select a.name,LEN(b.definition) lengths
 from remote_dbo.[smartlife-120300].sys.all_objects a,remote_dbo.[smartlife-120300].sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id) f
where  e.name=f.name and e.lengths<>f.lengths

-----------------------源库有，并且目标库没有的存储过程
select e.name,e.lengths
from (
select a.name,LEN(b.definition) lengths
 from remote_dbo.[smartlife-120300].sys.all_objects a,remote_dbo.[smartlife-120300].sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id) e
where e.name not in (
select a.name
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id
);

