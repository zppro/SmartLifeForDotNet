USE [SmartLife-120304]
GO
/****** Object:  StoredProcedure [dbo].[SP_DBA_ImportOldManBaseInfo]    Script Date: 05/08/2014 14:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************/
/* SP_DBA_ImportOldManBaseInfo                            */
/* 导入老人的基本信息                                     */
/* 前提条件是 temp_excelsource表存在                      */
/**********************************************************/
ALTER procedure [dbo].[SP_DBA_ImportOldManBaseInfo]
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

exec sp_rename @objname='Temp_ExcelSource',@newname='Temp_ExcelSource3'

select * into Oca_OldManBaseInfoOld0508
from Oca_OldManBaseInfo;

select * into Oca_OldManConfigInfoOld0508
from Oca_OldManConfigInfo;

exec SP_DBA_ImportOldManBaseInfo 

select * from Oca_OldManBaseInfo where AreaIdForCall is null;
