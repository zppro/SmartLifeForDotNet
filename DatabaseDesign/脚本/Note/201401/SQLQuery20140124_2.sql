insert into dbo.t_deploy_script (version_id,update_script,recover_script,update_validate_script,recover_validate_script,type)
select 3 version_id,VIEW_DEFINITION update_script,'drop view '+TABLE_NAME recover_script,
'select COUNT(*) from Sys.all_views where object_id>0 and name='''+TABLE_NAME+'''' update_validate_script ,
'select COUNT(*) from Sys.all_views where object_id>0 and name='''+TABLE_NAME+'''' recover_validate_script,'V' type
from [smartlife-120300].[INFORMATION_SCHEMA].VIEWS;

select COUNT(*) from [smartlife-120300].Sys.all_views where object_id>0 and name='vSystem_Info_Columns'

alter table dbo.t_deploy_script add type nvarchar(10) not null default ('T');
select * from dbo.t_deploy_script where TYPE='V';

select * from dbo.t_deploy_object where object_type='View';
select distinct type,type_desc from sys.all_objects;
select * from sys.all_objects where type='P' and object_id>0
select * from sys.all_objects where type='AF' and object_id>0 
select a.name,b.definition,a.modify_date,len(b.definition) version from sys.all_objects a,sys.all_sql_modules b where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id
select object_id from sys.all_objects where type='U' and object_id>0 and name='Oca_CallRecord'

select definition from sys.all_sql_modules where object_id=(select object_id from sys.all_objects where type='U' and object_id>0 and name='Oca_CallRecord')
1717581157;

 insert into dbo.t_deploy_object (database_name,user_name,table_name,object_type,change_type,version_id,function_id)
select DB_NAME() as database_name,user_name() as user_name,
replace(replace(SUBSTRING(update_script,CHARINDEX('.',update_script)+1,CHARINDEX('as',update_script)-CHARINDEX('.',update_script)-1),']',''),'[','') as table_name,
'View' object_type,
SUBSTRING(update_script,CHARINDEX('View',update_script)-7,6) as change_type,
version_id,
 '3' as function_id from dbo.t_deploy_script where TYPE='V'
 
 PRINT ''
PRINT 'Creating procedure p_make_createview_script ...'
go
IF (EXISTS (SELECT *
            FROM dbo.sysobjects
            WHERE (name = N'p_make_createview_script')
              AND (type = 'P')))
  DROP PROCEDURE p_make_createview_script
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create    PROCEDURE [dbo].[p_make_createview_script]
 AS
 BEGIN
SET NOCOUNT ON
IF (NOT EXISTS (select * from t_deploy_object where  object_type='View'))
insert into dbo.t_deploy_script (version_id,update_script,recover_script,update_validate_script,recover_validate_script,type)
select 3 version_id,VIEW_DEFINITION update_script,'drop view '+TABLE_NAME recover_script,
'select COUNT(*) from Sys.all_views where object_id>0 and name='''+TABLE_NAME+'''' update_validate_script ,
'select COUNT(*) from Sys.all_views where object_id>0 and name='''+TABLE_NAME+'''' recover_validate_script,'V' type
from [smartlife-120300].[INFORMATION_SCHEMA].VIEWS;
END

select 3 version_id,b.definition  update_script,'drop function '+a.name  recover_script,
'select count(c.name)  from sys.all_objects c where c.name='''+a.name+'''' update_validate_script,
'select count(d.name)  from sys.all_objects d where d.name='''+a.name+'''' recover_validate_script,'F' type
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id;

select count(c.name)  from sys.all_objects c where c.name='FUNC_Tol_GetPY'

select a.oldmanname,a.idno,a.tel,a.mobile,b.CallNo
from oca_oldmanbaseinfo a ,oca_oldmanconfiginfo b where a.OldManId=b.OldManId and b.LocateFlag=1