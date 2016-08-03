exec sp_help_job
exec sp_help_jobstep @job_id='9FE402B2-C664-4C61-96B7-59FE57BFABEA';

create table Cfg_ETL_Task
(
Id int not null primary key ,
Name nvarchar(100) not null,
Enabled bit default(1) not null,
Description nvarchar(4000) null,
StartStepId int default(1) not null,
CategoryId  int default(1) not null  ,
Owner       int null,
Remark      nvarchar(400) not null,
DateCreated datetime default getdate() not null,
DateModified datetime null,
LastRunDate  datetime null,
LastRunTime  datetime null,
LastRunOutcome int null

);
create table Cfg_ETL_TaskStep
(
Id      int not null primary key ,
StepId  int not null, 
StepName nvarchar(100) not null,
StepType nvarchar(10) not null,
SuccessProc varchar(100) null,
FailProc    varchar(100) null
);

select * from sys.tables where name like '%Family%'
select * from dbo.Cfg_ETL_TranslateRule;


