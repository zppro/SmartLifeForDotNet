USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Dey_CreateProcedureScript]    Script Date: 05/13/2014 17:22:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create    PROCEDURE [dbo].[SP_Dey_CreateProcedureScript]
 AS
 BEGIN
SET NOCOUNT ON
IF (NOT EXISTS (select * from Cfg_Object where ObjectType='Procedure'))
             insert into Dey_Script (VersionId,ObjectName,UpdateScript,RecoverScript,UpdateValidateScript,RecoverValidateScript,Type)
            select 3 version_id,a.name object_name,'drop PROCEDURE '+a.name+';'+b.definition  update_script,
                   'drop PROCEDURE '+a.name  recover_script,
                   'select count(c.name)  from sys.all_objects c where c.name='''+a.name+'''' update_validate_script,
                   'select count(d.name)  from sys.all_objects d where d.name='''+a.name+'''' recover_validate_script,'P' type
            from remote_dbo.[smartlife-120300].sys.all_objects a,remote_dbo.[smartlife-120300].sys.all_sql_modules b 
            where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)<40
             and a.name not in(select TableName from Cfg_Object where  ChangeType='not');
END

GO

