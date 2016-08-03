select top 10 * from dbo.Oca_CallService order by id desc ;

select DATEADD(S,-3*ceiling(RAND()*60),GETDATE())

insert into dbo.Oca_CallService ()
select 
drop procedure dbo.sp_dba_refresh_call_service;

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 alter procedure [dbo].[SP_SIM_refresh_call_service] 
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
set @rowcount=1
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
 insert into dbo.Oca_CallService (CallServiceId,checkintime,callseconds,OldManId,ServiceQueueId,ServiceQueueNo,ServiceCatalog)
 select newid() CallServiceId,DATEADD(S,-5*ceiling(RAND()*60),GETDATE()) CheckInTime,
3*ceiling(RAND()*60) callseconds, (select top 1 oldmanid from dbo.Oca_CallService)OldManId,
 '10000000-0000-0000-0000-710020000001' ServiceQueueId,
 '710020' ServiceQueueNo,@serveritemB ServiceCatalog;
 set @item=@item+1
-- print 'item:'+cast(@item as nvarchar(32)) 
 end
 end
 
 if(@status>0)
 begin
 update dbo.Oca_CallService set DoStatus=DoStatus+1 where DoStatus=@status-1 and ServiceCatalog=@serveritemB
 end
 -------------------------------------------
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create procedure [dbo].[SP_SIM_update_call_service_dostatus] 

 @serveritemB char(5)='00001',
 @status     tinyint=0 --,
 --@outStr     varchar(3500) out
 as

 
 if(@status>0)
 begin
 update dbo.Oca_CallService set DoStatus=DoStatus+1 where DoStatus=@status-1 and ServiceCatalog=@serveritemB
 end
 
 --set @outStr=substring(@str,1,LEN(@str)-1)


create table dbo.oca_call_service_stat_report
( dates datetime null,
  item1 int null,
  item2 int null,
  item3 int null,
  item4 int null,
  total int null 
)

insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2013-12-23',323,332,121,121);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2013-12-24',542,463,471,242);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2013-12-25',657,658,592,335);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2013-12-26',878,793,559,347);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2013-12-27',951,897,689,443);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2013-12-28',967,917,575,352);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2013-12-29',892,885,675,363);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2013-12-30',923,898,572,375);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2013-12-31',945,882,592,356);


insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-1',448,172,137,69);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-2',437,192,135,56);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-3',496,105,170,53);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-4',571,156,111,48);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-5',536,163,184,52);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-6',531,194,190,63);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-7',541,217,172,59);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-8',445,215,187,68);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-9',569,256,140,60);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-10',554,259,119,58);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-11',490,218,111,53);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-12',468,235,94,57);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-13',461,224,83,55);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-14',485,227,79,48);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-15',478,223,65,53);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-16',498,231,83,55);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-17',459,258,81,64);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-18',502,315,73,60);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-19',527,233,126,68);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-20',511,216,92,51);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-21',491,247,69,61);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-22',487,245,76,68);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-23',476,152,74,60);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-24',497,136,94,58);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-25',549,112,67,63);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-26',497,136,64,55);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-27',444,118,100,62);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-28',423,145,99,46);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-29',441,115,86,45);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-30',296,144,79,62);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-1-31',259,121,124,39);

insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-1',252,155,146,12);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-2',203,109,110,14);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-3',215,88,126,21);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-4',263,74,116,25);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-5',255,106,109,24);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-6',289,102,98,18);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-7',237,196,68,9);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-8',279,187,93,23);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-9',266,175,71,36);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-10',320,111,83,15);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-11',343,81,123,20);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-12',345,129,86,18);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-13',280,186,111,17);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-14',308,163,102,12);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-15',305,143,118,15);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-16',311,184,84,11);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-17',295,178,73,22);
insert into oca_call_service_stat_report (dates,item1,item2,item3,item4)values ('2014-2-18',351,134,84,11);

select * from oca_call_service_stat_report
update oca_call_service_stat_report set total=item1+item2+item3+item4;

alter table oca_call_service_stat_report add  	percent1	 float  null;
alter table oca_call_service_stat_report add  	percent2	 float null;
alter table oca_call_service_stat_report add  	percent3	float null;
alter table oca_call_service_stat_report add  	percent4	 float null;
alter table oca_call_service_stat_report add  	percent5	float null;
alter table oca_call_service_stat_report add  	percent6	float null;
alter table oca_call_service_stat_report add  	percent7	 float null;
alter table oca_call_service_stat_report add  	percent8	float null;
alter table oca_call_service_stat_report add  	percent9	 float null;
alter table oca_call_service_stat_report add  	percent10	 float null;
alter table oca_call_service_stat_report add  	percent11	float null;
alter table oca_call_service_stat_report add  	percent12	float null;
alter table oca_call_service_stat_report add  	percent13	float null;
alter table oca_call_service_stat_report add  	percent14	 float null;
alter table oca_call_service_stat_report add  	percent15	float null;
alter table oca_call_service_stat_report add  	percent16	float null;
alter table oca_call_service_stat_report add  	percent17	float null;
alter table oca_call_service_stat_report add  	percent18	float null;
alter table oca_call_service_stat_report add  	percent19	float null;
alter table oca_call_service_stat_report add  	percent20	 float null;
alter table oca_call_service_stat_report add  	percent21	float null;
alter table oca_call_service_stat_report add  	percent22	 float null;
alter table oca_call_service_stat_report add  	percent23	float null;
alter table oca_call_service_stat_report add  	percent24	 float null;
alter table oca_call_service_stat_report add  	value1	 int null;
alter table oca_call_service_stat_report add  	value2	 int null;
alter table oca_call_service_stat_report add  	value3	 int null;
alter table oca_call_service_stat_report add  	value4	 int null;
alter table oca_call_service_stat_report add  	value5	 int null;
alter table oca_call_service_stat_report add  	value6	 int null;
alter table oca_call_service_stat_report add  	value7	 int null;
alter table oca_call_service_stat_report add  	value8	 int null;
alter table oca_call_service_stat_report add  	value9	 int null;
alter table oca_call_service_stat_report add  	value10	 int null;
alter table oca_call_service_stat_report add  	value11	 int null;
alter table oca_call_service_stat_report add  	value12	 int null;
alter table oca_call_service_stat_report add  	value13	 int null;
alter table oca_call_service_stat_report add  	value14	 int null;
alter table oca_call_service_stat_report add  	value15	 int null;
alter table oca_call_service_stat_report add  	value16	 int null;
alter table oca_call_service_stat_report add  	value17	 int null;
alter table oca_call_service_stat_report add  	value18	 int null;
alter table oca_call_service_stat_report add  	value19	 int null;
alter table oca_call_service_stat_report add  	value20	 int null;
alter table oca_call_service_stat_report add  	value21	 int null;
alter table oca_call_service_stat_report add  	value22	 int null;
alter table oca_call_service_stat_report add  	value23	 int null;
alter table oca_call_service_stat_report add  	value24	 int null;
