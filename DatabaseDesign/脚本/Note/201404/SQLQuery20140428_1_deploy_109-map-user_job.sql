use [smartauth-12]
create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
go
 
use [smartauth-18]
create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go
use [smartauth-dev]
create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go
use [weixin-12]
create user job;
EXEC sys.sp_addrolemember  @roleName = N'db_owner',@membername='job'
 go