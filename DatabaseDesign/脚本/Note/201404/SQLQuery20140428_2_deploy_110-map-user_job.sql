exec sp_update_job @job_name='SmartLife-UI-江干模拟回拨',@owner_login_name = 'job'
exec sp_start_job @job_name='SmartLife-UI-江干模拟回拨'

select top 2 * from oca_callservice order by checkintime desc ;

use [SmartLife-1203]
--create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
go
 
use [SmartLife-120300]
--create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go
use [smartlife-120301]
--create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go
use [smartlife-120302]
--create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go
use [smartlife-120303]
--create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go
use [smartlife-120304]
--create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go
use [smartlife-120305]
--create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go
use [smartlife-120306]
--create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go
use [smartlife-120307]
--create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go
use [smartlife-120311]
--create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go
use [Smartlife-120395]
--create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go
use [smartlife-120396]
--create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
go
 
use [smartlife-120397]
--create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go
use [smartlife-120398]
--create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go
use [smartlife-120399]
--create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go