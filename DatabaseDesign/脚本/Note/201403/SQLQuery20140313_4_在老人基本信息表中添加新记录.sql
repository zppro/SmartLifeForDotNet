select top 30 * from Oca_OldManBaseInfo;
insert into Oca_OldManBaseInfo
select * from  temp_excelsource where idno not in (select idno from Oca_OldManBaseInfo);
select top 30 * from dbo.sheetSource

ALTER TABLE [dbo].[temp_excelsource] ADD  CONSTRAINT [DF__Oca___Check__35DCF99B]  DEFAULT (getdate()) FOR [CheckInTime]


ALTER TABLE [dbo].[temp_excelsource] ADD  CONSTRAINT [DF__Oca___Statu__36D11DD4]  DEFAULT ('1') FOR [Status]
----------step 1
insert into temp_excelsource(oldmanid,oldmanname,IdNo)
select 
newid() oldmanid,
f1
,f4
from  dbo.sheetSource where LEN(f4)=18 or LEN(f4)=15

--------step 2 
update dbo.temp_excelsource
set gender=(case b.f2 when 'ÄÐ' then 'M' when 'Å®' then 'F' else  'M' end)
from dbo.temp_excelsource a,dbo.sheetSource b
where a.idno=b.f4

---------step 3 
 update dbo.temp_excelsource
set birthday=cast(SUBSTRING(b.f4,7,8) as datetime)
from dbo.temp_excelsource a,dbo.sheetSource b
where a.idno=b.f4 and len(b.f4)=18 and substring(b.f4,7,8)  like '19%' ;

 update dbo.temp_excelsource
set birthday=cast('19'+SUBSTRING(f4,7,6) as datetime)
from dbo.temp_excelsource a,dbo.sheetSource b
where a.idno=b.f4 and len(f4)=15;
 
  update dbo.temp_excelsource
set HealthInsuranceFlag=(case ISNULL(b.f6,'0') when '0' then 0 else 1 end)
from dbo.temp_excelsource a,dbo.sheetSource b
where a.idno=b.f4;

  update dbo.temp_excelsource
set SocialInsuranceFlag=(case ISNULL(b.f7,'0') when '0' then 0 else 1 end)
from dbo.temp_excelsource a,dbo.sheetSource b
where a.idno=b.f4;

  update dbo.temp_excelsource
set livingstate=(select itemid from Sys_DictionaryItem where dictionaryid='11001' and ItemName=b.f8)
from dbo.temp_excelsource a,dbo.sheetSource b
where a.idno=b.f4;

update dbo.Temp_excelsource
set  areaid='01191' ,status=1,checkintime=GETDATE(),DataSource='00003'
from dbo.temp_excelsource a,dbo.sheetSource b
where a.idno=b.f4;

  update dbo.temp_excelsource
set areaid2=(select areaid from Pub_Area where AreaName=b.f16)
from dbo.temp_excelsource a,dbo.sheetSource b
where a.idno=b.f4;

  update dbo.temp_excelsource
set areaid3=(select areaid from Pub_Area where AreaName=b.f17)
from dbo.temp_excelsource a,dbo.sheetSource b
where a.idno=b.f4;

  update dbo.temp_excelsource
set address=b.f10
from dbo.temp_excelsource a,dbo.sheetSource b
where a.idno=b.f4;

  update dbo.temp_excelsource
set tel=cast(cast (b.f12 as bigint) as varchar(20))
from dbo.temp_excelsource a,dbo.sheetSource b
where a.idno=b.f4;

  update dbo.temp_excelsource
set mobile=cast(cast (b.f5 as bigint) as varchar(20))
from dbo.temp_excelsource a,dbo.sheetSource b
where a.idno=b.f4;
------------------------------------=========end

select areaid from Pub_Area where AreaName=b.f16
select f16 from dbo.sheetsource where 

select (
select itemid from Sys_DictionaryItem where 
--ItemName like '%¾Ó%' 
dictionaryid='11001' and ItemName=b.f8) 
from dbo.sheetSource b ;

 
 select case ISNULL(f6,'0') when '0' then 0 else 1 end  HealthInsuranceFlag
 from sheet2$ 
 where F5 is not null
 
 select * from sheet2$  where patindex('%[a-z]%',f4) <> 0
 
 update temp_mutisheets set areaid3=b.f17
          from Temp_MutiSheets a,dbo.Sheet26$ b
          where a.areaid3 is null and  a.IdNo=b.F4;
          
   /*      

----select case f2 when 'ÄÐ' then 'M' when 'Å®' then 'F' else  'M' end Gender from sheet2$;

select f4,f5,SUBSTRING(f4,7,8)
,cast(SUBSTRING(f4,7,8) as datetime)
 from sheet2$ 
 where F5 is not null and len(f4)=18;
 
 select cast('19'+SUBSTRING(f4,7,6) as datetime)
  from sheet2$ 
 where F5 is not null and len(f4)=15;

 
          select *
          from (
          select COUNT(*) num,callno from remote_dbo.[smartlife-120303].dbo.oca_oldmanconfiginfo 
         
          group by callno
           having COUNT(*)>1) a,dbo.
           where a.CallNo <>'0' and a.CallNo<>''
           
          where CallNo='13777566095'
          
         Select callno from remote_dbo.[smartlife-120303].dbo.Oca_OldManConfigInfo where CallNo='13777566095'*/