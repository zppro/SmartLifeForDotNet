USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Cfg_GetObjectPrimaryKey]    Script Date: 05/13/2014 18:10:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************/
/* SP_Cfg_GetObjectPrimaryKey                                 */
/* 取表及表类型的主键信息，存入配置库                         */
/*  @source_dbName 信息来源数据库名                           */
/**************************************************************/


CREATE    PROCEDURE [dbo].[SP_Cfg_GetObjectPrimaryKey]
@source_dbName nvarchar(100)
 AS
 BEGIN
      SET NOCOUNT ON
      DECLARE  @sql_str nvarchar(4000)  ,@databasename nvarchar(100)
       select @databasename=''''+replace(REPLACE(@source_dbName,'[',''),']','')+''''

        IF (EXISTS (select * from Cfg_ObjectPrimaryKey ))
             BEGIN
               delete from Cfg_ObjectPrimaryKey;        
             END
        IF (NOT EXISTS (select * from Cfg_ObjectPrimaryKey))
             BEGIN
           set  @sql_str='
                 insert into Cfg_ObjectPrimaryKey (databasename,username,objectname,columnname,type)
                        select '+@databasename+'  as  databasename,(select name from sys.schemas where SCHEMA_ID= m.schema_id) userName,
                           isnull((select name from '+@source_dbName+'.sys.tables where object_id=m.parent_object_id ),
                          (select name from '+@source_dbName+'.sys.table_types where parent_object_id=m.parent_object_id)
                           ) objectname,
                           dbo.joinStr(ac.name) columnname
                           , isnull((select object_id%10 from '+@source_dbName+'.sys.tables where object_id=m.parent_object_id ),255) type
                        from    '+@source_dbName+'.sys.key_constraints m ,
                                '+@source_dbName+'.sys.index_columns ic,
                                '+@source_dbName+'.sys.all_columns ac
                        where   m.type=''PK'' 
                                and m.parent_object_id=ic.object_id 
                                and m.parent_object_id=ac.object_id and m.unique_index_id=ic.index_id
                                and ic.column_id=ac.column_id              
                                group by m.schema_id,m.parent_object_id;'
                         --       print @sql_str
                          --  and ic.index_column_id=ac.column_id 
                  exec sp_executesql @sql_str
             END
 END

GO

