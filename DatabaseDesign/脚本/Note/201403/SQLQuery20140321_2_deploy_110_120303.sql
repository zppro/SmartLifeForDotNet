select top 3857 mobile from Oca_OldManBaseInfo
order by CheckInTime desc ;
 where AreaIdForCall is null;

select * into Oca_OldManBaseInfo0321 from Oca_OldManBaseInfo;
select * into Oca_OldManConfigInfo0321 from Oca_OldManConfigInfo;
exec SP_DBA_ImportOldManInfoOnlyMobile

update Oca_OldManBaseInfo
set AreaIdForCall='AAA01191-0999-0000-0000-000000000000'
where AreaIdForCall is null;


select cast (m.idno as varchar(40)) idno,m.OldManName,n.tel
from (
       select b.CallNo,a.IDNo,a.OldManName,a.OldManId 
       from dbo.Oca_OldManBaseInfo a left join Oca_OldManConfigInfo b
            on a.OldManId=b.OldManId 
       where b.CallNo is not null and  a.OldManName not like 'Œ¥÷™%' and a.OldManName not like '¿œ»À%'
       ) m,
      (
        select  f2 tel
        from dbo.Temp_Mobile
      )n
where  m.CallNo=n.tel;