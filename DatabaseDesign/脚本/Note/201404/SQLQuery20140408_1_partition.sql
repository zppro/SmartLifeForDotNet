select *
from sys.filegroups 
select *,
reverse(substring(reverse(physical_name),charindex('\',reverse(physical_name),0),LEN(physical_name)))
from sys.database_files where data_space_id=1;
select * 
from sys.data_spaces ;

select 
D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\

select @filePath=
reverse(substring(reverse(physical_name),charindex('\',reverse(physical_name),0),LEN(physical_name)))
from sys.database_files where data_space_id=1;

select * 
from sys.partitions
where object_id in(select object_id from sys.tables where name='Bas_ResidentBaseInfo')

select * 
from  sys.partition_range_values

ALTER PARTITION FUNCTION OldManBaseInfoPF1 ()
SPLIT RANGE ('01201');
ALTER PARTITION FUNCTION OldManBaseInfoPF1 ()
MERGE RANGE ('01200');

ALTER PARTITION FUNCTION OldManFamilyInfoPF1 ()
SPLIT RANGE (30);

ALTER PARTITION SCHEME OldManFamilyInfoPS1
NEXT USED test31fg;

