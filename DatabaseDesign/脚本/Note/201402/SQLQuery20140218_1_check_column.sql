--------在源库，不在目的库的表
select name from [smartlife-120301].sys.tables where name not in (select name from sys.tables )

--create table dd();

----------在目的
--order by table_name,column_name order by table_name,column_name
--------------------------------源库与目标库完全相同的表与其字段
select e.table_name+e.column_name pri_key
from (
select b.name  table_name,
a.name column_name,a.system_type_id,a.max_length,a.is_nullable from [smartlife-120301].sys.columns a right join [smartlife-120301].sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) e inner join
(
select b.name  table_name,
a.name column_name,a.system_type_id,a.max_length,a.is_nullable from sys.columns a right join sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) f on
 e.table_name=f.table_name and e.column_name=f.column_name and e.system_type_id=f.system_type_id and e.max_length=f.max_length
 and e.is_nullable=f.is_nullable

-------------------------------------在源库，不在目的库的表的字段的详细信息
insert into dbo.t_deploy_object_column_detail (database_name,user_name,table_name,column_name,system_type_id,max_length,
is_null,object_type,change_type,version_id,function_id)
select  DB_NAME() as database_name,user_name() as user_name,
e.table_name,e.column_name,e.system_type_id,e.max_length,
e.is_nullable,'column' object_type,'add' change_type,3 version_id,2 function_id
from (
select b.name  table_name,
a.name column_name,a.system_type_id,a.max_length,a.is_nullable from [smartlife-120301].sys.columns a right join [smartlife-120301].sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) e 
where e.table_name+e.column_name not in
(
select b.name+a.name column_name
--,a.system_type_id,a.max_length,a.is_nullable 
from sys.columns a right join sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) 

------------------------------源库与目的库的 表及字段是相同的，但是类型或者精度是不同的。
insert into dbo.t_deploy_object_column_detail (database_name,user_name,table_name,column_name,system_type_id,max_length,
is_null,system_type_id_old,max_length_old,
is_null_old,object_type,change_type,version_id,function_id)
select  DB_NAME() as database_name,user_name() as user_name,
m.table_name,m.column_name,m.system_type_id,m.max_length,
m.is_nullable,n.system_type_id system_type_id_old,n.max_length max_length_old,n.is_nullable is_null_old,
'column' object_type,'alter' change_type,3 version_id,2 function_id
from (
select b.name  table_name,
a.name column_name,a.system_type_id,a.max_length,a.is_nullable from [smartlife-120301].sys.columns a right join [smartlife-120301].sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) m,(
select b.name+a.name column_name
,a.system_type_id,a.max_length,a.is_nullable 
from sys.columns a right join sys.tables b on b.object_id=a.object_id
and b.object_id>0 )  n
where m.table_name+m.column_name =n.column_name
and m.table_name+m.column_name not in (
select e.table_name+e.column_name pri_key
from (
select b.name  table_name,
a.name column_name,a.system_type_id,a.max_length,a.is_nullable from [smartlife-120301].sys.columns a right join [smartlife-120301].sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) e inner join
(
select b.name  table_name,
a.name column_name,a.system_type_id,a.max_length,a.is_nullable from sys.columns a right join sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) f on
 e.table_name=f.table_name and e.column_name=f.column_name and e.system_type_id=f.system_type_id and e.max_length=f.max_length
 and e.is_nullable=f.is_nullable
);

--------------------------
ALTER TABLE t_deploy_execution_log alter column table_name nvarchar(41) NOT NULL;



select count(b.name),b.object_id from sys.tables b where b.object_id>0 group by b.object_id
select distinct OBJECT_ID from sys.columns a where object_id>0;


-------------------------------------在目的库，不在源库的表的字段的详细信息
insert into dbo.t_deploy_object_column_detail (database_name,user_name,table_name,column_name,system_type_id,max_length,
is_null,object_type,change_type,version_id,function_id)
select  DB_NAME() as database_name,user_name() as user_name,
e.table_name,e.column_name,e.system_type_id,e.max_length,
e.is_nullable,'column' object_type,'drop' change_type,3 version_id,2 function_id
from (
select b.name  table_name,
a.name column_name,a.system_type_id,a.max_length,a.is_nullable from sys.columns a right join sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) e 
where e.table_name+e.column_name not in
(
select b.name+a.name column_name
--,a.system_type_id,a.max_length,a.is_nullable 
from [smartlife-120301].sys.columns a right join [smartlife-120301].sys.tables b on b.object_id=a.object_id
and b.object_id>0 ) 


alter table t_deploy_object_column_detail add system_type_id_old int null;
alter table t_deploy_object_column_detail add max_length_old int null;
alter table t_deploy_object_column_detail add is_null_old int null;

select * from t_deploy_object;

IF (OBJECT_ID(N'dbo.t_deploy_object_column_detail', 'U') IS NULL)
BEGIN
   PRINT ''
   PRINT 'Creating table t_deploy_object_column_detail ...'
create table t_deploy_object_column_detail
(
id             int identity(1,1)  not null,
database_name  nvarchar(20)       not null,
user_name      nvarchar(30)       not null,
table_name     nvarchar(60)       not null,
column_name    varchar(60)        null,
system_type_id int                null,
max_length     int                null,
is_null        int                null,
object_type    nvarchar(20)       not null,
change_type    nvarchar(20)       not null,
version_id     int                not null,
function_id    int                not null
)
END
go

insert into t_deploy_script (version_id,update_script,recover_script,update_validate_script,recover_validate_script)
select 3 version_id, 'alter table '+table_name+' ' +change_type+ ' '+case change_type when 'alter' then ' column' else '' end 
+' '+a.column_name+' ' +
(select  name from sys.systypes where xusertype=a.system_type_id )+
case a.system_type_id when 167 then replace('('+CAST (a.max_length as nvarchar(32))+')','-1','max')
when 175 then '('+CAST (a.max_length as nvarchar(32))+')'
when 231 then  '('+replace(left(cast(a.max_length/2 as nvarchar(32)),1),'0','max')+SUBSTRING(cast(a.max_length/2 as nvarchar(32)),2,LEN(cast(a.max_length/2 as nvarchar(32))))+')' else '' end +
' ' +case a.is_null when 1 then ' null ' when 0 then ' not null ' else '' end +' ;' as columname
--,(select  name from sys.systypes where xusertype=a.system_type_id ) data_type
,'alter table '+table_name+' '+case change_type when 'alter' then ' alter column' else ' drop column ' end 
+' '+a.column_name+' ;'   recover_script,'select 1' update_validate_script,'select 1' recover_validate_script
from t_deploy_object_column_detail a where change_type ='add' or change_type='alter';

select * from sys.systypes where xtype=xusertype

select a.* from t_deploy_object_column_detail a;
--alter table t_deploy_object_column_detail drop column  id int null) ;
alter table t_deploy_object_column_detail drop column is_null ;--int  not null ;

alter table Pub_DeviceExtendInfo alter column ExtendValue nvarchar(max)  not null ;
alter table t_deploy_orderformat add do_person nvarchar(20)  not null ;


--------------------------============================
alter table Pub_DeviceExtendInfo alter column ExtendValue nvarchar(max)  null  ;
alter table Pub_MultiLanguageStorageBig alter column CurrentValue nvarchar(max)  not null  ;
alter table Pub_Group alter column GroupName nvarchar(30)  not null  ;
alter table Pub_Group alter column Description nvarchar(400)  null  ;
alter table Pub_EntityDefinationHistory alter column DefinationValue nvarchar(max)  null  ;
alter table Pub_EntityDefination alter column DefinationValue nvarchar(max)  null  ;
alter table Pub_Device alter column DeviceName nvarchar(100)  null  ;
alter table Pub_Attachment alter column OriginName nvarchar(500)  not null  ;
alter table Pub_Attachment alter column SaveThumbName nvarchar(500)  null  ;
alter table Pub_Area alter column AreaName nvarchar(100)  null  ;
alter table Oca_ServiceWorkOrder alter column WorkOrderContent nvarchar(4000)  not null  ;
alter table Oca_ServiceWorkOrder alter column OtherCharges nvarchar(100)  null  ;
alter table Oca_ServiceWorkOrder alter column RemarkRequired nvarchar(400)  null  ;
alter table Oca_ServiceWorkOrder alter column OldManName nvarchar(20)  not null  ;
alter table Oca_ServiceWorkOrder alter column Address nvarchar(500)  null  ;
alter table Oca_ServiceWorkOrder alter column ServeManName nvarchar(30)  null  ;
alter table Oca_ServiceWorkOrder alter column ServeResultRemark nvarchar(400)  null  ;
alter table Oca_ServiceWorkOrder alter column FeedbackRemarkToOldMan nvarchar(500)  null  ;
alter table Oca_ServiceWorkOrder alter column ServeFinalResultRemark nvarchar(400)  null  ;
alter table Oca_OldManHealthInfo alter column BloodType nvarchar(10)  null  ;
alter table Oca_OldManHealthInfo alter column PastMedicalHistory nvarchar(max)  null  ;
alter table Oca_OldManHealthInfo alter column PastallergicHistory nvarchar(max)  null  ;
alter table Oca_OldManHealthInfo alter column PhysicalExamination nvarchar(max)  null  ;
alter table sysdiagrams alter column name nvarchar(128)  not null  ;
alter table Oca_OldManBaseInfo alter column OldManName nvarchar(20)  not null  ;
alter table Oca_OldManBaseInfo alter column Remark nvarchar(400)  null  ;
alter table Oca_MutualAidPerson alter column MutualAidPersonName nvarchar(20)  not null  ;
alter table Oca_MutualAidPerson alter column Address nvarchar(200)  null  ;
alter table Oca_MutualAidPerson alter column Remark nvarchar(400)  null  ;
alter table Sys_Behavior alter column BehaviorName nvarchar(30)  not null  ;
alter table sTV_00$01$02 alter column Name nvarchar(100)  null  ;
alter table Oca_FamilyMember alter column FamilyMemberName nvarchar(10)  not null  ;
alter table Oca_FamilyCamera alter column DeviceName nvarchar(100)  not null  ;
alter table Oca_FamilyCamera alter column Remark nvarchar(400)  null  ;
alter table Log_SystemJob alter column Remark nvarchar(500)  not null  ;
alter table Sys_Platform alter column PlatformName nvarchar(100)  not null  ;
alter table Sys_Platform alter column AliasName nvarchar(100)  null  ;
alter table Sys_Platform alter column Description nvarchar(400)  null  ;
alter table Sys_Server alter column ServerName nvarchar(100)  not null  ;
--alter table t_deploy_script alter column update_script nvarchar(4000)  not null  ;
alter table Oca_CallService alter column Content nvarchar(500)  null  ;
alter table Oca_CallService alter column DoResults nvarchar(1000)  null  ;
alter table Inc_SyncIn_Resident alter column ResidentName nvarchar(20)  not null  ;
alter table Inc_SyncIn_Resident alter column Address nvarchar(200)  null  ;
alter table Inc_SyncIn_Resident alter column Remark nvarchar(400)  null  ;
alter table Inc_SyncIn_Resident alter column RelationWithFamilyHead nvarchar(20)  null  ;
alter table t_deploy_object alter column table_name nvarchar(60)  not null  ;
alter table Sys_TreeItem alter column ItemName nvarchar(100)  null  ;
alter table Sys_TreeItem alter column GenerateContent nvarchar(max)  not null  ;
alter table Sys_TreeItem alter column Description nvarchar(400)  null  ;
alter table Sys_Tree alter column TreeParamFormat nvarchar(500)  null  ;
alter table Sys_Tree alter column RootNodeFormat nvarchar(30)  null  ;
alter table Sys_Tree alter column RootNodeIconCls nvarchar(30)  null  ;
alter table Sys_Tree alter column Description nvarchar(400)  null  ;
alter table Sys_Parameter alter column ParameterName nvarchar(100)  not null  ;
alter table Sys_Parameter alter column Description nvarchar(400)  null  ;
alter table Sys_Menu alter column MenuName nvarchar(100)  not null  ;
alter table Sys_Menu alter column Description nvarchar(400)  null  ;
alter table Sys_Dictionary alter column Description nvarchar(400)  null  ;
alter table Sys_ApplicationAccessPoint alter column AccessPointName nvarchar(100)  not null  ;
alter table Sys_Application alter column ApplicationName nvarchar(100)  not null  ;
alter table Sys_Application alter column AliasName nvarchar(100)  null  ;
alter table Sys_Application alter column Description nvarchar(400)  null  ;
alter table Pub_User alter column UserName nvarchar(30)  not null  ;
alter table Pub_SmsSend alter column SendContent nvarchar(60)  not null  ;
alter table Pub_ServiceStation alter column StationName nvarchar(30)  not null  ;
alter table Pub_ServiceStation alter column Address nvarchar(200)  null  ;
alter table Pub_ServiceStation alter column Intro nvarchar(400)  null  ;
alter table Pub_ServiceStation alter column Remark nvarchar(400)  null  ;
alter table Pub_Attachment alter column SaveName nvarchar(500)  not null  ;
alter table Oca_ServiceWorkOrder alter column ServeStationName nvarchar(30)  null  ;
alter table Oca_ServiceWorkOrder alter column FeedbackRemarkToServiceStation nvarchar(500)  null  ;
alter table Oca_ServiceTrackLog alter column LogContent nvarchar(1000)  null  ;
alter table Oca_OldManBaseInfo alter column Address nvarchar(200)  null  ;
alter table sTV_00$01$01 alter column Name nvarchar(100)  null  ;
alter table Oca_FamilyMember alter column Address nvarchar(200)  null  ;
alter table Oca_FamilyMember alter column Remark nvarchar(400)  null  ;
alter table Log_SystemJob alter column JobName nvarchar(100)  not null  ;
alter table Sys_Tree alter column TreeName nvarchar(100)  not null  ;
alter table Sys_Dictionary alter column DictionaryName nvarchar(100)  not null  ;
alter table Pub_Reminder alter column RemindContent nvarchar(100)  null  ;
alter table Pub_MultiLanguageStorageSmall alter column CurrentValue nvarchar(500)  not null  ;
alter table t_deploy_bridge add  database_name varchar(20)  null  ;
alter table t_deploy_orderformat add  content nvarchar(100)  null  ;
alter table t_deploy_orderformat add  do_person nvarchar(20)  null  ;
alter table t_deploy_orderformat add  finish_time datetime  null  ;
alter table t_deploy_orderformat add  level int  null  ;
alter table t_deploy_orderformat add  name nvarchar(30)  null  ;
alter table t_deploy_orderformat add  order_time datetime  null  ;
alter table t_deploy_orderformat add  owner_person nvarchar(20)  null  ;
alter table t_deploy_orderformat add  response_person nvarchar(20)  null  ;
alter table t_deploy_orderformat add  start_time datetime  null  ;