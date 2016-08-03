select idno,oldmanname,oldmanid,OldManIdentity
from oca_oldmanbaseinfo
where oldmanidentity is null  ''oldmanname='林慧娟'

/* update dbo.temp_excelsource
set birthday=cast(SUBSTRING(b.f4,7,8) as datetime)
from dbo.temp_excelsource a,dbo.Sheet41$ b
where a.idno=b.f4 and len(b.f4)=18 and substring(b.f4,7,8)  like '19%' ;*/

 update dbo.temp_excelsource
set birthday=cast('19'+SUBSTRING(f4,7,6) as datetime)
from dbo.temp_excelsource a,dbo.Sheet41$ b
where a.idno=b.f4 and len(f4)=15;


create table temp_error
(
oldmanname nvarchar(10) null,
idNo varchar(20) null,
reason nvarchar(200) null
)

insert into temp_error (oldmanname,IDNo,reason)
select oldmanname,IDNo,'位数不对' reason  from Temp_ExcelSource where len(idno)<>18 and len(idno)<>15
union
select oldmanname,IDNo,'月份超范围' reason from Temp_ExcelSource where len(idno)=15 and substring(idno,9,2)>12
union
select oldmanname,IDNo,'日期超范围' reason from Temp_ExcelSource where len(idno)=15 and substring(idno,11,2)>31
union
select oldmanname,IDNo,'月份超范围' reason from Temp_ExcelSource where len(idno)=18 and substring(idno,11,2)>12
union
select oldmanname,IDNo,'日期超范围' reason from Temp_ExcelSource where len(idno)=18 and substring(idno,13,2)>31
union  
select oldmanname,IDNo,'年度超范围' reason from Temp_ExcelSource
 where len(idno)=18 and ((substring(idno,7,4))<1900 or (substring(idno,7,4))>2013 )
 
 
 update dbo.temp_excelsource
set birthday=cast(SUBSTRING(b.f4,7,8) as datetime)
from dbo.temp_excelsource a,dbo.Sheetsource b
where a.idno=b.f4 and len(b.f4)=18 and substring(b.f4,7,8)  like '19%' 
and a.idno not in (select distinct idno from temp_error);

 update dbo.temp_excelsource
set birthday=cast('19'+SUBSTRING(f4,7,6) as datetime)
from dbo.temp_excelsource a,sheetSource b
where a.idno=b.f4 and len(f4)=15
and a.idno not in (select distinct idno from temp_error);

update Oca_OldManBaseInfo
set Birthday=b.birthday
from Oca_OldManBaseInfo a,dbo.temp_excelsource b
where a.IDNo=b.idno

select a.birthday,b.birthday
from Oca_OldManBaseInfo a,dbo.temp_excelsource b
where a.IDNo=b.idno

update oca_oldmanbaseinfo set oldmanidentity='00001'
--from oca_oldmanbaseinfo
where oldmanidentity is null 

select * from temp_error;