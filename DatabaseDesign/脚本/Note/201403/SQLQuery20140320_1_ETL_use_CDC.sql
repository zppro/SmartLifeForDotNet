USE [smartlife-120303]
GO
/****** Object:  MessageType [http://schemas.microsoft.com/SQL/ServiceBroker/EndDialog]    Script Date: 03/20/2014 08:59:48 ******/
ALTER MESSAGE TYPE [http://schemas.microsoft.com/SQL/ServiceBroker/EndDialog] VALIDATION = EMPTY

ALTER AUTHORIZATION ON CONTRACT::[http://schemas.microsoft.com/SQL/Notifications/PostEventNotification] TO [dbo] 

ALTER SERVICE [http://schemas.microsoft.com/SQL/Notifications/EventNotificationService]  
ON QUEUE [dbo].[EventNotificationErrorsQueue] 

  CREATE MESSAGE TYPE
    [//Adventure-Works.com/Expenses/SubmitExpense]
    VALIDATION = WELL_FORMED_XML ;


select   dbo.joinStr(name)
from    sys.all_columns 
where  object_id   in 
            (   select  object_id 
                from    sys.tables 
               where  name='Oca_OldManBaseInfo'
             )

EXECUTE sys.sp_cdc_get_ddl_history 
    @capture_instance = 'Oca_OldManBaseInfo';
    
    
EXEC sp_changedbowner 'sa'; 
ALTER AUTHORIZATION ON DATABASE::[Smartlife-120302] TO [sa]
EXEC sys.sp_cdc_enable_db
    select name,is_cdc_enabled from sys.databases;
        
    EXEC sys.sp_cdc_enable_db

EXEC sys.sp_cdc_enable_table
@source_schema = N'dbo',
@source_name   = N'Pub_Area',
@role_name     = NULL,
@supports_net_changes = 1
GO

EXECUTE sys.sp_cdc_enable_table
    @source_schema = N'dbo' 
  , @source_name = N'Oca_CallService'
  , @role_name = N'cdc_Admin'--可以自动创建
  , @capture_instance=DEFAULT,
     @supports_net_changes = 1
EXECUTE sys.sp_cdc_help_change_data_capture

    select name,is_cdc_enabled from sys.databases;
    
    select * from cdc.captured_columns;
    
    select *
    from dbo.systranschemas;

DECLARE @begin_time datetime, @end_time datetime, @from_lsn binary(10), @to_lsn binary(10);
-- Obtain the beginning of the time interval.
SET @begin_time = GETDATE()-1; 

SET @end_time = GETDATE();
-- Map the time interval to a change data capture query range.
SELECT @from_lsn = sys.fn_cdc_map_time_to_lsn('smallest greater than or equal', @begin_time);
SELECT @to_lsn = sys.fn_cdc_map_time_to_lsn('largest less than or equal', @end_time);
print @begin_time
print @end_time
print @from_lsn
print @to_lsn 

--创建一个存储过程，根据开始时间和结束时间读取变更记录
CREATE PROC GetCDCResult
(@begin_time DATETIME,@end_time DATETIME)
AS
DECLARE @from_lsn binary(10), @to_lsn binary(10);
SELECT @from_lsn = sys.fn_cdc_map_time_to_lsn('smallest greater than or equal', @begin_time);
SELECT @to_lsn = sys.fn_cdc_map_time_to_lsn('largest less than or equal', @end_time);
SELECT * FROM cdc.dbo_FactInternetSales_CT WHERE __$start_lsn BETWEEN @from_lsn AND @to_lsn 

--调用该存储过程
EXEC GetCDCResult '2009-4-27','2009-4-29' 

 
 
--撤销CDC
EXEC sys.sp_cdc_disable_table 'dbo', 
'FactInternetSales','All' 

EXEC sys.sp_cdc_disable_db 


select max(areacode) areacode,MAX(levels) levels,
max(endflag) endflag,max(orderno) orderno ,parentid--,SUBSTRING(cast(areaid as varchar(42)),15,4)
from Pub_Area where EndFlag=1
group by parentid,areaid
