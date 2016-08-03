select a.oldmanname,a.idno,a.tel,a.mobile,b.CallNo
from oca_oldmanbaseinfo a ,oca_oldmanconfiginfo b where a.OldManId=b.OldManId and b.LocateFlag=1

from oca_oldmanbaseinfo a ,oca_oldmanconfiginfo b where a.OldManId=b.OldManId and b.LocateFlag=1

update oca_oldmanbaseinfo  set  Mobile='139'+substring(a.idno,11,8)
from oca_oldmanbaseinfo a ,oca_oldmanconfiginfo b where a.OldManId=b.OldManId and b.LocateFlag=1

select top 100* from dbo.Oca_OldManLocateInfo
 order by locatetime desc
 
 select cast(round(120.18246899999997*1000000+(RAND(right(SYSDATETIME(),6))-0.5)*0.005*1000000,12) as nvarchar(40)) +
 ((120.18246899999997+(RAND(right(SYSDATETIME(),6))-0.5)*0.005)*1000-floor((120.18246899999997+(RAND(right(SYSDATETIME(),6))-0.5)*0.005)*1000))*1000000 int_2
 ,
 (RAND(right(SYSDATETIME(),6))-0.5)*0.005 