---step 1 create db
create database [SmartLife-120395];
--CREATE USER [IPCC1203] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
--CREATE USER [SmartCare1203] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
--CREATE USER [SmartCare120395] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]

------step 2 create login and map
CREATE LOGIN [SmartCare120395] WITH PASSWORD=N'SmartCare120395@2013', DEFAULT_DATABASE=[Smartlife-120395], 
DEFAULT_LANGUAGE=[¼òÌåÖÐÎÄ], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF

USE [IPCC-1203]
GO
CREATE USER [SmartCare120395] FOR LOGIN [SmartCare120395] WITH DEFAULT_SCHEMA=[dbo]

GO
USE [SmartLife-120300]
GO
CREATE USER [SmartCare120395] FOR LOGIN [SmartCare120395]  WITH DEFAULT_SCHEMA=[dbo]

GO
USE [Smartlife-120395]
GO
CREATE USER [SmartCare120395] FOR LOGIN [SmartCare120395] WITH DEFAULT_SCHEMA=[dbo]

GO

--------step 3 create script and exec script 

---------step 4 import data
insert into cer_deploynode (id,ObjectId,ApplicationIdFrom,ApplicationIdTo,CheckInTime,ConnectString,AccessPoint,RunMode)
select id,'120395' ObjectId,ApplicationIdFrom,ApplicationIdTo,CheckInTime,ConnectString,AccessPoint,RunMode
 from [Smartlife-120300].[dbo].cer_deploynode;
 
 
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

insert into dbo.cer_redirect
select * from [smartlife-120300].dbo.Cer_Redirect

Update cer_deploynode 
Set accesspoint=REPLACE(accesspoint,'17000','17005')  
where runmode=0;

Update cer_deploynode
set connectstring='RGF0YSBTb3VyY2U9MTE1LjIzNi4xNzUuMTEwLDEwMDE3O0luaXRpYWwgQ2F0YWxvZz1TbWFydExpZmUtMTIwMzk1O1VzZXIgSUQ9U21hcnRDYXJlMTIwMzk1O1Bhc3N3b3JkPVNtYXJ0Q2FyZTEyMDM5NUAyMDEz'
where len(connectstring)>1 and runmode=0; 

Update cer_deploynode
set connectstring='RGF0YSBTb3VyY2U9MTkyLjE2OC4xLjE3LDEwMDE3O0luaXRpYWwgQ2F0YWxvZz1TbWFydExpZmUtMTIwMzk1O1VzZXIgSUQ9U21hcnRDYXJlMTIwMzk1O1Bhc3N3b3JkPVNtYXJ0Q2FyZTEyMDM5NUAyMDEz'
where len(connectstring)>1 and runmode=1; 


select a.name,dbo.joinstr(b.name)
from sys.tables a,
sys.all_columns b
where a.object_id=b.object_id
group by a.name

select * from cer_deployNode;
select * from sys_parameter;