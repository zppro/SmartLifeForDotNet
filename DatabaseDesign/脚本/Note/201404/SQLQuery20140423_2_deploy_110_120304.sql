select * into Oca_CallServiceOld0423 from Oca_CallService;
select * into Oca_OldManConfigInfoOld0423 from Oca_OldManConfigInfo;
---------------------------------------===============================================
create procedure SP_DBA_RepairCallNoError
as
begin
IF (NOT EXISTS (select * from sys.tables where name='Temp_CallRecord'))
BEGIN
CREATE TABLE [dbo].[Temp_CallRecord](
	[idno] [char](18) NOT NULL,
	[OldManName] [nvarchar](20) NOT NULL,
	[oldmanid] [uniqueidentifier] NOT NULL,
	[tel] [nvarchar](11) NULL,
	[IdNoNew] [nvarchar](255) NULL,
	[OldManNameNew] [nvarchar](255) NULL,
	[type] [varchar](3) NOT NULL,
	[oldmanidnew] [uniqueidentifier] NULL
);

END

insert into Temp_CallRecord  (idno,OldManName,oldmanid ,tel,IdNoNew,OldManNameNew,OldManIdNew,type )
select k.idno,k.oldmanname ,k.OldManId,k.CallNo,p.IDNo IdNoNew,p.OldManName OldManNameNew,p.OldManId OldManIdNew,'2' type
from (
       select b.CallNo,a.OldManId,a.OldManName,a.IDNo 
       from dbo.Oca_OldManBaseInfo a inner join Oca_OldManConfigInfo b
            on a.OldManId=b.OldManId 
      --where a.OldManName like '¿œ»À%' or a.OldManName like 'Œ¥÷™%'
     ) k,
     (
        select m.idno,m.OldManName,m.CallNo,n.tel,m.oldmanid
        from (
                select b.CallNo,a.IDNo,a.OldManName,a.OldManId 
                from dbo.Oca_OldManBaseInfo a left join Oca_OldManConfigInfo b
                     on a.OldManId=b.OldManId 
                where   --b.CallNo is null and   
                len(a.OldManName )<=3
              ) m,
             (
                 select  oldmanname,idno,mobile tel,areaid2 
                 from dbo.temp_excelsource 
             )n
        where m.OldManName=n.oldmanname and m.IDNo=n.idno
      ) p
where k.CallNo=p.tel;


update Oca_CallService 
set OldManId=o.oldmanidnew
from Oca_CallService a,(
    select a.oldmanid,a.oldmanidnew,b.CallServiceId 
    from temp_callrecord a,Oca_CallService b  
    where a.TYPE='2' and a.oldmanid=b.OldManId
    )  o
where a.CallServiceId=o.CallServiceId and a.CallServiceId in 
(
   select b.CallServiceId 
   from temp_callrecord a,Oca_CallService b  
   where a.TYPE='2' and a.oldmanid=b.OldManId
);


update  Oca_OldManConfigInfo  set CallNo=n.newcallno
    from  Oca_OldManConfigInfo m,
   ( select  -- a.IDNo,a.OldManName ,
a.Mobile newcallno,b.OldManId-- c.oldmanid--,a.Mobile newcallno,c.CallNo 
from Temp_ExcelSource  a left join Oca_OldManBaseInfo b 
    on a.IDNo=b.IDNo  --group by b.OldManId having COUNT(a.oldmanname)>1
     left join Oca_OldManConfigInfo c
    on b.oldmanid=c.OldManId   where c.OldManId not in('60F3845C-7CCC-4243-8F98-1B5420057AD3',
    'BFFCFA2B-F3BE-44E9-804A-2F182427957D')
    ) n
    where m.OldManId=n.OldManId
END    
------------------------------------------------===================================================