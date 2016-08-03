exec sp_help_job;
exec sp_rename @objname='MakeCallServiceRecord',@newname='Smartlife-MakeCallServiceRecord'
exec sp_update_job @job_name='Smartlife-MakeCallServiceRecord',@new_name='SmartLife-MakeCallServiceRecord'

exec sp_update_job @job_name='SmartLife-MakeCallServiceRecord',@new_name='SmartLife-UI-MakeCallServiceRecord'
exec sp_update_job @job_name='SmartLife-UI-MakeCallServiceRecord',@new_name='SmartLife-UI-江干模拟回拨'