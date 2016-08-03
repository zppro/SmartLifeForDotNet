set nocount on
select * from sys.tables where name='Oca_CC_Ext';
select dbo.joinstr(name) 
from sys.all_columns 
where object_id=2080726465

select callno
,COUNT(*) 
from Oca_OldManConfigInfo
where len(callno)=11 --and CallNo='13018938415'
group by callno
having COUNT(*)>1
order by callno desc
;

select * 
from  FUNC_Tol_GetLngLat()


select top 10 OldManName,len(oldmanname) lengths,replicate('*',LEN(oldmanname))  newname from Oca_OldManBaseInfo;

insert into aaaase
select * from aa

-----a
select   dbo.joinStr(name)
from    sys.all_columns 
where  object_id   in 
            (   select  object_id 
                from    sys.tables 
               where  name='Oca_OldManBaseInfo'
             )

-------b 
 select  object_id 
                from    sys.tables 
               where  name='Oca_OldManBaseInfo'

select   dbo.joinStr(name)
from    sys.all_columns 
where  object_id   in (
416720537);