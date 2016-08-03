USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Cfg_GetJobSchedule]    Script Date: 05/13/2014 09:26:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************/
/* SP_Cfg_GetJobSchedule                                      */
/* 取系统的作业计划的信息，存入配置库                         */
/* @source_dbName 源数据库名，@dest_dbName 目的数据库名       */
/**************************************************************/
CREATE PROCEDURE [dbo].[SP_Cfg_GetJobSchedule]
@source_dbName nvarchar(100),
@dest_dbName nvarchar(100)=''
 AS
 BEGIN
 DECLARE  @sql_str nvarchar(4000) ,@databasename nvarchar(100)
     -- SET NOCOUNT ON
      select @databasename=replace(REPLACE(@source_dbName,'[',''''),']','''')
     
        IF (EXISTS (select * from Cfg_JobSchedule ))
             BEGIN
                       
              delete from   Cfg_JobSchedule;
             END
        
         IF (NOT EXISTS (select * from   Cfg_JobSchedule))
             BEGIN
             
             if @dest_dbName <>''
             begin
                set @sql_str='  insert into cfg_jobschedule (JobId,Name,Enabled,FreqInterval,FreqSubDayType,FreqRelativeInterval,FreqRecurrenceFactor,ActiveStartDate,ActiveEndDate,ActiveStartTime,ActiveEndTime,ScheduleUid,freqtype,freqsubdayinterval)
								 select JobId,Name,Enabled,FreqInterval,FreqSubDayType,FreqRelativeInterval,FreqRecurrenceFactor,ActiveStartDate,ActiveEndDate,ActiveStartTime,ActiveEndTime,ScheduleUid,freqtype,freqsubdayinterval
								 from (
								 select b.job_id JobId,a.Name,a.Enabled,a.freq_interval FreqInterval,a.freq_subday_type FreqSubDayType
								 ,a.freq_relative_interval FreqRelativeInterval
								 ,a.freq_recurrence_factor FreqRecurrenceFactor,a.active_start_date ActiveStartDate,a.active_end_date ActiveEndDate
								 ,a.active_start_time ActiveStartTime,a.active_end_time ActiveEndTime,a.schedule_uid ScheduleUid,c.name jobname,a.freq_type freqtype,a.freq_subday_interval freqsubdayinterval
								 from '+@source_dbName+'.msdb.dbo.sysschedules a ,'+@source_dbName+'.msdb.dbo.sysjobschedules b ,'+@source_dbName+'.msdb.dbo.sysjobs c
								where a.schedule_id=b.schedule_id  and b.job_id=c.job_id) m
								where m.jobname+m.name not in 
								( select c.name+a.Name
								 from '+@dest_dbName+'.msdb.dbo.sysschedules a ,'+@dest_dbName+'.msdb.dbo.sysjobschedules b ,'+@dest_dbName+'.msdb.dbo.sysjobs c
								where a.schedule_id=b.schedule_id  and b.job_id=c.job_id);'
		       end
		       
		       if @dest_dbName=''
		       begin
		            set @sql_str=' insert into cfg_jobschedule (JobId,Name,Enabled,FreqInterval,FreqSubDayType,FreqRelativeInterval,FreqRecurrenceFactor,ActiveStartDate,ActiveEndDate,ActiveStartTime,ActiveEndTime,ScheduleUid,freqtype,freqsubdayinterval)
									 select b.job_id JobId,Name,Enabled,a.freq_interval FreqInterval,a.freq_subday_type FreqSubDayType
									 ,a.freq_relative_interval FreqRelativeInterval
									 ,a.freq_recurrence_factor FreqRecurrenceFactor,a.active_start_date ActiveStartDate,a.active_end_date ActiveEndDate
									 ,a.active_start_time ActiveStartTime,a.active_end_time ActiveEndTime,a.schedule_uid ScheduleUid
									 ,a.freq_type freqtype,a.freq_subday_interval freqsubdayinterval
									 from '+@source_dbName+'.msdb.dbo.sysschedules a ,'+@source_dbName+'.msdb.dbo.sysjobschedules b 
									where a.schedule_id=b.schedule_id;'     
		       end
		       
		   --select @sql_str
                exec sp_executesql @sql_str 
             END
 END
GO

