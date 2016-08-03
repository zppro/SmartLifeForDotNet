USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Cfg_GetJob]    Script Date: 05/13/2014 09:25:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************/
/* SP_Cfg_GetJob                                              */
/* 取系统的作业的信息，存入配置库                             */
/* @source_dbName 源数据库名，@dest_dbName 目的数据库名       */
/**************************************************************/
CREATE PROCEDURE [dbo].[SP_Cfg_GetJob]
@source_dbName nvarchar(100),
@dest_dbName nvarchar(100)=''
 AS
 BEGIN
 DECLARE  @sql_str nvarchar(4000) ,@databasename nvarchar(100)
     -- SET NOCOUNT ON
      select @databasename=replace(REPLACE(@source_dbName,'[',''''),']','''')
     
        IF (EXISTS (select * from Cfg_Job ))
             BEGIN
                       
              delete from   Cfg_Job;
             END
        
         IF (NOT EXISTS (select * from   Cfg_Job))
             BEGIN
             
             if @dest_dbName <>''
             begin
                set @sql_str='      insert into cfg_job(Name,Enabled,NotifyLevelEventLog,NotifyLevelEmail,NotifyLevelNetSend,NotifyLevelPage,Description,CategoryName,OwnerLoginName,JobId)
									 select Name,Enabled,notify_level_eventlog NotifyLevelEventLog,
									 notify_level_email NotifyLevelEmail,notify_level_netsend NotifyLevelNetSend,
									 notify_level_page NotifyLevelPage,Description,(select name from '+@source_dbName+'.msdb.dbo.syscategories where category_id=0) CategoryName,
									 (select name from '+@source_dbName+'.master.sys.sql_logins where sid=1) OwnerLoginName,job_id JobId
									 from  '+@source_dbName+'.msdb.dbo.sysjobs_view 
									 where category_id=0 and name not in 
									 (select name from  '+@dest_dbName+'.msdb.dbo.sysjobs_view where category_id=0);
		                              '     
		       end
		       
		       if @dest_dbName=''
		       begin
		            set @sql_str=' insert into cfg_job(Name,Enabled,NotifyLevelEventLog,NotifyLevelEmail,NotifyLevelNetSend,NotifyLevelPage,Description,CategoryName,OwnerLoginName,JobId)
									 select Name,Enabled,notify_level_eventlog NotifyLevelEventLog,
									 notify_level_email NotifyLevelEmail,notify_level_netsend NotifyLevelNetSend,
									 notify_level_page NotifyLevelPage,Description,(select name from '+@source_dbName+'.msdb.dbo.syscategories where category_id=0) CategoryName,
									 (select name from '+@source_dbName+'.master.sys.sql_logins where sid=1) OwnerLoginName,job_id JobId
									 from  '+@source_dbName+'.msdb.dbo.sysjobs_view where category_id=0 '     
		       end
		       
		   --select @sql_str
                exec sp_executesql @sql_str 
             END
 END
GO

