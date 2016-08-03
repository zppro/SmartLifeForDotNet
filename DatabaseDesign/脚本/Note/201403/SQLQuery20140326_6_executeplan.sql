set statistics profile on
select m.f,m.l
from  (
		select a.f,b.l
		from (
				select distinct SUBSTRING(oldmanname,1,1) f--,SUBSTRING(oldmanname,2,2) l
				from oca_oldmanbaseinfo 
				where len(OldManName)=2 --or LEN(oldmanname)=3
			  ) a,
			  (
				select distinct SUBSTRING(oldmanname,1,1) f,SUBSTRING(oldmanname,2,2) l
				from oca_oldmanbaseinfo 
				where 
				--len(OldManName)=2 or 
				LEN(oldmanname)=3
			  ) b
		)m	
where m.f='°ü' and m.l='²®É½'	
		  	  	;