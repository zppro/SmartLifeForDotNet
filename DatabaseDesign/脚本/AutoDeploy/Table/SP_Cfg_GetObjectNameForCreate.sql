USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Cfg_GetObjectNameForCreate]    Script Date: 05/13/2014 18:09:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/************************************************************************************/
/*  SP_Cfg_GetObjectNameForCreate                                           */
/*  源库有，目的库没有的 表及类型等。 create object        */
/*  @source_dbName 源数据库名，@dest_dbName 目的数据库名                            */
/************************************************************************************/
ALTER   PROCEDURE [dbo].[SP_Cfg_GetObjectNameForCreate]
@source_dbName nvarchar(100),
@dest_dbName   nvarchar(100),
@type varchar(10)
 AS
 BEGIN
DECLARE  @sql_str nvarchar(4000) 
     -- SET NOCOUNT ON
     
     if @type='table'
     begin
        IF (EXISTS (select * from  Cfg_ObjectNameForCreate where Type='table' ))
             BEGIN
                       
              delete from  Cfg_ObjectNameForCreate where Type='table';
             END
        
         IF (NOT EXISTS (select * from  Cfg_ObjectNameForCreate where Type='table'))
             BEGIN
             if @dest_dbName<>''
             begin
                set @sql_str='
				 insert into Cfg_ObjectNameForCreate (databasename,username,objectname,type)
				 select DB_NAME() as DatabaseName,user_name() as UserName,name,''table'' type  from '+@source_dbName+'.sys.tables 
				 where name not in (select name from '+@dest_dbName+'.sys.tables ) 
				 and is_ms_shipped=0 and name not in(select TableName from Cfg_Object where ObjectType=''table'' and ChangeType=''not'');
				 '
              end
              
              if  @dest_dbName=''
             begin
                  set @sql_str='
				 insert into Cfg_ObjectNameForCreate (databasename,username,objectname,type)
				 select DB_NAME() as DatabaseName,user_name() as UserName,name,''table'' type  from '+@source_dbName+'.sys.tables 
				 where  is_ms_shipped=0 and name not in(select TableName from Cfg_Object where ObjectType=''table'' and ChangeType=''not'');
				 ' 
             end
                 exec sp_executesql @sql_str 
             END
     end
     
       if @type='type'
       begin
        IF (EXISTS (select * from  Cfg_ObjectNameForCreate where Type='type' ))
             BEGIN
                       
              delete from  Cfg_ObjectNameForCreate where Type='type';
             END
        
         IF (NOT EXISTS (select * from  Cfg_ObjectNameForCreate where Type='type'))
             BEGIN
             
              if @dest_dbName<>''
             begin
                set @sql_str='
 insert into Cfg_ObjectNameForCreate (databasename,username,objectname,type)
 select DB_NAME() as DatabaseName,user_name() as UserName,name,''type'' type  from '+@source_dbName+'.sys.table_types 
 where name not in (select name from '+@dest_dbName+'.sys.table_types ) 
  and name not in(select TableName from Cfg_Object where ObjectType=''type'' and ChangeType=''not'');
 '
              end
              
                 if  @dest_dbName=''
             begin
                 set @sql_str='
				 insert into Cfg_ObjectNameForCreate (databasename,username,objectname,type)
				 select DB_NAME() as DatabaseName,user_name() as UserName,name,''type'' type  from '+@source_dbName+'.sys.table_types 
				 where  name not in(select TableName from Cfg_Object where ObjectType=''type'' and ChangeType=''not'');
				 '
             end
                
                 exec sp_executesql @sql_str 
             END
     end       
 END
GO

