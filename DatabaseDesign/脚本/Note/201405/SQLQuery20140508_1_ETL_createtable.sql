create table Cfg_ETL_Log
(
Id int not null primary key ,
ExecuteBeginTime datetime ,
ComputerName varchar(100),
UserName     varchar(100),
TaskName     varchar(100),  
EventName    varchar(100),
);

create table Cfg_ETL_Task
(
Id int not null primary key ,
TaskId int not null,
Name nvarchar(100) not null,
Enabled bit default(1) not null,
Description nvarchar(4000) null,
StartStepId int default(1) not null,
CategoryId  int default(1) not null  ,
Owner       int null,
Remark      nvarchar(400)  null,
DateCreated datetime default getdate() not null,
DateModiifed datetime null,
LastRunDate  datetime null,
LastRunTime  datetime null,
LastRunOutcome int null

);
alter table cfg_etl_task alter column remark nvarchar(400) null;

create table Cfg_ETL_TaskStep
(
Id      int not null primary key ,
StepId  int not null, 
StepName nvarchar(100) not null,
StepType nvarchar(10) not null,
SuccessProc varchar(100) null,
FailProc    varchar(100) null,
TaskId int not null
);
alter table cfg_etl_taskstep add TaskId int not null;

create table Cfg_ETL_InputTable
(
     Id int not null primary key ,
     TableId int not null,
     Name varchar(100) not null,
     TaskId int not null,
     LineageId int not null
);
create table Cfg_ETL_OutputTable
(
    Id int not null primary key ,
    TableId int not null,
     Name varchar(100) not null,
     TaskId int not null,
     LineageId int not null,
     ExternalMetadataTableId int null
);


create table Cfg_ETL_ExternalMetadataTable
(
    Id int not null primary key ,
    TableId int not null,
     Name varchar(100) not null,
     TaskId int not null,
     LineageId int not null
);

select name from sys.tables;
select  * from Cfg_ETL_ConnectManager;