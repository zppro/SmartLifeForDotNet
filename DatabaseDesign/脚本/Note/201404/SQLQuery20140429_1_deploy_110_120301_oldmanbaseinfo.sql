/**********************************************************/
/* SP_DBA_ImportOldManBaseInfo                            */
/* 导入老人的基本信息                                     */
/* 前提条件是 temp_excelsource表存在                      */
/**********************************************************/
create procedure [dbo].[SP_DBA_ImportOldManBaseInfo]
as 
BEGIN
if (exists (select *
            from  temp_excelsource 
            where idno not in (select idno from Oca_OldManBaseInfo)))
   begin 
         insert into   Oca_OldManConfigInfo  (OldManId,CheckInTime,DataSource,CallNo,LocateFlag,GovTurnkeyFlag)
         select oldmanid,GETDATE() checkintime,'00003' DataSource,mobile callno,0 locateflag,0 govturnkeyflag
         from  temp_excelsource 
         where mobile not in (select callno from Oca_OldManConfigInfo);
   
         insert into Oca_OldManBaseInfo(OldManId,CheckInTime,Status,OperatedBy,OperatedOn,DataSource,
         OldManName,Gender,Birthday,IDNo,HealthInsuranceFlag,HealthInsuranceNumber,SocialInsuranceFlag,
         SocialInsuranceNumber,LivingState,OldManIdentity,AreaId,AreaId2,AreaId3,Address,LongitudeS,
         LatitudeS,PostCode,Tel,Mobile,Remark,InputCode1,InputCode2)

         select OldManId,getdate() CheckInTime,Status,OperatedBy,OperatedOn,DataSource,OldManName,Gender,Birthday,
               IDNo,HealthInsuranceFlag,HealthInsuranceNumber,SocialInsuranceFlag,SocialInsuranceNumber,
               LivingState,OldManIdentity,AreaId,AreaId2,AreaId3,Address,LongitudeS,LatitudeS,PostCode,Tel,
               Mobile,Remark,InputCode1,InputCode2
         from  temp_excelsource 
         where idno not in (select idno from Oca_OldManBaseInfo);
         
        
  end 
END 

select * into Oca_OldManConfigInfoOld0429 
from Oca_OldManConfigInfo;

select * into Oca_OldManBaseInfoOld0429
from Oca_OldManBaseInfo;

select * from Oca_OldManBaseInfo where OldManName like '%/%';

exec SP_DBA_ImportOldManBaseInfo
update Oca_OldManBaseInfo set areaidforcall='AAA01189-0999-0000-0000-000000000000' where areaidforcall is null;

select  oldmanname, case substring(idno,17,1)%2  when 1 then 'M' else 'F' end  from Oca_OldManBaseInfo where DataSource='00003'
update Oca_OldManBaseInfo set Gender =case substring(idno,17,1)%2  when 1 then 'M' else 'F' end 
where DataSource='00003'

update Oca_OldManBaseInfo set OldManName=SUBSTRING(oldmanname,1,charindex('/',oldmanname)-1)
where DataSource='00003' and charindex('/',oldmanname)>1

 update dbo.Oca_OldManBaseInfo
set birthday=cast(SUBSTRING(idno,7,8) as datetime)
from dbo.Oca_OldManBaseInfo a
where  len(idno)=18 and substring(idno,7,8)  like '19%'  
and (substring(idno,11,2) between '01' and '12' )
and (substring(idno,13,2) between '01' and '30' )
and Birthday is null ;

select SUBSTRING(idno,7,8),cast(SUBSTRING(idno,7,8) as datetime)
from dbo.Oca_OldManBaseInfo a
where  len(idno)=18 and substring(idno,7,8)  like '19%'  
and (substring(idno,11,2) between '01' and '12' )
and (substring(idno,13,2) between '01' and '30' )
and Birthday is null ;

select  * --SUBSTRING(idno,7,8)
from dbo.Oca_OldManBaseInfo where substring(idno,13,2)='31' 

 update dbo.'+@UpdateTableName+'
set birthday=cast('19'+SUBSTRING(f4,7,6) as datetime)
from dbo.'+@UpdateTableName+' a,dbo.'+@TableName+' b
where a.idno=b.f4 and len(f4)=15;

select SUBSTRING(idno,7,6),cast('19'+SUBSTRING(idno,7,6) as datetime)
from dbo.Oca_OldManBaseInfo a
where  len(idno)=15 --and substring(idno,7,8)  like '19%'  
and (substring(idno,9,2) between '01' and '12' )
and (substring(idno,11,2) between '01' and '31' )
and Birthday is null ;

 update dbo.Oca_OldManBaseInfo
set birthday=cast('19'+SUBSTRING(idno,7,6) as datetime)
from dbo.Oca_OldManBaseInfo a
where  len(idno)=15 --and substring(idno,7,8)  like '19%'  
and (substring(idno,9,2) between '01' and '12' )
and (substring(idno,11,2) between '01' and '31' )
and Birthday is null ;