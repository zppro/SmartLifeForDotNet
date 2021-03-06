USE [smartlife-120301]
GO
/****** Object:  StoredProcedure [dbo].[p_make_createprocedure_script]    Script Date: 02/25/2014 09:28:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER    PROCEDURE [dbo].[p_make_createprocedure_script]
@source_db_name nvarchar(50)='Smartlife-120300',
@update_type  nvarchar(40)='Procedure'
 AS
 BEGIN
SET NOCOUNT ON
IF (NOT EXISTS (select * from t_deploy_object where  object_type='Procedure'))
insert into t_deploy_script (version_id,object_name,update_script,recover_script,update_validate_script,recover_validate_script,type)
select 3 version_id,a.name object_name,b.definition  update_script,'drop PROCEDURE '+a.name  recover_script,
'select count(c.name)  from sys.all_objects c where c.name='''+a.name+'''' update_validate_script,
'select count(d.name)  from sys.all_objects d where d.name='''+a.name+'''' recover_validate_script,'P' type
 from [smartlife-120300].sys.all_objects a,[smartlife-120300].sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)<4000;
END
