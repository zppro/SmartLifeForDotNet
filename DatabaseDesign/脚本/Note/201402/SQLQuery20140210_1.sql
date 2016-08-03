insert into dbo.Oca_OldManLocateInfo (oldmanid,homelongitudeS,homelatitudes,locatetime,locatelongitudeS,locatelatitudes)
select  OldManId,  homelongitudeS,HomeLatitudeS,GETDATE() locatetime,LocateLongitudeS,LocateLatitudeS
 from dbo.Oca_OldManLocateInfo 
 where 
 cast(DATENAME(HH,GETDATE()) as int)> 22 or cast(DATENAME(HH,GETDATE()) as int)<7 and 
 cast(DATENAME(HH,checkintime) as int)= 22 and CheckInTime > dateadd(day,-1,GETDATE()) and cast(DATENAME(n,checkintime) as int)= 0

 select LocateLongitudeS,LocateLatitudeS from dbo.Oca_OldManLocateInfo  where 
 checkintime in(select top 30 checkintime from dbo.Oca_OldManLocateInfo 
 order by checkintime desc) and OldManId =(select OldManId from dbo.Oca_OldManBaseInfo where ID=40);



select round(120.18246899999997+(RAND(right(SYSDATETIME(),6))-0.5)*0.005,9) aa,cast(CAST (round(120.18246899999997+(RAND(right(SYSDATETIME(),6))-0.5)*0.005,9)*1000000000  as bigint)/1000000000 as varchar(32))

select cast(a.aa as varchar(32))+cast(a.aa*1000000000-CAST(a.aa*1000 as bigint)*1000000 as varchar(32)) locatelongitudeS
from (select round(120.18246899999997+(RAND(right(SYSDATETIME(),6))-0.5)*0.005,9) aa) a

select cast(b.bb as varchar(32))+cast(b.bb*1000000000-CAST(b.bb*1000 as bigint)*1000000 as varchar(32)) locatelatitudes
from (select round(30.234391+(RAND(right(SYSDATETIME(),6))-0.5)*0.005,9) bb) b


insert into dbo.Oca_OldManLocateInfo  (oldmanid,homelongitudeS,homelatitudes,locatetime,locatelongitudeS,locatelatitudes)
select oldmanid ,120.18246899999997 homelongitudeS,30.234391 homelatitudes,getdate() locatetime,round(120.18246899999997+(RAND(right(SYSDATETIME(),6))-0.5)*0.005,12) locatelongitudeS,round(30.234391+(RAND(right(SYSDATETIME(),6))-0.5)*0.005,12) locatelatitudes 
from dbo.oca_oldmanbaseinfo where ID=169400 and OldManId in (select b.OldManId from oca_oldmanconfiginfo b where  b.LocateFlag=1);


insert into dbo.Oca_OldManLocateInfo  (oldmanid,homelongitudeS,homelatitudes,locatetime,locatelongitudeS,locatelatitudes)
select oldmanid ,120.18246899999997 homelongitudeS,30.234391 homelatitudes,getdate() locatetime,
(select cast(a.aa as varchar(32))+cast(a.aa*1000000000-CAST(a.aa*1000 as bigint)*1000000 as varchar(32)) locatelongitudeS
from (select round(120.18246899999997+(RAND(right(SYSDATETIME(),6))*RAND(right(SYSDATETIME(),3))-0.25)*0.005,9) aa) a
) locatelongitudeS,
(select cast(b.bb as varchar(32))+cast(b.bb*1000000000-CAST(b.bb*1000 as bigint)*1000000 as varchar(32)) locatelatitudes
from (select round(30.234391+(RAND(right(SYSDATETIME(),6))-0.5)*0.005,9) bb) b
) locatelatitudes 
from dbo.oca_oldmanbaseinfo where  OldManId in (select b.OldManId from oca_oldmanconfiginfo b where  b.LocateFlag=1);

select * from dbo.Oca_OldManBaseInfo where OldManName='温茂材';

select right(SYSDATETIME(),7)
select 40-40/4*4,39-39/4*4,38-38/4*4,37-37/4*4 (@id-@id/4*4)

RAND(right(SYSDATETIME(),6))*RAND(right(SYSDATETIME(),3))-0.25

select * from dbo.Oca_ServiceWorkOrder order by id;
select * from dbo.Pub_ServiceStation;
select * from dbo.Oca_OldManConfigInfo;
select * from dbo.oca_oldmanbaseinfo
select * from dbo.Oca_MerchantServeItem;
select * from dbo.Oca_MerchantServeMode;
select * from dbo.Sys_DictionaryItem;
select * from dbo.Pub_Reminder;

select * from sys.default_constraints where parent_object_id in(select object_id from sys.all_objects where  name='Oca_ServiceWorkOrder');

insert into dbo.Oca_ServiceWorkOrder (ID,workordercontent,serveItemA,ServeItemB,OldManId,OldManName,Gender,ServeStationId,ServeStationName,ServeManName,ServeBeginTime,
ServeEndTime)
select ID,workorderid,workordercontent,serveItemA,ServeItemB,OldManId,OldManName,Gender,ServeStationId,ServeStationName,ServeManName,ServeBeginTime,
ServeEndTime,CallServiceId
from dbo.Oca_ServiceWorkOrder;

select b.StationId,b.StationName from dbo.Pub_ServiceStation b;
insert into dbo.Oca_ServiceWorkOrder (workorderid,workordercontent,serveItemA,ServeItemB,OldManId,OldManName,Gender,ServeStationId,ServeStationName,ServeManName,ServeBeginTime,
ServeEndTime,CallServiceId)
select NEWID() workorderid,'洗衣服' workordercontent,'00003' serveitema,'03001' serveitemb,a.oldmanid,a.oldmanname,a.gender, b.StationId,b.StationName,'李小凤' servemanname,dateadd(HH,-1,GETDATE()) ServeBeginTime,GETDATE() ServeEndTime,NEWID() callserviceid
from dbo.Oca_OldManBaseInfo a,dbo.Pub_ServiceStation b
where StationName ='亮洁家政';
'青禾居家服务中心' ;
like '%家%';

insert into dbo.Pub_ServiceStation (stationid,StationName,StationType) values('00004000-0000-0000-0000-711000000002','上城区第一医院','00004');
insert into dbo.Pub_ServiceStation (stationid,StationName,StationType) values('00004000-0000-0000-0000-711000000003','上城区疗养院','00004');

select a.areaid,a.areaid2,b.AreaId,b.AreaId2 from dbo.Oca_ServiceWorkOrder a,dbo.Pub_ServiceStation b
where a.ServeStationName=b.StationName;

update dbo.Oca_ServiceWorkOrder  set areaid=b.areaid,areaid2=b.AreaId2 
from dbo.Oca_ServiceWorkOrder a,dbo.Pub_ServiceStation b
where a.ServeStationName=b.StationName;

select a.ServeFee,b.ServeFee from dbo.Oca_ServiceWorkOrder a,dbo.Oca_MerchantServeItem b
where a.ServeStationId=b.StationId and a.ServeItemB=b.ServeItemB;

update dbo.Oca_ServiceWorkOrder set ServeFee=b.ServeFee from dbo.Oca_ServiceWorkOrder a,dbo.Oca_MerchantServeItem b
where a.ServeStationId=b.StationId and a.ServeItemB=b.ServeItemB;

update dbo.Oca_ServiceWorkOrder set ServeFeeByGov=ServeFee

update dbo.Oca_ServiceWorkOrder set workordercontent='安装电视机',serveitemb='03003' where ID between 10 and 19

update dbo.Oca_ServiceWorkOrder set workordercontent='安装空调',serveitemb='03002' where ID between 31 and 35

update dbo.Oca_ServiceWorkOrder set workordercontent='搬家具',serveitemb='03006' where ID between 131 and 132

update dbo.Oca_ServiceWorkOrder set workordercontent='维护管道',serveitemb='03004' where ID between 156 and 158

update dbo.Oca_OldManBaseInfo set Address=REPLACE(Address,'西湖区','上城区')
select Address,REPLACE(Address,'西湖区','上城区') from dbo.Oca_OldManBaseInfo;
select 10%3 

