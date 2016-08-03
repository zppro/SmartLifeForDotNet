USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Cfg_GetViewDefinition]    Script Date: 05/13/2014 09:35:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************/
/* SP_Cfg_GetViewDefinition                                   */
/* 取视图的定义的信息，存入配置库                             */
/* @source_dbName 源数据库名，@dest_dbName 目的数据库名       */
/**************************************************************/
CREATE PROCEDURE [dbo].[SP_Cfg_GetViewDefinition]
@source_dbName nvarchar(100),
@dest_dbName nvarchar(100)
 AS
 BEGIN
 DECLARE  @sql_str nvarchar(4000) ,@databasename nvarchar(100)
     -- SET NOCOUNT ON
      select @databasename=replace(REPLACE(@source_dbName,'[',''''),']','''')
     
        IF (EXISTS (select * from cfg_objectversionhistory where type='V' ))
             BEGIN
                       
              delete from   cfg_objectversionhistory where type='V';
             END
        
         IF (NOT EXISTS (select * from   cfg_objectversionhistory where type='V'))
             BEGIN
               if @dest_dbName <>''
               begin
                set @sql_str='
		       insert into cfg_objectversionhistory(DatabaseName,UserName,ObjectName,
                     BeforeDefinition,BeforeLength,BeforeModifyDate,AfterDefinition,AfterLength,AfterModifyDate,Type)
                select DB_NAME() as DatabaseName,user_name() as UserName,m.name OjbectName,
                m.definition  BeforeDefinition,m.length BeforeLength,m.modify_date BeforeModifyDate,
                 isnull(n.definition,-1)  AfterDefinition ,isnull(n.length,-1) AfterLength ,isnull(n.modify_date,GETDATE()) AfterModifyDate  ,''V'' type
                from     
                (select a.name,b.definition,LEN(b.definition) length,a.modify_date
                from '+@source_dbName+'.sys.all_objects a,'+@source_dbName+'.sys.all_sql_modules b 
                where a.Type in (''V'') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>40)m
                left join 
                (select a.name,b.definition,LEN(b.definition) length,a.modify_date
                from '+@dest_dbName+'.sys.all_objects a,'+@dest_dbName+'.sys.all_sql_modules b 
                where a.Type in (''V'') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>40) n
                on m.name=n.name
                ;'     
		       end
		     
		      if @dest_dbName =''
               begin
                  set @sql_str='
		        insert into cfg_objectversionhistory(DatabaseName,UserName,ObjectName,
                     BeforeDefinition,BeforeLength,BeforeModifyDate,Type)
                select DB_NAME() as DatabaseName,user_name() as UserName,m.name OjbectName,
                m.definition  BeforeDefinition,m.length BeforeLength,m.modify_date BeforeModifyDate,
               ''V'' type
                from     
                (select a.name,b.definition,LEN(b.definition) length,a.modify_date
                from '+@source_dbName+'.sys.all_objects a,'+@source_dbName+'.sys.all_sql_modules b 
                where a.Type in (''V'') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>40)m 
                ;' 
                end
		   --select @sql_str
                exec sp_executesql @sql_str 
             END
 END
GO

