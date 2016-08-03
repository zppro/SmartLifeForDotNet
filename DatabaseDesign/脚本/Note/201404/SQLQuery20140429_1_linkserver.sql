/****** Object:  LinkedServer [remote_109]    Script Date: 04/29/2014 10:26:46 ******/
EXEC master.dbo.sp_addlinkedserver @server = N'remote_109', @provider=N'SQLNCLI', @datasrc=N'115.236.175.109'
exec sp_addlinkedserver  @server='remote_109' ,@srvproduct='',
 @provider='SQLOLEDB',
      @datasrc="115.236.175.109,10016" 
 /* For security reasons the linked server remote logins password is changed with ######## */
EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'remote_109',@useself=N'True',@locallogin=NULL,@rmtuser=NULL,@rmtpassword=NULL
EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'remote_109',@useself=N'False',@locallogin=N'dbo',@rmtuser=N'dbo',@rmtpassword='leblue@2014'
EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'remote_109',@useself=N'False',@locallogin=N'sa',@rmtuser=N'sa',@rmtpassword='1,leblue@2013'

GO
exec sp_addlinkedsrvlogin @rmtsrvname='remote1'
,@useself=false
,@locallogin='sa'
,@rmtuser='sa'
,@rmtpassword='******'

EXEC master.dbo.sp_serveroption @server=N'remote_109', @optname=N'collation compatible', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'remote_109', @optname=N'data access', @optvalue=N'true'
GO

EXEC master.dbo.sp_serveroption @server=N'remote_109', @optname=N'dist', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'remote_109', @optname=N'pub', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'remote_109', @optname=N'rpc', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'remote_109', @optname=N'rpc out', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'remote_109', @optname=N'sub', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'remote_109', @optname=N'connect timeout', @optvalue=N'0'
GO

EXEC master.dbo.sp_serveroption @server=N'remote_109', @optname=N'collation name', @optvalue=null
GO

EXEC master.dbo.sp_serveroption @server=N'remote_109', @optname=N'lazy schema validation', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'remote_109', @optname=N'query timeout', @optvalue=N'0'
GO

EXEC master.dbo.sp_serveroption @server=N'remote_109', @optname=N'use remote collation', @optvalue=N'true'
GO

EXEC master.dbo.sp_serveroption @server=N'remote_109', @optname=N'remote proc transaction promotion', @optvalue=N'true'
GO


