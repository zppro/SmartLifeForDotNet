---------------- order by newid()  实现随机取若干行数据
select top 1 newid() CallServiceId,DATEADD(S,-1*ceiling(RAND()*60),GETDATE()) CheckInTime,
3*ceiling(RAND()*60) callseconds,
 b.OldManId oldmanidold,b.ServiceQueueId servicequeueidold,b.ServiceQueueNo servicequeuenoold,b.ServiceCatalog servicecatalogold,
 b.AreaId areaidold,b.AreaId2 areaid2old,
 operatedby,OperatedOn,DataSource,AreaId3,Content,LongitudeS,LatitudeS,DoResults,ServiceExtId,ServiceExtNo 
 from  Oca_CallService b where b.CheckInTime <DATEADD(HH,-15,getdate()) and b.DoStatus=3 order by newid();
 
 
 select newid() CallServiceId,DATEADD(S,-1*ceiling(RAND()*60),GETDATE()) CheckInTime,
3*ceiling(RAND()*60) callseconds, (select top 1 oldmanid from dbo.Oca_CallService) OldManId,
(select top 1 serviceQueueId from dbo.Oca_CallService) ServiceQueueId,
 (select top 1 servicequeueno from dbo.Oca_CallService) ServiceQueueNo,@serveritemB ServiceCatalog,'01191' areaid,
 (select top 1 areaid2 from dbo.Oca_CallService) areaid2;
 
 
--------------------------------两表联合查询 复制字段值。
 update 
 Oca_CallService 
 set OldManId=c.OldManIdold,ServiceQueueId=c.servicequeueidold,ServiceQueueNo=c.servicequeuenoold,
 servicecatalog=c.servicecatalogold ,AreaId=c.areaidold,AreaId2=c.areaid2old,
operatedby=c.operatedby,OperatedOn=c.OperatedOn,DataSource=c.DataSource,AreaId3=c.AreaId3,
Content=c.Content,LongitudeS=c.LongitudeS,LatitudeS=c.LatitudeS,DoResults=c.DoResults,
ServiceExtId=c.serviceextid,ServiceExtNo=c.serviceextno
 from Oca_CallService a,(
  select a.*,b.CallServiceId callserviceidold,b.checkintime checkintimeold,b.callseconds callsecondsold,
 b.OldManId oldmanidold,b.ServiceQueueId servicequeueidold,b.ServiceQueueNo servicequeuenoold,b.ServiceCatalog servicecatalogold,
 b.AreaId areaidold,b.AreaId2 areaid2old,
 operatedby,OperatedOn,DataSource,AreaId3,Content,LongitudeS,LatitudeS,DoResults,ServiceExtId,ServiceExtNo
  from 
 ( select top 1000 ROW_NUMBER() OVER(order by  CheckInTime desc) AS id,
CallServiceId,checkintime,callseconds,OldManId,ServiceQueueId,ServiceQueueNo,ServiceCatalog,AreaId,AreaId2
 from Oca_CallService where DoStatus=3  and CheckInTime >DATEADD(hh,-14,getdate()) order by  CheckInTime desc) a

 inner join 
 (select top 1000 ROW_NUMBER() OVER(order by  NEWID() aSC) AS id,
CallServiceId,checkintime,callseconds,OldManId,ServiceQueueId,ServiceQueueNo,ServiceCatalog,AreaId,AreaId2,
operatedby,OperatedOn,DataSource,AreaId3,Content,LongitudeS,LatitudeS,DoResults,ServiceExtId,ServiceExtNo
 from Oca_CallService where DoStatus=3  and CheckInTime <DATEADD(hh,-14,getdate()) order by  NEWID() ) b
 on a.id=b.id
 ) c
 where a.callserviceid=c.callserviceid and a.CheckInTime >DATEADD(HH,-14,getdate()) and a.DoStatus=3 ;




/*****
update oca_oldmanbaseinfo set lognitudes=120.223078581,latitudes=30.2644447898 where id=25274;
update oca_oldmanbaseinfo set lognitudes=120.2176084539,latitudes=30.2645622609 where id=25277;
update oca_oldmanbaseinfo set lognitudes=120.217865946,latitudes=30.2620786333 where id=25270;
update oca_oldmanbaseinfo set lognitudes=120.2103149336,latitudes=30.2625558303 where id=25272;
update oca_oldmanbaseinfo set lognitudes=120.2103149336,latitudes=30.2618885809 where id=25276;
update oca_oldmanbaseinfo set lognitudes=120.2234032512,latitudes=30.2656125396 where id=25271;
update oca_oldmanbaseinfo set lognitudes=120.21524811,latitudes=30.2689733252 where id=25275;
update oca_oldmanbaseinfo set lognitudes=120.2076158809,latitudes=30.2722693153 where id=25273;

update oca_oldmanbaseinfo set longitudes=120.223078581,latitudes=30.2644447898 where id=25274;
update oca_oldmanbaseinfo set longitudes=120.2176084539,latitudes=30.2645622609 where id=25277;
update oca_oldmanbaseinfo set longitudes=120.217865946,latitudes=30.2620786333 where id=25270;
update oca_oldmanbaseinfo set longitudes=120.2103149336,latitudes=30.2625558303 where id=25272;
update oca_oldmanbaseinfo set longitudes=120.2103149336,latitudes=30.2618885809 where id=25276;
update oca_oldmanbaseinfo set longitudes=120.2234032512,latitudes=30.2656125396 where id=25271;
update oca_oldmanbaseinfo set longitudes=120.21524811,latitudes=30.2689733252 where id=25275;
update oca_oldmanbaseinfo set longitudes=120.2076158809,latitudes=30.2722693153 where id=25273;

***/
