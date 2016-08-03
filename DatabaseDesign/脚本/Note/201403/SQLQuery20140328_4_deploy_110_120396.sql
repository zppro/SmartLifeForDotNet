select * from Sys_DictionaryItem where DictionaryId='00005' and ParentId='00112';

delete from Sys_DictionaryItem where DictionaryId='00005' and ParentId='00112';
insert into Sys_DictionaryItem
select * from [Smartlife-120397].dbo.Sys_DictionaryItem where DictionaryId='00005' and ParentId='00112'

select a.ItemId,b.ItemId,a.ItemName,b.ItemName
from (select * from Sys_DictionaryItem where DictionaryId='00005' and ParentId='00112')a,
(select * from [Smartlife-120397].dbo.Sys_DictionaryItem where DictionaryId='00005' and ParentId='00112') b
where a.ItemId=b.itemid 

update Sys_DictionaryItem set ItemName=b.ItemName
from (select * from Sys_DictionaryItem where DictionaryId='00005' and ParentId='00112')a,
(select * from [Smartlife-120397].dbo.Sys_DictionaryItem where DictionaryId='00005' and ParentId='00112') b
where a.ItemId=b.itemid and a.DictionaryId='00005' and a.ParentId='00112'

select * into Sys_DictionaryItemOld0328
from Sys_DictionaryItem 
delete from Sys_DictionaryItem;

insert into Sys_DictionaryItem (DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2)
select DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2
 from Sys_DictionaryItemOld0328;
alter table sys_dictionaryitemOld0328  alter rename to sys_dictionaryitemOld0327; 

select   dbo.joinStr(name)
from    sys.all_columns 
where  object_id   in 
            (   select  object_id 
                from    sys.tables 
               where  name='Sys_DictionaryItem'
             )
             
             
 delete from Sys_DictionaryItem where DictionaryId='00005' and ParentId='00112';
insert into Sys_DictionaryItem (DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2)
select DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2
 from [Smartlife-120397].dbo.Sys_DictionaryItem where DictionaryId='00005' and ParentId='00112'            


select distinct b.CallNo--,''''+a.IDNo id,a.OldManName--,a.OldManId 
       from dbo.Oca_OldManBaseInfo a left join Oca_OldManConfigInfo b
            on a.OldManId=b.OldManId 
       where b.CallNo is not null and  len(a.OldManName )>3 and a.OldManName not like '≤‚ ‘%'
       and a.OldManName<>'1' and b.CallNo<>''
       order by callno
