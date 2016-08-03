SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 alter procedure [dbo].[SP_SIM_update_call_service_dostatus] 

 @serveritemB char(5)='00001',
 @status     tinyint=0 --,
 --@outStr     varchar(3500) out
 as

 update dbo.Oca_CallService set DoStatus=DoStatus+1 where DoStatus=2 --and ServiceCatalog=@serveritemB
 update dbo.Oca_CallService set DoStatus=DoStatus+1 where DoStatus=1 --and ServiceCatalog=@serveritemB
 update dbo.Oca_CallService set DoStatus=DoStatus+1 where DoStatus=0 --and ServiceCatalog=@serveritemB
 if(@status>0)
 begin
 update dbo.Oca_CallService set DoStatus=DoStatus+1 where DoStatus=@status-1 and ServiceCatalog=@serveritemB
 end