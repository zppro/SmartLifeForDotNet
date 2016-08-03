USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Dey_CreateJobScriptProc]    Script Date: 05/13/2014 09:28:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************************************/
/*  SP_Dey_CreateJobScriptProc */
/*  生成创建作业的脚本 */

/**************************************************************************************/

CREATE    PROCEDURE [dbo].[SP_Dey_CreateJobScriptProc]
 AS
 BEGIN
 declare @Name nvarchar(100),@JobId uniqueidentifier ,@Script nvarchar(max),@scriptTemp nvarchar(max),@scripthead nvarchar(max)
          ,@scripttail nvarchar(max)
--SET NOCOUNT ON
set @scripthead='BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT @ReturnCode = 0
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N''[Uncategorized (Local)]'' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N''JOB'', @type=N''LOCAL'', @name=N''[Uncategorized (Local)]''
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

DECLARE @jobId BINARY(16)'
set @scripttail='IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
    IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:

'
set @Script=''
set @scriptTemp=''
        IF (EXISTS (select * from Dey_Script where Type='J'))
             BEGIN
               delete from Dey_Script where Type='J';        
             END
       

             			DECLARE create_table_cursor CURSOR FOR 
						select Name,JobId from Cfg_Job;
						 
						OPEN create_table_cursor
						 
						FETCH NEXT FROM create_table_cursor 
						INTO  @Name,@JobId
						 
						WHILE @@FETCH_STATUS = 0
						 BEGIN
						 
						          set @Script=''
                                  set @scriptTemp=''
                                  select @scriptTemp='EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=N'''+name+''', 
									@enabled='+cast(enabled as varchar(10))+', 
									@notify_level_eventlog='+cast(NotifyLevelEventlog as varchar(10)) +', 
									@notify_level_email='+cast(NotifyLevelEmail as varchar(10))+', 
									@notify_level_netsend='+cast(NotifyLevelNetsend as varchar(10))+', 
									@notify_level_page='+cast(NotifyLevelPage as varchar(10))+', 
									@delete_level='+cast(DeleteLevel as varchar(10))+', 
									@description=N'''+description+''', 
									@category_name=N'''+CategoryName+''', 
									@owner_login_name=N'''+OwnerLoginName+''', @job_id = @jobId OUTPUT'
							      from Cfg_Job 
							      where Name=@Name;						 
						 
						           set @Script=@Script+'  
						            '+@scriptTemp+'
						            IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
						            '
						           
						           set @scriptTemp=''
						           ----------获取作业步骤的脚本，可能有多个步骤。	 
						           /* select 
											@scriptTemp='EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'''+stepname+''', 
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
										 from cfg_jobsteps where JobId=@JobId;*/
									/*****/
									select  @scriptTemp=dbo.Func_Dey_CreateJobStepsScript(@JobId)	
									/*------------------ */
						            set @Script=@Script+'  
						            '+@scriptTemp+'
						            EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
						            IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
						            '
						            
						            set @scriptTemp=''
						            select   @scriptTemp='EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, 
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
						            
						             set @Script=@Script+'  
						            '+@scriptTemp+'
						            IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
						            '
						            
						            set @scriptTemp=''
						             --select @scriptTemp='EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'''+a.ServerName+''''
						             select @scriptTemp='EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N''(local)'''
                                    from cfg_jobserver a where JobId=@Jobid
                                    
                                     set @Script=@Scripthead+' 
                                     '+@Script+'  
						            '+@scriptTemp+' 
						            '+@scripttail
						            
                                     print 's'+@Scripttemp
						             print @Script
				                   IF (NOT EXISTS (select * from Dey_Script where Type='J' and ObjectName=@Name))
                                  BEGIN
										   insert into Dey_Script (VersionId,ObjectName,UpdateScript,RecoverScript,UpdateValidateScript,RecoverValidateScript,Type)
										   select 3 version_id,@Name object_name,
										   '  
										   go 
								           '+@Script+'
										   go' update_script,
											'go
												 IF (NOT OBJECT_ID(N''dbo.'', ''V'') IS NULL)
														 BEGIN
															 drop view '';
														 END
														 go
											' recover_script,
											'select COUNT(*) from Sys.all_views where object_id>0 and name=''''' update_validate_script ,
											'select COUNT(*) from Sys.all_views where object_id>0 and name=''''' recover_validate_script,'J' type            
										     ;
				                    END  /* if end*/
				                 
							 FETCH NEXT FROM create_table_cursor 
							INTO  @Name,@JobId
						  END   /* while end*/
		
	      
	    
	    CLOSE create_table_cursor
		 DEALLOCATE create_table_cursor
END
GO

