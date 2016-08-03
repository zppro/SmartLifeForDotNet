--------在源库，不在目的库的表
select name from [smartlife-120300].sys.tables where name not in (select name from sys.tables )

select * from Dey_Script;

select * from dbo.oca_oldmanconfiginfo;


--------在源库，不在目的库的表
select name from [smartlife-120300].sys.tables where name not in (select name from sys.tables )

select * from dbo.oca_oldmanconfiginfo;

-----------------------未知老人的号码列表
select b.CallNo,a.OldManId,a.OldManName,a.IDNo 
from dbo.Oca_OldManBaseInfo a inner join Oca_OldManConfigInfo b
on a.OldManId=b.OldManId where a.OldManName like '未知%';

----------需要添加电话号的老人配置记录
select m.idno,m.OldManName,m.CallNo,n.tel,m.oldmanid
from (
select b.CallNo,a.IDNo,a.OldManName,a.OldManId 
from dbo.Oca_OldManBaseInfo a left join Oca_OldManConfigInfo b
on a.OldManId=b.OldManId where 
b.CallNo is null and 
a.OldManName not like '未知%' ) m,
(select f1 oldmanname,f4 idno,f5 tel,f16 areaid2 from dbo.callrecord)n
where m.OldManName=n.oldmanname and m.IDNo=n.idno
----------------

----step 1 在未知老人的电话号，与新加资料的电话号的冲突的情况下，在配置表中，把未知老人的电话号在配置表中清空。
select cast(b.CallNo as bigint),b.OldManId
from  Oca_OldManConfigInfo  b
 where b.OldManId in (select a.OldManId from dbo.Oca_OldManBaseInfo a where a.OldManName like '未知%' 
) and  b.CallNo in 
(select cast(cast(n.tel as bigint) as  varchar(11))
from (
select b.CallNo,a.OldManName,a.IDNo from dbo.Oca_OldManBaseInfo a left join Oca_OldManConfigInfo b
on a.OldManId=b.OldManId where 
b.CallNo is null and 
a.OldManName not like '未知%' ) m,
(select  oldmanname,idno,callno tel,areaid2 from dbo.temp_mutisheets)n
where m.OldManName=n.oldmanname and m.IDNo=n.idno
)
-----------------step 1 end

----------step 2 在配置表中，把需要添加电话号码的老人的号码 由空值改为实际的真实值。
select m.idno,m.OldManName,m.CallNo,n.tel,m.oldmanid
from (
select b.CallNo,a.IDNo,a.OldManName,a.OldManId from dbo.Oca_OldManBaseInfo a left join Oca_OldManConfigInfo b
on a.OldManId=b.OldManId where 
b.CallNo is null and 
a.OldManName not like '未知%' ) m,
(select  oldmanname,idno,callno tel,areaid2 from dbo.temp_mutisheets)n
where m.OldManName=n.oldmanname and m.IDNo=n.idno

-----------step 3 在callservice 表中，把 未知老人的oldmanid,改为 新加号码的老人的oldmanid.
select k.OldManId sourcevalue,k.oldmanname ,k.idno,k.CallNo,p.OldManId targetvalue,p.OldManName,p.IDNo
from (
select b.CallNo,a.OldManId,a.OldManName,a.IDNo 
from dbo.Oca_OldManBaseInfo a inner join Oca_OldManConfigInfo b
on a.OldManId=b.OldManId where a.OldManName like '未知%'
) k,(
select m.idno,m.OldManName,m.CallNo,n.tel,m.oldmanid
from (
select b.CallNo,a.IDNo,a.OldManName,a.OldManId from dbo.Oca_OldManBaseInfo a left join Oca_OldManConfigInfo b
on a.OldManId=b.OldManId where 
b.CallNo is null and 
a.OldManName not like '未知%' ) m,
(select  oldmanname,idno,callno tel,areaid2 from dbo.temp_mutisheets)n
where m.OldManName=n.oldmanname and m.IDNo=n.idno
) p
where k.CallNo=p.tel
 

------------------------------------

select top 1 oldmanname,idno,callno tel,areaid2 from dbo.temp_mutisheets;
select COUNT(*),f16 from dbo.callrecord group by f16;
select COUNT(*) from dbo.callrecord where f16 like '{%'

select COUNT(*) from Oca_OldManBaseInfo where AreaId2 is null;

select f1 oldmanname,f4 idno,f5 tel,f16 areaid2 from dbo.callrecord;

delete from dbo.callrecord where f16 like '{%';
insert into dbo.callrecord(f1,f4,f5,f16)
select f1 oldmanname,f4 idno,f5 tel,f16 areaid2 from dbo.sheet3$;

insert into dbo.callrecord(f1,f4,f5,f16)
select f1 oldmanname,f4 idno,f5 tel,f16 areaid2 from dbo.Sheet4$;

insert into dbo.callrecord(f1,f4,f5,f16)
select f1 oldmanname,f4 idno,f5 tel,f16 areaid2 from dbo.Sheet5$;

insert into dbo.callrecord(f1,f4,f5,f16)
select f1 oldmanname,f4 idno,f5 tel,f16 areaid2 from dbo.Sheet6$;

insert into dbo.callrecord(f1,f4,f5,f16)
select f1 oldmanname,f4 idno,f5 tel,f16 areaid2 from dbo.Sheet7$;

select f1  OldManName,f4 IdNo,f5 CallNo,f16 AreaId2  into dbo.Temp_MutiSheets from  dbo.sheet8$;

select COUNT(*) from dbo.temp_mutisheets;
delete from sheetSource
select COUNT(*) from sheetSource;
select * from sheetSource;

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet8$ where f5 is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet9$  where  f5  is not null and f5 not like '{%';
-----------------------===================================================
insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet10$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet11$ where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet12$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet13$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet14$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet15$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet16$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet17$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet18$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet19$  where  f5  is not null and f5 not like '{%';
--------------------
insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet20$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet21$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet22$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet23$  where  f5  is not null and f5 not like '{%';------------

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet24$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet25$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet26$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet27$  where  f5  is not null and f5 not like '{%';------------

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet28$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet29$  where  f5  is not null and f5 not like '{%';
-------------=================================================================
insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet30$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet31$  where  f5  is not null and f5 not like '{%';--------------

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet32$  where  f5  is not null and f5 not like '{%';-----------

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet33$  where  f5  is not null and f5 not like '{%';-----------

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet34$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet35$  where  f5  is not null and f5 not like '{%';----------------

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet36$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet37$  where  f5  is not null and f5 not like '{%';

insert into dbo.sheetSource
select f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15,f16,f17,f18,f19,f20,f21,f22,f23  from dbo.Sheet38$  where  f5  is not null and f5 not like '{%';----------------

--alter table temp_mutisheets alter column callno nvarchar(11) null;
--delete from Temp_MutiSheets;

alter  table temp_callrecord add oldmanidnew uniqueidentifier null;




