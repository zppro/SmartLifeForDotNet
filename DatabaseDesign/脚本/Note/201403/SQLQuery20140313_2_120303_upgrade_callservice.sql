exec SP_DBA_InsertCallNoChange

select * into Oca_OldManBaseInfoOld from dbo.oca_oldmanbaseinfo;
select * into Oca_OldManConfigInfoOld from dbo.Oca_OldManConfigInfo;
select * into Oca_CallServiceOld from dbo.Oca_CallService;
select * into Pub_AreaOld from dbo.Pub_Area;
exec  SP_DBA_SolveConfictOfCallNo

/***********************************************************************/
/* SP_DBA_UpdateAreaID                                                 */
/* 根据EXCEL文件为准，更新Oca_OldManBaseInfo 的字段areaid2,areaid3     */
/***********************************************************************/
create procedure SP_DBA_UpdateAreaID
as 
begin
---------------------
        --update Pub_Area set AreaName=REPLACE(areaname,'镇','街道') where AreaName like '%镇';
         
         update Oca_OldManBaseInfo set AreaId2=n.AreaId
         from Oca_OldManBaseInfo m, (
           select a.IdNo,a.OldManName name_inexcel,c.OldManName name_indb,b.AreaId,c.AreaId2
         from  (            
                    select idno,oldmanname,areaid2
                    from Temp_MutiSheets 
                )a,    
              Pub_Area b,
                (    
                    select idno,oldmanname,areaid2 
                    from oca_oldmanbaseinfo
                    where AreaId2 is null
                 ) c   
         where a.IdNo=c.IDNo 
          and replace(a.AreaId2,' ','')=replace(b.AreaName,' ','')
          ) n
          where m.IDNo=n.IdNo and m.AreaId2 is null
 ----------------------------------------
          update Oca_OldManBaseInfo set AreaId3=n.AreaId
         from Oca_OldManBaseInfo m, (
           select a.IdNo,a.OldManName name_inexcel,c.OldManName name_indb,b.AreaId,
                  c.AreaId3
         from  (            
                    select idno,oldmanname,areaid3
                    from Temp_MutiSheets --where areaid3 in (select distinct areaname from pub_area)
                )a,    
             Pub_Area b,
                (    
                    select idno,oldmanname,areaid3 
                    from oca_oldmanbaseinfo
                    where AreaId3 is null
                 ) c   
         where a.IdNo=c.IDNo and a.areaid3=b.AreaName
          and replace(a.AreaId3,' ','')=replace(b.AreaName,' ','')
          ) n
          where m.IDNo=n.IdNo and m.AreaId3 is null         
          
end
go         
-----------------
/****** Object:  StoredProcedure [dbo].[SP_DBA_InsertCallNoChange]    Script Date: 03/13/2014 13:28:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
/****************************/
/*生成临时表Temp_CallRecord */
/****************************/
CREATE PROCEDURE  [dbo].[SP_DBA_InsertCallNoChange]
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
GO


/****** Object:  StoredProcedure [dbo].[SP_DBA_SolveConfictOfCallNo]    Script Date: 03/13/2014 13:30:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
/*****************************************/
/*修复老人与未知老人的号码冲突问题       */
/*****************************************/
create procedure [dbo].[SP_DBA_SolveConfictOfCallNo]
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
      where type='add';
end

------------------------------step 3 在未知老人的电话号，与新加资料的电话号的冲突的情况下，在配置表中，把未知老人的电话号在配置表中清空。
update  Oca_OldManConfigInfo  set  CallNo=''
 from  Oca_OldManConfigInfo  b
where b.OldManId in 
                   (
                      select oldmanid
                      from Temp_CallRecord
                      where type='2'
                   )
                   
END   
GO


