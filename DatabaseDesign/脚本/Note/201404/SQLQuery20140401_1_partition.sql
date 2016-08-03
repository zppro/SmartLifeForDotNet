exec SP_Dey_CreateTypeScriptProc @source_dbName='[Smartlife-120300]',@dest_dbName='smartlife_test'

select * 
from sys.table_types;
select * 
from sys.tables;

select *
from Dey_Script
where TYPE='Y'


 create database [SmartLife-1203];
 use [SmartLife-1203]
 select * into Bas_ResidentBaseInfo 
 from remote_dbo.[SmartLife-1203].dbo.Bas_ResidentBaseInfo;
 
 select * into Bas_ResidentFamilyInfo
 from remote_dbo.[SmartLife-1203].dbo.Bas_ResidentFamilyInfo;
 
 select * from sys_dictionaryitem where itemcode like '1203%';
 
 declare @i int,@str nvarchar(4000)
set @i=21
set @str=''
while (select @i)<40
begin
set @i=@i+1
print @i
set @str='alter database [smartlife-1203] add filegroup test'+cast(@i as nvarchar(10))+'fg;'
exec sp_executesql @str
end

select * from sys.filegroups;

declare @i int,@str nvarchar(4000)
set @i=21
set @str=''
while (select @i)<40
begin
set @i=@i+1
print @i
set @str='ALTER DATABASE [smartlife-1203]
ADD FILE 
(
    NAME = Test'+cast(@i as nvarchar(10))+'data2,
    FILENAME =''C:\Program Files\Microsoft SQL Server\100\ttt'+cast(@i as nvarchar(10))+'dat2.mdf'',
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
) TO FILEGROUP test'+cast(@i as nvarchar(10))+'fg
;'
exec sp_executesql @str
end
go

CREATE PARTITION FUNCTION   OldManBaseInfoPF1 (char(5))
    AS RANGE LEFT FOR VALUES ('01189',
'01190',
'01191',
'01192',
'01193',
'01194',
'01195',
'01196',
'01197',
'01198',
'01199',
'01200') ;
GO

declare @i int,@str nvarchar(4000)
set @i=0
set @str=''
while (select @i)<30
begin
set @i=@i+1
print 'test'+cast(@i as nvarchar(10))+'fg,'
--print cast(@i as nvarchar(10))+','
end

CREATE PARTITION FUNCTION   OldManFamilyInfoPF1 (int)
    AS RANGE LEFT FOR VALUES (0,
1,
2,
3,
4,
5,
6,
7,
8,
9,
10,
11,
12,
13,
14,
15,
16,
17,
18,
19,
20,
21,
22,
23,
24,
25,
26,
27,
28,
29) ;
GO


CREATE PARTITION SCHEME OldManBaseInfoPS1
    AS PARTITION OldManBaseInfoPF1
    TO (test1fg,
test2fg,
test3fg,
test4fg,
test5fg,
test6fg,
test7fg,
test8fg,
test9fg,
test10fg,
test11fg,
test12fg,
test13fg,
test14fg,
test15fg,
test16fg,
test17fg,
test18fg,
test19fg,
test20fg,
test21fg) ;
GO

CREATE PARTITION SCHEME OldManFamilyInfoPS1
    AS PARTITION OldManFamilyInfoPF1
    TO (test1fg,
test2fg,
test3fg,
test4fg,
test5fg,
test6fg,
test7fg,
test8fg,
test9fg,
test10fg,
test11fg,
test12fg,
test13fg,
test14fg,
test15fg,
test16fg,
test17fg,
test18fg,
test19fg,
test20fg,
test21fg,
test22fg,
test23fg,
test24fg,
test25fg,
test26fg,
test27fg,
test28fg,
test29fg,
test30fg,
test31fg) ;
GO

------------14-022
select ((CONVERT([char](2),right(datepart(year,GETDATE()),2),0)+'-') +right('0000'+CONVERT([varchar](3),'22',0),(3)))


insert into  Bas_ResidentBaseInfo(   id,checkintime, ResidentId,Status,OperatedBy,OperatedOn,DataSource,ResidentName,
ResidentStatus,IDNo,Gender,AreaId,AreaId2)
select ID,CheckInTime,
OldManId,Status,OperatedBy,OperatedOn,DataSource,OldManName,
'00001' ResidentStatus,IDNo,Gender,AreaId,AreaId2
from remote_dbo.[smartlife-120303].dbo.Oca_OldManBaseInfo 
where AreaId2 is not  null;

insert into  Bas_ResidentBaseInfo(   id,checkintime, ResidentId,Status,OperatedBy,OperatedOn,DataSource,ResidentName,
ResidentStatus,IDNo,Gender,AreaId,AreaId2)
select ID,CheckInTime,
OldManId,Status,OperatedBy,OperatedOn,DataSource,OldManName,
'00001' ResidentStatus,IDNo,Gender,AreaId,AreaId2
from remote_dbo.[smartlife-120304].dbo.Oca_OldManBaseInfo 
where AreaId2 is not  null;
