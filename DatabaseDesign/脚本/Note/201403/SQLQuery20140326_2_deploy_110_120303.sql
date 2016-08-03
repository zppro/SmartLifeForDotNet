select * from sheetSource;
select * from  temp_excelsource;

select * into Oca_OldManBaseInfoOld0326
from dbo.Oca_OldManBaseInfo; ---31855

select * into Oca_OldManConfigInfoOld0326
from dbo.Oca_OldManConfigInfo; --28769

select * into Oca_OldManFamilyInfoOld0326
from dbo.Oca_OldManFamilyInfo; --44911

select * into Oca_FamilyMemberOld0326
from dbo.Oca_FamilyMember; --44808

select * into Oca_CallServiceOld0326
from dbo.Oca_CallService;  --50762


exec dbo.SP_DBA_ImportOldManBaseInfo  ---567
exec SP_DBA_ImportOldManFamilyInfo
exec SP_DBA_InsertCallNoChange
exec dbo.SP_DBA_SolveConfictOfCallNo