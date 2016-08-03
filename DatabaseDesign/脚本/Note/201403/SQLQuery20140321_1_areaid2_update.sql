select *
from sys.indexes

select [养老机构名称] stationname,
(select AreaId from pub_area where Levels=4 and areaname=a.所属街道) areaid2 --,[地址] address,[联系方式] mobile,[所属社区] areaid3,[负责人姓名] linkman
 from sheet53$ a;

select AreaId,areaname from pub_area where Levels=4;

select c.*,d.*
from (
select StationName,Address,LinkMan,LinkManMobile ,areaid2,areaid3
 from Pub_ServiceStation where AreaId2 is null )c,
 (select [养老机构名称] stationname,
(select AreaId from pub_area where Levels=4 and areaname=a.所属街道) areaid2 --,[地址] address,[联系方式] mobile,[所属社区] areaid3,[负责人姓名] linkman
 from sheet53$ a)d
 where c.StationName=d.stationname
 
 update Pub_ServiceStation set AreaId2=d.areaid2
 from Pub_ServiceStation c , (select [养老机构名称] stationname,
(select AreaId from pub_area where Levels=4 and areaname=a.所属街道) areaid2 --,[地址] address,[联系方式] mobile,[所属社区] areaid3,[负责人姓名] linkman
 from sheet53$ a)d
 where  c.AreaId2 is null and c.StationName=d.stationname
 
 
select * from pub_area where AreaName like '%景昙社区 %'

大塘苑社区居家养老服务站

select * from Oca_OldManBaseInfo;

select  *
from Temp_ExcelSource 
where day(CheckInTime)=DAY(getdate()) 
and MONTH(checkintime)=Month(GETDATE())
 and year(CheckInTime)=YEAR(getdate()) 
 
 and OldManName='陈爱宝'
 group by oldmanname;

select * from Sheet48$ where f5 is not null;
delete from Sheet48$ where f5 is null;

go
高杏花  沈水花
end     


--创建一个存储过程，根据开始时间和结束时间读取变更记录
CREATE PROC GetCDCResult
(@begin_time DATETIME,@end_time DATETIME)
AS
DECLARE @from_lsn binary(10), @to_lsn binary(10);
SELECT @from_lsn = sys.fn_cdc_map_time_to_lsn('smallest greater than or equal', @begin_time);
SELECT @to_lsn = sys.fn_cdc_map_time_to_lsn('largest less than or equal', @end_time);
SELECT * FROM cdc.dbo_FactInternetSales_CT WHERE __$start_lsn BETWEEN @from_lsn AND @to_lsn 

 delete  from Temp_ExcelSource 
 delete  from sheetSource;
 
 insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  
from dbo.Sheet48$

----------step 1
insert into temp_excelsource(oldmanid,oldmanname,IdNo)
select 
newid() oldmanid,
f1
,f4
from  dbo.Sheet48$ where LEN(f4)=18 or LEN(f4)=15

--------step 2 
update dbo.temp_excelsource
set gender=(case b.f2 when '男' then 'M' when '女' then 'F' else  'M' end)
from dbo.temp_excelsource a,dbo.Sheet48$ b
where a.idno=b.f4

---------step 3 
 update dbo.temp_excelsource
set birthday=cast(SUBSTRING(b.f4,7,8) as datetime)
from dbo.temp_excelsource a,dbo.Sheet48$ b
where a.idno=b.f4 and len(b.f4)=18 and substring(b.f4,7,8)  like '19%' ;

 update dbo.temp_excelsource
set birthday=cast('19'+SUBSTRING(f4,7,6) as datetime)
from dbo.temp_excelsource a,dbo.Sheet48$ b
where a.idno=b.f4 and len(f4)=15;
 
  update dbo.temp_excelsource
set HealthInsuranceFlag=(case ISNULL(b.f6,'0') when '0' then 0 else 1 end)
from dbo.temp_excelsource a,dbo.Sheet48$ b
where a.idno=b.f4;

  update dbo.temp_excelsource
set SocialInsuranceFlag=(case ISNULL(b.f7,'0') when '0' then 0 else 1 end)
from dbo.temp_excelsource a,dbo.Sheet48$ b
where a.idno=b.f4;

  update dbo.temp_excelsource
set livingstate=(select itemid from Sys_DictionaryItem where dictionaryid='11001' and ItemName=b.f8)
from dbo.temp_excelsource a,dbo.Sheet48$ b
where a.idno=b.f4;

update dbo.Temp_excelsource
set  areaid='01191' ,status=1,checkintime=GETDATE(),DataSource='00003'
from dbo.temp_excelsource a,dbo.Sheet48$ b
where a.idno=b.f4;

  update dbo.temp_excelsource
set areaid2=(select areaid from Pub_Area where AreaName=b.f16)
from dbo.temp_excelsource a,dbo.Sheet48$ b
where a.idno=b.f4;

  update dbo.temp_excelsource
set areaid3=(select areaid from Pub_Area where AreaName=b.f17)
from dbo.temp_excelsource a,dbo.Sheet48$ b
where a.idno=b.f4;

  update dbo.temp_excelsource
set address=b.f10
from dbo.temp_excelsource a,dbo.Sheet48$ b
where a.idno=b.f4;

  update dbo.temp_excelsource
set tel=cast(cast (b.f12 as bigint) as varchar(20))
from dbo.temp_excelsource a,dbo.Sheet48$ b
where a.idno=b.f4;

  update dbo.temp_excelsource
set mobile=cast(cast (b.f5 as bigint) as varchar(20))
from dbo.temp_excelsource a,dbo.Sheet48$ b
where a.idno=b.f4;

exec SP_DBA_ImportOldManBaseInfo
exec SP_DBA_ImportOldManFamilyInfo