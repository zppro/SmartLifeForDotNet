select * into Oca_OldManBaseInfoOld0403
from Oca_OldManBaseInfo

select * into Oca_OldManConfigInfoOld0403
from Oca_OldManConfigInfo

select * into Oca_OldManFamilyInfoOld0403
from Oca_OldManFamilyInfo

select * into Oca_FamilyMemberOld0403
from Oca_FamilyMember;

select * from Temp_ExcelSource;
update  Temp_ExcelSource  set areaid='01192'
where AreaId='01191'

select * from Oca_OldManBaseInfo;

exec dbo.SP_DBA_ImportOldManBaseInfo  ---567
exec SP_DBA_ImportOldManFamilyInfo

update Oca_OldManBaseInfo set areaidforcall='AAA01192-0999-0000-0000-000000000000'
where areaidforcall is null;

insert into   Oca_OldManConfigInfo  (OldManId,CheckInTime,DataSource,CallNo,LocateFlag,GovTurnkeyFlag)
         select oldmanid,GETDATE() checkintime,'00003' DataSource,mobile callno,0 locateflag,0 govturnkeyflag
         from  temp_excelsource 
         where mobile not in (select callno from Oca_OldManConfigInfo);
         
         select a.IDNo,a.OldManName
         from (
         select IDNo,OldManName from Oca_OldManBaseInfo where convert(varchar(10),CheckInTime ,120)<CONVERT(varchar(10),getdate(),120)) a,
         (select   f4,f1 from sheetSource) b
         where a.IDNo=b.F4
         
          where  areaidforcall is null;
         
         select * from Pub_Area;
         
         select f4,f1, count(*) from sheetSource group by f4,f1 having COUNT(*)>1
         order by f4,f1 ;