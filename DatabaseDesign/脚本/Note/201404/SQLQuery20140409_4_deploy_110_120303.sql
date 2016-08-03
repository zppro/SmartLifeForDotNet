/*select * from Oca_OldManBaseInfo where areaidforcall is null ;
where OldManName='¿Ó«Ïª™'

areaidforcall is null ;
AAA01191-0999-0000-0000-000000000000
select * from Oca_OldManConfigInfo
where OldManId in (select OldManId from Oca_OldManBaseInfo 
where OldManName='∫È≈Ù∑…'
)
*/

select * into Oca_OldManBaseInfoOld0409
from Oca_OldManBaseInfo;

select * into Oca_OldManConfigInfoOld0409
from Oca_OldManConfigInfo;

insert into   Oca_OldManConfigInfo  (OldManId,CheckInTime,DataSource,CallNo,LocateFlag,GovTurnkeyFlag)
         select oldmanid,GETDATE() checkintime,'00003' DataSource,mobile callno,0 locateflag,0 govturnkeyflag
         from  Oca_OldManBaseInfo 
         where mobile not in (select callno from Oca_OldManConfigInfo) and OldManId not in (select OldManId from Oca_OldManConfigInfo);
         
         update Oca_OldManBaseInfo set AreaIdForCall='AAA01191-0999-0000-0000-000000000000'
         where areaidforcall is null ;