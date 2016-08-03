USE [Leblue-Configuration]
GO
/****** Object:  StoredProcedure [dbo].[SP_Cfg_GetColumnDetailForCreateTable  ]    Script Date: 03/25/2014 16:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************/
/* SP_Cfg_GetColumnDetailForCreateTable                       */
/*  在源库，不在目的库的表的字段的详细信息  create table      */
/*  @source_dbName 源数据库名，@dest_dbName 目的数据库名      */
/**************************************************************/
create   PROCEDURE [dbo].[SP_Cfg_GetColumnDetailForCreateTable]
@source_dbName nvarchar(100),
@dest_dbName   nvarchar(100)
 AS
 BEGIN
DECLARE  @sql_str nvarchar(max) 
      SET NOCOUNT ON
     
        IF (EXISTS (select * from Cfg_TableColumnDetailForCreate where ChangeType='create' ))
             BEGIN
                       
                delete from   Cfg_TableColumnDetailForCreate where ChangeType='create' ;
             END
       IF (NOT EXISTS (select * from  Cfg_TableColumnDetailForCreate where ChangeType='create' ))
             BEGIN
      set @sql_str='  insert into dbo.Cfg_TableColumnDetailForCreate (
           DatabaseName,UserName,TableName,ColumnName,
	   SystemTypeIdDest,MaxLengthDest,IsNullDest,DefaultDefinitionDest,
	   IdentityDef,ChangeType,VersionId,FunctionId)
            select  DB_NAME() as DatabaseName,user_name() as UserName, e.TableName,e.ColumnName,
	e.SystemTypeId SystemTypeIdDest,e.MaxLength MaxLengthDest,
	e.IsNullable IsNullableDest,e.DefaultDefinition DefaultDefinitionDest,
	''s'' IdentityDef,''create'' ChangeType,3 VersionId,2 FunctionId
          from (
             select b.Name  TableName,  a.name ColumnName,
	     a.system_type_id    SystemTypeId,
	     a.max_length        MaxLength ,
	     a.is_nullable       IsNullable,  
	     ( select dc.definition from sys.default_constraints  dc 
                where dc.object_id=a.default_object_id
               and dc.parent_object_id=b.object_id) DefaultDefinition

           from '+@source_dbName+'.sys.columns a 
           right join '+@source_dbName+'.sys.tables b 
	   on b.object_id=a.object_id and b.object_id>0 
         ) e
           where e.TableName+e.ColumnName not in
            ( 
                 select b.Name+a.Name ColumnName
                 from '+@dest_dbName+'.sys.columns a 
                  inner join '+@dest_dbName+'.sys.tables b 
	          on b.object_id=a.object_id and b.object_id>0 
             ) 
              and  e.TableName in 
             (
                  select b.Name TableName
                   from '+@dest_dbName+'.sys.columns a inner join '+@dest_dbName+'.sys.tables b on b.object_id=a.object_id
                  and b.object_id>0 
              );'
        exec sp_executesql @sql_str 
          END

 END
