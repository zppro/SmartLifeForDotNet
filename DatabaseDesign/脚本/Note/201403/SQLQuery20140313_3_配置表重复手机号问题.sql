select * from Oca_OldManBaseInfo where OldManName like 'ÄÚ²¿²âÊÔ%'


select * from Oca_OldManBaseInfo Order By Id desc;

select * from Oca_OldManBaseInfo where AreaIdForCall is null;

update Oca_OldManBaseInfo

set AreaidForCall='AAA01191-0999-0000-0000-000000000000'

where AreaIdForCall is null;

Update Oca_OldManBaseInfo
set Status=1
where OldManId='FE59A17F-3AFD-4A02-8B6A-8467483170EE'

-----------------------=========================59 26group
select a.CallNo,b.OldManId,c.OldManName,c.IDNo,c.CheckInTime,(select COUNT(*) from Oca_CallService where OldManId=c.OldManId) as aaa
          from (
          select COUNT(*) num,callno from dbo.oca_oldmanconfiginfo 
          group by callno 
           having COUNT(*)>1
           ) a,dbo.oca_oldmanconfiginfo b,dbo.Oca_OldManBaseInfo c
           where a.CallNo <>'0' and a.CallNo<>'' and a.CallNo=b.CallNo and b.OldManId=c.OldManId
           order by a.CallNo,aaa
           
----------------------------
select a.CallNo,b.OldManId,c.OldManName,c.IDNo,c.CheckInTime,(select COUNT(*) from Oca_CallService where OldManId=c.OldManId) as aaa
          from (
          select COUNT(*) num,callno from dbo.oca_oldmanconfiginfo 
          group by callno 
           having COUNT(*)>1
           ) a,dbo.oca_oldmanconfiginfo b,dbo.Oca_OldManBaseInfo c
           where a.CallNo <>'0' and a.CallNo<>'' and a.CallNo=b.CallNo and b.OldManId=c.OldManId 
           order by a.CallNo,aaa


select a.CallNo,b.OldManId,c.OldManName,c.IDNo,c.CheckInTime,(select COUNT(*) from Oca_CallService where OldManId=c.OldManId) as aaa
          from (
          select COUNT(*) num,callno from dbo.oca_oldmanconfiginfo x inner join Oca_OldManBaseInfo y
          on x.OldManId=y.OldManId
         
          group by callno,OldManName
           having COUNT(*)>1
           ) a,dbo.oca_oldmanconfiginfo b,dbo.Oca_OldManBaseInfo c
           where a.CallNo <>'0' and a.CallNo<>'' and a.CallNo=b.CallNo and b.OldManId=c.OldManId
           order by a.CallNo
           
           select * frdManBaseInfo
           
           
           delete 
           from  dbo.oca_oldmanconfiginfo
           where oldmanid in('FF45A904-8763-4867-BA4F-D58AB85C52B5','4D1B86D9-441F-4232-8B79-FD83048310F2','EC813CB0-6C4E-476A-AD3F-FA6060CC3555','A0FAB724-DB4A-4A8B-A93E-384A97EDBCDC','AB35E8F2-6EE6-4C20-811E-20863F02967D','09DEF1BF-658B-4F75-B52A-6FF02810730A','95717E20-B8E1-4FA6-9CDF-C7F25D3E71C9','EB337BA0-1F4A-445D-A004-62D97476234B','91B57545-02A4-4AB1-B8BF-EBF20191FCEF','CC00AA26-B4A4-47D3-9E58-8B1EF3ADD319','414C09F0-EDFC-425D-9BF8-88E92C0327B0','70298D34-1D5C-4796-A5DD-512E06C57699','D8BFDA49-9CA0-4AD4-9CF3-8DB79515F8A8','D84A32F1-694D-4A12-B994-2863ADB47FC1','41163794-B658-4D5B-A99B-E8AAC57EF582','8B0E2909-EB4A-4CF5-91BA-361CAF2CD1A0','898731E8-6E52-4FFE-9727-3B2202C28AB0','89EDCB45-E01D-4085-A751-DC242C8D0C3E','2BFC0EAE-0CA0-424C-8615-1F3A21D40D74')
