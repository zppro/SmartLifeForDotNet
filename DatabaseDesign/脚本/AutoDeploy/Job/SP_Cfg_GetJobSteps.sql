USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Cfg_GetJobSteps]    Script Date: 05/13/2014 09:27:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************/
/* SP_Cfg_GetJob                                              */
/* 取系统的作业步骤的信息，存入配置库                         */
/* @source_dbName 源数据库名，@dest_dbName 目的数据库名       */
/**************************************************************/
CREATE PROCEDURE [dbo].[SP_Cfg_GetJobSteps]
@source_dbName nvarchar(100),
@dest_dbName nvarchar(100)=''
 AS
 BEGIN
 DECLARE  @sql_str nvarchar(4000) ,@databasename nvarchar(100)
     -- SET NOCOUNT ON
      select @databasename=replace(REPLACE(@source_dbName,'[',''''),']','''')
     
        IF (EXISTS (select * from Cfg_JobSteps ))
             BEGIN
                       
              delete from   Cfg_JobSteps;
             END
        
         IF (NOT EXISTS (select * from   Cfg_JobSteps))
             BEGIN
             
             if @dest_dbName <>''
             begin
                set @sql_str='   insert into cfg_jobsteps (JobId,StepId,StepName,CmdexecSuccessCode,OnSuccessAction,OnSuccessStepId,OnFailAction,OnfailStepId,RetryAttempts,RetryInterval,OsRunPriority,SubSystem,Command,DatabaseName,Flags)
								select JobId,StepId,StepName,CmdexecSuccessCode,OnSuccessAction,OnSuccessStepId,OnFailAction,OnfailStepId,RetryAttempts,RetryInterval,OsRunPriority,SubSystem,Command,DatabaseName,Flags
								from (
								 select a.job_id JobId,step_id StepId,step_name StepName,cmdexec_success_code CmdexecSuccessCode,
								 on_success_action OnSuccessAction,on_success_step_id OnSuccessStepId,
								 on_fail_action OnFailAction,on_fail_step_id OnfailStepId,
								 retry_attempts RetryAttempts,retry_interval RetryInterval,
								 os_run_priority OsRunPriority,SubSystem,Command Command,database_name DatabaseName,Flags,b.name
								 from '+@source_dbName+'.msdb.dbo.sysjobsteps  a,'+@source_dbName+'.msdb.dbo.sysjobs b
								 where a.job_id=b.job_id )m
								 where m.name+m.StepName not in
								 ( select  b.name+a.step_name
								 from '+@dest_dbName+'.msdb.dbo.sysjobsteps  a,'+@dest_dbName+'.msdb.dbo.sysjobs b
								 where a.job_id=b.job_id);'     
		       end
		       
		       if @dest_dbName=''
		       begin
		            set @sql_str=' insert into cfg_jobsteps (JobId,StepId,StepName,CmdexecSuccessCode,OnSuccessAction,OnSuccessStepId,OnFailAction,OnfailStepId,RetryAttempts,RetryInterval,OsRunPriority,SubSystem,Command,DatabaseName,Flags)
									 select job_id JobId,step_id StepId,step_name StepName,cmdexec_success_code CmdexecSuccessCode,
									 on_success_action OnSuccessAction,on_success_step_id OnSuccessStepId,
									 on_fail_action OnFailAction,on_fail_step_id OnfailStepId,
									 retry_attempts RetryAttempts,retry_interval RetryInterval,
									 os_run_priority OsRunPriority,SubSystem,Command Command,database_name DatabaseName,Flags
									 from '+@source_dbName+'.msdb.dbo.sysjobsteps;
		                          '     
		       end
		                   
		   --select @sql_str
                exec sp_executesql @sql_str 
             END         
 END
GO

