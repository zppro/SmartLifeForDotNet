USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_DBA_GetTableColumnDetailForCreate]    Script Date: 05/13/2014 18:11:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************************************************/
/* SP_DBA_GetTableColumnDetailForCreate */
/* 取表的元数据 */
/*  */
/*******************************************************************************/
CREATE procedure [dbo].[SP_DBA_GetTableColumnDetailForCreate]
@DatabaseName nvarchar(60),
@TableName  nvarchar(50)
as 
declare @sqlStr nvarchar(4000)
begin
      set @sqlStr='   insert into Cfg_TableColumnDetailForCreate (DatabaseName,UserName,TableName,ColumnName,SystemTypeIdDest,MaxLengthDest,
         IsNullDest,IdentityDef,ChangeType,CheckInTime)
         select '''+@DatabaseName+''' databasename,b.TABLE_SCHEMA,b.tablename,b.column_name,b.DATA_TYPE,
         b.CHARACTER_OCTET_LENGTH,b.IS_NULLABLE,b.identitycolumn,''create'' changetype,GETDATE() checkintime
        from 
        ( 
           select a.ordinal_position,a.table_schema,a.table_name as tablename,a.column_name,
           a.data_type,a.character_octet_length,a.is_nullable,d.column_name identitycolumn
          from (select * from '+@DatabaseName+'.INFORMATION_SCHEMA.COLUMNS where table_name ='''+@TableName+'''
         ) a 
        left join (
          
	       select user_name() constraint_schema,a.name table_name,b.name column_name
              from '+@DatabaseName+'.sys.tables  a,
	           '+@DatabaseName+'.sys.identity_columns b
               where a.object_id =b.object_id
          )d
         on  a.TABLE_SCHEMA = d.constraint_schema   and a.column_name = d.column_name and a.TABLE_NAME=d.TABLE_NAME) b
         '
        exec sp_executesql @sqlStr
end 
GO

