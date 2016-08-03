select Mobile from Oca_OldManBaseInfo where OldManName like '¿œ»À%' order by mobile

select (select AreaName from Pub_Area where AreaId=b.ParentId) street,AreaName 
from Pub_Area  b
where Levels=5 order by AreaCode;