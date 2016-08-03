select top 30 * from  dbo.Oca_OldManLocateInfo order by id desc;
select count(*) from dbo.Oca_OldManLocateInfo where ID=3000000;
select a.areaid,a.AreaId2,a.AreaId3,b.AreaId,b.AreaId2,b.AreaId3 from dbo.Oca_CallService a ,dbo.Oca_OldManBaseInfo b
where a.OldManId=b.OldManId;

update dbo.Oca_CallService set areaid=b.AreaId,AreaId2=b.AreaId2,AreaId3=b.AreaId3
from dbo.Oca_CallService a ,dbo.Oca_OldManBaseInfo b
where a.OldManId=b.OldManId and a.AreaId is null;

select * from dbo.Oca_CallService where ServiceExtId is null;

select  OldManId,  homelongitudeS,HomeLatitudeS,GETDATE() locatetime,checkintime,LocateLongitudeS,LocateLatitudeS
 from dbo.Oca_OldManLocateInfo 
 where 
 --cast(DATENAME(HH,GETDATE()) as int)> 22 or cast(DATENAME(HH,GETDATE()) as int)<7 and 
 cast(DATENAME(HH,checkintime) as int)= 22 --and CheckInTime > dateadd(day,-1,GETDATE()) 
 and cast(DATENAME(n,checkintime) as int)= 0
 
 select dateadd(day,-1,GETDATE())
 
 
 select oldmanid ,120.18246899999997 homelongitudeS,30.234391 homelatitudes,getdate() locatetime,
(select cast(a.aa as varchar(32))+cast(a.aa*1000000000-CAST(a.aa*1000 as bigint)*1000000 as varchar(32)) locatelongitudeS
from (select round((select top 1 LocateLongitudeS from dbo.Oca_OldManLocateInfo  where 
 checkintime in(select top 30 checkintime from dbo.Oca_OldManLocateInfo 
 order by checkintime desc) and OldManId =(select OldManId from dbo.Oca_OldManBaseInfo where ID=40))+(RAND(right(SYSDATETIME(),7))-0.25*(40-40/4*4))*0.001,9) aa) a
) locatelongitudeS,
(select cast(b.bb as varchar(32))+cast(b.bb*1000000000-CAST(b.bb*1000 as bigint)*1000000 as varchar(32)) locatelatitudes
from (select round((select top 1 LocateLatitudeS from dbo.Oca_OldManLocateInfo  where 
 checkintime in(select top 30 checkintime from dbo.Oca_OldManLocateInfo 
 order by checkintime desc) and OldManId =(select OldManId from dbo.Oca_OldManBaseInfo where ID=40))+(RAND(right(SYSDATETIME(),7))-0.25*(40-40/4*4))*0.001,9) bb) b
) locatelatitudes 
from dbo.oca_oldmanbaseinfo where ID=40 and OldManId in (select b.OldManId from oca_oldmanconfiginfo b where  b.LocateFlag=1)
and cast(DATENAME(HH,GETDATE()) as int)< 22 and cast(DATENAME(HH,GETDATE()) as int)>7;





update dbo.Oca_Callservice set servicequeueno=711002 from dbo.Oca_Callservice a 
where a.ID>60 and ID<90 and a.ServiceCatalog='00001'

select * 
from dbo.Oca_Callservice a 
where a.ID>90 and a.ServiceCatalog='00001'