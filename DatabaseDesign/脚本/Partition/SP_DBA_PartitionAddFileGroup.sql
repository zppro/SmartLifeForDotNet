/***********************************************************************/
/*   SP_DBA_PartitionAddFileGroup             */
/*   */
/***********************************************************************/
create procedure SP_DBA_PartitionAddFileGroup
@databasename varchar(100)
as
begin
		declare @i int,@str nvarchar(4000)
		set @i=1
		set @str=''
		
		while (select @i)<40
		begin
				set @i=@i+1
				print @i
				set @str='alter database '+@databasename+' add filegroup test'+cast(@i as nvarchar(10))+'fg;'
				exec sp_executesql @str
		end
end