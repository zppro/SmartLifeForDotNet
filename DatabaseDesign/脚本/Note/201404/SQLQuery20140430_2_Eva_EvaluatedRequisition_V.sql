EXEC dbo.sp_update_job
    @job_name = N'refresh_oldman_locate_10min',
    @new_name = N'refresh_oldman_locate_5min',
     @owner_login_name = 'job'
     
     exec sp_help_job
     
     select * from sys.all_sql_modules;
     
select * from sys.dm_db_script_level; 
select * from sys.dm_db_index_usage_stats order by user_scans desc

/***********************************************************************/
/* Eva_EvaluatedRequisition_V                                          */
/*  申请表的视图                                                       */
/*  为申请表 添加 数据源，学历，居住情况等字段的 描述信息字段          */
/***********************************************************************/
create view Eva_EvaluatedRequisition_V
as 
select  RequisitionId,Id,CheckInTime,Status,OperatedBy,OperatedOn,DataSource,ResidentId,
		ResidentName,ResidentStatus,ResidentBizId,IDNo,Gender,Nation,NativePlace,
		HouseholdRegister,Familys,EducationLevel,EducationLevelNodes,Marriage,LivingStatus,
		HousingStatus,HousingStatusNodes,IncomeStatus,IncomeStatusNodes,AreaId,AreaId2,
		AreaId3,ResidentialAddress,PlaceOfHouseholdRegister,PostCode,Tel,Mobile,ServeItemType,
		ProxyName,ProxyAddress,ProxyPostCode,ProxyTel,ProxyMobile,RelationshipOfResident ,
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

select   dbo.joinStr(name)
from    sys.all_columns 
where  object_id   in 
            (   select  object_id 
                from    sys.tables 
               where  name='Eva_EvaluatedRequisition'
             )
