@echo off
set source_dir="Published.ManageServices"
set dest_dir="SmartAuth.ManageServices-12"  
xcopy %source_dir%  %dest_dir% /S /E /C /R /Y /I /EXCLUDE:exclude-SmartAuthManageServices.ini
