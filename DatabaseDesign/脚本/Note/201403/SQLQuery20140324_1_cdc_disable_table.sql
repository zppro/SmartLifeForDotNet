SELECT * FROM cdc.dbo_Pub_Area_CT

EXEC sys.sp_cdc_enable_table
@source_schema = N'dbo',
@source_name   = N'Oca_OldManConfigInfo',
@role_name     = NULL,
@supports_net_changes = 1
GO

select * from Oca_OldManConfigInfo
update Oca_OldManConfigInfo set OperatedOn =GETDATE()
where CallNo='18888946994'

select * from cdc.dbo_Oca_OldManConfigInfo_CT

select * 
from sys.dm_cdc_errors;

EXEC sys.sp_cdc_disable_db 

exec dbo.SP_DBA_CDC_AppUse @tablename='Oca_OldManConfigInfo'

sys.sp_cdc_help_change_data_capture 
 select *
 from cdc.fn_cdc_get_all_changes_dbo_Oca_OldManConfigInfo()

select * from Sys_Parameter;

select * from Pub_User


create procedure [dbo].[SP_DBA_CDC_DisableAppUse]
@tableName varchar(255)
as
begin 
	declare @is_enable bit


    select @is_enable=is_tracked_by_cdc 
    from    sys.tables 
    where name=@tableName;
    
    if(@is_enable=1)
    begin
			EXEC sys.sp_cdc_disable_table 'dbo', @tablename,'All'
	end		
 end; 