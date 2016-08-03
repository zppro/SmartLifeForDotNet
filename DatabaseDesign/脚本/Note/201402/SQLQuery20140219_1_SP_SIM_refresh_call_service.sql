USE [SmartLife-120399]
GO
/****** Object:  StoredProcedure [dbo].[SP_SIM_refresh_call_service]    Script Date: 02/19/2014 14:19:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 ALTER  procedure [dbo].[SP_SIM_refresh_call_service] 
 @begin_time datetime ,
 @end_time   datetime ,
 @rowcount  int=20,
 @serveritemB char(5)='00001',
 @status     tinyint=0 --,
 --@outStr     varchar(3500) out
 as
 declare @item int
 declare @str varchar(2000)
 set @item=0
-- print 'status'+cast(@status as nvarchar(32))
if( DATENAME(HH,getdate())>21 or (DATENAME(HH,getdate())<6))
begin 
set @rowcount=12
end 

if (DATENAME(HH,getdate())=6)
begin 
set @rowcount=12
end 

if (DATENAME(HH,getdate())=7)
begin 
set @rowcount=24
end 

if (DATENAME(HH,getdate())=8)
begin 
set @rowcount=36
end 

if (DATENAME(HH,getdate())=9)
begin 
set @rowcount=48
end 

if( DATENAME(HH,getdate())>9 and (DATENAME(HH,getdate())<18))
begin 
set @rowcount=60
end 

if (RAND()<0.62)
begin
set @serveritemB='00001'
end

if (RAND()>0.62 and (RAND()<0.62+0.16666))
begin
set @serveritemB='00002'
end

if (RAND()>0.62+0.16666 and (RAND()<0.62+0.16666+0.16666))
begin
set @serveritemB='00003'
end

 if (@status=0)
 begin
 while (@item<@rowcount/12)
 begin
 /*insert into dbo.Oca_CallService (CallServiceId,checkintime,callseconds,OldManId,ServiceQueueId,ServiceQueueNo,ServiceCatalog,AreaId,AreaId2)
 select newid() CallServiceId,DATEADD(S,-1*ceiling(RAND()*60),GETDATE()) CheckInTime,
3*ceiling(RAND()*60) callseconds, (select top 1 oldmanid from dbo.Oca_CallService) OldManId,
(select top 1 serviceQueueId from dbo.Oca_CallService) ServiceQueueId,
 (select top 1 servicequeueno from dbo.Oca_CallService) ServiceQueueNo,@serveritemB ServiceCatalog,'01191' areaid,
 (select top 1 areaid2 from dbo.Oca_CallService) areaid2;*/
 
 
  insert into dbo.Oca_CallService (CallServiceId,checkintime,callseconds,OldManId,ServiceQueueId,ServiceQueueNo,ServiceCatalog,AreaId,AreaId2,
 operatedby,OperatedOn,DataSource,AreaId3,Content,LongitudeS,LatitudeS,DoResults,ServiceExtId,ServiceExtNo)
 select top 1 newid() CallServiceId,DATEADD(S,-1*ceiling(RAND()*60),GETDATE()) CheckInTime,
3*ceiling(RAND()*60) callseconds,
 b.OldManId oldmanidold,b.ServiceQueueId servicequeueidold,b.ServiceQueueNo servicequeuenoold,b.ServiceCatalog servicecatalogold,
 b.AreaId areaidold,b.AreaId2 areaid2old,
 operatedby,OperatedOn,DataSource,AreaId3,Content,LongitudeS,LatitudeS,DoResults,ServiceExtId,ServiceExtNo 
 from  Oca_CallService b where b.CheckInTime <DATEADD(HH,-15,getdate()) and b.DoStatus=3 order by newid();
 
 set @item=@item+1
-- print 'item:'+cast(@item as nvarchar(32)) 
 end
 end
 
 if(@status>0)
 begin
 update dbo.Oca_CallService set DoStatus=DoStatus+1 where DoStatus=@status-1 and ServiceCatalog=@serveritemB
 end


--select COUNT(*)  from Oca_CallService where CheckInTime > dateadd(HH,-15,getdate()) and DataSource='00002' and DATEADD(HH,-2,getdate())) and DoStatus=3

--update Oca_CallService  set datasource='00003' where CheckInTime > dateadd(HH,-15,getdate()) and DataSource='00002'

