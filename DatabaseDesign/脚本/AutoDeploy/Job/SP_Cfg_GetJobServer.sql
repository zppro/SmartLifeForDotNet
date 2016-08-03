USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Cfg_GetJobServer]    Script Date: 05/13/2014 09:27:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************/
/* SP_Cfg_GetJobServer                                        */
/* 取系统的作业所在的服务器的信息，存入配置库                 */
/* @source_dbName 源数据库名，@dest_dbName 目的数据库名       */
/**************************************************************/
CREATE PROCEDURE [dbo].[SP_Cfg_GetJobServer]
@source_dbName nvarchar(100),
@dest_dbName nvarchar(100)=''
 AS
 BEGIN
 DECLARE  @sql_str nvarchar(4000) ,@databasename nvarchar(100)
     -- SET NOCOUNT ON
      select @databasename=replace(REPLACE(@source_dbName,'[',''''),']','''')
     
        IF (EXISTS (select * from Cfg_JobServer ))
             BEGIN
                       
              delete from   Cfg_JobServer;
             END
        
         IF (NOT EXISTS (select * from   Cfg_JobServer))
             BEGIN
             
             if @dest_dbName <>''
             begin
                set @sql_str=' insert into cfg_jobserver(JobId,ServerName)
								 select m.JobId,m.ServerName
								 from (
								 select a.job_id JobId, (select name from '+@source_dbName+'.msdb.sys.servers where server_id=a.server_id) ServerName
								 ,c.name
								 from   '+@source_dbName+'.msdb.dbo.sysjobservers a,'+@source_dbName+'.msdb.dbo.sysjobs c
								 where a.job_id=c.job_id) m
								 where m.name not in (
								 select name from '+@dest_dbName+'.msdb.dbo.sysjobs
								 )
								 ; '     
		       end
		       
		       if @dest_dbName=''
		       begin
		            set @sql_str=' insert into cfg_jobserver(JobId,ServerName)
									 select a.job_id JobId, (select name from '+@source_dbName+'.msdb.sys.servers where server_id=a.server_id) ServerName
									 from   '+@source_dbName+'.msdb.dbo.sysjobservers a; '     
		       end
		       
		   --select @sql_str
                exec sp_executesql @sql_str 
             END
 END
GO

