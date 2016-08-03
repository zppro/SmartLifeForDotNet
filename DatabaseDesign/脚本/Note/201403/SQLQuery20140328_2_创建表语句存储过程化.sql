select columname 
from temp_callservice 
where tablename='Oca_CC_Ext'

go
/**************************************************************/
/* dbo.Func_Tol_ContactColumn                                 */
/* 各个字段定义聚合成一个字符串                               */
/* 参数 字段所属的表 @tablename, 字段定义存放的表 @fromtable  */
/**************************************************************/
alter  function [dbo].[Func_Tol_ContactColumn](@TableName nvarchar(50))
returns varchar(4000) 
as

begin
declare @sql nvarchar(2000) ,  @ProcessTrackingTableName varchar(4000)
        set @ProcessTrackingTableName=''
        select @ProcessTrackingTableName =@ProcessTrackingTableName+columname 
                   from temp_callservice
                   where tablename=@TableName;
         --select @sql
         --exec sp_executesql @sql, N'@ProcessTrackingTableName varchar(4000)', @ProcessTrackingTableName out 

         return @ProcessTrackingTableName;
 end    
 go
 
 declare @sql nvarchar(2000) 
declare @cou int 
declare @id varchar(20) 
set @id='1' 
set @sql='select @count=count(*) from emp where id=@id' 
exec sp_executesql @sql, N'@count int out,@id varchar(20)', @cou out ,@id 
print @cou    

print dbo.Func_Tol_ContactColumn('Oca_CC_Ext')


USE [Leblue-Configuration]
GO
/****** Object:  StoredProcedure [dbo].[SP_Dey_CreateTableScript]    Script Date: 03/27/2014 09:08:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************************************/
/*  SP_Dey_CreateTableScriptProc */
/*   */
/**************************************************************************************/
alter   PROCEDURE [dbo].[SP_Dey_CreateTableScriptProc]
@sync_type  char(1)='2'
 AS
 BEGIN
SET NOCOUNT ON
 
PRINT 'sync_type:'+@sync_type
DECLARE  @table_name nvarchar(50),@ProcessTrackingTableName varchar(4000),@primarykey varchar(4000)
 
PRINT '-------- make create table script --------'
set @ProcessTrackingTableName=''
set @primarykey=''
 exec SP_Cfg_GetObjectNameForCreate @source_dbName='[smartlife-120300]',@dest_dbName='[smartlife-120301]'


DECLARE create_table_cursor CURSOR FOR 
select ObjectName tablename from Cfg_ObjectNameForCreate;
--select name from remote_dbo.[smartlife-120300].sys.tables where name not in (select name from remote_dbo.[smartlife-120395].sys.tables ) and is_ms_shipped=0 and name not in(select TableName from Cfg_Object where ObjectType='table' and ChangeType='not');
---select name from $(source_db_name).sys.tables order by name desc ;
 
OPEN create_table_cursor
 
FETCH NEXT FROM create_table_cursor 
INTO  @table_name
 
WHILE @@FETCH_STATUS = 0
     BEGIN
           exec SP_DBA_GetTableColumnDetailForCreate @databasename='[Smartlife-120303]' ,@tablename=@table_name
           exec   SP_DBA_MergeColumnDefinition @tablename=@table_name
           -- set @ProcessTrackingTableName=dbo.Func_Tol_ContactColumn(@table_name)
            
              select @ProcessTrackingTableName =@ProcessTrackingTableName+columname 
                   from temp_callservice
                   where tablename=@table_name;
            
            exec SP_Cfg_GetObjectPrimaryKey @source_dbName='[Smartlife-120300]'
            
             select @primarykey='CONSTRAINT  PK_'+@table_name+'  PRIMARY KEY CLUSTERED  ('+columnname+'),'
               from Cfg_ObjectPrimaryKey
               where ObjectName=@table_name
               
                 set @ProcessTrackingTableName =@ProcessTrackingTableName+@primarykey

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
 
 
 exec SP_Cfg_GetObjectPrimaryKey @source_dbName='[Smartlife-120300]'
 
 select @primarykey='CONSTRAINT  PK_   Cer_ObjectToken  PRIMARY KEY CLUSTERED  ('+columnname+'),'
  from Cfg_ObjectPrimaryKey
 where ObjectName='Cer_ObjectToken'
 
  set @primarykey='CONSTRAINT  PK_'+@table_name+'  PRIMARY KEY CLUSTERED  ('+left(@primarykey,LEN(@primarykey)-1)+'),'
 go
 ----------------------
/************************************************************************************/
/*  SP_Cfg_GetObjectNameForCreate                                           */
/*  源库有，目的库没有的 表及类型等。 create object        */
/*  @source_dbName 源数据库名，@dest_dbName 目的数据库名                            */
/************************************************************************************/
create   PROCEDURE [dbo].[SP_Cfg_GetObjectNameForCreate]
@source_dbName nvarchar(100),
@dest_dbName   nvarchar(100)
 AS
 BEGIN
DECLARE  @sql_str nvarchar(4000) 
      SET NOCOUNT ON
     
     
        IF (EXISTS (select * from  Cfg_ObjectNameForCreate where Type='table' ))
             BEGIN
                       
              delete from  Cfg_ObjectNameForCreate where Type='table';
             END
        
         IF (NOT EXISTS (select * from  Cfg_ObjectNameForCreate where Type='table'))
             BEGIN
                set @sql_str='
 insert into Cfg_ObjectNameForCreate (databasename,username,objectname,type)
 select DB_NAME() as DatabaseName,user_name() as UserName,name,''table'' type  from '+@source_dbName+'.sys.tables 
 where name not in (select name from '+@dest_dbName+'.sys.tables ) 
 and is_ms_shipped=0 and name not in(select TableName from Cfg_Object where ObjectType=''table'' and ChangeType=''not'');
 '
                 exec sp_executesql @sql_str 
             END
 END
 go
CREATE TABLE [dbo].[Cfg_ObjectNameForCreate](
	[DatabaseName] [nvarchar](60) NOT NULL,
	[UserName] [nvarchar](30) NOT NULL,
	[ObjectName] [varchar](60) NOT NULL,
	
	[BeforeModifyDate] [datetime] NULL,
	[AfterModifyDate] [datetime] NULL,
	[Type] varchar(40) NOT NULL
)

exec SP_Cfg_GetObjectNameForCreate @source_dbName='[smartlife-120300]',@dest_dbName='[smartlife-120301]'

select ObjectName tablename from Cfg_ObjectNameForCreate;