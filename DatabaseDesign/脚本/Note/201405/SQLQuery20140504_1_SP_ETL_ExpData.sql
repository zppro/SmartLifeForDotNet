USE [Leblue-Etl]
GO
/****** Object:  StoredProcedure [dbo].[SP_ETL_ExpData]    Script Date: 05/04/2014 14:32:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/************************************************************************/
/* SP_ETL_ExpData */
/* 导出源数据 */
/************************************************************************/
ALTER procedure [dbo].[SP_ETL_ExpData]
@tablename varchar(100)
as 
begin
declare  @filename varchar(255),@sql varchar(2550),@whereClause varchar(100),@table varchar(100),@sqlstr nvarchar(max),@strsql varchar(2550), @Statement nvarchar(4000),@rowcountSrc int,@rowcountDest int
declare  @path varchar(max),@connectstring varchar(max)
set @table=dbo.gettablename(@tablename)
set @filename=@table+CONVERT(varchar(10),getdate(),120)+'.txt'

set @path=''
set @connectstring=''
select @connectstring=connectstring from Cfg_ETL_ConnectManager where Name='Exp';
select @path=path from Cfg_ETL_Directory where Name='bcp导出目录';
--DATEDIFF(DAY,CheckInTime,Convert(datetime,'2014-04-01',120))=0
-- ' where convert(varchar(10),checkintime,120)=convert(varchar(10),dateadd(d,0,GETDATE()),120) '

set @whereClause='DATEDIFF(DAY,CheckInTime,getdate())='+'1'

set @sql=' '+'BCP "SELECT  * FROM '+@tablename+'  " queryout '+@path+@filename+' -c -k '+@connectstring+' '+' '
--
--EXEC master..xp_cmdshell 'BCP "SELECT TOP 20 * FROM [Smartlife-120300].dbo.oca_oldmanbaseinfo" queryout e:\oldmanbaseinfo0421.txt -c -U"sa" -P"1,leblue@2013" -S"115.236.175.110,10017" '
print @sql
Exec master..xp_cmdshell @sql
if (not exists (select * from sys.tables where name=@table+'step1'))
begin
		/*select * into oca_step1oldmanbaseinfo 
		from remote_dbo.[Smartlife-120300].dbo.oca_oldmanbaseinfo
		where 1=0*/
		set @sqlstr='select * into '+@table+'step1 
		from '+@tablename+'
		where 1=0'
		exec sp_executesql @sqlstr
end

    set @Statement='select @rowcountDest=COUNT(*) from  '+ @table+'step1'
           exec sp_executesql  @Statement  , N'@rowcountDest int out',@rowcountDest out
        --   print @rowcountDest
		if(@rowcountDest>0)
		begin
		     set @Statement='delete from '+ @table+'step1'
		     
		     exec sp_executesql  @Statement
		end

set @connectstring=''
select @connectstring=connectstring from Cfg_ETL_ConnectManager where Name='imp';
set @strsql=' '+'BCP '+@table+'step1 in '+@path+@filename+' -c -k '+@connectstring+' -d"Leblue-Etl" ' +' '
print @strsql

EXEC master..xp_cmdshell @strsql---'BCP @table in e:\'+@filename+' -c -U"sa" -P"123456" -d"Leblue-Etl"' 
end

