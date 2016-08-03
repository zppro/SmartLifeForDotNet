------------------检查函数的定义的文本长度
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
;

---------------------检查存储过程的定义的文本长度

select e.name,e.lengths,f.lengths
from (
select a.name,LEN(b.definition) lengths
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id) e,
 (
select a.name,LEN(b.definition) lengths
 from [Smartlife-120396].sys.all_objects a,[Smartlife-120396].sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id) f
where  e.name=f.name and e.lengths<>f.lengths
;

-----------------------源库有，并且目标库没有的存储过程
select e.name,e.lengths
from (
select a.name,LEN(b.definition) lengths
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id) e
where e.name not in (
select a.name
 from [Smartlife-120399].sys.all_objects a,[Smartlife-120399].sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id
);