select top 30 * from Oca_OldManLocateInfo order by CheckInTime desc;

select datename(WEEK,CheckInTime),COUNT(*) from Oca_CallService where ServiceCatalog=2 and  checkintime >DATEADD(D,-6,GETDATE()) and CheckInTime < DATEADD(D,1,GETDATE())
group by datename(WEEK,CheckInTime);

select * from vSystem_Info_Columns where table_name='Oca_EPay_OldManAccount'
select * from vSystem_Info_Tables;

select OldManName,serveitembname,Remain from Oca_EPay_OldManAccount;

select dictionaryid,itemname,itemcode from Sys_DictionaryItem where DictionaryId=11013 and ItemId like '01%'

--------------------**********************老人账户查询
select a.OldManName as '老人姓名',sum(case b.serveitemb when '01001' then b.Remain else 0 end ) as  '家政清洁',
sum(case b.serveitemb when '01002' then b.Remain else 0 end ) as  '水电家电维修' ,
sum(case b.serveitemb when '01003' then b.Remain else 0 end ) as  '医疗保健服务' 
from (
select oldmanname from oca_oldmanbaseinfo where  oldmanid in(
select oldmanid from oca_oldmanconfiginfo where govturnkeyflag=1) ) a 
left join (select OldManName,serveitemb,Remain from Oca_EPay_OldManAccount) b
on a.oldmanname=b.oldmanname
group by a.OldManName

--------------------**********************老人账户查询
select a.OldManName as '老人姓名',(case b.serveitemb when '01001' then b.Remain else 0 end ) as  '家政清洁',
(case b.serveitemb when '01002' then b.Remain else 0 end ) as  '水电家电维修' ,
(case b.serveitemb when '01003' then b.Remain else 0 end ) as  '医疗保健服务' 
from (
select oldmanname from oca_oldmanbaseinfo where  oldmanid in(
select oldmanid from oca_oldmanconfiginfo where govturnkeyflag=1) ) a 
left join (select OldManName,serveitemb,Remain from Oca_EPay_OldManAccount) b
on a.oldmanname=b.oldmanname

家政清洁
水电家电维修
医疗保健服务

select CallServiceId from Oca_CallService where DoStatus=3 and ServiceCatalog='00001';


select sum(dbo.FUNC_Oca_CeilingHour(ServeBeginTime,ServeEndTime))  fee,datename(weekday,checkintime) weekdays --,ServeManArriveTime,ServeManLeaveTime
 from dbo.Oca_ServiceWorkOrder where ServeItemB='03001' group by datename(weekday,checkintime);
 
 ------------*****************************************周报表 第一类服务
 select sum(case a.weekdays when  '星期一' then a.fee else 0 end) as   '星期一' ,
 sum(case a.weekdays when  '星期二' then a.fee else 0 end)  as '星期二' ,
 sum(case a.weekdays when  '星期三' then a.fee else 0 end)  as '星期三' ,
 sum(case a.weekdays when  '星期四' then a.fee else 0 end)  as '星期四' ,
 sum(case a.weekdays when  '星期五' then a.fee else 0 end)  as '星期五' ,
 sum(case a.weekdays when  '星期六' then a.fee else 0 end)  as '星期六' ,
 sum(case a.weekdays when  '星期日' then a.fee else 0 end)  as '星期日' 
 --,datename(weekday,checkintime)--,ServeManArriveTime,ServeManLeaveTime
 from (select sum(dbo.FUNC_Oca_CeilingHour(ServeBeginTime,ServeEndTime))  fee,1 groupcolumn,left(checkintime,10) days,datename(weekday,checkintime) weekdays --,ServeManArriveTime,ServeManLeaveTime
 from dbo.Oca_ServiceWorkOrder where ServeItemB='03001' 
 and CallServiceId in (select CallServiceId from Oca_CallService where DoStatus=3 and ServiceCatalog='00001')
 group by datename(weekday,checkintime),left(checkintime,10)) a
 where days between dateadd(D,-5,getdate()) and dateadd(D,1,getdate())
 group by a.groupcolumn   ;
 ---------------************************************************
 
 select checkintime ,DATENAME(WEEKDAY,getdate()) from dbo.Oca_ServiceWorkOrder ;
 
 
 select COUNT(*) fee,1 groupcolumn ,datename(weekday,checkintime) weekdays  --,ServeManArriveTime,ServeManLeaveTime
 from dbo.Oca_ServiceWorkOrder where ServeItemB='03003' group by datename(weekday,checkintime);
 
 ---------------------*********************************
  select sum(case a.weekdays when  '星期一' then a.fee else 0 end) as   '星期一' ,
 sum(case a.weekdays when  '星期二' then a.fee else 0 end)  as '星期二' ,
 sum(case a.weekdays when  '星期三' then a.fee else 0 end)  as '星期三' ,
 sum(case a.weekdays when  '星期四' then a.fee else 0 end)  as '星期四' ,
 sum(case a.weekdays when  '星期五' then a.fee else 0 end)  as '星期五' ,
 sum(case a.weekdays when  '星期六' then a.fee else 0 end)  as '星期六' ,
 sum(case a.weekdays when  '星期日' then a.fee else 0 end)  as '星期日' 
 --,datename(weekday,checkintime)--,ServeManArriveTime,ServeManLeaveTime
 from (select COUNT(*) fee,1 groupcolumn ,left(checkintime,10) days,datename(weekday,checkintime) weekdays  --,ServeManArriveTime,ServeManLeaveTime
 from dbo.Oca_ServiceWorkOrder where ServeItemB='03002' 
 and CallServiceId in (select CallServiceId from Oca_CallService where DoStatus=3 and ServiceCatalog='00001')
 group by datename(weekday,checkintime),left(checkintime,10)) a
 where days between dateadd(D,-5,getdate()) and dateadd(D,1,getdate())
 group by a.groupcolumn;
 
 
 select COUNT(*) fee,1 groupcolumn ,left(checkintime,10) days,datename(weekday,checkintime) weekdays  --,ServeManArriveTime,ServeManLeaveTime
 from dbo.Oca_ServiceWorkOrder where ServeItemB='03002' 
 and CallServiceId in (select CallServiceId from Oca_CallService 
 --where DoStatus=3 and ServiceCatalog='00001'
 )
 group by datename(weekday,checkintime),left(checkintime,10)
 
 select 星期一 ,星期二 ,星期三 ,星期四,星期五,星期六,星期日
 from 
 (select sum(dbo.FUNC_Oca_CeilingHour(ServeBeginTime,ServeEndTime))  fee,1 groupcolumn,datename(weekday,checkintime) weekdays --,ServeManArriveTime,ServeManLeaveTime
 from dbo.Oca_ServiceWorkOrder where ServeItemB='03001' 
 and CallServiceId in (select CallServiceId from Oca_CallService where DoStatus=3 and ServiceCatalog='00001')
 group by datename(weekday,checkintime),left(checkintime,10)) a
 pivot(sum (a.fee) for a.weekdays in (星期一 ,星期二 ,星期三 ,星期四,星期五,星期六,星期日)) as pivotss;
 

--------88888888888888
alter FUNCTION [dbo].[FUNC_dba_DateTimeTovarchar] 
(
      @DateTime as datetime
)
RETURNS varchar(28)--JSON Formated \/Date(1278930470649+0800)\/
AS
BEGIN
 
  DECLARE  @var varchar(28)
  if(@DateTime is not null)
  begin
	set @var = '日期'+datename(Year,@DateTime)+'年'+datename(m,@DateTime)+'月'+datename(d,@DateTime)+'日'
  end
  else
  begin 
	set @var = '' 
  end
  return @var
 
end 

GO
----------88888888888
select dbo.FUNC_dba_DateTimeTovarchar(GETDATE()) 
 select datename(Year,GETDATE())+'-'+datename(m,GETDATE())+'-'+datename(d,GETDATE())+DATENAME(HH,getdate())
 
------------------***************************************
 create procedure SP_DBA_Get_date_str 
 @begin_time datetime ,
 @end_time   datetime ,
 @outStr     varchar(3500) out
 as
 declare @temp datetime
 declare @str varchar(2000)
 
set @str=''
--dbo.FUNC_dba_DateTimeTovarchar(@begin_time) 

 set @temp=@begin_time
 while (@temp<@end_time)
 begin
convert(datetime,@temp,120)
 set @str=@str+dbo.FUNC_dba_DateTimeTovarchar(@temp)+','
 set @temp=DATEADD(d,1,@temp)
 end
 set @outStr=substring(@str,1,LEN(@str)-1)
 
 ----------------------------------***********************************
create procedure SP_DBA_Get_callservice_report 
 @begin_time datetime ,
 @end_time   datetime 
 as
 declare @S varchar(6300)
 --declare @begin_time datetime 
 --declare @end_time   datetime 
 declare @sql varchar(6300)
 set @begin_time=dateadd(D,-16,getdate())
 set @end_time=dateadd(D,13,getdate())
 exec SP_DBA_Get_date_str @begin_time,@end_time, @S out
 --select @S,@begin_time,@end_time
 
 set @sql='select '+@S+'
 from (
  select sum(dbo.FUNC_Oca_CeilingHour(ServeBeginTime,ServeEndTime))  fee,1 groupcolumn,dbo.FUNC_dba_DateTimeTovarchar(checkintime) days 
 from dbo.Oca_ServiceWorkOrder where ServeItemB=''03001'' 
 and CallServiceId in (select CallServiceId from Oca_CallService where DoStatus=3 and ServiceCatalog=''00001'')
 group by dbo.FUNC_dba_DateTimeTovarchar(checkintime) ) a
 pivot(sum (a.fee) for a.days in ('+@S+')) as pivotss;'
 
-- select @sql
 exec(@sql) 
 
 
