USE [Leblue-Configuration]
GO
/****** Object:  StoredProcedure [dbo].[SP_Dey_AddTableColumnCommentScript]    Script Date: 06/19/2014 15:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************/
/*  SP_Dey_AddTableColumnCommentScript                        */
/*  生成表及字段的注释的脚本。                                */  
/*  @source_dbName 源数据库名，@dest_dbName 目的数据库名      */
/**************************************************************/

create   PROCEDURE [dbo].[SP_Dey_AddTableColumnCommentScript]
@source_dbName nvarchar(100),
@dest_dbName   nvarchar(100)
 AS
 BEGIN
 DECLARE  @databasenameDest nvarchar(100) ,@databasenameSrc nvarchar(100)
     -- SET NOCOUNT ON
     set @databasenameSrc=''+replace(REPLACE(@source_dbName,'[',''),']','')+''
     set @databasenameDest=''+replace(REPLACE(@dest_dbName,'[',''),']','')+''

        IF (EXISTS (select * from Dey_Script where Type='M' ))
             BEGIN
                delete from   Dey_Script where Type='M';     
             END
        IF (NOT  EXISTS (select * from Dey_Script where Type='M' ))
             BEGIN
             
                if @dest_dbName<> ''
                begin
                insert into Dey_Script (VersionId,objectName,UpdateScript,RecoverScript,UpdateValIdateScript,RecoverValIdateScript,Type)
                  select 3 VersionId,
                        a.TableName objectName,
                       '
                       EXEC sys.sp_addextendedproperty @name=''MS_Description'', @value='''+a.BeforeComment+''' , 
                       @level0type=''SCHEMA'',@level0name='''+a.UserName+''', 
                       @level1type=''TABLE'',@level1name='''+a.TableName+''', 
                       @level2type=''COLUMN'',@level2name='''+a.ColumnName +'''
                      GO
                      '  UpdateScript,
		      ' ' RecoverScript,
                      'select 1' UpdateValIdateScript,
                      'select 1' RecoverValIdateScript,
                      'M' Type
                 from   ( select * from cfg_tablecolumncomment where 
                    datediff(SECOND,BeforeModifyDate, (select MAX(beforemodifydate) from cfg_tablecolumncomment))<300)  a
		      where  a.Type<>0 and a.databasename=@databasenameSrc
                      and a.tablename+a.columnname+a.beforecomment not in(
                      select ab.tablename+ab.columnname+ab.beforecomment  tcb
                      from   ( select * from cfg_tablecolumncomment where 
                    datediff(SECOND,BeforeModifyDate, (select MAX(beforemodifydate) from cfg_tablecolumncomment))<300) ab ,
                      ( select * from cfg_tablecolumncomment where 
                    datediff(SECOND,BeforeModifyDate, (select MAX(beforemodifydate) from cfg_tablecolumncomment))<300) bb
                       where ab.databasename=@databasenameSrc and bb.databasename=@databasenameDest
                      and ab.tablename=bb.tablename and ab.columnname=bb.columnname and ab.beforecomment=bb.beforecomment
                     );
 
                 insert into Dey_Script (VersionId,objectName,UpdateScript,RecoverScript,UpdateValIdateScript,RecoverValIdateScript,Type)
                   select 3 VersionId,
                         a.TableName objectName,
                          '
                          EXEC sys.sp_addextendedproperty @name=''MS_Description'', @value='''+a.BeforeComment+''' , 
                           @level0type=''SCHEMA'',@level0name='''+a.UserName+''', 
                           @level1type=''TABLE'',@level1name='''+a.TableName+'''
                          GO
                          '  UpdateScript,
			  ' ' RecoverScript,
                          'select 1' UpdateValIdateScript,
                          'select 1' RecoverValIdateScript,
                          'M' Type
                  from   ( select * from cfg_tablecolumncomment where 
                    datediff(SECOND,BeforeModifyDate, (select MAX(beforemodifydate) from cfg_tablecolumncomment))<300) a 
                  where  a.Type=0 and a.databasename=@databasenameSrc
                       and a.tablename+a.beforecomment not in(
                           select ab.tablename+ab.beforecomment  tcb
                           from   ( select * from cfg_tablecolumncomment where 
                    datediff(SECOND,BeforeModifyDate, (select MAX(beforemodifydate) from cfg_tablecolumncomment))<300) ab ,
                             ( select * from cfg_tablecolumncomment where 
                    datediff(SECOND,BeforeModifyDate, (select MAX(beforemodifydate) from cfg_tablecolumncomment))<300) bb
                           where ab.databasename=@databasenameSrc and bb.databasename=@databasenameDest
                           and ab.tablename=bb.tablename  and ab.beforecomment=bb.beforecomment and ab.type=0
                          );
                  end
                  
                if @dest_dbName=''
                begin
                      insert into Dey_Script (VersionId,objectName,UpdateScript,RecoverScript,UpdateValIdateScript,RecoverValIdateScript,Type)
                  select 3 VersionId,
                        a.TableName objectName,
                       '
                       EXEC sys.sp_addextendedproperty @name=''MS_Description'', @value='''+a.BeforeComment+''' , 
                       @level0type=''SCHEMA'',@level0name='''+a.UserName+''', 
                       @level1type=''TABLE'',@level1name='''+a.TableName+''', 
                       @level2type=''COLUMN'',@level2name='''+a.ColumnName +'''
                      GO
                      '  UpdateScript,
		              ' ' RecoverScript,
                      'select 1' UpdateValIdateScript,
                      'select 1' RecoverValIdateScript,
                      'M' Type
                 from   ( select * from cfg_tablecolumncomment where 
                    datediff(SECOND,BeforeModifyDate, (select MAX(beforemodifydate) from cfg_tablecolumncomment))<300)  a
		      where  a.Type<>0 and a.databasename=@databasenameSrc
                     ;
 
                 insert into Dey_Script (VersionId,objectName,UpdateScript,RecoverScript,UpdateValIdateScript,RecoverValIdateScript,Type)
                   select 3 VersionId,
                         a.TableName objectName,
                          '
                          EXEC sys.sp_addextendedproperty @name=''MS_Description'', @value='''+a.BeforeComment+''' , 
                           @level0type=''SCHEMA'',@level0name='''+a.UserName+''', 
                           @level1type=''TABLE'',@level1name='''+a.TableName+'''
                          GO
                          '  UpdateScript,
			              ' ' RecoverScript,
                          'select 1' UpdateValIdateScript,
                          'select 1' RecoverValIdateScript,
                          'M' Type
                  from   ( select * from cfg_tablecolumncomment where 
                    datediff(SECOND,BeforeModifyDate, (select MAX(beforemodifydate) from cfg_tablecolumncomment))<300) a
                   where  a.Type=0 and a.databasename=@databasenameSrc
                       ;
                  end
               
                          
             END   /* IF END */
 END
