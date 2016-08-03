select * from Oca_OldManConfigInfo;

select MAX(ID) id,IDNo,','
from Temp_ExcelSource
group by OldManName,IDNo
having COUNT(*)>1

select  IDNo,Birthday from Temp_ExcelSource where OldManName ='程培坤'
王秀英
胡秀英
蒋菊仙
霍月珍
程培坤
俞逸琴
屠梦良
於根法
where OldManName='沈仁钦';

-----------------------------sql server 2008R2 删除 非完全重复行的例子
delete from Temp_ExcelSource 
where id in(
			select MAX(ID) id
			from Temp_ExcelSource
			group by OldManName,IDNo
			having COUNT(*)>1
           )
--------------------------------
delete from sheetSource
            select '''',F4,''','
			from sheetSource
			group by F1,f4
			having COUNT(*)>1
;

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  
from dbo.Sheet54$ where f4 is not null;

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  
from dbo.Sheet55$ where f4 is not null;

----------------------------------sql server 2008R2 删除完全重复行的例子
with a as (
			select ROW_NUMBER() OVER(PARTITION BY f4 ORDER BY f4 aSC) AS Rowid
			      ,ROW_NUMBER() OVER( ORDER BY f4 aSC) AS Row
			      ,* 
			from dbo.sheetSource 
			where F4 in (
						'330102192103281823',
						'330102192901281828',
						'330102193010250926',
						'330103192911111343',
						'330104192503051616',
						'330104192810310067',
						'330104193301091614',
						'330104193304242713'
						)  
			)
delete  from a where Rowid=2;
-------------------------------------

exec SP_DBA_ImportOldManFamilyInfo
exec SP_DBA_InsertCallNoChange

exec SP_DBA_MergeTempExcelSource @TableName='Sheet54$',@UpdateTableName='temp_excelsource'
exec SP_DBA_MergeTempExcelSource @TableName='Sheet55$',@UpdateTableName='temp_excelsource'


update dbo.temp_excelsource
set birthday=cast(SUBSTRING(b.f4,7,8) as datetime)
from dbo.temp_excelsource a,dbo.Sheetsource b
where a.idno=b.f4 and len(b.f4)=18 and substring(b.f4,7,8)  like '19%' 
and a.idno not in (select distinct idno from temp_error);

 update dbo.temp_excelsource
set birthday=cast('19'+SUBSTRING(f4,7,6) as datetime)
from dbo.temp_excelsource a,sheetSource b
where a.idno=b.f4 and len(f4)=15
and a.idno not in (select distinct idno from temp_error);

select  IDNo,Birthday from Temp_ExcelSource where len(idno)=15
select IDNo,Birthday from Temp_ExcelSource where len(idno)=18

create table temp_error
(
oldmanname nvarchar(10) null,
idNo varchar(20) null,
reason nvarchar(200) null
)

insert into temp_error (oldmanname,IDNo,reason)
select oldmanname,IDNo,'位数不对' reason  from Temp_ExcelSource where len(idno)<>18 and len(idno)<>15
union
select oldmanname,IDNo,'月份超范围' reason from Temp_ExcelSource where len(idno)=15 and substring(idno,9,2)>12
union
select oldmanname,IDNo,'日期超范围' reason from Temp_ExcelSource where len(idno)=15 and substring(idno,11,2)>31
union
select oldmanname,IDNo,'月份超范围' reason from Temp_ExcelSource where len(idno)=18 and substring(idno,11,2)>12
union
select oldmanname,IDNo,'日期超范围' reason from Temp_ExcelSource where len(idno)=18 and substring(idno,13,2)>31
union  
select oldmanname,IDNo,'年度超范围' reason from Temp_ExcelSource
 where len(idno)=18 and ((substring(idno,7,4))<1900 or (substring(idno,7,4))>2013 )

select distinct idno from temp_error;


----------------------------------------------------------------------------

/****************************************************************************************/
/* 生成临时表Temp_Error   ,对指定表，指定字段，进行身份证验证检查 ，异常内容存入此表    */ 
/* 参数：检查对象表@TableName varchar(100),检查的字段 @ColumnName varchar(100)          */
/****************************************************************************************/

create PROCEDURE  [dbo].[SP_DBA_InsertTempError]
@TableName varchar(100),
@ColumnName varchar(100)
AS 
BEGIN
declare @sqlStr nvarchar(4000)
IF (NOT EXISTS (select * from sys.tables where name='Temp_Error'))
BEGIN
create table Temp_Error
(
oldmanname nvarchar(10) null,
idNo varchar(20) null,
reason nvarchar(200) null
)

END

IF (EXISTS (select * from Temp_Error ))
BEGIN
     delete from Temp_Error;
END

-----------------------============检查身份证的位数，出生日期是否超出范围
set @sqlStr='insert into temp_error (oldmanname,'+@ColumnName+',reason)
select oldmanname,'+@ColumnName+',''位数不对'' reason  from '+@TableName+' where len('+@ColumnName+')<>18 and len('+@ColumnName+')<>15
union
select oldmanname,'+@ColumnName+',''月份超范围'' reason from '+@TableName+' where len('+@ColumnName+')=15 and substring('+@ColumnName+',9,2)>12
union
select oldmanname,'+@ColumnName+',''日期超范围'' reason from '+@TableName+' where len('+@ColumnName+')=15 and substring('+@ColumnName+',11,2)>31
union
select oldmanname,'+@ColumnName+',''月份超范围'' reason from '+@TableName+' where len('+@ColumnName+')=18 and substring('+@ColumnName+',11,2)>12
union
select oldmanname,'+@ColumnName+',''日期超范围'' reason from '+@TableName+' where len('+@ColumnName+')=18 and substring('+@ColumnName+',13,2)>31
union  
select oldmanname,'+@ColumnName+',''年度超范围'' reason from '+@TableName+'
 where len('+@ColumnName+')=18 and ((substring('+@ColumnName+',7,4))<1900 or (substring('+@ColumnName+',7,4))>2013 );'
 exec sp_executesql @sqlStr

END


exec SP_DBA_InsertTempError @TableName='Temp_ExcelSource',@ColumnName='idno'

select * 
from temp_error;