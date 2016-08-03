/***********************************************************************/
/*   SP_DBA_PartitionAddFile             */
/*   */
/***********************************************************************/
create procedure SP_DBA_PartitionAddFile
@databasename varchar(100)
as
begin
		declare @i int,@str nvarchar(4000),@filePath varchar(4000)
		set @i=0
		set @str=''
		select @filePath=
		reverse(substring(reverse(physical_name),charindex('\',reverse(physical_name),0),LEN(physical_name)))
		from sys.database_files where data_space_id=1;

		while (select @i)<40
		begin
				set @i=@i+1
				print @i
				set @str='ALTER DATABASE  '+@databasename+' 
				ADD FILE 
				(
					NAME = Test'+cast(@i as nvarchar(10))+'data2,
					FILENAME ='''+@filePath+cast(@i as nvarchar(10))+'dat2.mdf'',
					SIZE = 5MB,
					MAXSIZE = 1000MB,
					FILEGROWTH = 5MB
				) TO FILEGROUP test'+cast(@i as nvarchar(10))+'fg
				;'
				exec sp_executesql @str
		end
end
go