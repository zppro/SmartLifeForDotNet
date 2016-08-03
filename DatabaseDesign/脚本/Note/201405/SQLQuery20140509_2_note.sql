   insert into   Oca_OldManConfigInfo  (OldManId,CheckInTime,DataSource,CallNo,LocateFlag,GovTurnkeyFlag)
         select oldmanid,GETDATE() checkintime,'00003' DataSource,mobile callno,0 locateflag,0 govturnkeyflag
         from  temp_excelsource 
         where mobile not in (select callno from Oca_OldManConfigInfo);
         
         select * from Sheet1$_filterdatabase;
         insert into Sheet1$_filterdatabase([330105193108280321],[孙文田],f6)values ('330105193108280321','孙文田','88803734')
         
         exec sp_rename @objname='dbo.Sheet1$_filterdatabase.[330105193108280321]',@newname='IDNo' ,@objtype =  'COLUMN'
         exec sp_rename @objname='dbo.Sheet1$_filterdatabase.[孙文田]',@newname='OldManName' ,@objtype =  'COLUMN'
          exec sp_rename @objname='dbo.Sheet1$_filterdatabase.F6',@newname='Mobile' ,@objtype =  'COLUMN'
         alter table Sheet1$_filterdatabase alter column [330105193108280321] rename to 'IDNo';
         
         insert into   Oca_OldManConfigInfo  (OldManId,CheckInTime,DataSource,CallNo,LocateFlag,GovTurnkeyFlag)
         select OldManId,CheckInTime,DataSource,CallNo,LocateFlag,GovTurnkeyFlag 
         from (
           select (select oldmanid from Oca_OldManBaseInfo where IDNo=a.idno) oldmanid
                  ,GETDATE() checkintime,'00003' DataSource,F6 callno,0 locateflag,0 govturnkeyflag
         from  Sheet1$_filterdatabase a
         where F6 not in (select callno from Oca_OldManConfigInfo)
         ) m where 
         m.oldmanid is not null;
         
         select COUNT(*) from remote_dbo.[smartlife-120304].dbo.Oca_OldManConfigInfo  --15108
         select COUNT(*) from Oca_OldManConfigInfo;
         
         insert into Oca_OldManConfigInfo(OldManId,CheckInTime,DataSource,CallNo,LocateFlag,GovTurnkeyFlag)
         select OldManId,CheckInTime,DataSource,CallNo,LocateFlag,GovTurnkeyFlag
         from remote_dbo.[smartlife-120304].dbo.Oca_OldManConfigInfo 
         where OldManId not in (select OldManId from Oca_OldManConfigInfo )
         
         select * from Temp_CallRecord;
         exec SP_DBA_InsertCallNoChange;
         exec SP_DBA_SolveConfictOfCallNo;
         select * from Oca_OldManConfigInfo where OldManId='481B2F00-B35F-43AD-AEE8-7ECF60E1793A'
         select * from Oca_OldManConfigInfo where CallNo='600647040'
       --  delete from Temp_CallRecord;
       select * from Oca_OldManBaseInfo where OldManId='6DB5CF3B-C10E-4A33-B15F-004447FB85EF'
       
       
       insert into Temp_CallRecord (idno,OldManName,oldmanid ,tel,IdNoNew,OldManNameNew,OldManIdNew,type )
select m.idno,m.OldManName,m.oldmanid ,n.tel,n.idno IdNoNew,n.oldmanname OldManNameNew,null OldManIdNew,'add' type 
from (
      select b.CallNo,a.IDNo,a.OldManName,a.OldManId 
      from dbo.Oca_OldManBaseInfo a left join Oca_OldManConfigInfo b
           on a.OldManId=b.OldManId
       where  b.CallNo is null and len(a.OldManName )<=3
       ) m,
      (
         select  oldmanname,idno,mobile tel--,areaid2 
        from Sheet1$_filterdatabase 
      )n
where m.OldManName=n.oldmanname and m.IDNo=n.idno;

----------------------------------------==========正常 2786
insert into Temp_CallRecord  (idno,OldManName,oldmanid ,tel,IdNoNew,OldManNameNew,OldManIdNew,type )
select m.idno,m.OldManName,m.oldmanid,n.tel,n.idno IdNoNew,n.oldmanname OldManNameNew,null OldManIdNew,'0' TYPE
from (
       select b.CallNo,a.IDNo,a.OldManName,a.OldManId 
       from dbo.Oca_OldManBaseInfo a left join Oca_OldManConfigInfo b
            on a.OldManId=b.OldManId 
       where b.CallNo is not null and  len(a.OldManName )<=3
       ) m,
      (
        select  oldmanname,idno,mobile tel--,areaid2 
        from Sheet1$_filterdatabase 
      )n
where m.OldManName=n.oldmanname and m.IDNo=n.idno and m.CallNo=n.tel;

--------------------=============================oca_callservice 这个表呼叫服务表 需要修改老人ID的记录。
insert into Temp_CallRecord  (idno,OldManName,oldmanid ,tel,IdNoNew,OldManNameNew,OldManIdNew,type )
select k.idno,k.oldmanname ,k.OldManId,k.CallNo,p.IDNo IdNoNew,p.OldManName OldManNameNew,p.OldManId OldManIdNew,'2' type
from (
       select b.CallNo,a.OldManId,a.OldManName,a.IDNo 
       from dbo.Oca_OldManBaseInfo a inner join Oca_OldManConfigInfo b
            on a.OldManId=b.OldManId 
      where len(a.OldManName)<=3 --like '老人%' or a.OldManName like '未知%'
     ) k,
     (
        select m.idno,m.OldManName,m.CallNo,n.tel,m.oldmanid
        from (
                select b.CallNo,a.IDNo,a.OldManName,a.OldManId 
                from dbo.Oca_OldManBaseInfo a left join Oca_OldManConfigInfo b
                     on a.OldManId=b.OldManId 
                where   b.CallNo is null and   len(a.OldManName )<=3
              ) m,
             (
                 select  oldmanname,idno,mobile tel--,areaid2 
                 from Sheet1$_filterdatabase 
             )n
        where m.OldManName=n.oldmanname and m.IDNo=n.idno
      ) p
where k.CallNo=p.tel;


------------------------------step 3 在未知老人的电话号，与新加资料的电话号的冲突的情况下，在配置表中，把未知老人的电话号在配置表中清空。
update  Oca_OldManConfigInfo  set  OldManId=c.oldmanidnew
 from  Oca_OldManConfigInfo  b ,(
                      select *
                      from Temp_CallRecord
                      where type='2'
                   ) c 
where b.CallNo in 
                   (
                      select tel
                      from Temp_CallRecord
                      where type='2'
                   )
                   
                   
  select * into remote_dbo.Sheet1$_filterdatabase   
  
  SELECT * FROM fn_builtin_permissions(default);
  select * from sys.login_token;
  select  * from sys.server_principals
  
   SELECT * FROM  sys.server_permissions 
   
   VIEW ANY DATABASE
   
   REVOKE VIEW ANY DATABASE  FROM public
   grant view any database to test;
   REVOKE VIEW ANY DATABASE  FROM test
    
   grant VIEW SERVER STATE to test;
   grant VIEW SERVER STATE to public;
   grant view database to job;

   USE [smartlife-1203];
GRANT VIEW DATABASE STATE on user::dbo TO test;
grant view definition to test;
exec sp_grantdbaccess N'test'    
   USE [smartlife-120300];
GRANT VIEW DATABASE STATE TO test;
grant view definition to test;
GO

alter authorization on Database::[Smartlife-120300] to test;

如何让SQL用户只能看到自己拥有权限的库