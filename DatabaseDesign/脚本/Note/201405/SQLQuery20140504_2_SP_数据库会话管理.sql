exec SP_ETL_ExpData @tablename='[Smartlife-120301].dbo.Oca_CallService'
exec sp_etl_setmapexpression
exec SP_ETL_Table @sourcetabname='Oca_CallServicestep1',@desttabname='Oca_CallServiceStep2',@sourcejoincolname='CallServiceId',@destjoincolname='CallServiceId'
exec SP_ETL_ImpData @tableName='Oca_CallServiceStep2',@DestTable='Oca_CallService'

exec SP_ETL_ExpData @tablename='[Smartlife-120301].dbo.Oca_ServiceWorkOrder'
exec sp_etl_setmapexpression
exec SP_ETL_Table @sourcetabname='Oca_ServiceWorkOrderstep1',@desttabname='Oca_ServiceWorkOrderStep2',@sourcejoincolname='WorkOrderId',@destjoincolname='WorkOrderId'
exec SP_ETL_ImpData @tableName='Oca_ServiceWorkOrderStep2',@DestTable='Oca_ServiceWorkOrder'

select COUNT(*) ,'Oca_OldManBaseInfo' tablename from  Oca_OldManBaseInfo; 

select name from sys.tables;

declare @str varchar(max)
select @str =@str+'select COUNT(*) ,'''+name+''' tablename from  '+name+' union' from sys.tables;
print @str


select * from Cfg_ETL_Component;
select connectstring from Cfg_ETL_ConnectManager where Name='Exp';
select path from Cfg_ETL_Directory where Name='bcp导出目录';
select * from Cfg_ETL_ExternalMetadataColumn
select * from Cfg_ETL_TypeConvert;

insert into Cfg_ETL_ConnectManager (Name,Description,ConnectString,Abbr) values('exp','导出的连接器','-U sa -P 1,leblue@2013 -S 115.236.175.110,10017','BCP');
insert into Cfg_ETL_ConnectManager (Name,Description,ConnectString,Abbr) values('imp','导入的连接器','-U sa -P 123456 -S 192.168.1.109','BCP');


select connectstring from Cfg_ETL_ConnectManager where Name='Exp';
select path from Cfg_ETL_Directory where Name='bcp导出目录';

select DATEDIFF(DAY,Convert(datetime,'2014-05-03',120),GETDATE())

select session_id,login_time,login_name,original_login_name from sys.dm_exec_sessions;
kill 57;
kill 58;

