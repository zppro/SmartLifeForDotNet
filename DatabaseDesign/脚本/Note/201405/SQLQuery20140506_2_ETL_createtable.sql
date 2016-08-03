select object_id,referenced_major_id
,(select name from sys.all_objects where object_id=referenced_major_id) name
 from sys.sql_dependencies 
where object_id =(select object_id from sys.all_objects where name='SP_ETL_DataStreamTask')
order by object_id;

select object_id from sys.all_objects where name='SP_ETL_ExpData';

日志记录：
执行开始时间，
计算机名称，
用户名称，
任务名称，
事件名称，

create table Cfg_ETL_Log
(
Id int not null primary key ,
ExecuteBeginTime datetime ;
ComputerName varchar(100),
UserName     varchar(100),
TaskName     varchar(100),  
EventName    varchar(100),
);


select 'select 
cast(count('+name+') as float)/COUNT(OldManId) value,'''+name+''' columnname,''oca_oldman'' tablename
 from oca_oldman union' 
 from sys.all_columns 
 where object_id=(select object_id from sys.tables where name='oca_oldman')
 
select 
cast(count(remark) as float)/COUNT(OldManId) value,'remark' columnname,'oca_oldman' tablename
 from oca_oldman
 
 create table Cfg_ETL_ColumnNullRatio
(
TableName   varchar(100) not null,
ColumnName  varchar(100) not null,
Value      float not null
);

insert into Cfg_ETL_ColumnNullRatio(Value,ColumnName,TableName)
select a.* from (
select   cast(count(OldManId) as float)/COUNT(OldManId) value,'OldManId' columnname,'oca_oldman' tablename   from oca_oldman union
select   cast(count(Name) as float)/COUNT(OldManId) value,'Name' columnname,'oca_oldman' tablename   from oca_oldman union
select   cast(count(IDNo) as float)/COUNT(OldManId) value,'IDNo' columnname,'oca_oldman' tablename   from oca_oldman union
select   cast(count(Gender) as float)/COUNT(OldManId) value,'Gender' columnname,'oca_oldman' tablename   from oca_oldman union
select   cast(count(UrlHead) as float)/COUNT(OldManId) value,'UrlHead' columnname,'oca_oldman' tablename   from oca_oldman union
select   cast(count(CheckInTime) as float)/COUNT(OldManId) value,'CheckInTime' columnname,'oca_oldman' tablename   from oca_oldman union
select   cast(count(Status) as float)/COUNT(OldManId) value,'Status' columnname,'oca_oldman' tablename   from oca_oldman union
select   cast(count(Mobile) as float)/COUNT(OldManId) value,'Mobile' columnname,'oca_oldman' tablename   from oca_oldman union
select   cast(count(Remark) as float)/COUNT(OldManId) value,'Remark' columnname,'oca_oldman' tablename   from oca_oldman 
) a;

select * from Cfg_ETL_ColumnNullRatio;

alter table SP_ETL_CheckBusiness add Id  int not null identity(1,1)  primary key;

select 'alter table '+name+' add constraint PK_'+name+'_Id primary key(Id);' from sys.tables where name like '%Cfg%'
select 'alter table '+name+' alter column id int not null ; ' from sys.tables where name like '%Cfg%'
消息 4909，级别 16，状态 1，第 1 行
无法更改 'SP_ETL_CheckBusiness'，因为它不是表。

消息 2705，级别 16，状态 4，第 5 行
各表中的列名必须唯一。在表 'Cfg_ETL_ConnectManager' 中多次指定了列名 'Id'。

alter table Cfg_ETL_TranslateRule alter column id int not null ; 
alter table Cfg_ETL_Procedure alter column id int not null ; 
alter table Cfg_ETL_Variable alter column id int not null ; 
alter table Cfg_ETL_ColumnNullRatio alter column id int not null ; 
alter table Cfg_ETL_ConnectManager alter column id int not null ; 
alter table Cfg_ETL_Directory alter column id int not null ; 
alter table Cfg_ETL_OutputColumn alter column id int not null ; 
alter table Cfg_ETL_InputColumn alter column id int not null ; 
alter table Cfg_ETL_ExternalMetadataColumn alter column id int not null ; 
alter table Cfg_ETL_Package alter column id int not null ; 
alter table Cfg_ETL_Component alter column id int not null ; 
alter table Cfg_ETL_TypeMapping alter column id int not null ; 
alter table Cfg_ETL_TypeMappingFromToDatabase alter column id int not null ; 
alter table Cfg_ETL_TypeConvert alter column id int not null ;  

alter table Cfg_ETL_TranslateRule add constraint PK_Cfg_ETL_TranslateRule_Id  primary key(Id);
alter table Cfg_ETL_TranslateRule add constraint PK_Cfg_ETL_TranslateRule_Id primary key(Id);
alter table Cfg_ETL_Procedure add constraint PK_Cfg_ETL_Procedure_Id primary key(Id);
alter table Cfg_ETL_Variable add constraint PK_Cfg_ETL_Variable_Id primary key(Id);
alter table Cfg_ETL_ColumnNullRatio add constraint PK_Cfg_ETL_ColumnNullRatio_Id primary key(Id);
alter table Cfg_ETL_ConnectManager add constraint PK_Cfg_ETL_ConnectManager_Id primary key(Id);
alter table Cfg_ETL_Directory add constraint PK_Cfg_ETL_Directory_Id primary key(Id);
alter table Cfg_ETL_OutputColumn add constraint PK_Cfg_ETL_OutputColumn_Id primary key(Id);
alter table Cfg_ETL_InputColumn add constraint PK_Cfg_ETL_InputColumn_Id primary key(Id);
alter table Cfg_ETL_ExternalMetadataColumn add constraint PK_Cfg_ETL_ExternalMetadataColumn_Id primary key(Id);
alter table Cfg_ETL_Package add constraint PK_Cfg_ETL_Package_Id primary key(Id);
alter table Cfg_ETL_Component add constraint PK_Cfg_ETL_Component_Id primary key(Id);
alter table Cfg_ETL_TypeMapping add constraint PK_Cfg_ETL_TypeMapping_Id primary key(Id);
alter table Cfg_ETL_TypeMappingFromToDatabase add constraint PK_Cfg_ETL_TypeMappingFromToDatabase_Id primary key(Id);
alter table Cfg_ETL_TypeConvert add constraint PK_Cfg_ETL_TypeConvert_Id primary key(Id);

ALTER TABLE Production.TransactionHistoryArchive WITH NOCHECK 
ADD CONSTRAINT PK_TransactionHistoryArchive_TransactionID PRIMARY KEY CLUSTERED (TransactionID)
WITH (FILLFACTOR = 75, ONLINE = ON, PAD_INDEX = ON);
