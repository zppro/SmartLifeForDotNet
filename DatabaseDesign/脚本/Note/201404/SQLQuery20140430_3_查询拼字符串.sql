select * from sys.endpoint_webmethods;
select * from sys.endpoints;

EXEC dbo.sp_update_job
    @job_name = N'refresh_oldman_locate_10min',
    @new_name = N'refresh_oldman_locate_5min',
     @owner_login_name = 'job'
     
     select * from [smartlife-1203].dbo.Bas_ResidentBaseInfo where NativePlace is null;
     
CREATE LOGIN [test] WITH PASSWORD=N'test@123', DEFAULT_DATABASE=[smartlife-1203], 
DEFAULT_LANGUAGE=[简体中文], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
create user test;
use [smartlife-120300]
create user test;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='test'

EXEC sys.sp_addsrvrolemember @logiName = N'dbo', @roleName = N'sysadmin'

go
create view Cfg_ETL_Component_V
as 
select * from Cfg_ETL_Component;

select 'create view '+name+'_V
 as 
select * from '+name+';' from sys.tables where name like 'Cfg_ETL%'

create view Cfg_ETL_TranslateRule_V
 as 
select * from Cfg_ETL_TranslateRule; 
go
create view Cfg_ETL_Procedure_V
 as 
select * from Cfg_ETL_Procedure; 
go
create view Cfg_ETL_ConnectManager_V
 as 
select * from Cfg_ETL_ConnectManager;
 go
create view Cfg_ETL_Directory_V
 as 
select * from Cfg_ETL_Directory; 
go
create view Cfg_ETL_OutputColumn_V
 as 
select * from Cfg_ETL_OutputColumn; 
go
create view Cfg_ETL_InputColumn_V
 as 
select * from Cfg_ETL_InputColumn; 
go
create view Cfg_ETL_ExternalMetadataColumn_V
 as 
select * from Cfg_ETL_ExternalMetadataColumn; 
go
create view Cfg_ETL_Package_V
 as 
select * from Cfg_ETL_Package; 
go
create view Cfg_ETL_Component_V
 as 
select * from Cfg_ETL_Component; 
go
create view Cfg_ETL_TypeMapping_V
 as 
select * from Cfg_ETL_TypeMapping; 
go
create view Cfg_ETL_TypeMappingFromToDatabase_V
 as 
select * from Cfg_ETL_TypeMappingFromToDatabase;
 go
create view Cfg_ETL_TypeConvert_V
 as 
select * from Cfg_ETL_TypeConvert; 
go


select * into Eva_EvaluatedRequisition
from remote_dbo.[smartlife-120300].dbo.Eva_EvaluatedRequisition
 go
create view Eva_EvaluatedRequisition_V
as 
select * ,
case gender when 'M' then '男'  when 'F' then '女'  else '女' end GenderName,
case status when 0 then '删除' when 1 then '正常' else '未知' end StatusName,
case ResidentStatus when 1 then '正常' when 3 then '迁出' else '未知' end ResidentStatusName,
dbo.FUNC_Tol_GetDictionaryItemName('00012',DataSource) DataSourceName,
dbo.FUNC_Tol_GetDictionaryItemName('00017',EducationLevel) EducationLevelName,
dbo.FUNC_Tol_GetDictionaryItemName('00005',NativePlace) NativePlaceName,
dbo.FUNC_Tol_GetDictionaryItemName('00016',HouseholdRegister) HouseholdRegisterName,
dbo.FUNC_Tol_GetDictionaryItemName('00018',Marriage) MarriageName,
dbo.FUNC_Tol_GetDictionaryItemName('00019',LivingStatus) LivingStatusName,
dbo.FUNC_Tol_GetDictionaryItemName('00020',HousingStatus) HousingStatusName,
dbo.FUNC_Tol_GetDictionaryItemName('00021',IncomeStatus) IncomeStatusName,
dbo.FUNC_Tol_GetDictionaryItemName('01005',ResidentbizId) ResidentbizIdName,
dbo.FUNC_Tol_GetDictionaryItemName('12002',ServeItemType) ServeItemTypeName,
dbo.FUNC_Tol_GetDictionaryItemName('00005',AreaId) AreaIdName,
(select top 1 areaname from Pub_Area where areaid2=a.areaid2) AreaId2Name,
(select top 1 areaname from Pub_Area where areaid3=a.areaid3) AreaId3Name
from Eva_EvaluatedRequisition a
;
go

select * from Eva_EvaluatedRequisition_V;
/*
select * from dbo.Sys_Dictionary;
drop table dbo.sys_dictionary

set IDENTITY_INSERT  sys_dictionary on
insert into sys_dictionary
select * into sys_dictionary from remote_dbo.[smartlife-120300].dbo.sys_dictionary;

select * from dbo.Sys_DictionaryItem where ItemName='初中'
drop table dbo.Sys_DictionaryItem
select * into dbo.Sys_DictionaryItem from remote_dbo.[smartlife-120300].dbo.sys_dictionaryItem

*/

select * from dbo.Sys_DictionaryItem where DictionaryId='00017'