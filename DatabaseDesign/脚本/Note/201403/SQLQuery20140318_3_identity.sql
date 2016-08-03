select --(select count(*) from sys.tables where object_id=object_id group by object_id),
object_id,name,column_id,seed_value,increment_value
 from sys.identity_columns;
 
 
  --------列改名
   select 'EXEC sp_rename ''dbo.'+a.name+'.'+ b.name+''','''+b.name+'2'',''COLUMN'';'
 from remote_dbo.[Smartlife-120300].sys.tables  a,remote_dbo.[Smartlife-120300].sys.identity_columns b
 where a.object_id =b.object_id;
 --------------加标识列
 select 'alter table '+a.name+' add '+ b.name+ (case b.system_type_id when 56 then ' int ' else '' end )+
 (case b.is_nullable when 0 then ' not null ' else ' null ' end )+' identity ('+cast(seed_value as nvarchar(10))+','+cast(increment_value as nvarchar(10))+');'
 from remote_dbo.[Smartlife-120300].sys.tables  a,remote_dbo.[Smartlife-120300].sys.identity_columns b
 where a.object_id =b.object_id;
 
 ---------------
 select 'alter table '+constraint_schema+'.'+table_name +' drop constraint  '+constraint_name+';'
from INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
where upper(constraint_name) like 'PK%'  and column_name='Id2';

------------------
select 'alter table '+constraint_schema+'.'+table_name +' add constraint  '+constraint_name+'  PRIMARY KEY CLUSTERED  (id);'
from INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
where upper(constraint_name) like 'PK%'  and column_name='Id2';
 
 ----------删除列
 select 'alter table '+a.name+' drop column '+ b.name+'2;'
  from remote_dbo.[Smartlife-120300].sys.tables  a,remote_dbo.[Smartlife-120300].sys.identity_columns b
 where a.object_id =b.object_id;
 

------------------------------------------------=============================  
 EXEC sp_rename 'Sales.SalesTerritory.TerritoryID', 'TerrID', 'COLUMN';

 
SET IDENTITY_INSERT tname1 ON
INSERT INTO tname1 FROM tname2
SET IDENTITY_INSERT tname1 OFF

 alter table tblFoo add  bar int null identity(1,1);
 select * from sys.all_columns where name='id' and is_identity=0;
 select name from sys.tables where object_id=25767149
 select * from sys.all_objects
 
 EXEC sys.sp_identitycolumnforreplication 21575115, 1
 
 
 -----------------
 EXEC sp_rename 'dbo.Oca_EPay_Package.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_ReminderObject.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_PlatformApplication.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_FamilyCamera.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_EPay_OldManAccount.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_FamilyMember.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_EPay_RechargeRecord.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_MutualAidPerson.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_Tree.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_Behavior.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.sysdiagrams.diagram_id','diagram_id2','COLUMN';
EXEC sp_rename 'dbo.Oca_ServiceFamilyResponse.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_MenuBehavior.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_Parameter.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_GroupMember.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_User.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_CC_Ext.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_Dictionary.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_ServiceVoice.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_Merchant.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_DictionaryItem.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_Ewallet_OldMan.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_Group.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_GroupPermit.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_Ewallet_Merchant.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_UserPermit.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_TreeItem.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_DictionaryItemExtend.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_Ewallet_Recharge_Record.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_UserRePermit.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_ApplicationAccessPoint.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_Area.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_Ewallet_Charge_Record.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_Device.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_ServiceReceive.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Inc_SyncIn_Resident.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_ServiceWorkOrder.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Cer_Redirect.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_DeviceExtendInfo.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Cer_ObjectToken.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Cer_DeployNode.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_Geography.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_Attachment.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_UserArea.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_EntityDefination.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_CC_SeatExtBind.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_OldManDeviceInfo.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_OldManBaseInfo.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_CC_SeatExtBindHistory.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_OldManLocateInfo.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_CallRecord.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_Server.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_EPay_PackageItem.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_MerchantServeItem.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_ApplicationAccess.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_MerchantServeMode.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_ServiceStation.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_Reminder.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_OldManFamilyInfo.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_WhiteList.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_Accounting_Period.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_ApplicationAccessibility.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_ServiceTrackLog.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Log_SystemJob.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_Platform.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_CallService.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_Application.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Oca_MerchantServePeriod.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Sys_Menu.Id','Id2','COLUMN';
EXEC sp_rename 'dbo.Pub_SmsSend.Id','Id2','COLUMN';
 
 alter table Oca_EPay_Package add Id int  not null  identity (1,1);
alter table Pub_ReminderObject add Id int  not null  identity (1,1);
alter table Sys_PlatformApplication add Id int  not null  identity (1,1);
alter table Oca_FamilyCamera add Id int  not null  identity (1,1);
alter table Oca_EPay_OldManAccount add Id int  not null  identity (1,1);
alter table Oca_FamilyMember add Id int  not null  identity (1,1);
alter table Oca_EPay_RechargeRecord add Id int  not null  identity (1,1);
alter table Oca_MutualAidPerson add Id int  not null  identity (1,1);
alter table Sys_Tree add Id int  not null  identity (1,1);
alter table Sys_Behavior add Id int  not null  identity (1,1);
alter table sysdiagrams add diagram_id int  not null  identity (1,1);
alter table Oca_ServiceFamilyResponse add Id int  not null  identity (1,1);
alter table Sys_MenuBehavior add Id int  not null  identity (1,1);
alter table Sys_Parameter add Id int  not null  identity (1,1);
alter table Pub_GroupMember add Id int  not null  identity (1,1);
alter table Pub_User add Id int  not null  identity (1,1);
alter table Oca_CC_Ext add Id int  not null  identity (1,1);
alter table Sys_Dictionary add Id int  not null  identity (1,1);
alter table Oca_ServiceVoice add Id int  not null  identity (1,1);
alter table Oca_Merchant add Id int  not null  identity (1,1);
alter table Sys_DictionaryItem add Id int  not null  identity (1,1);
alter table Oca_Ewallet_OldMan add Id int  not null  identity (1,1);
alter table Pub_Group add Id int  not null  identity (1,1);
alter table Pub_GroupPermit add Id int  not null  identity (1,1);
alter table Oca_Ewallet_Merchant add Id int  not null  identity (1,1);
alter table Pub_UserPermit add Id int  not null  identity (1,1);
alter table Sys_TreeItem add Id int  not null  identity (1,1);
alter table Sys_DictionaryItemExtend add Id int  not null  identity (1,1);
alter table Oca_Ewallet_Recharge_Record add Id int  not null  identity (1,1);
alter table Pub_UserRePermit add Id int  not null  identity (1,1);
alter table Sys_ApplicationAccessPoint add Id int  not null  identity (1,1);
alter table Pub_Area add Id int  not null  identity (1,1);
alter table Oca_Ewallet_Charge_Record add Id int  not null  identity (1,1);
alter table Pub_Device add Id int  not null  identity (1,1);
alter table Oca_ServiceReceive add Id int  not null  identity (1,1);
alter table Inc_SyncIn_Resident add Id int  not null  identity (1,1);
alter table Oca_ServiceWorkOrder add Id int  not null  identity (1,1);
alter table Cer_Redirect add Id int  not null  identity (1,1);
alter table Pub_DeviceExtendInfo add Id int  not null  identity (1,1);
alter table Cer_ObjectToken add Id int  not null  identity (1,1);
alter table Cer_DeployNode add Id int  not null  identity (1,1);
alter table Pub_Geography add Id int  not null  identity (1,1);
alter table Pub_Attachment add Id int  not null  identity (1,1);
alter table Pub_UserArea add Id int  not null  identity (1,1);
alter table Pub_EntityDefination add Id int  not null  identity (1,1);
alter table Oca_CC_SeatExtBind add Id int  not null  identity (1,1);
alter table Oca_OldManDeviceInfo add Id int  not null  identity (1,1);
alter table Oca_OldManBaseInfo add Id int  not null  identity (1,1);
alter table Oca_CC_SeatExtBindHistory add Id int  not null  identity (1,1);
alter table Oca_OldManLocateInfo add Id int  not null  identity (1,1);
alter table Oca_CallRecord add Id int  not null  identity (1,1);
alter table Sys_Server add Id int  not null  identity (1,1);
alter table Oca_EPay_PackageItem add Id int  not null  identity (1,1);
alter table Oca_MerchantServeItem add Id int  not null  identity (1,1);
alter table Sys_ApplicationAccess add Id int  not null  identity (1,1);
alter table Oca_MerchantServeMode add Id int  not null  identity (1,1);
alter table Pub_ServiceStation add Id int  not null  identity (1,1);
alter table Pub_Reminder add Id int  not null  identity (1,1);
alter table Oca_OldManFamilyInfo add Id int  not null  identity (1,1);
alter table Sys_WhiteList add Id int  not null  identity (1,1);
alter table Pub_Accounting_Period add Id int  not null  identity (1,1);
alter table Sys_ApplicationAccessibility add Id int  not null  identity (1,1);
alter table Oca_ServiceTrackLog add Id int  not null  identity (1,1);
alter table Log_SystemJob add Id int  not null  identity (1,1);
alter table Sys_Platform add Id int  not null  identity (1,1);
alter table Oca_CallService add Id int  not null  identity (1,1);
alter table Sys_Application add Id int  not null  identity (1,1);
alter table Oca_MerchantServePeriod add Id int  not null  identity (1,1);
alter table Sys_Menu add Id int  not null  identity (1,1);
alter table Pub_SmsSend add Id int  not null  identity (1,1);




alter table dbo.Cer_DeployNode drop constraint  PK_Cer_DeployNode;
alter table dbo.Inc_SyncIn_Resident drop constraint  PK_Inc_SyncIn_Resident;
alter table dbo.Log_SystemJob drop constraint  PK_Log_SystemJob;
alter table dbo.Oca_CallRecord drop constraint  PK_Oca_CallRecord;
alter table dbo.Oca_CC_SeatExtBindHistory drop constraint  PK_Oca_CC_SeatExtBindHistory;
alter table dbo.Oca_EPay_OldManAccount drop constraint  PK_Oca_EPay_OldManAccount;
alter table dbo.Oca_EPay_PackageItem drop constraint  PK_Oca_EPay_PackageItem;
alter table dbo.Oca_EPay_RechargeRecord drop constraint  PK_Oca_EPay_RechargeRecord;
alter table dbo.Oca_Ewallet_Charge_Record drop constraint  PK_Oca_Ewallet_Charge_Record;
alter table dbo.Oca_Ewallet_Recharge_Record drop constraint  PK_Oca_Ewallet_Recharge_Record;
alter table dbo.Oca_MerchantServeItem drop constraint  PK_Oca_MerchantServeItem;
alter table dbo.Oca_MerchantServeMode drop constraint  PK_Oca_MerchantServeMode;
alter table dbo.Oca_MerchantServePeriod drop constraint  PK_Oca_MerchantServePeriod;
alter table dbo.Oca_OldManFamilyInfo drop constraint  PK_Oca_OldManFamilyInfo;
alter table dbo.Oca_OldManLocateInfo drop constraint  PK_Oca_OldManLocateInfo;
alter table dbo.Oca_ServiceFamilyResponse drop constraint  PK_Oca_ServiceFamilyResponse;
alter table dbo.Oca_ServiceTrackLog drop constraint  PK_Oca_ServiceTrackLog;
alter table dbo.Oca_ServiceVoice drop constraint  PK_Oca_ServiceVoice;
alter table dbo.Pub_Accounting_Period drop constraint  PK_Pub_Accounting_Period;
alter table dbo.Pub_Reminder drop constraint  PK_Pub_Reminder;
alter table dbo.Pub_ReminderObject drop constraint  PK_Pub_ReminderObject;
alter table dbo.Pub_SmsSend drop constraint  PK_Pub_SmsSend;
alter table dbo.Sys_ApplicationAccess drop constraint  PK_Sys_ApplicationAccess;
alter table dbo.Sys_WhiteList drop constraint  PK_Sys_WhiteList;


alter table dbo.Cer_DeployNode add constraint  PK_Cer_DeployNode  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Inc_SyncIn_Resident add constraint  PK_Inc_SyncIn_Resident  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Log_SystemJob add constraint  PK_Log_SystemJob  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Oca_CallRecord add constraint  PK_Oca_CallRecord  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Oca_CC_SeatExtBindHistory add constraint  PK_Oca_CC_SeatExtBindHistory  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Oca_EPay_OldManAccount add constraint  PK_Oca_EPay_OldManAccount  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Oca_EPay_PackageItem add constraint  PK_Oca_EPay_PackageItem  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Oca_EPay_RechargeRecord add constraint  PK_Oca_EPay_RechargeRecord  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Oca_Ewallet_Charge_Record add constraint  PK_Oca_Ewallet_Charge_Record  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Oca_Ewallet_Recharge_Record add constraint  PK_Oca_Ewallet_Recharge_Record  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Oca_MerchantServeItem add constraint  PK_Oca_MerchantServeItem  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Oca_MerchantServeMode add constraint  PK_Oca_MerchantServeMode  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Oca_MerchantServePeriod add constraint  PK_Oca_MerchantServePeriod  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Oca_OldManFamilyInfo add constraint  PK_Oca_OldManFamilyInfo  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Oca_OldManLocateInfo add constraint  PK_Oca_OldManLocateInfo  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Oca_ServiceFamilyResponse add constraint  PK_Oca_ServiceFamilyResponse  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Oca_ServiceTrackLog add constraint  PK_Oca_ServiceTrackLog  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Oca_ServiceVoice add constraint  PK_Oca_ServiceVoice  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Pub_Accounting_Period add constraint  PK_Pub_Accounting_Period  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Pub_Reminder add constraint  PK_Pub_Reminder  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Pub_ReminderObject add constraint  PK_Pub_ReminderObject  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Pub_SmsSend add constraint  PK_Pub_SmsSend  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Sys_ApplicationAccess add constraint  PK_Sys_ApplicationAccess  PRIMARY KEY CLUSTERED  (id);
alter table dbo.Sys_WhiteList add constraint  PK_Sys_WhiteList  PRIMARY KEY CLUSTERED  (id);


alter table Oca_EPay_Package drop column Id2;
alter table Pub_ReminderObject drop column Id2;
alter table Sys_PlatformApplication drop column Id2;
alter table Oca_FamilyCamera drop column Id2;
alter table Oca_EPay_OldManAccount drop column Id2;
alter table Oca_FamilyMember drop column Id2;
alter table Oca_EPay_RechargeRecord drop column Id2;
alter table Oca_MutualAidPerson drop column Id2;
alter table Sys_Tree drop column Id2;
alter table Sys_Behavior drop column Id2;
--alter table sysdiagrams drop column diagram_id2;
alter table Oca_ServiceFamilyResponse drop column Id2;
alter table Sys_MenuBehavior drop column Id2;
alter table Sys_Parameter drop column Id2;
alter table Pub_GroupMember drop column Id2;
alter table Pub_User drop column Id2;
alter table Oca_CC_Ext drop column Id2;
alter table Sys_Dictionary drop column Id2;
alter table Oca_ServiceVoice drop column Id2;
alter table Oca_Merchant drop column Id2;
alter table Sys_DictionaryItem drop column Id2;
alter table Oca_Ewallet_OldMan drop column Id2;
alter table Pub_Group drop column Id2;
alter table Pub_GroupPermit drop column Id2;
alter table Oca_Ewallet_Merchant drop column Id2;
alter table Pub_UserPermit drop column Id2;
alter table Sys_TreeItem drop column Id2;
alter table Sys_DictionaryItemExtend drop column Id2;
alter table Oca_Ewallet_Recharge_Record drop column Id2;
alter table Pub_UserRePermit drop column Id2;
alter table Sys_ApplicationAccessPoint drop column Id2;
alter table Pub_Area drop column Id2;
alter table Oca_Ewallet_Charge_Record drop column Id2;
alter table Pub_Device drop column Id2;
alter table Oca_ServiceReceive drop column Id2;
alter table Inc_SyncIn_Resident drop column Id2;
alter table Oca_ServiceWorkOrder drop column Id2;
alter table Cer_Redirect drop column Id2;
alter table Pub_DeviceExtendInfo drop column Id2;
alter table Cer_ObjectToken drop column Id2;
alter table Cer_DeployNode drop column Id2;
alter table Pub_Geography drop column Id2;
alter table Pub_Attachment drop column Id2;
alter table Pub_UserArea drop column Id2;
alter table Pub_EntityDefination drop column Id2;
alter table Oca_CC_SeatExtBind drop column Id2;
alter table Oca_OldManDeviceInfo drop column Id2;
alter table Oca_OldManBaseInfo drop column Id2;
alter table Oca_CC_SeatExtBindHistory drop column Id2;
alter table Oca_OldManLocateInfo drop column Id2;
alter table Oca_CallRecord drop column Id2;
alter table Sys_Server drop column Id2;
alter table Oca_EPay_PackageItem drop column Id2;
alter table Oca_MerchantServeItem drop column Id2;
alter table Sys_ApplicationAccess drop column Id2;
alter table Oca_MerchantServeMode drop column Id2;
alter table Pub_ServiceStation drop column Id2;
alter table Pub_Reminder drop column Id2;
alter table Oca_OldManFamilyInfo drop column Id2;
alter table Sys_WhiteList drop column Id2;
alter table Pub_Accounting_Period drop column Id2;
alter table Sys_ApplicationAccessibility drop column Id2;
alter table Oca_ServiceTrackLog drop column Id2;
alter table Log_SystemJob drop column Id2;
alter table Sys_Platform drop column Id2;
alter table Oca_CallService drop column Id2;
alter table Sys_Application drop column Id2;
alter table Oca_MerchantServePeriod drop column Id2;
alter table Sys_Menu drop column Id2;
alter table Pub_SmsSend drop column Id2;

select 'drop table '+name+';'
from sys.tables;

select * 
from Dey_Script;
select TableName from Cfg_Object where ObjectType='table' and ChangeType='not'

