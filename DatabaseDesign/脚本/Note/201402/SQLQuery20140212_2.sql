alter table dbo.sys_dictionaryitem alter column itemname nvarchar(100);
insert dbo.sys_dictionaryitem
select * from [smartlife-120300].[dbo].sys_dictionaryitem

select * from [dbo].sys_dictionaryitem where DictionaryId=11013 and ItemId like '01%'