select address,ID,oldmanname from dbo.Oca_OldManBaseInfo where
 OldManname in('韦朋义','萧宾鸿','安锐达') 
select OldManId from dbo.Oca_OldManConfigInfo where locateflag=1)

update dbo.Oca_OldManBaseInfo set Address=null,LongitudeS=null ,LatitudeS=null where  ID<56 and ID>52 or ID=48
select address,ID,oldmanname from dbo.Oca_OldManBaseInfo where OldManId in (select OldManId from dbo.Oca_OldManConfigInfo where locateflag=1)

update dbo.oca_oldmanbaseinfo set Address='兴隆西村13幢301',LongitudeS='120.1941794083',LatitudeS='30.2462822276' where id=2;
update dbo.oca_oldmanbaseinfo set Address='霞晖北村12幢505',LongitudeS='120.1895683511',LatitudeS='30.2436933494' where id=34;
update dbo.oca_oldmanbaseinfo set Address='霞晖北村3幢502',LongitudeS='120.1890319093',LatitudeS='30.2434894295' where id=38;


insert into dbo.Oca_OldManLocateInfo  (oldmanid,homelongitudeS,homelatitudes,locatetime,locatelongitudeS,locatelatitudes)
select oldmanid ,120.18246899999997 homelongitudeS,30.234391 homelatitudes,getdate() locatetime,
(select cast(a.aa as varchar(32))+cast(a.aa*1000000000-CAST(a.aa*1000 as bigint)*1000000 as varchar(32)) locatelongitudeS
from (select round((select top 1 LocateLongitudeS from dbo.Oca_OldManLocateInfo  where 
 checkintime in(select top 30 checkintime from dbo.Oca_OldManLocateInfo 
 order by checkintime desc) and OldManId =(select OldManId from dbo.Oca_OldManBaseInfo where ID=@id))+(RAND(right(SYSDATETIME(),7))-0.25*(@id-@id/4*4))*0.001,9) aa) a
) locatelongitudeS,
(select cast(b.bb as varchar(32))+cast(b.bb*1000000000-CAST(b.bb*1000 as bigint)*1000000 as varchar(32)) locatelatitudes
from (select round((select top 1 LocateLatitudeS from dbo.Oca_OldManLocateInfo  where 
 checkintime in(select top 30 checkintime from dbo.Oca_OldManLocateInfo 
 order by checkintime desc) and OldManId =(select OldManId from dbo.Oca_OldManBaseInfo where ID=40))+(RAND(right(SYSDATETIME(),7))-0.25*(@id-@id/4*4))*0.001,9) bb) b
) locatelatitudes 
from dbo.oca_oldmanbaseinfo where ID=@id 
and cast(DATENAME(HH,GETDATE()) as int)< 22 and cast(DATENAME(HH,GETDATE()) as int)>7;

select top 30 checkintime from Oca_OldManLocateInfo order by checkintime desc 