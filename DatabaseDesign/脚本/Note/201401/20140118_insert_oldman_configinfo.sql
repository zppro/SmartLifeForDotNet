insert into dbo.Oca_OldManConfigInfo (OldManId,LocateFlag,FenceRadius,GovTurnkeyFlag)
select oldmanid,1 LocateFlag,300 FenceRadius,1 GovTurnkeyFlag from dbo.Oca_OldManBaseInfo where ID between 138 and 147;
insert into dbo.Oca_OldManConfigInfo (OldManId,LocateFlag,FenceRadius,GovTurnkeyFlag)
select oldmanid,1 LocateFlag,300 FenceRadius,0 GovTurnkeyFlag from dbo.Oca_OldManBaseInfo where ID between 148 and 160;
insert into dbo.Oca_OldManConfigInfo (OldManId,LocateFlag,FenceRadius,GovTurnkeyFlag)
select oldmanid,1 LocateFlag,300 FenceRadius,0 GovTurnkeyFlag from dbo.Oca_OldManBaseInfo where ID between 131 and 137;
insert into dbo.Oca_OldManConfigInfo (OldManId,LocateFlag,FenceRadius,GovTurnkeyFlag)
select oldmanid,0 LocateFlag,300 FenceRadius,0 GovTurnkeyFlag from dbo.Oca_OldManBaseInfo where ID between 161 and 211;
insert into dbo.Oca_OldManConfigInfo (OldManId,LocateFlag,FenceRadius,GovTurnkeyFlag)
select oldmanid,0 LocateFlag,300 FenceRadius,0 GovTurnkeyFlag from dbo.Oca_OldManBaseInfo where ID between 99 and 130;

