USE [SmartLife-120300]
GO

/****** Object:  View [dbo].[Eva_EvaluatedRequisition_V]    Script Date: 04/30/2014 15:49:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************/
/* Eva_EvaluatedRequisition_V                                          */
/*  申请表的视图                                                       */
/*  为申请表 添加 数据源，学历，居住情况等字段的 描述信息字段          */
/***********************************************************************/
alter view [dbo].[Eva_EvaluatedRequisition_V]
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
		--dbo.FUNC_Tol_GetDictionaryItemName('00005',NativePlace) NativePlaceName,
		dbo.FUNC_Tol_GetDictionaryItemNameAndParentAndRoot('00005',NativePlace,'') NativePlaceName,
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
GO

select ;
go
/*************************************************************************/
/* FUNC_Tol_GetDictionaryItemNameAndParentAndRoot                        */
/* 根据 字典编号和项的编号 取得 字典内容，包括祖结点，父结点及本身       */
/*************************************************************************/
alter FUNCTION [dbo].[FUNC_Tol_GetDictionaryItemNameAndParentAndRoot] 
( 
@DictionaryId char(5),
@ItemId   char(5),
@separator char(1)='-'
)
RETURNS nvarchar(max)
AS
BEGIN
	declare @Ret nvarchar(max),@rownum int 
    declare @area TABLE (
		itemid char(5) ,
		parentid char(5),
		itemname nvarchar(10)
		)
;
with a(itemid,parentid,itemname)as 
(
select Itemid,ParentId ,itemname
from Sys_DictionaryItem 
where ItemId=@ItemId and DictionaryId=@DictionaryId
union all
select b.Itemid,b.ParentId ,b.itemname
from Sys_DictionaryItem b,a
where  b.ItemId=a.parentid and b.DictionaryId=@DictionaryId
 ) 
 insert into @area
 select --dbo.JoinStrEx(itemname,'-')
 *  from a  order by itemid; 
 
 select @rownum=COUNT(*) from  @area
 if (@rownum>0)
 begin
		 select @ret=dbo.JoinStrEx(b.itemname,@separator)
		 from @area b ;
 end
 else
 begin
         set @Ret=''
 end   
  
  	return @Ret
END
go

select dbo.FUNC_Tol_GetDictionaryItemNameAndParentAndRoot('00005','01189','')

----------------------------------=======================CTE 示例
declare @Ret nvarchar(max)
    declare @area TABLE (
		itemid char(5) ,
		parentid char(5),
		itemname nvarchar(10)
		)
;
with a(itemid,parentid,itemname)as 
(
select Itemid,ParentId ,itemname
from Sys_DictionaryItem 
where ItemId='01189' and DictionaryId='00005'
union all
select b.Itemid,b.ParentId ,b.itemname
from Sys_DictionaryItem b,a
where  b.ItemId=a.parentid and b.DictionaryId='00005'
 ) 
 insert into @area
 select --dbo.JoinStrEx(itemname,'-')
 *  from a  order by itemid; 
 select * from @area;
 select dbo.JoinStrEx(b.itemname,'/')
 from @area b ;
 
 select * from Eva_EvaluatedRequisition_V;