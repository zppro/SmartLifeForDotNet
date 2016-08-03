select count(*)  ,CONVERT(varchar(7),checkintime,120)
  from dbo.IPCC_CallRecord
group by CONVERT(varchar(7),checkintime,120);
-------------=======================================
alter procedure SP_DBA_MoveDataForCallRecord
@dateStr datetime =''
as
if @dateStr=''
    begin
          set @dateStr=GETDATE()
    end
    
declare @tablename varchar(100) ,@datewhere varchar(100), @Statement nvarchar(4000),@rowcountSrc int,@rowcountDest int
select  @tablename='IPCC_CallRecord_'+convert(varchar(6),dateadd(MONTH,-1,@dateStr),112)
select @datewhere=convert(varchar(6),dateadd(MONTH,-1,@dateStr),112)
--print @tablename
if (not exists (select name from sys.tables where name=@tablename))
    begin 
           set @Statement=' select * into '+@tablename+' 
                 from dbo.IPCC_CallRecord 
                 where convert(varchar(6),checkintime,112)='+@datewhere;
          ---select @Statement
           exec sp_executesql  @Statement   
    end 

if (exists (select name from sys.tables where name=@tablename))
    begin
          select @rowcountSrc=COUNT(*) from dbo.IPCC_CallRecord 
          where convert(varchar(6),checkintime,112)=convert(varchar(6),dateadd(MONTH,-1,@dateStr),112);
            set @Statement='select @rowcountDest=COUNT(*) from  '+ @tablename 
           exec sp_executesql  @Statement  , N'@rowcountDest int out',@rowcountDest out
  --  print @rowcountSrc
  --  print @rowcountDest
    if (@rowcountSrc=@rowcountDest)
       begin
          delete  from dbo.IPCC_CallRecord 
          where convert(varchar(6),checkintime,112)=convert(varchar(6),dateadd(MONTH,-1,@dateStr),112);
       end
   end

--------------------===================================

exec SP_DBA_MoveDataForCallRecord @dateStr='2014-03-07 17:40:24'
select convert(varchar(6),dateadd(MONTH,-1,getdate()),112),convert(varchar(20),dateadd(MONTH,-1,getdate()),120)

insert into IPCC_CallRecord_201402 select * from dbo.IPCC_CallRecord   where convert(varchar(6),checkintime,112)=convert(varchar(6),dateadd(MONTH,-1,getdate()),112)

select * into IPCC_CallRecord_201402 from dbo.IPCC_CallRecord   where convert(varchar(6),checkintime,112)=convert(varchar(6),dateadd(MONTH,-1,getdate()),112)


insert into cfg_objectversionhistory(DatabaseName,UserName,ObjectName,
BeforeDefinition,BeforeLength,BeforeModifyDate,Type)
select DB_NAME() as DatabaseName,user_name() as UserName,a.name,b.definition,
LEN(b.definition) length,a.modify_date,'F' type
from [smartlife-120300].sys.all_objects a,[smartlife-120300].sys.all_sql_modules b 
where a.Type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>40;


PRINT 'source_db_name $(source_db_name) ,sync_type $(sync_type)'
PRINT 'Creating procedure SP_Cfg_GetFunctionDefinition ...'

IF (EXISTS (SELECT *
            FROM dbo.sysobjects
            WHERE (name = N'SP_Cfg_GetFunctionDefinition')
              AND (type = 'P')))
  DROP PROCEDURE SP_Cfg_GetFunctionDefinition
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************/
/* SP_Cfg_GetFunctionDefinition                               */
/* 取函数的定义的信息，存入配置库                             */
/*  @source_dbName 信息来源数据库名                           */
/**************************************************************/
CREATE PROCEDURE [dbo].[SP_Cfg_GetFunctionDefinition]
@source_dbName nvarchar(100)
 AS
 BEGIN
 DECLARE  @sql_str nvarchar(4000) ,@databasename nvarchar(100)
      SET NOCOUNT ON
      select @databasename=replace(REPLACE(@source_dbName,'[',''''),']','''')
     
        IF (EXISTS (select * from cfg_objectversionhistory where type='F' ))
             BEGIN
                       
              delete from   cfg_objectversionhistory where type='F';
             END
        
         IF (NOT EXISTS (select * from   cfg_objectversionhistory where type='F'))
             BEGIN
                set @sql_str='
		insert into cfg_objectversionhistory(DatabaseName,UserName,ObjectName,
                     BeforeDefinition,BeforeLength,BeforeModifyDate,Type)
                select DB_NAME() as DatabaseName,user_name() as UserName,a.name,b.definition,
                      LEN(b.definition) length,a.modify_date,''F'' type
                from '+@source_dbName+'.sys.all_objects a,'+@source_dbName+'.sys.all_sql_modules b 
                where a.Type in (''AF'',''TF'',''FN'') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>40;
		'
                       
                 exec sp_executesql @sql_str 
             END
 END
GO


