select b.definition,LEN(b.definition)  
from [smartlife-120300].sys.all_objects a,[smartlife-120300].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>4000;

----*******************
 declare @str nvarchar(max)
DECLARE id_cursor CURSOR FOR 
select b.definition str
from [smartlife-120300].sys.all_objects a,[smartlife-120300].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>4000;
 
OPEN id_cursor
 
FETCH NEXT FROM id_cursor 
INTO  @str
 
WHILE @@FETCH_STATUS = 0
 BEGIN
 
exec sp_executesql @str

    FETCH NEXT FROM id_cursor 
    INTO  @str
 END 
CLOSE id_cursor
 DEALLOCATE id_cursor
------***************************** 
 
 select * from dbo.vSystem_Info_Columns where table_name ='oca_serviceworkorder'
 
 select * from oca_serviceworkorder;