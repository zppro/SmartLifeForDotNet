USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Cfg_GetColumnDetailForAddColumn]    Script Date: 05/13/2014 18:08:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************/
/* SP_Cfg_GetColumnDetailForAddColumn                         */
/*  在源库，不在目的库的表的字段的详细信息  add column        */
/*  @source_dbName 源数据库名，@dest_dbName 目的数据库名      */
/**************************************************************/
CREATE   PROCEDURE [dbo].[SP_Cfg_GetColumnDetailForAddColumn]
@source_dbName nvarchar(100),
@dest_dbName   nvarchar(100)
 AS
 BEGIN
DECLARE  @sql_str nvarchar(max) 
      SET NOCOUNT ON
     
        IF (EXISTS (select * from Cfg_ObjectColumnDetail where ChangeType='add' and  ObjectType='column'))
             BEGIN
                       
                delete from  Cfg_ObjectColumnDetail where ChangeType='add' and  ObjectType='column';
             END
       IF (NOT EXISTS (select * from Cfg_ObjectColumnDetail where ChangeType='add' and  ObjectType='column'))
             BEGIN
             if @dest_dbName <>'' 
             begin
      set @sql_str='  insert into dbo.Cfg_ObjectColumnDetail (
           DatabaseName,UserName,TableName,ColumnName,
	   SystemTypeIdDest,MaxLengthDest,IsNullDest,DefaultDefinitionDest,
	   ObjectType,ChangeType,VersionId,FunctionId)
            select  DB_NAME() as DatabaseName,user_name() as UserName, e.TableName,e.ColumnName,
	e.SystemTypeId SystemTypeIdDest,e.MaxLength MaxLengthDest,
	e.IsNullable IsNullableDest,e.DefaultDefinition DefaultDefinitionDest,
	''column'' ObjectType,''add'' ChangeType,3 VersionId,2 FunctionId
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
              end
              
               if @dest_dbName ='' 
             begin
                    set @sql_str='  insert into dbo.Cfg_ObjectColumnDetail (
           DatabaseName,UserName,TableName,ColumnName,
	   SystemTypeIdDest,MaxLengthDest,IsNullDest,DefaultDefinitionDest,
	   ObjectType,ChangeType,VersionId,FunctionId)
            select  DB_NAME() as DatabaseName,user_name() as UserName, e.TableName,e.ColumnName,
	e.SystemTypeId SystemTypeIdDest,e.MaxLength MaxLengthDest,
	e.IsNullable IsNullableDest,e.DefaultDefinition DefaultDefinitionDest,
	''column'' ObjectType,''add'' ChangeType,3 VersionId,2 FunctionId
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
         ;'
             end
        exec sp_executesql @sql_str 
          END

 END

GO

