USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Dey_CreateViewScript]    Script Date: 05/13/2014 09:34:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create    PROCEDURE [dbo].[SP_Dey_CreateViewScript]
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
           select 3 version_id,TABLE_NAME object_name,
           'go
              IF (NOT OBJECT_ID(N''dbo.'+TABLE_NAME+''', ''V'') IS NULL)
                     BEGIN
                         drop view '+TABLE_NAME+';
                     END
                     go
           '+VIEW_DEFINITION+';
           go' update_script,
            'go
                 IF (NOT OBJECT_ID(N''dbo.'+TABLE_NAME+''', ''V'') IS NULL)
                         BEGIN
                             drop view '+TABLE_NAME+';
                         END
                         go
            ' recover_script,
            'select COUNT(*) from Sys.all_views where object_id>0 and name='''+TABLE_NAME+'''' update_validate_script ,
            'select COUNT(*) from Sys.all_views where object_id>0 and name='''+TABLE_NAME+'''' recover_validate_script,'V' type
           from remote_dbo.[smartlife-120300].[INFORMATION_SCHEMA].VIEWS
           where TABLE_NAME not in(select TableName from Cfg_Object where  ChangeType='not');
	    END
END

GO

