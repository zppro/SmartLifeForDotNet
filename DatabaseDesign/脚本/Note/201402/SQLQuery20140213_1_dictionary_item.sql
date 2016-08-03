select * from  Sys_DictionaryItem  where dictionaryId=11013 and itemid like '01%'

insert into Sys_DictionaryItem (dictionaryid,itemid,id,itemcode,itemname,parentid,orderno,levels,endflag,systemflag,description) 
values(11013,'01003',(select MAX(ID)+1 from  Sys_DictionaryItem),'01003','医疗保健服务',null,3,1,1,1,'');

update  Sys_DictionaryItem set ItemName='家政清洁'  where DictionaryId=11013 and ItemId='01001';
update  Sys_DictionaryItem set ItemName='水电家电维修'  where DictionaryId=11013 and ItemId='01002';

select MAX(ID)+1 from  Sys_DictionaryItem

select * from  Sys_DictionaryItem
 where DictionaryId=11013 and ItemId='01001'
 update Sys_DictionaryItem set ItemId='01003' ,ItemCode='01003' where ID=3650 
 -----==================================================
update  Sys_DictionaryItem set ItemName='统包服务'  where DictionaryId=11013 and ItemId='01001';
update  Sys_DictionaryItem set ItemName='家政清洁'  where DictionaryId=11013 and ItemId='01002';
delete from  Sys_DictionaryItem  where dictionaryId=11013 and itemid='01003';

select name databasename from sys.databases where name like 'smart%' or name like 'IPCC%';