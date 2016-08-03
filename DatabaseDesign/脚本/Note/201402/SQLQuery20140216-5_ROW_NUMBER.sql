 select top 1 a.LongitudeS,a.LatitudeS
 from (
 select  ROW_NUMBER() OVER(ORDER BY id aSC) AS id,LongitudeS,LatitudeS from dbo.oca_oldmanbaseinfo where LongitudeS is not null) a
 where id=50%15 ;
 
 select ID,idno from dbo.Oca_OldManBaseInfo where  OldManId in (select b.OldManId from oca_oldmanconfiginfo b where  b.LocateFlag=1) order by id;
 
 update dbo.oca_oldmanbaseinfo set ID=1000 where ID=2;
  update dbo.oca_oldmanbaseinfo set ID=2 where ID=39;
    update dbo.oca_oldmanbaseinfo set ID=39 where ID=1000;
  
  select * from Oca_OldManBaseInfo  where LongitudeS in(
  select top 30 LocateLongitudeS LongitudeS from  Oca_OldManLocateInfo  order by CheckInTime desc )
  and LatitudeS in( select top 30 LocateLatitudeS LatitudeS from  Oca_OldManLocateInfo  order by CheckInTime desc );