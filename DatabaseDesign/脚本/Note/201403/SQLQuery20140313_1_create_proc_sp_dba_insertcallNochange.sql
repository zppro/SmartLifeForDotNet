ALTER PROCEDURE  SP_DBA_InsertCallNoChange
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
       where  b.CallNo is null and a.OldManName not like '未知%' 
       ) m,
      (
        select  oldmanname,idno,callno tel,areaid2 
        from dbo.temp_mutisheets
      )n
where m.OldManName=n.oldmanname and m.IDNo=n.idno;

----------------------------------------==========正常 2786
insert into Temp_CallRecord  (idno,OldManName,oldmanid ,tel,IdNoNew,OldManNameNew,OldManIdNew,type )
select m.idno,m.OldManName,m.oldmanid,n.tel,n.idno IdNoNew,n.oldmanname OldManNameNew,null OldManIdNew,'0' TYPE
from (
       select b.CallNo,a.IDNo,a.OldManName,a.OldManId 
       from dbo.Oca_OldManBaseInfo a left join Oca_OldManConfigInfo b
            on a.OldManId=b.OldManId 
       where b.CallNo is not null and  a.OldManName not like '未知%' 
       ) m,
      (
        select  oldmanname,idno,callno tel,areaid2 
        from dbo.temp_mutisheets
      )n
where m.OldManName=n.oldmanname and m.IDNo=n.idno and m.CallNo=n.tel;

--------------------=============================oca_callservice 这个表呼叫服务表 需要修改老人ID的记录。
insert into Temp_CallRecord  (idno,OldManName,oldmanid ,tel,IdNoNew,OldManNameNew,OldManIdNew,type )
select k.idno,k.oldmanname ,k.OldManId,k.CallNo,p.IDNo IdNoNew,p.OldManName OldManNameNew,p.OldManId OldManIdNew,'2' type
from (
       select b.CallNo,a.OldManId,a.OldManName,a.IDNo 
       from dbo.Oca_OldManBaseInfo a inner join Oca_OldManConfigInfo b
            on a.OldManId=b.OldManId 
      where a.OldManName like '未知%'
     ) k,
     (
        select m.idno,m.OldManName,m.CallNo,n.tel,m.oldmanid
        from (
                select b.CallNo,a.IDNo,a.OldManName,a.OldManId 
                from dbo.Oca_OldManBaseInfo a left join Oca_OldManConfigInfo b
                     on a.OldManId=b.OldManId 
                where   b.CallNo is null and   a.OldManName not like '未知%' 
              ) m,
             (
                select  oldmanname,idno,callno tel,areaid2 
                from    dbo.temp_mutisheets
             )n
        where m.OldManName=n.oldmanname and m.IDNo=n.idno
      ) p
where k.CallNo=p.tel;

END
