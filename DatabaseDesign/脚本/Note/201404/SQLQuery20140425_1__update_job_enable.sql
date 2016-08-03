-- changes the name, description, and enables status of the job NightlyBackups.
USE msdb ;
GO

EXEC dbo.sp_update_job
    @job_name = N'backup_database',
    --@new_name = N'backup_database--Disabled',
     @owner_login_name = 'job', 
    @description = N'backup_database disabled during server migration.',
    @enabled = 1 ;
GO

EXEC dbo.sp_update_job
    @job_name = N'backup_database--Disabled',
    @new_name = N'backup_database',
   
    @description = N'backup_database disabled during server migration.',
    @enabled = 1 ;
GO

use msdb
go
if (NOT EXISTS  (select * from sys.login_token where Name='job' and Type='SQL LOGIN'))
begin
CREATE LOGIN [job] WITH PASSWORD=N'leblue@2014', DEFAULT_DATABASE=[msdb], DEFAULT_LANGUAGE=[简体中文], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
create user job;
EXEC sys.sp_addrolemember  @roleName = N'SQLAgentOperatorRole',@membername='job'
end
GO

ALTER LOGIN [job] WITH PASSWORD = 'leblue@2013' 

exec sp_help_jobschedule @job_name='backup_database'

/***************************************************************************************/
/* 以下的存储过程的调用 是为了修改作业执行的间隔                                       */
/* @job_name 作业名 ,@name 作业的计划时刻表的名称 ,                                    */
/* @fre_subday_type 间隔的单位, 2 为秒 ,4为 分 8 为 小时                               */
/* @freq_subday_interval 间隔的数值 大于等于1的整数，如果单位为秒时，是大于等于10的整数*/
/***************************************************************************************/
exec sp_update_jobschedule @job_name='MakeCallServiceRecord',@name='plan',@freq_subday_type='2',@freq_subday_interval='12'

--EXEC sys.sp_addsrvrolemember @logiName = N'dbo', @roleName = N'SQLAgentOperatorRole'
--EXEC sys.sp_addrole  @roleName = N'SQLAgentOperatorRole',@ownername='job'