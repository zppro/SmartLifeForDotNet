/*****************************************/
/*SP_DBA_SolveConfictOfCallNo            */
/*修复老人与未知老人的号码冲突问题       */
/*前提条件：temp_callrecord存在          */
/*****************************************/
ALTER procedure [dbo].[SP_DBA_SolveConfictOfCallNo]
as 
BEGIN
------------------------------step 1 在callservice 表中，把 未知老人的oldmanid,改为 新加号码的老人的oldmanid. 24726
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

----------------------------step 2 在配置表中，把需要添加电话号码的老人的号码 由空值改为实际的真实值。

if (not exists (select * from Oca_OldManConfigInfo where OldManId in(select OldManId from Temp_CallRecord where type='add')))
begin
      insert into   Oca_OldManConfigInfo  (OldManId,CheckInTime,CallNo,LocateFlag,GovTurnkeyFlag)
      select oldmanid,GETDATE() checkintime,tel callno,0 locateflag,0 govturnkeyflag
      from Temp_CallRecord 
      where type='add' and tel not in(select distinct callno from Oca_OldManConfigInfo where CallNo  is not  null and CallNo<>'' );
end

------------------------------step 3 在未知老人的电话号，与新加资料的电话号的冲突的情况下，在配置表中，把未知老人的电话号在配置表中清空。
/*update  Oca_OldManConfigInfo  set  CallNo=''
 from  Oca_OldManConfigInfo  b
where b.OldManId in 
                   (
                      select oldmanid
                      from Temp_CallRecord
                      where type='2'
                   )*/
------------------------------step 3 在未知老人的电话号，与新加资料的电话号的冲突的情况下，在配置表中，把冲突的电话号在配置表由未知老人的ID 修改为真实老人的ID.。                   
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
                   
END  


/************************************************/
/*生成临时表Temp_CallRecord                     */
/*前提条件是：Sheet1$_filterdatabase 存在             */
/************************************************/
ALTER PROCEDURE  [dbo].[SP_DBA_InsertCallNoChange]
AS 
BEGIN

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
-----------------------=================需要添加电话号的老人配置记录  8170
IF (EXISTS (select * from Temp_CallRecord ))
BEGIN
   delete from Temp_CallRecord;
END

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
        from dbo.Sheet1$_filterdatabase
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
        from dbo.Sheet1$_filterdatabase
      )n
where m.OldManName=n.oldmanname and m.IDNo=n.idno and m.CallNo=n.tel;

--------------------=============================oca_callservice 这个表呼叫服务表 需要修改老人ID的记录。
insert into Temp_CallRecord  (idno,OldManName,oldmanid ,tel,IdNoNew,OldManNameNew,OldManIdNew,type )
select k.idno,k.oldmanname ,k.OldManId,k.CallNo,p.IDNo IdNoNew,p.OldManName OldManNameNew,p.OldManId OldManIdNew,'2' type
from (
       select b.CallNo,a.OldManId,a.OldManName,a.IDNo 
       from dbo.Oca_OldManBaseInfo a inner join Oca_OldManConfigInfo b
            on a.OldManId=b.OldManId 
      where a.OldManName like '老人%' or a.OldManName like '未知%' or len(a.oldmanname)<=3
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

END

 select * from Temp_CallRecord;
 --delete from temp_callrecord;
         exec SP_DBA_InsertCallNoChange;
         exec SP_DBA_SolveConfictOfCallNo;
         
         select * into oca_callserviceOld0509 from Oca_CallService;
         select * into Oca_OldManConfigInfoOld0509 from Oca_OldManConfigInfo;
         
         select COUNT(*) from Oca_OldManConfigInfoOld0509; ---15108
          select COUNT(*) from Oca_OldManConfigInfo; --16125