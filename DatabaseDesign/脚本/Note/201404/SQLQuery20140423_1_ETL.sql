select * from dbo.Cfg_ETL_TranslateRule

go
create function gettablename
(@string varchar(max))
returns varchar(max)
as
begin
declare @result varchar(max)
select  @result=reverse(substring(REVERSE(@string),1,case charindex('.',REVERSE(@string),1)
 when 0 then LEN(@string) else charindex('.',REVERSE(@string),1)-1 end ))
 return @result
 end
go

select dbo.gettablename('[smartlife-120300].dbo.ms')

EXEC master..xp_cmdshell 'BCP "SELECT TOP 20 * FROM [Smartlife-1203].dbo.Bas_ResidentBaseInfo" queryout e:\Bas_ResidentBaseInfo0423.txt -c -U"sa" -P"1,leblue@2013" -S"115.236.175.110,10017" '

exec SP_ETL_ExpData @tablename='[Smartlife-1203].dbo.Bas_ResidentBaseInfo'

EXEC sp_configure 'show advanced options', 1;
 RECONFIGURE;
EXEC sp_configure 'xp_cmdshell', 1;
 RECONFIGURE;
 
exec master..xp_cmdshell 'BCP "SELECT TOP 20 * FROM [Smartlife-1203].dbo.Bas_ResidentBaseInfo where convert(varchar(10),checkintime,120)=convert(varchar(10),dateadd(d,-1,GETDATE()),120)" queryout e:\Bas_ResidentBaseInfo2014-04-23.txt -c -U"sa" -P"1,leblue@2013" -S"115.236.175.110,10017" '
EXEC master..xp_cmdshell 'BCP Bas_ResidentBaseInfostep1 in e:\Bas_ResidentBaseInfo2014-04-23.txt -c -U"sa" -P"123456" -d"Leblue-Etl"' 

select * from dbo.bas_residentbaseinfostep1

select *  into dbo.bas_residentbaseinfostep2
from dbo.oca_oldman;
exec sp_rename 'bas_residentbaseinfostep2','Bas_ResidentBaseInfoStep2' 

''BCP' 不是内部或外部命令，也不是可运行的程序


CREATE TABLE [dbo].[Oca_OldMan](
	[OldManId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[IDNo] [char](18) NOT NULL,
	[Gender] [char](1) NOT NULL,
	[UrlHead] [varchar](255) NULL,
	[CheckInTime] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[Mobile] [varchar](30) NULL,
	[Remark] [nvarchar](400) NULL,
 CONSTRAINT [PK_Oca_OldMan] PRIMARY KEY NONCLUSTERED 
(
	[OldManId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'OldManId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'老人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'老人身份证' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'IDNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'老人性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'Gender'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'老人头像路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'UrlHead'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据进入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'CheckInTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态:1有效,0无效' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'老人联系号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'Mobile'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'Remark'
GO

ALTER TABLE [dbo].[Oca_OldMan] ADD  DEFAULT ((1)) FOR [Status]
GO

'
insert into Cfg_ETL_TranslateRule(sourcetablename,sourcecolumnname,desttablename,destcolumnname)values('Bas_ResidentBaseInfostep1','ResidentId','Bas_ResidentBaseInfoStep2','OldManId');
insert into Cfg_ETL_TranslateRule(sourcetablename,sourcecolumnname,desttablename,destcolumnname)values('Bas_ResidentBaseInfostep1','ResidentName','Bas_ResidentBaseInfoStep2','Name');
insert into Cfg_ETL_TranslateRule(sourcetablename,sourcecolumnname,desttablename,destcolumnname)values('Bas_ResidentBaseInfostep1','IDNo','Bas_ResidentBaseInfoStep2','IDNo');
insert into Cfg_ETL_TranslateRule(sourcetablename,sourcecolumnname,desttablename,destcolumnname)values('Bas_ResidentBaseInfostep1','Gender','Bas_ResidentBaseInfoStep2','Gender');
--insert into Cfg_ETL_TranslateRule(sourcetablename,sourcecolumnname,desttablename,destcolumnname)values('Bas_ResidentBaseInfostep1','','Bas_ResidentBaseInfoStep2','UrlHead');
insert into Cfg_ETL_TranslateRule(sourcetablename,sourcecolumnname,desttablename,destcolumnname)values('Bas_ResidentBaseInfostep1','CheckInTime','Bas_ResidentBaseInfoStep2','CheckInTime');
insert into Cfg_ETL_TranslateRule(sourcetablename,sourcecolumnname,desttablename,destcolumnname)values('Bas_ResidentBaseInfostep1','Status','Bas_ResidentBaseInfoStep2','Status');
insert into Cfg_ETL_TranslateRule(sourcetablename,sourcecolumnname,desttablename,destcolumnname)values('Bas_ResidentBaseInfostep1','Mobile','Bas_ResidentBaseInfoStep2','Mobile');
insert into Cfg_ETL_TranslateRule(sourcetablename,sourcecolumnname,desttablename,destcolumnname)values('Bas_ResidentBaseInfostep1','Remark','Bas_ResidentBaseInfoStep2','Remark');

select * from Cfg_ETL_TranslateRule
select * from cfg_etl_procedure;
select * from Bas_ResidentBaseInfoStep2;

declare @columnstr varchar(max),@columnstr2 varchar(max)
 select @columnstr=dbo.JoinStr(sourcecolumnname),@columnstr2=dbo.JoinStr(Destcolumnname) 
 from Cfg_ETL_TranslateRule 
            where  sourcetablename='Bas_ResidentBaseInfostep1'
  print @columnstr +' '+@columnstr2

update Cfg_ETL_TranslateRule set oldvalue=sourcecolumnname,type=1
where desttablename='Bas_ResidentBaseInfoStep2' and type is null;

select dbo.func_etl_GetMapExpression('Bas_ResidentBaseInfostep1','Status','Bas_ResidentBaseInfoStep2')
exec SP_ETL_ExpData @tablename='[Smartlife-1203].dbo.Bas_ResidentBaseInfo'
exec sp_etl_setmapexpression
exec SP_ETL_Table @sourcetabname='Bas_ResidentBaseInfostep1',@desttabname='Bas_ResidentBaseInfoStep2',@sourcejoincolname='ResidentId',@destjoincolname='OldManId'

select 'insert Sys_DictionaryItemExtend(DictionaryId,ItemId,ExtendCol,V00001,V00002,V00003,V00004,V00005) values('
+''''+isnull(replace(DictionaryId,'''',''''''),'null ')+''''+','+''''+isnull(replace(ItemId,'''',''''''),'null ')
+''''+','+''''+isnull(replace(ExtendCol,'''',''''''),'null ')+''''+','+''''+isnull(replace(V00001,'''',''''''),'null ')
+''''+','+''''+isnull(convert(varchar(23),V00002,121),'null ')+''''+','+isnull(cast(V00003 as varchar(1)),'null ')+','
+''''+isnull(cast(V00004 as varchar(30)),'null ')+''''+','+''''+isnull(cast(V00005 as varchar(30)),'null ')+''''+')' updatescript,'' targetscript  
from Sys_DictionaryItemExtend 
where ItemId in ('00001','00003','00006')


select * from Sys_DictionaryItemExtend;
select * from sys.types;

insert Sys_DictionaryItemExtend(DictionaryId,ItemId,ExtendCol,V00001,V00002,V00003,V00004,V00005) values('11015','00003','Radius',null,null,'0','2000',null)

--@存储过程：SP_Pub_OutputData导出表数据
ALTER PROCEDURE [dbo].[SP_Tol_UspOutputDataEx] 
@TableName sysname ,
@WhereClause  varchar(max)=''--,
--@ResultScript nvarchar(max) output
AS 

BEGIN  

	--BEGIN TRY
	--BEGIN TRAN; 
	
	declare @placeHolderForNull char(4)
	set @placeHolderForNull = 'Null'
	declare @InputParams varchar(max)
	set @InputParams = '@TableName='+isnull(@TableName,@placeHolderForNull)
	set @InputParams = ',@WhereClause='+isnull(@WhereClause,@placeHolderForNull)
	print @InputParams
	
declare	@ErrorCode int 
declare @ErrorMessage as nvarchar(500)
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
						when @xtype=48 then '''''''''+isnull(cast('+@name+' as varchar(10)),''null '')+''''''''' --int 
						when @xtype=52 then '''''''''+isnull(cast('+@name+' as varchar(10)),''null '')+''''''''' --int 
						when @xtype=56 then '''''''''+isnull(cast('+@name+' as varchar(30)),''null '')+''''''''' --int 
						when @xtype=104 then '''''''''+isnull(cast('+@name+' as varchar(1)),''0'')+''''''''' --bit
						when @xtype=62 then '''''''''+isnull(cast('+@name+' as varchar(30)),''null '')+''''''''' --float 
						else 'isnull('+@name+',''null '')' end 
				end 
		end 
		fetch next from syscolumns_cursor into @name,@xtype 
end 
close syscolumns_cursor 
deallocate syscolumns_cursor 


	
	set @sql=N'set nocount on select ''insert '+@tablename+'('+@column+') values(''+'+@columndata+'+'')'' updatescript,'''' targetscript  from '+@tablename +' '+@WhereClause

print '--'+@sql 
--exec(@sql) 

  insert into  @TableScript
 exec sp_executesql @sql
	
	update @TableScript set targetscript=replace(updatescript,'''null ''','null')
   select targetscript from @TableScript
--if @ident is not null 
--print 'SET IDENTITY_INSERT '+@TableName+' OFF' 

/* COMMIT TRAN;
	 
	 PRINT '<<< 成功 >>>' 
		 RETURN 0 -- Success!!!
	 
	 END TRY
	 
	 BEGIN CATCH
		 IF @@TRANCOUNT > 0
		 BEGIN
			PRINT '<<< 失败 >>>' 
			if cursor_status( 'global', 'curTreeItems') <> -3
			begin 
				print 'deallocate curTreeItems'
				close   curTreeItems 
				deallocate   curTreeItems 
			end
				
			print('ERROR_MESSAGE:'+ERROR_MESSAGE())
			SET @ErrorCode = ERROR_NUMBER()
			SET @ErrorMessage = N'错误代码:'+cast(@ErrorCode as varchar(20))+N',错误消息:' + ERROR_MESSAGE()+ N',来源:'+ERROR_PROCEDURE()+ N',行号:' + cast(ERROR_LINE() as varchar(5))  +N',输入参数:'+ @InputParams;
			
			if(@ErrorCode < 50000 or @ErrorCode >= 60000)
				begin
					RAISERROR @ErrorCode @ErrorMessage
				end
			ROLLBACK TRAN
			RETURN -1  -- Failure ???
		 END 
	 END CATCH
 */
End 


/************************************************************************/
/* SP_ETL_ImpData */
/* 导出源数据 */
/************************************************************************/
alter procedure [dbo].[SP_ETL_ImpData]
@tablename varchar(100)
as 
begin
declare  @filename varchar(255),@sql varchar(2550),@table varchar(100),@sqlstr nvarchar(max),@strsql varchar(2550)
declare @DestTable varchar(255)
set @DestTable='Oca_OldMan'
set @table=dbo.gettablename(@tablename)
set @filename=@table+CONVERT(varchar(10),getdate(),120)+'.txt'
set @sql=' '+'BCP "SELECT   * FROM '+@tablename+' " queryout e:\'+@filename+' -c -U sa -P 123456 -d Leblue-Etl '+' '

--EXEC master..xp_cmdshell 'BCP "SELECT TOP 20 * FROM [Smartlife-120300].dbo.oca_oldmanbaseinfo" queryout e:\oldmanbaseinfo0421.txt -c -U"sa" -P"1,leblue@2013" -S"115.236.175.110,10017" '
print @sql
Exec master..xp_cmdshell @sql

set @strsql=' '+'BCP '+@DestTable+' in e:\'+@filename+' -c -U sa -P 1,leblue@2013 -S 115.236.175.109,10016 -d WeiXin-12 ' +' '
print @strsql

EXEC master..xp_cmdshell @strsql---'BCP @table in e:\'+@filename+' -c -U"sa" -P"123456" -d"Leblue-Etl"' 
end

exec SP_ETL_ImpData @tableName='Bas_ResidentBaseInfoStep2'