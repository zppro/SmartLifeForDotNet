select * from Temp_ExcelSource;
select * from dbo.sheetSource

create table Cfg_Job
(
Name                   nvarchar(128)    not null,
Enabled                tinyint          not null,
NotifyLevelEventLog    int              not null,
NotifyLevelEmail       int              not null,
NotifyLevelNetSend     int              not null,
NotifyLevelPage        int              not null,
Description            nvarchar(512)        null,
CategoryName           nvarchar(128)    not null,
OwnerLoginName         nvarchar(128)    not null,
JobId                  uniqueidentifier not null
)

create table Cfg_JobSteps
(
JobId                 uniqueidentifier  not null,
StepId                int               not null,
StepName              nvarchar(128)     not null,
CmdexecSuccessCode    int               not null,
OnSuccessAction       tinyint           not null,
OnSuccessStepId       int               not null,
OnFailAction          tinyint           not null,
OnfailStepId          int               not null,
RetryAttempts         int               not null,
RetryInterval         int               not null,
OsRunPriority         int               not null,
SubSystem             nvarchar(40)      not null,
Command               nvarchar(max)         null,
DatabaseName          nvarchar(128)     not null,
Flags                 int               not null
)


create table Cfg_JobServer
(
JobId                  uniqueidentifier  not null,  
ServerName             nvarchar(128)     not null
)

create table Cfg_JobSchedule
(
JobId                 uniqueidentifier  not null,
Name                  nvarchar(128)     not null,
Enabled               int               not null, 
FreqInterval          int               not null,
FreqSubDayType        int               not null,
FreqRelativeInterval  int               not null,
FreqRecurrenceFactor  int               not null,
ActiveStartDate       int               not null,
ActiveEndDate         int               not null,
ActiveStartTime       int               not null,
ActiveEndTime         int               not null,
ScheduleUid           uniqueidentifier  not null
)

select a.job_id,a.name,a.enabled,a.description
from 
(exec msdb.dbo.sp_get_composite_job_info) a;

select 'EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=N'''+name+''', 
		@enabled='+cast(enabled as varchar(10))+', 
		@notify_level_eventlog='+cast(notify_level_eventlog as varchar(10)) +', 
		@notify_level_email='+cast(notify_level_email as varchar(10))+', 
		@notify_level_netsend='+cast(notify_level_netsend as varchar(10))+', 
		@notify_level_page='+cast(notify_level_page as varchar(10))+', 
		@delete_level='+cast(delete_level as varchar(10))+', 
		@description=N'''+description+''', 
		@category_name=N'''+(select name from msdb.dbo.syscategories where category_id=0)+''', 
		@owner_login_name=N'''+(select name from master.sys.sql_logins where sid=1)+''', @job_id = @jobId OUTPUT'
 from msdb.dbo.sysjobs_view where category_id=0 
 
 select * from Cfg_Job where Name ='';
 
 select 'EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=N'''+name+''', 
		@enabled='+cast(enabled as varchar(10))+', 
		@notify_level_eventlog='+cast(NotifyLevelEventlog as varchar(10)) +', 
		@notify_level_email='+cast(NotifyLevelEmail as varchar(10))+', 
		@notify_level_netsend='+cast(NotifyLevelNetsend as varchar(10))+', 
		@notify_level_page='+cast(NotifyLevelPage as varchar(10))+', 
		@delete_level='+cast(DeleteLevel as varchar(10))+', 
		@description=N'''+description+''', 
		@category_name=N'''+CategoryName+''', 
		@owner_login_name=N'''+OwnerLoginName+''', @job_id = @jobId OUTPUT'
 from Cfg_Job where Name='syspolicy_purge_history'
 
 alter table cfg_job add DeleteLevel int default 0 not null;
 insert into cfg_job(Name,Enabled,NotifyLevelEventLog,NotifyLevelEmail,NotifyLevelNetSend,NotifyLevelPage,Description,CategoryName,OwnerLoginName,JobId)
 select Name,Enabled,notify_level_eventlog NotifyLevelEventLog,
 notify_level_email NotifyLevelEmail,notify_level_netsend NotifyLevelNetSend,
 notify_level_page NotifyLevelPage,Description,(select name from msdb.dbo.syscategories where category_id=0) CategoryName,
 (select name from master.sys.sql_logins where sid=1) OwnerLoginName,job_id JobId
 from  msdb.dbo.sysjobs_view where category_id=0  and name not in (select name from  remote_dbo.msdb.dbo.sysjobs_view where category_id=0)
 
 select   dbo.joinStr(name)
from    sys.all_columns 
where  object_id   in 
            (   select  object_id 
                from    sys.tables 
               where  name='cfg_jobserver'
             )


select * from cfg_etl_translaterule;


 select 
    'EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'''+step_name+''', 
		@step_id='+cast(step_id as varchar(10)) +', 
		@cmdexec_success_code='+cast(cmdexec_success_code as varchar(10)) +', 
		@on_success_action='+cast(on_success_action as varchar(10)) +', 
		@on_success_step_id='+cast(on_success_step_id as varchar(10)) +', 
		@on_fail_action='+cast(on_fail_action as varchar(10)) +', 
		@on_fail_step_id='+cast(on_fail_step_id as varchar(10)) +', 
		@retry_attempts='+cast(retry_attempts as varchar(10)) +', 
		@retry_interval='+cast(retry_interval as varchar(10)) +', 
		@os_run_priority='+cast(os_run_priority as varchar(10)) +', @subsystem=N'''+subsystem+''', 
		@command=N'''+replace(command,'''','''''')+''', 
		@database_name=N'''+database_name+''', 
		@flags='++cast(flags as varchar(10)) ++''
 from msdb.dbo.sysjobsteps;
 
  select 
    'EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'''+stepname+''', 
		@step_id='+cast(stepid as varchar(10)) +', 
		@cmdexec_success_code='+cast(cmdexecsuccesscode as varchar(10)) +', 
		@on_success_action='+cast(onsuccessaction as varchar(10)) +', 
		@on_success_step_id='+cast(onsuccessstepid as varchar(10)) +', 
		@on_fail_action='+cast(onfailaction as varchar(10)) +', 
		@on_fail_step_id='+cast(onfailstepid as varchar(10)) +', 
		@retry_attempts='+cast(retryattempts as varchar(10)) +', 
		@retry_interval='+cast(retryinterval as varchar(10)) +', 
		@os_run_priority='+cast(osrunpriority as varchar(10)) +', @subsystem=N'''+subsystem+''', 
		@command=N'''+replace(command,'''','''''')+''', 
		@database_name=N'''+DatabaseName+''', 
		@flags='++cast(flags as varchar(10)) ++''
 from cfg_jobsteps where JobId='F097CE18-428E-47E9-B2C0-82338939A4CF';
 
 insert into cfg_jobsteps (JobId,StepId,StepName,CmdexecSuccessCode,OnSuccessAction,OnSuccessStepId,OnFailAction,OnfailStepId,RetryAttempts,RetryInterval,OsRunPriority,SubSystem,Command,DatabaseName,Flags)
select JobId,StepId,StepName,CmdexecSuccessCode,OnSuccessAction,OnSuccessStepId,OnFailAction,OnfailStepId,RetryAttempts,RetryInterval,OsRunPriority,SubSystem,Command,DatabaseName,Flags
from (
 select a.job_id JobId,step_id StepId,step_name StepName,cmdexec_success_code CmdexecSuccessCode,
 on_success_action OnSuccessAction,on_success_step_id OnSuccessStepId,
 on_fail_action OnFailAction,on_fail_step_id OnfailStepId,
 retry_attempts RetryAttempts,retry_interval RetryInterval,
 os_run_priority OsRunPriority,SubSystem,Command Command,database_name DatabaseName,Flags,b.name
 from msdb.dbo.sysjobsteps  a,msdb.dbo.sysjobs b
 where a.job_id=b.job_id )m
 where m.name+m.StepName not in
 ( select  b.name+a.step_name
 from remote_dbo.msdb.dbo.sysjobsteps  a,remote_dbo.msdb.dbo.sysjobs b
 where a.job_id=b.job_id);
 
 
 
  select 'EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, 
        @name=N'''+a.name+''', 
		@enabled='+cast(a.enabled as varchar(10)) +', 
		@freq_type='+cast(a.freq_type as varchar(10)) +', 
		@freq_interval='+cast(a.freq_interval as varchar(10)) +', 
		@freq_subday_type='+cast(a.freq_subday_type as varchar(10)) +', 
		@freq_subday_interval='+cast(a.freq_subday_interval as varchar(10)) +', 
		@freq_relative_interval='+cast(a.freq_relative_interval as varchar(10)) +', 
		@freq_recurrence_factor='+cast(a.freq_recurrence_factor as varchar(10)) +', 
		@active_start_date='+cast(a.active_start_date as varchar(10)) +', 
		@active_end_date='+cast(a.active_end_date as varchar(10)) +', 
		@active_start_time='+cast(a.active_start_time as varchar(10)) +', 
		@active_end_time='+cast(a.active_end_time as varchar(10)) +', 
		@schedule_uid=N'''+cast(a.schedule_uid as varchar(40))+'''' 
 from msdb.dbo.sysschedules a ,msdb.dbo.sysjobschedules b 
where a.schedule_id=b.schedule_id;

select   'EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, 
        @name=N'''+a.name+''', 
		@enabled='+cast(a.enabled as varchar(10)) +', 
		@freq_type='+cast(a.freqtype as varchar(10)) +', 
		@freq_interval='+cast(a.freqinterval as varchar(10)) +', 
		@freq_subday_type='+cast(a.freqsubdaytype as varchar(10)) +', 
		@freq_subday_interval='+cast(a.freqsubdayinterval as varchar(10)) +', 
		@freq_relative_interval='+cast(a.freqrelativeinterval as varchar(10)) +', 
		@freq_recurrence_factor='+cast(a.freqrecurrencefactor as varchar(10)) +', 
		@active_start_date='+cast(a.activestartdate as varchar(10)) +', 
		@active_end_date='+cast(a.activeenddate as varchar(10)) +', 
		@active_start_time='+cast(a.activestarttime as varchar(10)) +', 
		@active_end_time='+cast(a.activeendtime as varchar(10)) +', 
		@schedule_uid=N'''+cast(a.scheduleuid as varchar(40))+'''' 
from Cfg_JobSchedule a where JobId=@JobId


alter table cfg_jobschedule add freqtype int null;
alter table cfg_jobschedule add freqsubdayinterval int null;

insert into cfg_jobschedule (JobId,Name,Enabled,FreqInterval,FreqSubDayType,FreqRelativeInterval,FreqRecurrenceFactor,ActiveStartDate,ActiveEndDate,ActiveStartTime,ActiveEndTime,ScheduleUid)
 select JobId,Name,Enabled,FreqInterval,FreqSubDayType,FreqRelativeInterval,FreqRecurrenceFactor,ActiveStartDate,ActiveEndDate,ActiveStartTime,ActiveEndTime,ScheduleUid
 from (
 select b.job_id JobId,a.Name,a.Enabled,a.freq_interval FreqInterval,a.freq_subday_type FreqSubDayType
 ,a.freq_relative_interval FreqRelativeInterval
 ,a.freq_recurrence_factor FreqRecurrenceFactor,a.active_start_date ActiveStartDate,a.active_end_date ActiveEndDate
 ,a.active_start_time ActiveStartTime,a.active_end_time ActiveEndTime,a.schedule_uid ScheduleUid,c.name jobname
 from msdb.dbo.sysschedules a ,msdb.dbo.sysjobschedules b ,msdb.dbo.sysjobs c
where a.schedule_id=b.schedule_id  and b.job_id=c.job_id) m
where m.jobname+m.name not in 
( select c.name+a.Name
 from msdb.dbo.sysschedules a ,msdb.dbo.sysjobschedules b ,msdb.dbo.sysjobs c
where a.schedule_id=b.schedule_id  and b.job_id=c.job_id);


 select
 'EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'''+
 (select name from msdb.sys.servers where server_id=a.server_id)
--cast (server_id as varchar(10))
 +''''
 from msdb.dbo.sysjobservers a;
 
 select 'EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'''+a.ServerName+''''
 from cfg_jobserver a where JobId=@jobid
 
 insert into cfg_jobserver(JobId,ServerName)
 select m.JobId,m.ServerName
 from (
 select a.job_id JobId, (select name from msdb.sys.servers where server_id=a.server_id) ServerName
 ,c.name
 from   msdb.dbo.sysjobservers a,msdb.dbo.sysjobs c
 where a.job_id=c.job_id) m
 where m.name not in (
 select name from --remote_dbo
 .msdb.dbo.sysjobs
 )
 ;
 
 
 在109机器上的OutputData 加上where 条件的参数的存储过程如下调用
exec SP_Dey_OutputData @tablename='Cfg_Bridge',@where='where NodeId>10' 

select * from Cfg_Bridge where NodeId>10;

select Name,JobId from Cfg_Job;