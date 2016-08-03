create database [Smartlife-120395];
CREATE USER [IPCC1203] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
CREATE USER [SmartCare1203] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
--CREATE USER [SmartCare120395] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]

CREATE LOGIN [SmartCare120395] WITH PASSWORD=N'SmartCare120395@2013', DEFAULT_DATABASE=[Smartlife-120395], 
DEFAULT_LANGUAGE=[¼òÌåÖÐÎÄ], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON

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

insert into cer_deploynode (id,ObjectId,ApplicationIdFrom,ApplicationIdTo,CheckInTime,ConnectString,AccessPoint,RunMode)
select id,'120395' ObjectId,ApplicationIdFrom,ApplicationIdTo,CheckInTime,ConnectString,AccessPoint,RunMode
 from [Smartlife-120300].[dbo].cer_deploynode;

select   dbo.joinStr(name)
from    sys.all_columns 
where  object_id   in 
            (   select  object_id 
                from    sys.tables 
               where  name='Cer_DeployNode'
             )
             
             
             select * from pub_servicestation;
select updatescript from dey_script where TYPE='D';

exec sp_dey_insertdatascript @source_dbName='remote_dbo.[Smartlife-120300]';
select TableName from Cfg_Object where ObjectType='table' and ChangeType='not'
delete from Sys_PlatformApplication;insert into Sys_PlatformApplication select *  from remote_dbo.[Smartlife-120300].dbo.Sys_PlatformApplication;
delete from Sys_Tree;insert into Sys_Tree select *  from remote_dbo.[Smartlife-120300].dbo.Sys_Tree;
delete from Sys_Behavior;insert into Sys_Behavior select *  from remote_dbo.[Smartlife-120300].dbo.Sys_Behavior;
delete from sysdiagrams;insert into sysdiagrams select *  from remote_dbo.[Smartlife-120300].dbo.sysdiagrams;
delete from Sys_MenuBehavior;insert into Sys_MenuBehavior select *  from remote_dbo.[Smartlife-120300].dbo.Sys_MenuBehavior;
delete from Sys_Parameter;insert into Sys_Parameter select *  from remote_dbo.[Smartlife-120300].dbo.Sys_Parameter;
delete from Sys_Dictionary;insert into Sys_Dictionary select *  from remote_dbo.[Smartlife-120300].dbo.Sys_Dictionary;
delete from Sys_DictionaryItem;insert into Sys_DictionaryItem select *  from remote_dbo.[Smartlife-120300].dbo.Sys_DictionaryItem;
delete from Sys_TreeItem;insert into Sys_TreeItem select *  from remote_dbo.[Smartlife-120300].dbo.Sys_TreeItem;
delete from Sys_DictionaryItemExtend;insert into Sys_DictionaryItemExtend select *  from remote_dbo.[Smartlife-120300].dbo.Sys_DictionaryItemExtend;
delete from Sys_ApplicationAccessPoint;insert into Sys_ApplicationAccessPoint select *  from remote_dbo.[Smartlife-120300].dbo.Sys_ApplicationAccessPoint;
delete from Sys_Server;insert into Sys_Server select *  from remote_dbo.[Smartlife-120300].dbo.Sys_Server;
delete from Sys_ApplicationAccess;insert into Sys_ApplicationAccess select *  from remote_dbo.[Smartlife-120300].dbo.Sys_ApplicationAccess;
delete from Sys_WhiteList;insert into Sys_WhiteList select *  from remote_dbo.[Smartlife-120300].dbo.Sys_WhiteList;
delete from Sys_ApplicationAccessibility;insert into Sys_ApplicationAccessibility select *  from remote_dbo.[Smartlife-120300].dbo.Sys_ApplicationAccessibility;
delete from Sys_Platform;insert into Sys_Platform select *  from remote_dbo.[Smartlife-120300].dbo.Sys_Platform;
delete from Sys_Application;insert into Sys_Application select *  from remote_dbo.[Smartlife-120300].dbo.Sys_Application;
delete from Sys_Menu;insert into Sys_Menu select *  from remote_dbo.[Smartlife-120300].dbo.Sys_Menu;

select * from sys_behavior;