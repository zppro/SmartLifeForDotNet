exec SP_Cfg_GetFunctionDefinition @source_dbName='remote_dbo.[Smartlife-120300]',@dest_dbName='remote_dbo.[Smartlife-120301]'
exec SP_Cfg_GetFunctionDefinition @source_dbName='remote_dbo.[Smartlife-120300]',@dest_dbName=''
select * from Dey_Script where TYPE='F'

select * from Cfg_ObjectVersionHistory where TYPE='F';

exec SP_Dey_CreateFunctionMetaScript @source_dbName='remote_dbo.[Smartlife-120300]',@dest_dbName='remote_dbo.[Smartlife-120301]'

exec SP_Dey_CreateFunctionMetaScript @source_dbName='remote_dbo.[Smartlife-120300]',@dest_dbName=''

alter table dey_script alter column updateScript nvarchar(max) not null;


select * from Cfg_Bridge;

exec SP_Dey_InsertDataScriptForTable @source_dbName='remote_dbo.[Smartlife-120300]',@dest_dbName='remote_dbo.[Smartlife-120301]'
,@tablename=''


select object_id
from sys.tables
where name='Oca_OldManBaseInfo'

select * 
from sys.indexes;

CREATE CLUSTERED INDEX IN_OldManBaseInfo ON Oca_OldManBaseInfo([AreaId])  
ON OldManBaseInfoPS1([AreaId])

select * from sys.partitions
where object_id in(select object_id
from sys.tables
where name='Oca_OldManBaseInfo');

select * from