select * from sheet56$ where len(f4)>18;
exec SP_DBA_MergeTempExcelSource @TableName='Sheet56$',@UpdateTableName='Temp_ExcelSource'
select * into Temp_ExcelSource  from [smartlife-120303].dbo.Temp_ExcelSource where 1=2;

--delete from dbo.Temp_ExcelSource
--delete from sheetSource
select * from sheetSource;
select idno,oldmanname
from Temp_ExcelSource where IDNo='330103194101190413'
where IDNo in (
select idno from Temp_ExcelSource 
group by idno
having COUNT(*)>1)


insert into sheetSource(F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23)
select F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23
from Sheet56$ where f4 not in (select f4 from sheetSource);


where OldManName='¿Õæ’”¢'
select 'exec SP_DBA_MergeTempExcelSource @TableName='''+  name+''',@UpdateTableName=''Temp_ExcelSource''' 
select 'select  COUNT(*) from '+name+' where len(f4)< 18 and len(f4)> 15 union'

select 'insert into sheetSource(F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23)
select F1,F2,F3,F4,'''' F5,F6,F7,F8,F9,F10,F11,'''' F12,'''' F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23
from  '+name+'  where f4 not in (select f4 from sheetSource);'
--select 'select f1,f4 from  '+name+' union all'
from sys.tables 
where name like 'sheet%$' and name >'sheet55$'
and len(name)=8

group by name

---------------------------------------------------------------------
select * from Sheet56$ order by f4,f1 where len(f4)>18; 
select * from sheet57$ where len(f4)>18; 
select * from sheet58$ where len(f4)>18; 
select * from Sheet59$ where len(f4)>18; 
select * from Sheet60$ where len(f4)>18; 
select * from Sheet61$ order by f4,f1  where len(f4)>18; 
select * from Sheet62$ where len(f4)>18; 
select * from Sheet63$ where len(f4)>18; 
select * from Sheet64$ where len(f4)>18; 
select * from Sheet65$ where len(f4)>18; 
select * from Sheet66$ where len(f4)>18; 

select * from Sheet56$ where len(f4)>20; 
select * from sheet57$ where len(f4)>20; 
select * from sheet58$ where len(f4)>20; 
select * from Sheet59$ where len(f4)>20; 
select * from Sheet60$ where len(f4)>20; 
select * from Sheet61$ where len(f4)>20; 
select * from Sheet62$ where len(f4)>20; 
select * from Sheet63$ where len(f4)>20; 
select * from Sheet64$ where len(f4)>20; 
select * from Sheet65$ where len(f4)>20; 
select * from Sheet66$ where len(f4)>20; 

delete from Sheet56$ where len(f4)>20; 
delete from sheet57$ where len(f4)>20; 
delete from sheet58$ where len(f4)>20; 
delete from Sheet59$ where len(f4)>20; 
delete from Sheet60$ where len(f4)>20; 
delete from Sheet61$ where len(f4)>20; 
delete from Sheet62$ where len(f4)>20; 
delete from Sheet63$ where len(f4)>20; 
delete from Sheet64$ where len(f4)>20; 
delete from Sheet65$ where len(f4)>20; 
delete from Sheet66$ where len(f4)>20; 

select f1,f4 from Sheet56$ where len(f4)>18 union 
select f1,f4 from sheet57$ where len(f4)>18 union 
select f1,f4 from sheet58$ where len(f4)>18 union 
select f1,f4 from Sheet59$ where len(f4)>18 union 
select f1,f4 from Sheet60$ where len(f4)>18 union 
select f1,f4 from Sheet61$ where len(f4)>18 union 
select f1,f4 from Sheet62$ where len(f4)>18 union 
select f1,f4 from Sheet63$ where len(f4)>18 union 
select f1,f4 from Sheet64$ where len(f4)>18 union 
select f1,f4 from Sheet65$ where len(f4)>18 union 
select f1,f4 from Sheet66$ where len(f4)>18 ;

exec SP_DBA_MergeTempExcelSource @TableName='Sheet56$',@UpdateTableName='Temp_ExcelSource'
exec SP_DBA_MergeTempExcelSource @TableName='sheet57$',@UpdateTableName='Temp_ExcelSource'
exec SP_DBA_MergeTempExcelSource @TableName='sheet58$',@UpdateTableName='Temp_ExcelSource'
exec SP_DBA_MergeTempExcelSource @TableName='Sheet59$',@UpdateTableName='Temp_ExcelSource'
exec SP_DBA_MergeTempExcelSource @TableName='Sheet60$',@UpdateTableName='Temp_ExcelSource'
exec SP_DBA_MergeTempExcelSource @TableName='Sheet61$',@UpdateTableName='Temp_ExcelSource'
exec SP_DBA_MergeTempExcelSource @TableName='Sheet62$',@UpdateTableName='Temp_ExcelSource'
exec SP_DBA_MergeTempExcelSource @TableName='Sheet63$',@UpdateTableName='Temp_ExcelSource'
exec SP_DBA_MergeTempExcelSource @TableName='Sheet64$',@UpdateTableName='Temp_ExcelSource'
exec SP_DBA_MergeTempExcelSource @TableName='Sheet65$',@UpdateTableName='Temp_ExcelSource'
exec SP_DBA_MergeTempExcelSource @TableName='Sheet66$',@UpdateTableName='Temp_ExcelSource'

select  COUNT(*) from Sheet56$ where len(f4)< 18 and len(f4)> 15 union
select  COUNT(*) from sheet57$ where len(f4)< 18 and len(f4)> 15 union
select  COUNT(*) from sheet58$ where len(f4)< 18 and len(f4)> 15 union
select  COUNT(*) from Sheet59$ where len(f4)< 18 and len(f4)> 15 union
select  COUNT(*) from Sheet60$ where len(f4)< 18 and len(f4)> 15 union
select  COUNT(*) from Sheet61$ where len(f4)< 18 and len(f4)> 15 union
select  COUNT(*) from Sheet62$ where len(f4)< 18 and len(f4)> 15 union
select  COUNT(*) from Sheet63$ where len(f4)< 18 and len(f4)> 15 union
select  COUNT(*) from Sheet64$ where len(f4)< 18 and len(f4)> 15 union
select  COUNT(*) from Sheet65$ where len(f4)< 18 and len(f4)> 15 union
select  COUNT(*) from Sheet66$ where len(f4)< 18 and len(f4)> 15  ;


select f4,COUNT(*) from Sheet56$ group by f4  having COUNT(*)>1 union
select f4,COUNT(*) from sheet57$ group by f4  having COUNT(*)>1 union
select f4,COUNT(*) from sheet58$ group by f4  having COUNT(*)>1 union
select f4,COUNT(*) from Sheet59$ group by f4  having COUNT(*)>1 union
select f4,COUNT(*) from Sheet60$ group by f4  having COUNT(*)>1 union
select f4,COUNT(*) from Sheet61$ group by f4  having COUNT(*)>1 union
select f4,COUNT(*) from Sheet62$ group by f4  having COUNT(*)>1 union
select f4,COUNT(*) from Sheet63$ group by f4  having COUNT(*)>1 union
select f4,COUNT(*) from Sheet64$ group by f4  having COUNT(*)>1 union
select f4,COUNT(*) from Sheet65$ group by f4  having COUNT(*)>1 union
select f4,COUNT(*) from Sheet66$ group by f4  having COUNT(*)>1 ;


select f1,f4 ,COUNT(*)
from (
select f1,f4 from  Sheet56$ union all
select f1,f4 from  sheet57$ union all
select f1,f4 from  sheet58$ union all
select f1,f4 from  Sheet59$ union all
select f1,f4 from  Sheet60$ union all
select f1,f4 from  Sheet61$ union all
select f1,f4 from  Sheet62$ union all
select f1,f4 from  Sheet63$ union all
select f1,f4 from  Sheet64$ union all
select f1,f4 from  Sheet65$ union all
select f1,f4 from  Sheet66$  ) a
group by a.f1,a.f4
having COUNT(*)>1;


select   dbo.joinStr(name)
from    sys.all_columns 
where  object_id   in 
            (   select  object_id 
                from    sys.tables 
               where  name='Sheet65$'
             )

select * from sheetSource;
insert into sheetSource(F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23)  select F1,F2,F3,F4,'' F5,F6,F7,F8,F9,F10,F11,'' F12,'' F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23  from  Sheet56$  where f4 not in (select f4 from sheetSource);
insert into sheetSource(F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23)  select F1,F2,F3,F4,'' F5,F6,F7,F8,F9,F10,F11,'' F12,'' F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23  from  sheet57$  where f4 not in (select f4 from sheetSource);
insert into sheetSource(F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23)  select F1,F2,F3,F4,'' F5,F6,F7,F8,F9,F10,F11,'' F12,'' F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23  from  sheet58$  where f4 not in (select f4 from sheetSource);
insert into sheetSource(F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23)  select F1,F2,F3,F4,'' F5,F6,F7,F8,F9,F10,F11,'' F12,'' F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23  from  Sheet59$  where f4 not in (select f4 from sheetSource);
insert into sheetSource(F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23)  select F1,F2,F3,F4,'' F5,F6,F7,F8,F9,F10,F11,'' F12,'' F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23  from  Sheet60$  where f4 not in (select f4 from sheetSource);
insert into sheetSource(F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23)  select F1,F2,F3,F4,'' F5,F6,F7,F8,F9,F10,F11,'' F12,'' F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23  from  Sheet61$  where f4 not in (select f4 from sheetSource);
insert into sheetSource(F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23)  select F1,F2,F3,F4,'' F5,F6,F7,F8,F9,F10,F11,'' F12,'' F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23  from  Sheet62$  where f4 not in (select f4 from sheetSource);
insert into sheetSource(F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23)  select F1,F2,F3,F4,'' F5,F6,F7,F8,F9,F10,F11,'' F12,'' F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23  from  Sheet63$  where f4 not in (select f4 from sheetSource);
insert into sheetSource(F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23)  select F1,F2,F3,F4,'' F5,F6,F7,F8,F9,F10,F11,'' F12,'' F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23  from  Sheet64$  where f4 not in (select f4 from sheetSource);
insert into sheetSource(F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23)  select F1,F2,F3,F4,'' F5,F6,F7,F8,F9,F10,F11,'' F12,'' F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23  from  Sheet65$  where f4 not in (select f4 from sheetSource);
insert into sheetSource(F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23)  select F1,F2,F3,F4,'' F5,F6,F7,F8,F9,F10,F11,'' F12,'' F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23  from  Sheet66$  where f4 not in (select f4 from sheetSource);