exec SP_Dey_OutputData @tablename='Sys_Dictionary',@where=' where DictionaryId between 11012 and 11013'

exec SP_Dey_OutputData @tablename='Sys_Dictionaryitem',@where='where DictionaryId=''11012'' or DictionaryId=''11013'''



insert Sys_Dictionary(DictionaryId,Status,CodeClass,DictionaryName,Description,CodeRule) values('11013',1,'00012','生活服务项目B(小类)','null','5')

insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','02001',1,'02001','建议','null','null',1,1,1,1,'JY','VY')


-----------------------------------------------------------------

--@存储过程：SP_Dey_OutputData导出表数据
ALTER PROCEDURE [dbo].[SP_Dey_OutputData] 
@TableName sysname ,
@Where  varchar(max)=''--,
--@ResultScript nvarchar(max) output
AS 
declare @column varchar(1000) 
declare @columndata varchar(1000) 
declare @sql nvarchar(4000) 
declare @xtype tinyint 
declare @name sysname 
declare @objectId int 
declare @objectname sysname 
declare @ident int 
declare @ResultMessage nvarchar(max)
declare @TableScript Table(updatescript varchar(max),targetscript varchar(max))

set nocount on 
set @objectId=object_id(@Tablename) 

if @objectId is null -- 判斷對象是否存在 
begin 
		print 'The object not exists' 
		return 
end 
set @objectname=rtrim(object_name(@objectId)) 

/*if @objectname is null or charindex(@objectname,@Tablename)=0 --此判断不严密 
begin 
		print 'object not in current database' 
		return 
end */

if OBJECTPROPERTY(@objectId,'IsTable') < > 1 -- 判斷對象是否是table 
begin 
		print 'The object is not table' 
		return 
end 

select @ident=status&0x80 from syscolumns where id=@objectid and status&0x80=0x80 

if @ident is not null 
		print 'SET IDENTITY_INSERT '+@TableName+' ON' 

declare syscolumns_cursor cursor
for select c.name,c.xtype from syscolumns c 
where c.id=@objectid and c.name <>'id'
order by c.colid 

open syscolumns_cursor 
set @column='' 
set @columndata='' 

fetch next from syscolumns_cursor into @name,@xtype 
while @@fetch_status < >-1 
begin 
		if @@fetch_status < >-2 
		begin 
				if @xtype not in(189,34,35,99,98) and @name <>'Id' --timestamp不需处理，image,text,ntext,sql_variant 暂时不处理 
				begin 
						set @column=@column+case when len(@column)=0 then'' else ','end+@name 
						
						set @columndata=@columndata+case when len(@columndata)=0 then '' else '+'',''+'end 
						+case when @xtype in(167,175) then '''''''''+isnull(replace('+@name+','''''''',''''''''''''),''null '')+''''''''' --varchar,char 
						when @xtype in(231,239) then '''''''''+isnull(replace('+@name+','''''''',''''''''''''),''null '')+''''''''' --nvarchar,nchar 
						when @xtype=61 then '''''''''+isnull(convert(varchar(23),'+@name+',121),''null '')+''''''''' --datetime 
						when @xtype=58 then '''''''''+isnull(convert(varchar(16),'+@name+',120),''null '')+''''''''' --smalldatetime 
						when @xtype=36 then '''''''''+isnull(convert(varchar(40),'+@name+'),''null '')+''''''''' --uniqueidentifier 
						when @xtype=48 then 'isnull(cast('+@name+' as varchar(10)),''null '')' --int 
						when @xtype=52 then 'isnull(cast('+@name+' as varchar(10)),''null '')' --int 
						when @xtype=56 then 'isnull(cast('+@name+' as varchar(30)),''null '')' --int 
						when @xtype=104 then 'isnull(cast('+@name+' as varchar(1)),''null '')' --bit
						else 'isnull('+@name+',''null '')' end 
				end 
		end 
		fetch next from syscolumns_cursor into @name,@xtype 
end 
close syscolumns_cursor 
deallocate syscolumns_cursor 


	
	set @sql=N'set nocount on select ''insert '+@tablename+'('+@column+') values(''+'+@columndata+'+'')'' updatescript,'''' targetscript  from '+@tablename +' '+@where

print '--'+@sql 
--exec(@sql) 

  insert into  @TableScript
 exec sp_executesql @sql
	
	update @TableScript set targetscript=replace(updatescript,'''null ''','null')
   select * from @TableScript
if @ident is not null 
print 'SET IDENTITY_INSERT '+@TableName+' OFF' 