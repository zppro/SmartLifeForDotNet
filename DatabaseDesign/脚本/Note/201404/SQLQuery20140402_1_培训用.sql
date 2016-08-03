select * 
from oca_oldmanbaseinfo  ; ----33584
-- ssls
/*  so*/
select *
from Oca_OldManConfigInfo;   ---32263

select a.Gender,a.IDNo,a.OldManName,a.OldManId,b.OldManId,b.CallNo
from oca_oldmanbaseinfo a  inner join Oca_OldManConfigInfo b
   on a.OldManId=b.OldManId
   
   select name,user_type_id from sys.types 
   
   select name,user_type_id,object_id from sys.all_columns
   where object_id=1325247776


select a.name,--(select name from sys.types where user_type_id=a.user_type_id) typename,
b.name,a.user_type_id
 from sys.all_columns a,(select name,user_type_id from sys.types ) b
where object_id=1325247776 and a.user_type_id =b.user_type_id


select  'select count(*) ns,'''+name+'''         tablename from '+name +' union ' as "a-b"
 from sys.tables 
 where name like 'Sys%' or name like 'Pub%'
 
declare @str varchar(100) 
set @str=''

set @str='select '+'a'+',b from c'
select @str
exec sp_executesql @str

javascript eval(str)

select * from dbo.callrecord
select *,'abc' a into callrecord_temp_a from dbo.callrecord
select f1,f2,A from callrecord_temp_a

select a.birthday,b.birthday,a.IDNo,b.IDNo
from Oca_OldManBaseInfo a  inner join dbo.temp_excelsource b
on a.IDNo=b.idno 
where a.Birthday is  null or b.Birthday is null;

update Oca_OldManBaseInfo
set Birthday=b.birthday
from Oca_OldManBaseInfo a,dbo.temp_excelsource b
where a.IDNo=b.idno

a.birthday=b.brithday
a.birthday  is not null and b.birthday is null

select birthday,idno
from temp_excelsource
where idno in (
select idno
from temp_excelsource b
group by IDNo
having COUNT(*)>1)

where IDNo  

 in (select a.IDNo
from Oca_OldManBaseInfo a)

update  Oca_OldManBaseInfo  set birthday='1935-12-11 00:00:00.000'
where birthday='1925-12-11 00:00:00.000'


			select MAX() id
			from Temp_ExcelSource
			group by OldManName,IDNo
			having COUNT(*)>1
			----------------------------------------------==============================
			1.找出重复的号
			2.找出每一组要删除的记录
			3.删除相应记录
			
		delete from Oca_OldManConfigInfo
         where checkintime in(

								select MAX(checkintime) checkintime,callno
								from Oca_OldManConfigInfo
								where CallNo in (
													select callno
													from Oca_OldManConfigInfo
													group by callno
													having COUNT(*)>1
												)
								group by callno
			
			                  )
			----------------------------------------------=========================
            --having COUNT(*)>1
            
            select COUNT(*),callno
			from Oca_OldManConfigInfo
			group by callno
            having COUNT(*)>1
            
            
            with a as (
	 select ROW_NUMBER() OVER(PARTITION     BY f4 ORDER BY f4        ASC) AS Rowid
	 ,ROW_NUMBER() OVER( ORDER BY f4 ASC) AS Row,* 
	from dbo.sheetSource 
	--where F4 in ('330102192103281823')  
	)
delete  from a where Rowid=2;

select b.IDNo,COUNT(*)
from 
--Oca_OldManBaseInfo a,
dbo.temp_excelsource b
--where a.IDNo=b.idno
group by b.idno
having COUNT(*)>1
order by b.IDNo

--------------------------771
select birthday ,idno 
from Oca_OldManBaseInfo
where IDNo in (
select idno
from temp_excelsource
)
order by Birthday desc;
-----------------------------
select birthday ,idno 
from temp_excelsource
where IDNo not in (
						select idno 
						from Oca_OldManBaseInfo
						where IDNo in (
										select idno
										from temp_excelsource
						              )
			   )
order by Birthday desc;

-----------------
   select COUNT(*),idno
			from Oca_OldManBaseInfo
			where IDNo in (
			                        select b.IDNo
									from 
									--Oca_OldManBaseInfo a,
									dbo.temp_excelsource b
									--where a.IDNo=b.idno
									group by b.idno
									having COUNT(*)>1
									
			              )
			group by idno
            having COUNT(*)>1
            
            ---------------------------1938-02-01 00:00:00.000
       select birthday 
       from Oca_OldManBaseInfo
       where IDno='330104193802011926'
       
          select birthday 
       from temp_excelsource
       where IDno='330104193802011926'