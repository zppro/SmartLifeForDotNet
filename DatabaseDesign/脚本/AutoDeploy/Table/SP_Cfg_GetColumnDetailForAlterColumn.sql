USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Cfg_GetColumnDetailForAlterColumn]    Script Date: 05/13/2014 18:08:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/************************************************************************************/
/*  SP_Cfg_GetColumnDetailForAlterColumn                                            */
/*  源库与目的库的 表及字段是相同的，但是类型或者精度是不同的。 alter column        */
/*  @source_dbName 源数据库名，@dest_dbName 目的数据库名                            */
/************************************************************************************/
CREATE   PROCEDURE [dbo].[SP_Cfg_GetColumnDetailForAlterColumn]
@source_dbName nvarchar(100),
@dest_dbName   nvarchar(100)
 AS
 BEGIN
DECLARE  @sql_str nvarchar(4000) 
      SET NOCOUNT ON
     
     
        IF (EXISTS (select * from  Cfg_ObjectColumnDetail where ChangeType='alter' and  ObjectType='column'))
             BEGIN
                       
              delete from  Cfg_ObjectColumnDetail where ChangeType='alter' and  ObjectType='column';
             END
        
         IF (NOT EXISTS (select * from  Cfg_ObjectColumnDetail where ChangeType='alter' and  ObjectType='column'))
             BEGIN
             if @dest_dbName<>''
             begin
                set @sql_str=' insert into dbo.Cfg_ObjectColumnDetail (
          DatabaseName,UserName,TableName,ColumnName,
	  SystemTypeIdDest,MaxLengthDest,IsNullDest,DefaultDefinitionDest,
          SystemTypeIdSrc,MaxLengthSrc,IsNullSrc,DefaultDefinitionSrc,
	  ObjectType,ChangeType,VersionId,FunctionId
	  )
select  DB_NAME() as DatabaseName,user_name() as UserName, m.TableName,m.ColumnName,
	m.SystemTypeId SystemTypeIdDest,m.MaxLength MaxLengthDest,
        m.IsNullable  IsNullableDest,m.DefaultDefinition DefaultDefinitionDest,
	n.SystemTypeId SystemTypeIdSrc,n.MaxLength MaxLengthSrc,
        n.IsNullable IsNullableSrc,n.DefaultDefinition DefaultDefinitionDest,
	''column'' ObjectType,''alter'' ChangeType,3 VersionId,2 FunctionId
from ( select b.Name  TableName, a.name ColumnName,
	       a.system_type_id   SystemTypeId,
	       a.max_length       MaxLength ,
	       a.is_nullable      IsNullable ,
	      ( select dc.definition from '+@source_dbName+'.sys.default_constraints  dc 
                where dc.object_id=a.default_object_id
               and dc.parent_object_id=b.object_id) DefaultDefinition
        from '+@source_dbName+'.sys.columns a 
	     right join '+@source_dbName+'.sys.tables b  on b.object_id=a.object_id  and b.object_id>0
     ) m,
     (  select b.Name+a.name      ColumnName,
	        a.system_type_id   SystemTypeId,
		a.max_length       MaxLength ,
		a.is_nullable      IsNullable,
               ( select dc.definition from '+@dest_dbName+'.sys.default_constraints  dc 
                 where dc.object_id=a.default_object_id
                 and dc.parent_object_id=b.object_id) DefaultDefinition
         from '+@dest_dbName+'.sys.columns a 
	       right join '+@dest_dbName+'.sys.tables b   on b.object_id=a.object_id  and b.object_id>0 
      ) n
    where m.TableName+m.ColumnName  =n.ColumnName 
     and m.TableName+m.ColumnName not in ( select e.TableName+e.ColumnName pri_key
                                          from ( select b.Name  TableName,
                                               a.name ColumnName,a.system_type_id  SystemTypeId,a.max_length MaxLength ,a.is_nullable IsNullable,
					        ( select dc.definition from '+@source_dbName+'.sys.default_constraints  dc 
                                                  where dc.object_id=a.default_object_id
                                                 and dc.parent_object_id=b.object_id) DefaultDefinition
                                              from '+@source_dbName+'.sys.columns a right join '+@source_dbName+'.sys.tables b on b.object_id=a.object_id
                                               and b.object_id>0 
					      ) e 
                                            inner join
                                            (  select b.Name  TableName,
                                               a.name ColumnName,a.system_type_id  SystemTypeId,a.max_length MaxLength ,a.is_nullable IsNullable ,
					        ( select dc.definition from '+@dest_dbName+'.sys.default_constraints  dc 
                                                    where dc.object_id=a.default_object_id
                                                   and dc.parent_object_id=b.object_id) DefaultDefinition
                                               from '+@dest_dbName+'.sys.columns a 
					              right join '+@dest_dbName+'.sys.tables b on b.object_id=a.object_id
                                                and b.object_id>0 
					     ) f 
                                            on
                                               e.TableName=f.TableName and e.ColumnName=f.ColumnName and e.SystemTypeId=f.SystemTypeId 
					       and e.MaxLength=f.MaxLength  and e.IsNullable=f.IsNullable
					       and isnull(e.DefaultDefinition,''9'')=isnull(f.DefaultDefinition,''9'') ); '
			       exec sp_executesql @sql_str 
				end	       
                   if @dest_dbName<>''
                  begin
                  print ''
                   end
             END
 END

GO

