select * from sys.syslockinfo

select * from sys.databases;

select * from sys.dm_tran_locks;
exec sp_pub_outputdata @tablename='',@whereclause=''