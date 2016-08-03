use [SmartLife-120300]
select * from vSystem_Info_Tables;
select * from vSystem_Info_Columns;
set nocount on;
go

DBCC USEROPTIONS
GO

--------数据库的一致性检查，频率一周一次。
use smartlife
go
dbcc CHECKDB
go

select 'drop table '+name+';' from sys.tables order by name desc ;

查询，升级前的数据库中 存在的数据表 并且，在升级后的数据库中，不存在的表 。
生成删除数据表的语句如下
select 'drop table '+name+';' from sys.tables where name not in (select name from [SmartLife-120300].sys.tables);

-----------------------------
select ordinal_position,table_schema+table_name as tablename,column_name+' '+data_type+
 case DATA_TYPE when 'varchar' then ' ('+CAST (character_octet_length as nvarchar(32))+')'
when 'char' then '('+CAST (character_octet_length as nvarchar(32))+')' else '' end +
' ' +case is_nullable when 'NO' then ' not null' when 'YES' then ' null' else '' end +',' as coluname
from INFORMATION_SCHEMA.COLUMNS  where table_name = 'Pub_ReminderObject'

-------------------------------------------------------
SET NOCOUNT ON
 
DECLARE  @table_name nvarchar(50),@ProcessTrackingTableName varchar(4000),
 
@message varchar(80), @product nvarchar(50)
 
PRINT '-------- Vendor Products Report --------'
set @ProcessTrackingTableName=''
 
DECLARE vendor_cursor CURSOR FOR 
select name from sys.tables order by name desc ;
 
OPEN vendor_cursor
 
FETCH NEXT FROM vendor_cursor 
INTO  @table_name
 
WHILE @@FETCH_STATUS = 0
 BEGIN
     PRINT ' '
     SELECT @message = '----- ' +  @table_name
 
    PRINT @message
 
    -- Declare an inner cursor based   
    -- on vendor_id from the outer cursor.
set @ProcessTrackingTableName=''
 select @ProcessTrackingTableName =@ProcessTrackingTableName+ a.columname+'' 
 from 
 (select ordinal_position,table_schema+'.'+table_name as tablename,column_name+' '+data_type+
 case DATA_TYPE when 'varchar' then ' ('+CAST (character_octet_length as nvarchar(32))+')'
when 'char' then '('+CAST (character_octet_length as nvarchar(32))+')' else '' end +
' ' +case is_nullable when 'NO' then ' not null' when 'YES' then ' null' else '' end +',' as columname
from INFORMATION_SCHEMA.COLUMNS  where table_name = @table_name) a
  select @ProcessTrackingTableName,@table_name
         -- Get the next vendor.
     FETCH NEXT FROM vendor_cursor 
    INTO  @table_name
 END 
CLOSE vendor_cursor
 DEALLOCATE vendor_cursor
 

 
