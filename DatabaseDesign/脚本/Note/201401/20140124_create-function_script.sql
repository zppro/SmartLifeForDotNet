/**************************************************************/
/* p_make_createfunction_script                               */
/**************************************************************/

PRINT ''
PRINT 'Creating procedure p_make_createfunction_script ...'
go
IF (EXISTS (SELECT *
            FROM dbo.sysobjects
            WHERE (name = N'p_make_createfunction_script')
              AND (type = 'P')))
  DROP PROCEDURE p_make_createfunction_script
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create    PROCEDURE [dbo].[p_make_createfunction_script]
 AS
 BEGIN
SET NOCOUNT ON
IF (NOT EXISTS (select * from t_deploy_object where  object_type='Function'))
insert into t_deploy_script (version_id,update_script,recover_script,update_validate_script,recover_validate_script,type)
select 3 version_id,b.definition  update_script,'drop function '+a.name  recover_script,
'select count(c.name)  from sys.all_objects c where c.name='''+a.name+'''' update_validate_script,
'select count(d.name)  from sys.all_objects d where d.name='''+a.name+'''' recover_validate_script,'F' type
 from [$(source_db_name)].sys.all_objects a,[$(source_db_name)].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id;
END


--a.modify_date,len(b.definition) version

/**************************************************************/
/* p_make_createprocedure_script                               */
/**************************************************************/

PRINT ''
PRINT 'Creating procedure p_make_createprocedure_script ...'
go
IF (EXISTS (SELECT *
            FROM dbo.sysobjects
            WHERE (name = N'p_make_createprocedure_script')
              AND (type = 'P')))
  DROP PROCEDURE p_make_createprocedure_script
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create    PROCEDURE [dbo].[p_make_createprocedure_script]
 AS
 BEGIN
SET NOCOUNT ON
IF (NOT EXISTS (select * from t_deploy_object where  object_type='Procedure'))
insert into t_deploy_script (version_id,update_script,recover_script,update_validate_script,recover_validate_script,type)
select 3 version_id,b.definition  update_script,'drop function '+a.name  recover_script,
'select count(c.name)  from sys.all_objects c where c.name='''+a.name+'''' update_validate_script,
'select count(d.name)  from sys.all_objects d where d.name='''+a.name+'''' recover_validate_script,'P' type
 from [$(source_db_name)].sys.all_objects a,[$(source_db_name)].sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id;
END