select * 
from Cfg_ETL_TranslateRule;

select * 
from Cfg_ETL_Procedure;

exec SP_ETL_Table @sourcetabname='temp_excelsource',@desttabname='oca_oldmanbaseinfo',@sourcejoincolname='f4',@destjoincolname='idno'

exec SP_ETL_Table @sourcetabname='bas_residentinfo',@desttabname='oca_oldman',@sourcejoincolname='idno',@destjoincolname='idno'

 exec sp_etl_column @sourcetabname='bas_residentinfo',@desttabname='oca_oldman',
		        @sourcecolname='Gender',@destcolname='Gender',@sourcejoincolname='idno',@destjoincolname='idno'

exec sp_etl_setmapexpression

select dbo.func_etl_GetMapExpression('temp_excelsource','gender','oca_oldmanbaseinfo')
select dbo.func_etl_GetMapExpression('bas_residentinfo','CheckInTime','oca_oldman')

select mapExpression from Cfg_ETL_TranslateRule
		where sourcecolumnname='gender' and sourcetablename='bas_residentinfo' and desttablename='oca_oldman'

create table Cfg_ETL_Procedure
(
ProcedureName varchar(100) null,
Definition    varchar(max) null
)

select * from Cfg_ETL_Procedure;

	
	show advanced options
	
	
EXEC sp_configure 'show advanced options', 1;
 RECONFIGURE;
EXEC sp_configure 'xp_cmdshell', 1;
 RECONFIGURE;
	 
	 create   table   #table( txt varchar(max) null)    
insert   into   #table(txt)  exec xp_cmdshell   'net user'    
select   *   from   #table  
drop table #table  

EXEC master..xp_cmdshell 'BCP "SELECT TOP 20 * FROM [leblue-etl].dbo.cfg_etl_procedure" queryout c:\currency2.txt -c -U"sa" -P"123456"' 

select @@ROWCOUNT;

	SELECT * FROM sys.fn_builtin_permissions(default)
	
	
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'EOhnDGS6!7JKv'
CREATE CERTIFICATE MyServerCert WITH SUBJECT = 'My DEK Certificate';

CREATE DATABASE ENCRYPTION KEY
WITH ALGORITHM = AES_128
ENCRYPTION BY SERVER CERTIFICATE MyServerCert;
GO
ALTER DATABASE [Leblue-Etl]
SET ENCRYPTION ON;

select * from sys.certificates 
select * from sys.dm_database_encryption_keys 

