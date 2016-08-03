 declare @S varchar(6300)
 declare @begin_time datetime 
 declare @end_time   datetime 
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
 
   select sum(dbo.FUNC_Oca_CeilingHour(ServeBeginTime,ServeEndTime))  fee,COUNT(*),1 groupcolumn,
   '['+convert(varchar(10),checkintime,120)+']' days 
 from dbo.Oca_ServiceWorkOrder where ServeItemB='03003' 
 and CallServiceId in (select CallServiceId from Oca_CallService where DoStatus>=0 and ServiceCatalog='00001')
 group by '['+convert(varchar(10),checkintime,120)+']'
 
 select CallServiceId from Oca_CallService where ServiceCatalog='00002'
 select a.id,a.CallServiceId,b.callserviceid from dbo.Oca_ServiceWorkOrder a, Oca_CallService b 
 where  a.id=b.id and a.Id<234 order by a.id;
 
  update dbo.Oca_ServiceWorkOrder set CallServiceId=b.callserviceid from dbo.Oca_ServiceWorkOrder a, Oca_CallService b 
 where  a.id=b.id and a.Id<234  
 
 update dbo.Oca_ServiceWorkOrder set ServeItemB='03002' where ID>36 and ID<70 and ServeItemB='03001'
 update dbo.Oca_ServiceWorkOrder set ServeItemB='03003' where ID>99 and ID<150 and ServeItemB='03001'
 
 update dbo.Oca_ServiceWorkOrder set CheckInTime=DATEADD(day,-id/10,CheckInTime) where ID<234;
  order by a.id;
 select count(ID),serveitemB from dbo.Oca_ServiceWorkOrder where  CallServiceId in (select callserviceid from Oca_CallService) 
 group by serveitemb 
 select id,serveitemB,checkintime from dbo.Oca_ServiceWorkOrder order by id;
 
 update Oca_CallService set DoStatus=3,DoResults='处理完成' where DoStatus=0
select * from Oca_CallService where DoStatus=3;

-------------------************************************** 
 alter procedure SP_DBA_Get_callservice_report 
 @begin_time datetime ,
 @end_time   datetime ,
 @type       char(5) 
 as
 declare @S varchar(6300)
 --declare @begin_time datetime 
 --declare @end_time   datetime 
 declare @sql varchar(6300)
 set @begin_time=isnull(@begin_time,dateadd(D,-16,getdate()))
 set @end_time=isnull(@end_time,dateadd(D,13,getdate()))
 set @type=ISNULL(@type,'03001')
 exec SP_DBA_Get_date_str @begin_time,@end_time, @S out
 --select @S,@begin_time,@end_time
 
 if @type='03001'
 begin
 set @sql='select '+@S+'
 from (
  select sum(dbo.FUNC_Oca_CeilingHour(ServeBeginTime,ServeEndTime))  fee,1 groupcolumn,
 convert(varchar(10),checkintime,120) days 
 from dbo.Oca_ServiceWorkOrder where ServeItemB='''+@type+''' 
 and CallServiceId in (select CallServiceId from Oca_CallService where DoStatus=3 and ServiceCatalog=''00001'')
 group by convert(varchar(10),checkintime,120) ) a
 pivot(sum(a.fee) for a.days in ('+@S+')) as pivotss;'
 end
 else if @type='03002' or @type='03003'
 begin
 set @sql='select '+@S+'
 from (
  select count(*)  fee,1 groupcolumn,
 convert(varchar(10),checkintime,120) days 
 from dbo.Oca_ServiceWorkOrder where ServeItemB='''+@type+''' 
 and CallServiceId in (select CallServiceId from Oca_CallService where DoStatus=3 and ServiceCatalog=''00001'')
 group by convert(varchar(10),checkintime,120) ) a
 pivot(sum(a.fee) for a.days in ('+@S+')) as pivotss;'
 end
 --select @sql
 exec(@sql) 
 
 ------------------------------------***************************************
 declare @begin_time datetime 
 declare @end_time   datetime 
 set @begin_time=isnull(@begin_time,dateadd(D,-18,getdate()))
 set @end_time=isnull(@end_time,dateadd(D,-16,getdate()))
 exec SP_DBA_Get_callservice_report  @begin_time,@end_time,'03002'
 
 select ' '+convert(varchar(10),getdate(),120)
 
 --------------------------------
 
 
 
 
 
 
  alter procedure SP_DBA_Get_date_str 
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

 set @str=@str+'['+convert(varchar(10),@temp,120)+'],'
 set @temp=DATEADD(d,1,@temp)
 end
 set @outStr=substring(@str,1,LEN(@str)-1)
 
 ----------------------==========================
 with b as(select area.AreaName areaName,a.Number,a.Time,a.Servetype 
 from Pub_Area area,
(select distinct AreaId2 Area,SUM(1)Number,SUM(case when CAST(callseconds as float)/60 between 0.01 
and 1 then 1 else (CallSeconds/60)+1 end)Time,substring(ServiceQueueNo,len(ServiceQueueNo)-0,1)Servetype 
from  Oca_CallService
 where Status=1  and (AreaId2='99999' or AreaId='99999' or AreaId3 ='99999' ) 
group by substring(ServiceQueueNo,len(ServiceQueueNo)-0,1),AreaId2
union
select distinct AreaId2 Area,SUM(1)Number,SUM(case when CAST(callseconds as float)/60 between 0.01 
and 1 then 1 else (CallSeconds/60)+1 end)Time,4 Servetype 
from  Oca_CallService 
 where Status=1  and (AreaId2='99999' or AreaId='99999' or AreaId3 ='99999' ) 
 and OldManId in (select distinct oldmanid from Oca_OldManConfigInfo where GovTurnkeyFlag=1)
group by AreaId2
)a 
where area.AreaId = a.Area)

select * from b order by areaname,servetype asc


select distinct AreaId2 Area,SUM(1)Number,SUM(case when CAST(callseconds as float)/60 between 0.01 
and 1 then 1 else (CallSeconds/60)+1 end)Time,4 Servetype 
from  Oca_CallService 
 where Status=1  and (AreaId2='99999' or AreaId='99999' or AreaId3 ='99999' ) 
 and OldManId in (select distinct oldmanid from Oca_OldManConfigInfo where GovTurnkeyFlag=1)
group by AreaId2