USE  [master];
GO
declare @@date  nvarchar(50)
declare @@path  nvarchar(50)
declare @@sql   nvarchar(4000)
--declare @@databasename nvarchar(35)
DECLARE  @databasename nvarchar(50)

select @@path='e:\sqlserverbackup\115.236.175.110'
select  @@date=left(CONVERT(VARCHAR(24),GETDATE(),120),10)+DATENAME(hh,getdate())+datename(MI,GETDATE())


 
DECLARE databasename_cursor CURSOR FOR 
select name databasename from sys.databases where database_id>7 or database_id=1;
 
OPEN databasename_cursor
 
FETCH NEXT FROM databasename_cursor 
INTO  @databasename

WHILE @@FETCH_STATUS = 0
 BEGIN
set @@sql='BACKUP DATABASE ['+@databasename+'] TO DISK ='''+@@path+'\'+@databasename+'_'+@@date+'.bck'' WITH INIT ,compression;'
--select @@sql
--BACKUP DATABASE smartlife TO DISK ='@@path\smartlilfe_@@date.bck' WITH INIT ,compression;
exec sp_executesql @@sql

  FETCH NEXT FROM databasename_cursor 
    INTO  @databasename
 END
CLOSE databasename_cursor
 DEALLOCATE databasename_cursor
go

--select name from sys.databases where database_databasename>7 or database_databasename=1

