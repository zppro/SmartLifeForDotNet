@echo off
set source_dir="Published.ManageCMS"
set dest_dir="SmartAuth.ManageCMS-12"  
xcopy %source_dir%  %dest_dir% /S /E /C /R /Y /I /EXCLUDE:exclude-SmartAuthManageCMS.ini