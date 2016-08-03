USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Dey_CreateTableScriptProc]    Script Date: 05/13/2014 18:15:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************************************/
/*  SP_Dey_CreateTableScriptProc */
/*  生成创建表的脚本 */
/*  @source_dbName 源数据库名，@dest_dbName 目的数据库名      */
/**************************************************************************************/
CREATE   PROCEDURE [dbo].[SP_Dey_CreateTableScriptProc]
@source_dbName nvarchar(100),
@dest_dbName   nvarchar(100)
 AS
 BEGIN
SET NOCOUNT ON
 
--PRINT 'sync_type:'+@sync_type
DECLARE  @table_name nvarchar(50),@ProcessTrackingTableName varchar(4000),@primarykey varchar(4000),@sql nvarchar(4000)
 
PRINT '-------- make create table script --------'
set @ProcessTrackingTableName=''
set @primarykey=''
 exec SP_Cfg_GetObjectNameForCreate @source_dbName=@source_dbName,@dest_dbName=@dest_dbName,@type='table'

   IF (EXISTS (select * from  Dey_Script  where TYPE='T' ))
             BEGIN
              delete from  Dey_Script  where TYPE='T';
             END

DECLARE create_table_cursor CURSOR FOR 
select ObjectName tablename from Cfg_ObjectNameForCreate where TYPE='table';
--select name from remote_dbo.[smartlife-120300].sys.tables where name not in (select name from remote_dbo.[smartlife-120395].sys.tables ) and is_ms_shipped=0 and name not in(select TableName from Cfg_Object where ObjectType='table' and ChangeType='not');
---select name from $(source_db_name).sys.tables order by name desc ;
 
OPEN create_table_cursor
 
FETCH NEXT FROM create_table_cursor 
INTO  @table_name
 
WHILE @@FETCH_STATUS = 0
     BEGIN
           exec SP_DBA_GetTableColumnDetailForCreate @databasename=@source_dbName ,@tablename=@table_name
           exec   SP_DBA_MergeColumnDefinition @tablename=@table_name
            set @ProcessTrackingTableName=dbo.Func_Tol_ContactColumn(@table_name)
            
            /*  select @ProcessTrackingTableName =@ProcessTrackingTableName+columname 
                   from temp_callservice
                   where tablename=@table_name;*/
            
            exec SP_Cfg_GetObjectPrimaryKey @source_dbName=@source_dbName
            
             /*select @primarykey='CONSTRAINT  PK_'+@table_name+'  PRIMARY KEY CLUSTERED  ('+columnname+'),'
               from Cfg_ObjectPrimaryKey
               where ObjectName=@table_name*/
               
               set @primarykey=''
               set @sql='select @count=''CONSTRAINT  PK_'+@table_name+'  PRIMARY KEY CLUSTERED  (''+columnname+''),'' from Cfg_ObjectPrimaryKey
               where ObjectName='''+@table_name +''''
                 print @sql
               exec sp_executesql @sql, N'@count varchar(4000) out', @primarykey out 
               --select @count=columnname from Cfg_ObjectPrimaryKey where ObjectName='sysdiagrams'
              --select @primarykey 
               
                 set @ProcessTrackingTableName =@ProcessTrackingTableName+isnull(@primarykey,'')

         IF (NOT EXISTS (select * from Cfg_Object where ChangeType='create' and TableName=@table_name))
                insert into Dey_Script (VersionId,ObjectName,UpdateScript,RecoverScript,UpdateValidateScript,RecoverValidateScript)
               select 3,@table_name  object_name,'
              IF (OBJECT_ID(N''dbo.'+@table_name+''', ''U'') IS NULL)
              BEGIN
                   PRINT ''''
                   PRINT ''Creating table '+@table_name+' ...''
                   create table [dbo].'+@table_name+' ('+left(@ProcessTrackingTableName,LEN(@ProcessTrackingTableName)-1)+');
               END   
              go',
              'drop table [dbo].'+@table_name,
            'select COUNT(object_id) from sys.tables where name='''+@table_name+''';  
           go','select COUNT(object_id) from sys.tables where name='''+@table_name+''';  
          go'
 
     FETCH NEXT FROM create_table_cursor 
    INTO  @table_name
 END 
CLOSE create_table_cursor
 DEALLOCATE create_table_cursor
 END
GO

