select * into Oca_OldManBaseInfoOld0408
from Oca_OldManBaseInfo;

select 
;
 update Oca_OldManBaseInfo
set areaid3=b.areaid3
from Oca_OldManBaseInfo a,Temp_ExcelSource b
where a.idno=b.IDNo;

 update Oca_OldManBaseInfo
set areaid2=b.areaid2
from Oca_OldManBaseInfo a,Temp_ExcelSource b
where a.idno=b.IDNo;

 update Oca_OldManBaseInfo
set tel=b.tel
from Oca_OldManBaseInfo a,Temp_ExcelSource b
where a.idno=b.IDNo and len(b.Tel)>12;