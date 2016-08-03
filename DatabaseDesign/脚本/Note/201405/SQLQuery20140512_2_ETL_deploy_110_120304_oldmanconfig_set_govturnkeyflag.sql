  select * from temp_excelsource;
  --delete from temp_excelsource;
  select * from Sheet2$ where len(f3)>18
  --delete from sheet2$ where len(f3)>18;
  
  exec SP_DBA_MergeTempExcelSource @tablename='Sheet2$',@updatetablename='temp_excelsource'
  
   update dbo.temp_excelsource
set areaid3=(select areaid from Pub_Area where AreaName+'社区'=b.f16)
from dbo.temp_excelsource a,dbo.Sheet2$ b
where a.idno=b.f3;

update dbo.temp_excelsource
set Remark='-'
where Remark is null;


  update temp_excelsource set idno=SUBSTRING(idno,1,8)+cast(SUBSTRING(idno,9,1)-1 as varchar(1))+SUBSTRING(idno,10,9)
    where Remark='-'
    
     update temp_excelsource set idno=REPLACE(idno,'0229','0228')
    where Remark='-' and substring(idno,11,4)='0229'
  
   update temp_excelsource set birthday= cast(substring(idno,7,8) as datetime)
    where Remark='-'
    
 exec   SP_DBA_ImportOldManBaseInfo

select * from Pub_Area order by AreaName;

select COUNT(*) from Oca_OldManBaseInfo;

select COUNT(*) from remote_dbo.[smartlife-120304].dbo.Oca_OldManBaseInfo;

select * from Oca_OldManConfigInfo where OldManId in(
select oldmanid from Oca_OldManBaseInfo 
where OldManName+IDNo in(
select 姓名+身份证号 from temp_govserviceoldman$));


select  a.OldManId,c.[姓名]+c.[身份证号] mark
from Oca_OldManConfigInfo a,Oca_OldManBaseInfo b,temp_govserviceoldman$  c
where a.OldManId=b.OldManId and b.OldManName+b.IDNo=c.[姓名]+c.[身份证号]

1687
1733

select b.oldmanid,b.govturnkeyflag
from Oca_OldManConfigInfo b,(select  a.OldManId,c.[姓名]+c.[身份证号] mark
from Oca_OldManConfigInfo a,Oca_OldManBaseInfo b,temp_govserviceoldman$  c
where a.OldManId=b.OldManId and b.OldManName+b.IDNo=c.[姓名]+c.[身份证号])m
where b.OldManId=m.OldManId and GovTurnkeyFlag=0

update    Oca_OldManConfigInfo set GovTurnkeyFlag=1
from Oca_OldManConfigInfo b,(select  a.OldManId,c.[姓名]+c.[身份证号] mark
from Oca_OldManConfigInfo a,Oca_OldManBaseInfo b,temp_govserviceoldman$  c
where a.OldManId=b.OldManId and b.OldManName+b.IDNo=c.[姓名]+c.[身份证号])m
where b.OldManId=m.OldManId and GovTurnkeyFlag=0

select * into temp_govserviceoldmanNoMatch$ from temp_govserviceoldman$
where [姓名]+[身份证号] not in
(select  c.[姓名]+c.[身份证号] mark
from Oca_OldManConfigInfo a,Oca_OldManBaseInfo b,temp_govserviceoldman$  c
where a.OldManId=b.OldManId and b.OldManName+b.IDNo=c.[姓名]+c.[身份证号])

