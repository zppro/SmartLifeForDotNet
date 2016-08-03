select * from cfg_job;
select StepId,stepname from Cfg_JobSteps where JobId in (select JobId from Cfg_Job where Name='syspolicy_purge_history') order by stepid;
select * from Cfg_JobSchedule;
select * from Cfg_JobServer;
select UpdateScript from Dey_Script where TYPE='J' and ObjectName='backup_database';


exec SP_Cfg_GetJobMain @source_dbName='remote_dbo'
exec SP_Dey_CreateJobScriptProc

select dbo.Func_Dey_CreateJobStepsScript('7AAC7BDD-6008-4734-A98E-031D6221B75D')
exec SP_Dey_OutputData @tablename='Sys_Tree',@where='where TreeCode=''00000'''
select * from Sys_Tree where TreeCode='00000';


USE

 smartlife;
GO
declare @date  nvarchar(50)
select  @date=left(CONVERT(VARCHAR(24),GETDATE(),120),10)
 
BACKUP

 DATABASE [leblue-Etl] TO DISK ='f:\sql_server_backup\leblue-Etl_@date.bck' WITH INIT;

go

USE master
GO
BACKUP DATABASE  [leblue-configuration] TO DISK='f:\sql_server_backup\leblue-configuration_@date.bck' WITH INIT;
GO
USE model
GO
BACKUP DATABASE model TO DISK='f:\sql_server_backup\model_@date.bck' WITH INIT;
GO
USE msdb
GO
BACKUP DATABASE msdb TO DISK='f:\sql_server_backup\msdb_@date.bck' WITH INIT;
GO

select name databasename from sys.databases where name like 'Smartlife%' or name like 'IPCC%' or name like 'leblue%';