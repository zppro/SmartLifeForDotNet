select * from remote_dbo_64.[smartlife-120300].dbo.oca_oldmanbaseinfo;
select * from [remote_dbo].[smartlife-120399].[INFORMATION_SCHEMA].VIEWS
select * from remote_dbo_64.[smartlife-120300].sys.tables where is_ms_shipped=1
select * from remote_dbo_64.[smartlife-120300].sys.objects where is_ms_shipped=0 and type='U'

select * from [remote_dbo].[IPCC-1203].dbo.IPCC_caller;

(select Caller callno from [IPCC-1203].dbo.IPCC_caller where Caller is not null and Caller <>'');

select name,REPLACE(name,'-','_') from sys.databases where name like 'smartlife-1203%';
---delete from dbo.Oca_OldManConfigInfo;
select x.*
from (
select a.CallNo,
case isnull(smartlife_120300.CallNo,'9') when '9'  then 0 else 1 end smartlife_120300,
case isnull(b.CallNo,'9') when '9'  then 0 else 1 end smartlife_120301,
case isnull(c.CallNo,'9') when '9' then 0 else 1 end smartlife_120302, 
case isnull(f.CallNo,'9') when '9' then 0 else 1 end smartlife_03,
case isnull(d.CallNo,'9') when '9'  then 0 else 1 end smartlife_04,
case isnull(e.CallNo,'9') when '9' then 0 else 1 end smartlife_05,
case isnull(g.CallNo,'9') when '9' then 0 else 1 end smartlife_06,
case isnull(h.CallNo,'9') when '9'  then 0 else 1 end smartlife_07,
case isnull(i.CallNo,'9') when '9' then 0 else 1 end smartlife_11, 
case isnull(j.CallNo,'9') when '9' then 0 else 1 end smartlife_96,
case isnull(k.CallNo,'9') when '9'  then 0 else 1 end smartlife_97,
case isnull(m.CallNo,'9') when '9' then 0 else 1 end smartlife_98,
case isnull(n.CallNo,'9') when '9' then 0 else 1 end smartlife_99
,(case isnull(k.CallNo,'9') when '9'  then 0 else 1 end )+
(case isnull(m.CallNo,'9') when '9' then 0 else 1 end )+
(case isnull(n.CallNo,'9') when '9' then 0 else 1 end ) checkresult
--b.CallNo,c.CallNo
from (select Caller CallNo from [IPCC-1203].dbo.IPCC_caller where Caller is not null and Caller <>'') a
left join [smartlife-120300].dbo.Oca_OldManConfigInfo smartlife_120300  on a.CallNo=smartlife_120300.CallNo 
left join [smartlife-120301].dbo.Oca_OldManConfigInfo b  on a.CallNo=b.CallNo 
left join  [smartlife-120302].dbo.Oca_OldManConfigInfo c on a.CallNo=c.callno
left join [smartlife-120303].dbo.Oca_OldManConfigInfo f on a.CallNo=f.CallNo
left join [smartlife-120304].dbo.Oca_OldManConfigInfo d  on a.CallNo=d.CallNo 
left join  [smartlife-120305].dbo.Oca_OldManConfigInfo e on a.CallNo=e.callno
left join  [smartlife-120306].dbo.Oca_OldManConfigInfo g on a.CallNo=g.callno
left join [smartlife-120307].dbo.Oca_OldManConfigInfo h  on a.CallNo=h.CallNo 
left join  [smartlife-120311].dbo.Oca_OldManConfigInfo i on a.CallNo=i.callno
left join [smartlife-120396].dbo.Oca_OldManConfigInfo j on a.CallNo=j.CallNo
left join [smartlife-120397].dbo.Oca_OldManConfigInfo k  on a.CallNo=k.CallNo 
left join  [smartlife-120398].dbo.Oca_OldManConfigInfo m on a.CallNo=m.callno
left join  [smartlife-120399].dbo.Oca_OldManConfigInfo n on a.CallNo=n.callno
) x 
where x.checkresult=1
--select * from dbo.Oca_OldManConfigInfo;

select a.CallNo 
,
case isnull(smartlife_120300.CallNo,'9') when '9'  then 0 else 1 end smartlife_120300,
case isnull(b.CallNo,'9') when '9'  then 0 else 1 end smartlife_120301
from (select Caller CallNo from [IPCC-1203].dbo.IPCC_caller where Caller is not null and Caller <>'') a
left join [smartlife-120300].dbo.Oca_OldManConfigInfo smartlife_120300  on a.CallNo=smartlife_120300.CallNo 
left join [smartlife-120301].dbo.Oca_OldManConfigInfo b  on a.CallNo=b.CallNo 

restore filelistonly from disk =  N'e:\SmartLife-120397_2014-02-27230.bck'
restore filelistonly from disk =  N'e:\SmartLife-120398_2014-02-27230.bck'
RESTORE DATABASE [smartlife-120397] FROM DISK = N'e:\SmartLife-120397_2014-02-27230.bck' WITH MOVE 'SmartLife' TO 'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\smartlife-120397_Data.MDF', 
 MOVE 'SmartLife_log' TO 'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\smartlife-120397_Log.LDF',
 STATS = 10, REPLACE 
 
 RESTORE DATABASE [smartlife-120398] FROM DISK = N'e:\SmartLife-120398_2014-02-27230.bck' WITH 
 MOVE 'SmartLife' TO 'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\smartlife-120398_Data.MDF', 
 MOVE 'SmartLife_log' TO 'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\smartlife-120398_Log.LDF',
 STATS = 10, REPLACE 
 
 RESTORE DATABASE [smartlife-120399] FROM DISK = N'e:\SmartLife-120399_2014-02-27230.bck' WITH 
 MOVE 'SmartLife' TO 'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\smartlife-120399_Data.MDF', 
 MOVE 'SmartLife_log' TO 'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\smartlife-120399_Log.LDF',
 STATS = 10, REPLACE 
 
  RESTORE DATABASE [smartlife-120396] FROM DISK = N'e:\SmartLife-120396_2014-02-27230.bck' WITH 
 MOVE 'SmartLife' TO 'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\smartlife-120396_Data.MDF', 
 MOVE 'SmartLife_log' TO 'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\smartlife-120396_Log.LDF',
 STATS = 10, REPLACE 

select * from sys.tables where is_ms_shipped=1

insert [Smartlife-120300].dbo.Oca_OldManConfigInfo select * from [remote_dbo].[Smartlife-120300].dbo.Oca_OldManConfigInfo;
insert [smartlife-120301].dbo.Oca_OldManConfigInfo select * from [remote_dbo].[Smartlife-120301].dbo.Oca_OldManConfigInfo;
insert [smartlife-120302].dbo.Oca_OldManConfigInfo select * from [remote_dbo].[Smartlife-120302].dbo.Oca_OldManConfigInfo;
insert [smartlife-120303].dbo.Oca_OldManConfigInfo select * from [remote_dbo].[Smartlife-120303].dbo.Oca_OldManConfigInfo;
insert [smartlife-120304].dbo.Oca_OldManConfigInfo select * from [remote_dbo].[Smartlife-120304].dbo.Oca_OldManConfigInfo;
insert [smartlife-120305].dbo.Oca_OldManConfigInfo select * from [remote_dbo].[Smartlife-120305].dbo.Oca_OldManConfigInfo;
insert [smartlife-120306].dbo.Oca_OldManConfigInfo select * from [remote_dbo].[smartlife-120306].dbo.Oca_OldManConfigInfo;


select * from t_deploy_script  
--where object_name='Oca_CallRecord' 
order by id;

select * from dbo.t_deploy_object_column_detail
select table_name from t_deploy_object where  change_type='not'

sysdiagrams