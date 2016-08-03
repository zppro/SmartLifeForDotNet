select * from sys.remote_logins;
select * from sys.linked_logins;
select * from sys.servers;
select * from sys.server_principals;

SELECT local.name AS LocalLogins, linked.name AS LinkedLogins
FROM master.sys.server_principals AS local
LEFT JOIN remote_dbo.master.sys.server_principals AS linked
ON local.name = linked.name ;
GO

exec sp_addlogin  'dbo','leblue@2014','SmartLife-120300'
exec sp_adduser  'dbo'

CREATE LOGIN [dbo] WITH PASSWORD=N'leblue@2014', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[简体中文], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO

EXEC sys.sp_addsrvrolemember @loginame = N'sa', @rolename = N'sysadmin'
GO

--grant select on TB1 to username
grant select all table to dbo

还可以用系统存储过程sp_configure 查看与配置远程服务器

如 

--EXEC sp_configure 'show advanced options', '1' RECONFIGURE

--EXEC sp_configure 'remote access', '1' RECONFIGURE
------------=====================*********************************************

CREATE LOGIN [dbo] WITH PASSWORD=N'leblue@2014', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[简体中文], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO

EXEC sys.sp_addsrvrolemember @loginame = N'dbo', @rolename = N'sysadmin'
GO

exec sp_addlinkedserver  @server='remote_dbo_64' ,@srvproduct='',
 @provider='SQLOLEDB',
      @datasrc="192.168.1.64" 
      
exec sp_addlinkedsrvlogin @rmtsrvname='remote_dbo_64'
,@useself=false
,@locallogin='dbo'
,@rmtuser='dbo'
,@rmtpassword='leblue@2014'

------------------==============***********************************************


select * from t_deploy_script where type<>'D' OBJECT_NAME='Sys_Server';


USE  [master];
GO
create procedure backup_database
@databasename nvarchar(50)
as 
declare @@date  nvarchar(50)
declare @@path  nvarchar(50)
declare @@sql   nvarchar(4000)
--declare @@databasename nvarchar(35)
--DECLARE  @databasename nvarchar(50)

select @@path='f:\sql_server_backup\192.168.1.109'
select  @@date=left(CONVERT(VARCHAR(24),GETDATE(),120),10)+DATENAME(hh,getdate())+datename(MI,GETDATE())

set @@sql='BACKUP DATABASE ['+@databasename+'] TO DISK ='''+@@path+'\'+@databasename+'_'+@@date+'.bck'' WITH INIT ,compression;'
--select @@sql
--BACKUP DATABASE smartlife TO DISK ='@@path\smartlilfe_@@date.bck' WITH INIT ,compression;
exec sp_executesql @@sql

go

----------
exec backup_database @databasename='smartlife-120302'
select 3 version_id,TABLE_NAME object_name,'2'+VIEW_DEFINITION+';' update_script,'drop view '+TABLE_NAME+';' recover_script,
'select COUNT(*) from Sys.all_views where object_id>0 and name='''+TABLE_NAME+'''' update_validate_script ,
'select COUNT(*) from Sys.all_views where object_id>0 and name='''+TABLE_NAME+'''' recover_validate_script,'V' type
from [smartlife-120301].[INFORMATION_SCHEMA].VIEWS
where TABLE_NAME not in(select table_name from t_deploy_object where  change_type='not')

insert into t_deploy_script (version_id,object_name,update_script,recover_script,update_validate_script,recover_validate_script,type)
select 3 version_id,a.name object_name,' declare @str nvarchar(max)
select top 1 @str=b.definition
from [$(source_db_name)].sys.all_objects a,[$(source_db_name)].sys.all_sql_modules b 
where a.type in (''V'') and a.object_id>0 and a.object_id=b.object_id 
and a.name='''+a.name+''' and  len(b.definition)>40;
exec sp_executesql @str;
go'  update_script,'drop procedure '+a.name+';'  recover_script,
'select count(c.name)  from sys.all_objects c where c.name='''+a.name+'''' update_validate_script,
'select count(d.name)  from sys.all_objects d where d.name='''+a.name+'''' recover_validate_script,'V' type,rtrim(b.definition)+';
go' updatesc,LEN(b.definition),right(b.definition,5)
from [smartlife-120301].sys.all_objects a,[smartlife-120301].sys.all_sql_modules b 
where a.type in ('V') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>40;


SELECT local.name AS LocalLogins, linked.name AS LinkedLogins
FROM master.sys.server_principals AS local
LEFT JOIN remote_dbo_64.master.sys.server_principals AS linked
ON local.name = linked.name ;


select * from remote_dbo_64.[Smartlife-120300].dbo.oca_oldmanbaseinfo;
select * from t_deploy_script;