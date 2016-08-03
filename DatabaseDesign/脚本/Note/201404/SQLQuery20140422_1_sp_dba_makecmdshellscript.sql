
    exec sp_dba_makecmdshellscript    
alter procedure sp_dba_makecmdshellscript
as 
begin
DECLARE  @databasename nvarchar(50),@sql nvarchar(max)
DECLARE databasename_cursor CURSOR FOR 
select name databasename from sys.databases where name like 'Smartlife%' and name <>'SmartLife-120701' order by name;
OPEN databasename_cursor
 
FETCH NEXT FROM databasename_cursor 
INTO  @databasename

WHILE @@FETCH_STATUS = 0
 BEGIN
 --use @databasename
--print @databasename
set @sql=' 
use ['+@databasename+']
--增加字段[对象编号]ObjectId
IF not EXISTS(SELECT * FROM  DBO.SysColumns WHERE ID = Object_ID(''Cer_Redirect'') AND Name=''ObjectId'')
BEGIN
      ALTER TABLE Cer_Redirect ADD ObjectId varchar(10)  NOT NULL default  '''+right(@databasename,6)+'''
      EXEC sp_addextendedproperty N''MS_Description'',N''对象编号'',N''user'', N''dbo'', N''table'', N''Cer_Redirect'', N''column'', N''ObjectId''

End
GO'
--set @sql=' exec master..xp_cmdshell ''osql -U sa -P 123456 -d  ['+@databasename+']  -i e:\wangwei\20140422_execute.sql'' '
print @sql
--print 'execute file.sql'
--exec sp_executesql @sql
--select DB_NAME()
  FETCH NEXT FROM databasename_cursor 
    INTO  @databasename
 END
CLOSE databasename_cursor
 DEALLOCATE databasename_cursor


end
go

