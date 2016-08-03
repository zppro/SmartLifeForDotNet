USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Dey_CreateViewScriptProc]    Script Date: 05/13/2014 09:35:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************************************/
/*  SP_Dey_CreateViewScriptProc */
/*  生成创建视图的脚本 */

/**************************************************************************************/

CREATE    PROCEDURE [dbo].[SP_Dey_CreateViewScriptProc]
 AS
 BEGIN
SET NOCOUNT ON
        IF (EXISTS (select * from Dey_Script where Type='V'))
             BEGIN
               delete from Dey_Script where Type='V';        
             END
        IF (NOT EXISTS (select * from Dey_Script where Type='V'))
             BEGIN
           insert into Dey_Script (VersionId,ObjectName,UpdateScript,RecoverScript,UpdateValidateScript,RecoverValidateScript,Type)
           select 3 version_id,ObjectName object_name,
           'go
              IF (NOT OBJECT_ID(N''dbo.'+ObjectName+''', ''V'') IS NULL)
                     BEGIN
                         drop view '+ObjectName+';
                     END
                     go
           '+BeforeDefinition+';
           go' update_script,
            'go
                 IF (NOT OBJECT_ID(N''dbo.'+ObjectName+''', ''V'') IS NULL)
                         BEGIN
                             drop view '+ObjectName+';
                         END
                         go
            ' recover_script,
            'select COUNT(*) from Sys.all_views where object_id>0 and name='''+ObjectName+'''' update_validate_script ,
            'select COUNT(*) from Sys.all_views where object_id>0 and name='''+ObjectName+'''' recover_validate_script,'V' type            
             from   cfg_objectversionhistory a
             where a.Type in ('V')   and BeforeLength<>AfterLength;	
          -- from remote_dbo.[smartlife-120300].[INFORMATION_SCHEMA].VIEWS
           --where TABLE_NAME not in(select TableName from Cfg_Object where  ChangeType='not');
	    END
END
GO

