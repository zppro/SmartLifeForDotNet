
-----------------
select * from Pub_Group;
select * from Sys_DictionaryItem where ItemName like 'обиЁ%'
select * from Cer_DeployNode;
select * from Sys_Parameter;
select * 
from Pub_ServiceStation;
----------------------------------
  update Pub_Group
 set AreaId='99995' 
 where AreaId='99999'

 update Pub_User
 set Area1='99995'
 where Area1='99999';

 update Pub_ServiceStation
 set AreaId='99995'
 where AreaId='99999';
 
 update Sys_Parameter 
 set ParameterValue='{"id":"99995","code":"120395"}'
 where ID=10
 
 
 
 select a.name,b.name
 from (
 select OBJECT_ID,name 
 from sys.tables 
 where name like 'Sys%' or name like 'Pub%'
 ) a,(
 select name,OBJECT_ID 
 from sys.all_columns
 where name='AreaId') b
 where a.object_id=b.object_id;
 
 select  'select count(*) ns,'''+name+''' tablename from '+name +' union '
 from sys.tables 
 where name like 'Sys%' or name like 'Pub%'
 
 -------------------------------------------------
 select count(*) ns,'Sys_Parameter' tablename from Sys_Parameter union 
select count(*) ns,'Sys_PlatformApplication' tablename from Sys_PlatformApplication union 
select count(*) ns,'Sys_Server' tablename from Sys_Server union 
select count(*) ns,'Sys_Tree' tablename from Sys_Tree union 
select count(*) ns,'Sys_TreeItem' tablename from Sys_TreeItem union 
select count(*) ns,'Sys_WhiteList' tablename from Sys_WhiteList union 
select count(*) ns,'sysdiagrams' tablename from sysdiagrams union 
select count(*) ns,'Pub_Accounting_Period' tablename from Pub_Accounting_Period union 
select count(*) ns,'Pub_Area' tablename from Pub_Area union 
select count(*) ns,'Pub_Attachment' tablename from Pub_Attachment union 
select count(*) ns,'Pub_Device' tablename from Pub_Device union 
select count(*) ns,'Pub_DeviceExtendInfo' tablename from Pub_DeviceExtendInfo union 
select count(*) ns,'Pub_EntityDefination' tablename from Pub_EntityDefination union 
select count(*) ns,'Pub_EntityDefinationHistory' tablename from Pub_EntityDefinationHistory union 
select count(*) ns,'Pub_Geography' tablename from Pub_Geography union 
select count(*) ns,'Pub_Group' tablename from Pub_Group union 
select count(*) ns,'Pub_GroupMember' tablename from Pub_GroupMember union 
select count(*) ns,'Pub_GroupPermit' tablename from Pub_GroupPermit union 
select count(*) ns,'Pub_MultiLanguageStorageBig' tablename from Pub_MultiLanguageStorageBig union 
select count(*) ns,'Pub_MultiLanguageStorageSmall' tablename from Pub_MultiLanguageStorageSmall union 
select count(*) ns,'Pub_Reminder' tablename from Pub_Reminder union 
select count(*) ns,'Pub_ReminderObject' tablename from Pub_ReminderObject union 
select count(*) ns,'Pub_ServiceStation' tablename from Pub_ServiceStation union 
select count(*) ns,'Pub_SmsSend' tablename from Pub_SmsSend union 
select count(*) ns,'Pub_User' tablename from Pub_User union 
select count(*) ns,'Pub_UserArea' tablename from Pub_UserArea union 
select count(*) ns,'Pub_UserPermit' tablename from Pub_UserPermit union 
select count(*) ns,'Pub_UserRePermit' tablename from Pub_UserRePermit union 
select count(*) ns,'Sys_ApplicationAccess' tablename from Sys_ApplicationAccess union 
select count(*) ns,'Sys_ApplicationAccessibility' tablename from Sys_ApplicationAccessibility union 
select count(*) ns,'Sys_ApplicationAccessPoint' tablename from Sys_ApplicationAccessPoint union 
select count(*) ns,'Sys_DictionaryItem' tablename from Sys_DictionaryItem union 
select count(*) ns,'Sys_Application' tablename from Sys_Application union 
select count(*) ns,'Sys_DictionaryItemExtend' tablename from Sys_DictionaryItemExtend union 
select count(*) ns,'Sys_Behavior' tablename from Sys_Behavior union 
select count(*) ns,'Sys_Menu' tablename from Sys_Menu union 
select count(*) ns,'Sys_Dictionary' tablename from Sys_Dictionary union 
select count(*) ns,'Sys_MenuBehavior' tablename from Sys_MenuBehavior union 
select count(*) ns,'Sys_Platform' tablename from Sys_Platform ;

------------
--------------------------
select * 
from pub_user;

select *
--accesspoint,REPLACE(accesspoint,'17000','17005') 
from cer_deploynode
where len(connectstring)>1 and runmode=0;


select checkintime,caller,callee 
from Oca_callservice 
where substring(servicequeueno,6,1)=0
and (convert(varchar(20),checkintime,120) between '2014-03-17 20:30:15'  and '2014-03-18 06:30:15')
order by checkintime desc

select convert(varchar(20),GETDATE(),120)

select checkintime,caller,callee 
from ipcc_callrecord 
where callee=58108580
and (convert(varchar(20),checkintime,120) between '2014-03-17 20:30:15'  and '2014-03-18 06:30:15')
order by checkintime desc

 
 
select checkintime,caller,callee 
from ipcc_callrecord 
where callee=28080210
and (convert(varchar(20),checkintime,120) between '2014-03-17 20:30:15'  and '2014-03-18 06:30:15')
and caller not in(
select --checkintime,
caller--,callee 
from [Smartlife-120303].dbo.Oca_callservice 
where substring(servicequeueno,6,1)=0
and (convert(varchar(20),checkintime,120) between '2014-03-17 20:30:15'  and '2014-03-18 06:30:15')
--order by checkintime desc
)
order by checkintime desc

select * 
from ipcc_caller
where caller='18668001381'

select * 
from ipcc_caller
where caller in(
select --checkintime,
caller--,callee 
from [Smartlife-120304].dbo.Oca_callservice 
where substring(servicequeueno,6,1)=0
and (convert(varchar(20),checkintime,120) between '2014-03-17 20:30:15'  and '2014-03-18 06:30:15')
)

select * 
from ipcc_caller
where caller in(
select --checkintime,
caller
--,callee 
from ipcc_callrecord 
where callee=58108580
and (convert(varchar(20),checkintime,120) between '2014-03-17 20:30:15'  and '2014-03-18 06:30:15')
)