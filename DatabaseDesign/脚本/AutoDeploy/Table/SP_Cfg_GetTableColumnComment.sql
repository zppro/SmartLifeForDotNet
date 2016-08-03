USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Cfg_GetTableColumnComment]    Script Date: 05/13/2014 18:10:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************/
/* SP_Cfg_GetTableColumnComment                               */
/* 取表及字段的注释信息，存入配置库                           */
/*  @source_dbName 信息来源数据库名                           */
/**************************************************************/
CREATE PROCEDURE [dbo].[SP_Cfg_GetTableColumnComment]
@source_dbName nvarchar(100)
 AS
 BEGIN
 DECLARE  @sql_str nvarchar(4000) ,@databasename nvarchar(100)
      SET NOCOUNT ON
      select @databasename=''''+replace(REPLACE(@source_dbName,'[',''),']','')+''''
     
        IF (EXISTS (select * from  Cfg_TableColumnComment where DatabaseName=@databasename ))
             BEGIN
                       
              delete from  Cfg_TableColumnComment  where DatabaseName=@databasename;
             END
        
         IF (NOT EXISTS (select * from  Cfg_TableColumnComment where DatabaseName=@databasename))
             BEGIN
                set @sql_str='
                      insert into Cfg_TableColumnComment (DatabaseName,UserName,TableName,ColumnName,
                                 BeforeComment,BeforeLength,BeforeModifyDate,Type)
                      select '+@databasename+' as DatabaseName,user_name() as UserName,
                          (select name from '+@source_dbName+'.sys.tables where object_id=a.major_id) tablename,
                          (select name from '+@source_dbName+'.sys.columns where object_id=a.major_id and column_id=a.minor_id) columnname,
                           cast(value as nvarchar(max))    BeforeComment,
                           LEN(cast(value as nvarchar(3200))) BeforeLength,
                           GETDATE() BeforModifyDate,
                           a.minor_id Type 
                           from '+@source_dbName+'.sys.extended_properties a where a.name=''MS_Description''; '
                 exec sp_executesql @sql_str 
             END
 END

GO

