update Oca_FamilyCamera set inputcode1=dbo.FUNC_Tol_GetPY(DeviceName),inputcode2=dbo.FUNC_Tol_GetWB(DeviceName) where  InputCode1 is null or InputCode2 is null;
update Oca_FamilyMember set inputcode1=dbo.FUNC_Tol_GetPY(FamilyMemberName),inputcode2=dbo.FUNC_Tol_GetWB(FamilyMemberName) where  InputCode1 is null or InputCode2 is null;
update Oca_MutualAidPerson set inputcode1=dbo.FUNC_Tol_GetPY(MutualAidPersonName),inputcode2=dbo.FUNC_Tol_GetWB(MutualAidPersonName) where  InputCode1 is null or InputCode2 is null;
update Sys_DictionaryItem set inputcode1=dbo.FUNC_Tol_GetPY(ItemName),inputcode2=dbo.FUNC_Tol_GetWB(ItemName) where  InputCode1 is null or InputCode2 is null;
update Pub_Group set inputcode1=dbo.FUNC_Tol_GetPY(GroupName),inputcode2=dbo.FUNC_Tol_GetWB(GroupName) where  InputCode1 is null or InputCode2 is null;
update Oca_OldManBaseInfo set inputcode1=dbo.FUNC_Tol_GetPY(OldManName),inputcode2=dbo.FUNC_Tol_GetWB(OldManName) where  InputCode1 is null or InputCode2 is null;
update Pub_Area set inputcode1=dbo.FUNC_Tol_GetPY(AreaName),inputcode2=dbo.FUNC_Tol_GetWB(AreaName) where  InputCode1 is null or InputCode2 is null;
update Pub_ServiceStation set inputcode1=dbo.FUNC_Tol_GetPY(StationName),inputcode2=dbo.FUNC_Tol_GetWB(StationName) where  InputCode1 is null or InputCode2 is null;

select 'select ' +column_name+ ' ,inputcode1,inputcode2 from ' +table_name+' where inputcode1 is null or inputcode2 is null;' from dbo.vsystem_info_columns where column_name like '%name' and table_name in (select table_name from dbo.vsystem_info_columns where column_name='InputCode2') 
and comments  is not null;


select DeviceName ,inputcode1,inputcode2 from Oca_FamilyCamera where inputcode1 is null or inputcode2 is null;
select FamilyMemberName ,inputcode1,inputcode2 from Oca_FamilyMember where inputcode1 is null or inputcode2 is null;
select MutualAidPersonName ,inputcode1,inputcode2 from Oca_MutualAidPerson where inputcode1 is null or inputcode2 is null;
select ItemName ,inputcode1,inputcode2 from Sys_DictionaryItem where inputcode1 is null or inputcode2 is null;
select GroupName ,inputcode1,inputcode2 from Pub_Group where inputcode1 is null or inputcode2 is null;
select OldManName ,inputcode1,inputcode2 from Oca_OldManBaseInfo where inputcode1 is null or inputcode2 is null;
select AreaName ,inputcode1,inputcode2 from Pub_Area where inputcode1 is null or inputcode2 is null;
select StationName ,inputcode1,inputcode2 from Pub_ServiceStation where inputcode1 is null or inputcode2 is null;

declare @str nvarchar(max)
select top 1 @str=b.definition
from [smartlife-120300].sys.all_objects a,[smartlife-120300].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>4000;

 select * from oca_serviceworkorder;