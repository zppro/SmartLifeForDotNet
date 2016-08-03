exec msdb.dbo.sp_get_script @name='Oca_OldManBaseInfo'

select * from temp_test;

alter table temp_test add inputcode3 varchar(100) default(name)

alter table temp_test add name varchar(100) null;


select * from dbo.Oca_OldManBaseInfo;
xp_get_script

exec msdb.dbo.sp_get_composite_job_info;

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
 'EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'''+
 (select name from msdb.sys.servers where server_id=a.server_id)
--cast (server_id as varchar(10))
 +''''
 from msdb.dbo.sysjobservers a;
 
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

 
select * from msdb.dbo.sysjobs;
select name from master.sys.sql_logins where sid=1;
msdb.dbo.
select * from msdb.dbo.sysjobactivity;
select * from msdb.dbo.sysjobschedules;
select * from msdb.dbo.sysschedules a ,msdb.dbo.sysjobschedules b 
where a.schedule_id=b.schedule_id
select * from msdb.dbo.sysjobsteps;
select * from msdb.dbo.sysjobservers;
select * from msdb.sys.servers where server_id=0;
select name from msdb.dbo.syscategories where category_id=0;
exec SP_Tol_UspOutputData @tablename='Oca_OldManBaseInfo',@where='' 

select * from cfg_etl_translateRule;

select dbo.FUNC_Tol_OutputData('Oca_OldManBaseInfo','where id>10')

create table Oca_OldMan
(
OldManId uniqueidentifier not null,
Name nvarchar(64) not null,
IDNo char(18) not null,
Gender char(18) not null,
UrlHead varchar(255) null,
CheckInTime Datetime not null,
Status  TinyInt   default(1)   not null,
Mobile varchar(30)   null,
Remark Nvarchar(400) null,
 CONSTRAINT [PK_Oca_OldMan] PRIMARY KEY NONCLUSTERED 
(
	[OldManId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;

 CONSTRAINT [PK_Oca_OldMan] PRIMARY KEY NONCLUSTERED 
(
	[OldManId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId,        
   @name=N'refresh-call-service',     @enabled=1,     @freq_type=4,  
      @freq_interval=1,     @freq_subday_type=4,     @freq_subday_interval=3,    
       @freq_relative_interval=0,     @freq_recurrence_factor=0,    
        @active_start_date=20140218,     @active_end_date=99991231,   
          @active_start_time=0,     @active_end_time=235959,   
            @schedule_uid=N'9F5DBD42-CD37-49B9-B84C-EAA1DDCE75CF'
            
            select dbo.JoinStr(name) from sys.all_columns 
            where object_id=44 and is_nullable=0
            
            select * from sys.all_columns where is_nullable=0;
