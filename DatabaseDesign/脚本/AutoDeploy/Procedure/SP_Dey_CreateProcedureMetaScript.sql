USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Dey_CreateProcedureMetaScript]    Script Date: 05/13/2014 17:22:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************/
/* SP_Dey_CreateProcedureMetaScript                                    */
/* 生成创建函数的元数据的脚本                                 */
/*  @source_dbName 数据来源数据库名                           */
/*  @dest_dbName 从目标数据库连接到源数据库的名称   */
/**************************************************************/


CREATE    PROCEDURE [dbo].[SP_Dey_CreateProcedureMetaScript]
@source_dbName nvarchar(100),
@dest_dbName nvarchar(100)
 AS
 BEGIN
   --   SET NOCOUNT ON
      DECLARE  @sql_str nvarchar(4000) 
        IF (EXISTS (select * from Dey_Script where Type='P'))
             BEGIN
               delete from Dey_Script where Type='P';        
             END
        IF (NOT EXISTS (select * from Dey_Script where Type='P'))
             BEGIN
                 if @dest_dbName<>''
                begin
                insert into Dey_Script (VersionId,objectName,UpdateScript,RecoverScript,UpdateValIdateScript,RecoverValIdateScript,Type)
               select 3 VersionId,
                      a.ObjectName ,
                    /* ' declare @str nvarchar(max)
                           IF (EXISTS (SELECT *
                                       FROM dbo.sysobjects
                                        WHERE (Name = N'''+a.ObjectName+''')
                                       AND (Type = ''P'')))
                            begin
                                     drop procedure '+a.ObjectName+';
                            end
                          select top 1 @str=b.definition
                          from '+@dest_dbName+'.sys.all_objects a,'+@dest_dbName+'.sys.all_sql_modules b 
                          where a.Type in (''P'') and a.object_id>0 and a.object_id=b.object_id 
                          and a.Name='''+a.ObjectName+''' and  len(b.definition)>40;
                          print ''create procedure '+a.ObjectName+'.... ''
                          exec sp_executesql @str;
                           go
	                 ' */
	                 ' 
                           IF (EXISTS (SELECT *
                                       FROM dbo.sysobjects
                                        WHERE (Name = N'''+a.ObjectName+''')
                                       AND (Type = ''P'')))
                            begin
                                     drop procedure '+a.ObjectName+';
                            end
                        '+a.BeforeDefinition+';
                          print ''create procedure '+a.ObjectName+'.... ''
                           go
	                 '
	                  UpdateScript,
                         'drop procedure '+a.ObjectName+';'  RecoverScript,
                         'select count(c.Name)  from sys.all_objects c where c.Name='''+a.ObjectName+'''' UpdateValIdateScript,
                         'select count(d.Name)  from sys.all_objects d where d.Name='''+a.ObjectName+'''' RecoverValIdateScript,
	                 'P' Type
                       from   cfg_objectversionhistory a
                       where a.Type in ('P')   and BeforeLength<>AfterLength;	
                 end
                 
                  if @dest_dbName=''
                  begin
                
					  insert into Dey_Script (VersionId,objectName,UpdateScript,RecoverScript,UpdateValIdateScript,RecoverValIdateScript,Type)
					  select 3 VersionId,a.ObjectName ,
							   ' 
                           IF (EXISTS (SELECT *
                                       FROM dbo.sysobjects
                                        WHERE (Name = N'''+a.ObjectName+''')
                                       AND (Type = ''P'')))
                            begin
                                     drop procedure '+a.ObjectName+';
                            end
                        '+a.BeforeDefinition+';
                          print ''create procedure '+a.ObjectName+'.... ''
                           go
	                 ' UpdateScript,
							 'drop procedure '+a.ObjectName+';'  RecoverScript,
							 'select count(c.Name)  from sys.all_objects c where c.Name='''+a.ObjectName+'''' UpdateValIdateScript,
							 'select count(d.Name)  from sys.all_objects d where d.Name='''+a.ObjectName+'''' RecoverValIdateScript,
							 'P' Type
					   from   cfg_objectversionhistory a
					   where a.Type in ('P')  ;
                  end
                        
             END
 END
GO

