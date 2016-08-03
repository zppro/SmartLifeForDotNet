select * into Oca_OldManBaseInfoOld0331
from dbo.Oca_OldManBaseInfo; ---32422

select * into Oca_OldManConfigInfoOld0331
from dbo.Oca_OldManConfigInfo; --29150

exec SP_DBA_ImportOldManInfoOnlyMobile

   select top 400 * from Oca_OldManBaseInfo order by CheckInTime desc;
   select top 400 * from Oca_OldManConfigInfo order by CheckInTime desc;