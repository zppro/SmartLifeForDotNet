update dbo.Oca_OldManBaseInfo set LivingState='00002' where LivingState is null and ID>18 and ID<39;

update dbo.Oca_OldManBaseInfo set LivingState='00003' where LivingState is null and ID>38 and ID<79;

update dbo.Oca_OldManBaseInfo set LivingState='00004' where LivingState is null and ID>78 and ID<39;
select * from dbo.Oca_OldManBaseInfo
select * from  dbo.Oca_OldManLocateInfo;
select * from dbo.Oca_Callservice;
select * from dbo.sys_dictionaryitem where itemname='½ô¼±¾ÈÖú';
select * from dbo.oca_merchantservemode;

update dbo.Oca_OldManBaseInfo set OldManIdentity='00001' where OldManIdentity is null and ID<30
update dbo.Oca_OldManBaseInfo set OldManIdentity='00001' where OldManIdentity is null and ID>60
update dbo.Oca_OldManBaseInfo set OldManIdentity='00002' where OldManIdentity is null 

insert into dbo.Oca_Callservice (CallServiceId,oldmanid,callseconds,servicequeueid,servicequeueno,ServiceCatalog)
select newid() callserviceid,a.OldManId oldmanid,60 callseconds,newid() servicequeueid,711000 servicequeueno,'00001' ServiceCatalog
from dbo.Oca_OldManBaseInfo a

insert into dbo.Oca_Callservice (CallServiceId,oldmanid,callseconds,servicequeueid,servicequeueno,ServiceCatalog)
select newid() callserviceid,a.OldManId oldmanid,floor(RAND(20)*1000) callseconds,newid() servicequeueid,711003 servicequeueno,'00002' ServiceCatalog
from dbo.Oca_OldManBaseInfo a 
where ID>80

update dbo.Oca_Callservice set servicequeno=711003 from dbo.Oca_Callservice a 
where a.ID>80 and a.ServiceCatalog='00002'

select * 
from dbo.Oca_Callservice a 
where a.ID>80 and a.ServiceCatalog='00002'