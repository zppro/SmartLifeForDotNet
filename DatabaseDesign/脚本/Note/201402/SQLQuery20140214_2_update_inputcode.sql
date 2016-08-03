select * from dbo.vsystem_info_columns where column_name='InputCode2';
select * from Oca_FamilyMember;

select 'select ' +column_name+ 'inputcode1,inputcode2 from ' +table_name+' where inputcode1 is null;' from dbo.vsystem_info_columns where column_name like '%name' and table_name in (select table_name from dbo.vsystem_info_columns where column_name='InputCode2') 
and comments  is not null;

select DeviceName,inputcode1 from Oca_FamilyCamera;
select FamilyMemberName,inputcode1 from Oca_FamilyMember;
select MutualAidPersonName,inputcode1 from Oca_MutualAidPerson;
select ItemName,inputcode1 from Sys_DictionaryItem;
select GroupName,inputcode1 from Pub_Group;

select AreaName from Pub_Area;
select StationName from Pub_ServiceStation;

select 'update ' +table_name+ ' set inputcode1=dbo.FUNC_Tol_GetPY(' +column_name+'),inputcode2=dbo.FUNC_Tol_GetWB('+column_name+') where  InputCode1 is null or InputCode2 is null;' from dbo.vsystem_info_columns where column_name like '%name' and table_name in (select table_name from dbo.vsystem_info_columns where column_name='InputCode2') 
and comments  is not null;

update oca_oldmanbaseinfo set inputcode1=dbo.FUNC_Tol_GetPY(oldmanname),inputcode2=dbo.FUNC_Tol_GetWB(oldmanname) where  InputCode1 is null or InputCode2 is null;

update Oca_FamilyCamera set inputcode1=dbo.FUNC_Tol_GetPY(DeviceName),inputcode2=dbo.FUNC_Tol_GetWB(DeviceName) where  InputCode1 is null or InputCode2 is null;
update Oca_FamilyMember set inputcode1=dbo.FUNC_Tol_GetPY(FamilyMemberName),inputcode2=dbo.FUNC_Tol_GetWB(FamilyMemberName) where  InputCode1 is null or InputCode2 is null;
update Oca_MutualAidPerson set inputcode1=dbo.FUNC_Tol_GetPY(MutualAidPersonName),inputcode2=dbo.FUNC_Tol_GetWB(MutualAidPersonName) where  InputCode1 is null or InputCode2 is null;
update Sys_DictionaryItem set inputcode1=dbo.FUNC_Tol_GetPY(ItemName),inputcode2=dbo.FUNC_Tol_GetWB(ItemName) where  InputCode1 is null or InputCode2 is null;
update Pub_Group set inputcode1=dbo.FUNC_Tol_GetPY(GroupName),inputcode2=dbo.FUNC_Tol_GetWB(GroupName) where  InputCode1 is null or InputCode2 is null;
update Oca_OldManBaseInfo set inputcode1=dbo.FUNC_Tol_GetPY(OldManName),inputcode2=dbo.FUNC_Tol_GetWB(OldManName) where  InputCode1 is null or InputCode2 is null;
update Pub_Area set inputcode1=dbo.FUNC_Tol_GetPY(AreaName),inputcode2=dbo.FUNC_Tol_GetWB(AreaName) where  InputCode1 is null or InputCode2 is null;
update Pub_ServiceStation set inputcode1=dbo.FUNC_Tol_GetPY(StationName),inputcode2=dbo.FUNC_Tol_GetWB(StationName) where  InputCode1 is null or InputCode2 is null;