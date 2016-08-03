dbcc ind (smartlife ,t_deploy_errordefine,1)
dbcc traceon(3604)
dbcc page(smartlife,1,80,1)
dbcc fileheader('smartlife',1)
select MAX(LEN(describe)) from t_deploy_errordefine;
select * from t_deploy_errordefine;

SELECT    [name],    [backup_start_date],    [type],    [first_lsn],    [database_backup_lsn]
FROM    [msdb].[dbo].[backupset]WHERE    [database_name] = N'smartlife';
GO


USE

 smartlife;
GO

 
BACKUP

 DATABASE smartlife TO DISK ='f:\sql_server_backup\smartlilfe_20140124.bck' WITH INIT;
GO

USE master
GO
BACKUP DATABASE master TO DISK='f:\sql_server_backup\master_20140124.bck' WITH INIT;
GO
USE model
GO
BACKUP DATABASE model TO DISK='f:\sql_server_backup\model_20140124.bck' WITH INIT;
GO
USE msdb
GO
BACKUP DATABASE msdb TO DISK='f:\sql_server_backup\msdb_20140124.bck' WITH INIT;
GO

SELECT schema_name(schema_id) AS SchemaName,  object_name(o.object_id) AS ObjectName, 
    i.name AS IndexName, index_id, o.type, 
    STATS_DATE(o.object_id, index_id) AS statistics_update_date 
FROM sys.indexes i join sys.objects o 
       on i.object_id = o.object_id 
WHERE o.object_id > 100 AND index_id > 0 
  AND is_ms_shipped = 0;