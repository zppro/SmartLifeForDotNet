USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Dey_AlterColumnSetDefaultScript]    Script Date: 05/13/2014 18:15:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**********************************************************************/
/* SP_Dey_AlterColumnSetDefaultScript                                 */
/* 生成修改表字段的设置新的默认值的脚本                               */
/**********************************************************************/


CREATE    PROCEDURE [dbo].[SP_Dey_AlterColumnSetDefaultScript]
 AS
 BEGIN
      SET NOCOUNT ON
     
         IF (EXISTS (select * from Dey_Script where Type='S'))
             BEGIN
               delete from Dey_Script where Type='S';        
             END
        IF (NOT EXISTS (select * from Dey_Script where Type='S'))
             BEGIN
                    insert into Dey_Script (VersionId,objectName,UpdateScript,RecoverScript,UpdateValIdateScript,RecoverValIdateScript,Type)
                    select 3 VersionId,
                           TableName objectName,
                           'alter table '+TableName+' drop constraints dk_' +TableName+a.ColumnName+ '; '+
                           'alter table '+TableName+' add constraints dk_' +TableName+a.ColumnName+ ' '+
	                   case isnull(a.DefaultDefinitionDest,'9') when '9'  then '' else  ' default '+ a.DefaultDefinitionDest end +
                           ' for ' +a.ColumnName +' ;'    
			   UpdateScript,

                            'alter table '+TableName+' drop constraints dk_' +TableName+a.ColumnName+ '; '+
                            'alter table '+TableName+' add constraints dk_' +TableName+a.ColumnName+ ' '+
	                    case isnull(a.DefaultDefinitionDest,'9') when '9'  then '' else  ' default '+ isnull(a.DefaultDefinitionSrc,'') end +
                           ' for ' +a.ColumnName +' ;'
                           RecoverScript,
                          'select 1' UpdateValIdateScript,
                          'select 1' RecoverValIdateScript,
                          'S' Type
                   from Cfg_ObjectColumnDetail a 
                   where  ChangeType='alter' and (isnull(DefaultDefinitionDest,'9')<>isnull(DefaultDefinitionSrc,'9'));   
             END
 END
GO

