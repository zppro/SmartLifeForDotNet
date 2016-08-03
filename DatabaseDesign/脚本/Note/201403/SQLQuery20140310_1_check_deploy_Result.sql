select objectname,type from dey_script where type='T'
--order by id 
where type='L' where ObjectName='sysdiagrams';

select TableName,ColumnName,systemtypeiddest,MaxLengthDest,ISNULLdest 
from Cfg_ObjectColumnDetail where tablename='oca_callservice'    ChangeType='alter'
select * from Cfg_TableColumnComment where DatabaseName='smartlife-120301';
select tablename from  dbo.Cfg_TableColumnComment where DatabaseName='smartlife-120301'@dbname
select * from Cfg_ObjectVersionHistory where TYPE='P' and BeforeLength<>AfterLength;
select * from cfg_object where ObjectType='column' and ChangeType='add'; 

execute [dbo].[SP_Cfg_GetTableColumnComment]            @source_dbName='[smartlife-120301]'
exec SP_Dey_AddTableColumnCommentScript @source_dbName='remote_dbo.smartlife-120300',@dest_dbName='smartlife-120305'

                        insert into Cfg_TableColumnComment (DatabaseName,UserName,TableName,ColumnName,
                                                           BeforeComment,BeforeLength,BeforeModifyDate,Type) 
                                                                                 
            select 'smartlife-120305' as DatabaseName,user_name() as UserName,                          
              (select name from [smartlife-120305].sys.tables where object_id=a.major_id) tablename,  
             (select name from [smartlife-120305].sys.columns where object_id=a.major_id and column_id=a.minor_id) columnname,   
                                       cast(value as nvarchar(max))    BeforeComment,          
                                                          LEN(cast(value as nvarchar(3200))) BeforeLength,  
                                                                                     GETDATE() BeforModifyDate,   
                                                                                                               a.minor_id Type                             
                         from [smartlife-120305].sys.extended_properties a where a.name='MS_Description'; 
                         
insert into dbo.Cfg_Object (DatabaseName,UserName,TableName,ObjectType,ChangeType,VersionId,FunctionId)
select  DB_NAME() as DatabaseName,user_name() as UserName,
--SUBSTRING(UpdateScript,CHARINDEX(']',UpdateScript)+2,(case CHARINDEX('ADD ',UpdateScript) when 0 then CHARINDEX('(',UpdateScript) else CHARINDEX('ADD ',UpdateScript) ---end)-CHARINDEX(']',UpdateScript)-2) as TableName,
ObjectName TableName,
'table' ObjectType,
SUBSTRING(UpdateScript,0,CHARINDEX('table',UpdateScript)) as ChangeType,
--cast(Id as nvarchar(32))+'_'+cast(VersionId as nvarchar(32)) 
VersionId,
 '3' as FunctionId from dbo.Dey_Script where Type='T'

select  Name,xusertype from sys.sysTypes where xuserType=a.SystemTypeIdDest

varbinary

select * from   cfg_objectversionhistory where type='P' and BeforeLength=AfterLength
------------调用过程如下
exec SP_Cfg_GetFunctionDefinition @source_dbName='[smartlife-120300]',@dest_dbName='[smartlife-120301]'
exec SP_Cfg_GetProcedureDefinition @source_dbName='remote_dbo.[smartlife-120300]',@dest_dbName='[smartlife-120301]'
exec SP_Dey_CreateFunctionMetaScript  @source_dbName='[smartlife-120300]'
exec SP_Dey_CreateProcedureMetaScript  @source_dbName='remote_dbo.[smartlife-120300]'


execute [dbo].[SP_Cfg_GetTableColumnComment]            @source_dbName='[smartlife-120300]'
--------------end 


select m.name,m.definition ,m.length,m.modify_date ,n.definition,n.length,n.modify_date 
from (
  select a.name,b.definition,LEN(b.definition) length,a.modify_date       
   from [smartlife-120300].sys.all_objects a,[smartlife-120300].sys.all_sql_modules b 
where a.Type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>40) m
left join (
 select a.name,b.definition,LEN(b.definition) length,a.modify_date       
   from [smartlife-120301].sys.all_objects a,[smartlife-120301].sys.all_sql_modules b 
where a.Type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>40) n
on m.name=n.name;

    insert into cfg_objectversionhistory(DatabaseName,UserName,ObjectName,                      
     BeforeDefinition,BeforeLength,BeforeModifyDate,AfterDefinition,AfterLength,AfterModifyDate,Type)        
               select DB_NAME() as DatabaseName,user_name() as UserName,m.name OjbectName,           
                      m.definition  BeforeDefinition,m.length BeforeLength,m.modify_date BeforeModifyDate,     
                                   isnull(n.definition,-1)  AfterDefinition ,isnull(n.length,-1) AfterLength ,isnull(n.modify_date,GETDATE()) AfterModifyDate ,'F' type                 
                                    from                      
                                    (select a.name,b.definition,LEN(b.definition) length,a.modify_date                 
                                     from [smartlife-120300].sys.all_objects a,[smartlife-120300].sys.all_sql_modules b                   
                                     where a.Type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>40)m  
                                                     left join                 
                                                      (select a.name,b.definition,LEN(b.definition) length,a.modify_date           
                                                             from [smartlife-120301].sys.all_objects a,[smartlife-120301].sys.all_sql_modules b      
                                                 where a.Type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>40) n                 
    on m.name=n.name     
    
 PRINT 'source_db_name $(source_db_name) ,sync_type $(sync_type)'
PRINT 'Creating procedure  SP_Dey_CreateFunctionMetaScript ...'

IF (EXISTS (SELECT *
            FROM dbo.sysobjects
            WHERE (name = N' SP_Dey_CreateFunctionMetaScript')
              AND (type = 'P')))
  DROP PROCEDURE  SP_Dey_CreateFunctionMetaScript
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************/
/* SP_Dey_CreateFunctionMetaScript                                    */
/* 生成创建函数的元数据的脚本                                 */
/*                                                            */
/**************************************************************/


CREATE    PROCEDURE [dbo].[SP_Dey_CreateFunctionMetaScript]
@source_dbName nvarchar(100)
 AS
 BEGIN
      SET NOCOUNT ON
      DECLARE  @sql_str nvarchar(4000) 
        IF (EXISTS (select * from Dey_Script where Type='F'))
             BEGIN
               delete from Dey_Script where Type='F';        
             END
        IF (NOT EXISTS (select * from Dey_Script where Type='F'))
             BEGIN
           
                insert into Dey_Script (VersionId,objectName,UpdateScript,RecoverScript,UpdateValIdateScript,RecoverValIdateScript,Type)
               select 3 VersionId,
                      a.ObjectName ,
                     ' declare @str nvarchar(max)
                           IF (EXISTS (SELECT *
                                       FROM dbo.sysobjects
                                        WHERE (Name = N'''+a.ObjectName+''')
                                       AND (Type = ''F'')))
                            begin
                                     drop procedure '+a.ObjectName+';
                            end
                          select top 1 @str=b.definition
                          from '+@source_dbName+'.sys.all_objects a,'+@source_dbName+'.sys.all_sql_modules b 
                          where a.Type in (''F'') and a.object_id>0 and a.object_id=b.object_id 
                          and a.ObjectName='''+a.ObjectName+''' and  len(b.definition)>40;
                          print ''create procedure '+a.ObjectName+'.... ''
                          exec sp_executesql @str;
                           go
	                 '  UpdateScript,
                         'drop procedure '+a.ObjectName+';'  RecoverScript,
                         'select count(c.Name)  from sys.all_objects c where c.Name='''+a.ObjectName+'''' UpdateValIdateScript,
                         'select count(d.Name)  from sys.all_objects d where d.Name='''+a.ObjectName+'''' RecoverValIdateScript,
	                 'F' Type
                from   cfg_objectversionhistory a
                 where a.Type in ('F');	       
             END
 END
GO


PRINT 'source_db_name $(source_db_name) ,sync_type $(sync_type)'
PRINT 'Creating procedure  SP_Dey_CreateProcedureMetaScript ...'

IF (EXISTS (SELECT *
            FROM dbo.sysobjects
            WHERE (name = N' SP_Dey_CreateProcedureMetaScript')
              AND (type = 'P')))
  DROP PROCEDURE  SP_Dey_CreateProcedureMetaScript
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************/
/* SP_Dey_CreateProcedureMetaScript                                    */
/* 生成创建函数的元数据的脚本                                 */
/*                                                            */
/**************************************************************/


CREATE    PROCEDURE [dbo].[SP_Dey_CreateProcedureMetaScript]
@source_dbName nvarchar(100)
 AS
 BEGIN
      SET NOCOUNT ON
      DECLARE  @sql_str nvarchar(4000) 
        IF (EXISTS (select * from Dey_Script where Type='P'))
             BEGIN
               delete from Dey_Script where Type='P';        
             END
        IF (NOT EXISTS (select * from Dey_Script where Type='P'))
             BEGIN
           
                insert into Dey_Script (VersionId,objectName,UpdateScript,RecoverScript,UpdateValIdateScript,RecoverValIdateScript,Type)
               select 3 VersionId,
                      a.ObjectName ,
                     ' declare @str nvarchar(max)
                           IF (EXISTS (SELECT *
                                       FROM dbo.sysobjects
                                        WHERE (Name = N'''+a.ObjectName+''')
                                       AND (Type = ''P'')))
                            begin
                                     drop procedure '+a.ObjectName+';
                            end
                          select top 1 @str=b.definition
                          from '+@source_dbName+'.sys.all_objects a,'+@source_dbName+'.sys.all_sql_modules b 
                          where a.Type in (''P'') and a.object_id>0 and a.object_id=b.object_id 
                          and a.ObjectName='''+a.ObjectName+''' and  len(b.definition)>40;
                          print ''create procedure '+a.ObjectName+'.... ''
                          exec sp_executesql @str;
                           go
	                 '  UpdateScript,
                         'drop procedure '+a.ObjectName+';'  RecoverScript,
                         'select count(c.Name)  from sys.all_objects c where c.Name='''+a.ObjectName+'''' UpdateValIdateScript,
                         'select count(d.Name)  from sys.all_objects d where d.Name='''+a.ObjectName+'''' RecoverValIdateScript,
	                 'P' Type
                from   cfg_objectversionhistory a
                 where a.Type in ('P');	       
             END
 END
GO

------------------
 insert into Dey_Script (VersionId,objectName,UpdateScript,RecoverScript,UpdateValIdateScript,RecoverValIdateScript,Type)
  select 3 VersionId,
                        a.TableName objectName,
                       'EXEC sys.sp_addextendedproperty @name=''MS_Description'', @value='''+a.BeforeComment+''' , 
                       @level0type=''SCHEMA'',@level0name='''+a.UserName+''', 
                       @level1type=''TABLE'',@level1name='''+a.TableName+''', 
                       @level2type=''COLUMN'',@level2name='''+a.ColumnName +'''
                      GO'  UpdateScript,
		      '' RecoverScript,
                      'select 1' UpdateValIdateScript,
                      'select 1' RecoverValIdateScript,
                      'M' Type
                 from Cfg_TableColumnComment  a
		 where  a.Type=0 and a.databasename='remote_dbo.smartlife-120300'
                      and a.tablename+a.columnname+a.beforecomment not in(
                      select ab.tablename+ab.columnname+ab.beforecomment  tcb
                      from cfg_tablecolumncomment ab ,cfg_tablecolumncomment bb
                       where ab.databasename='remote_dbo.smartlife-120300' and bb.databasename='remote_dbo.smartlife-120304'
                      and ab.tablename=bb.tablename and ab.columnname=bb.columnname and ab.beforecomment=bb.beforecomment
                     );