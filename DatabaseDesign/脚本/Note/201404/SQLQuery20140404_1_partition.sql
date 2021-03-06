create procedure SP_DBA_PartitionAddFileGroup

as
begin
 declare @i int,@str nvarchar(4000)
set @i=1
set @str=''
while (select @i)<40
begin
set @i=@i+1
print @i
set @str='alter database [smartlife-1203] add filegroup test'+cast(@i as nvarchar(10))+'fg;'
exec sp_executesql @str
end
end
select * from sys.filegroups;

declare @i int,@str nvarchar(4000),@filePath varchar(4000)
set @i=1
set @str=''
select @filePath=
reverse(substring(reverse(physical_name),charindex('\',reverse(physical_name),0),LEN(physical_name)))
from sys.database_files where data_space_id=1;

while (select @i)<40
begin
set @i=@i+1
print @i
set @str='ALTER DATABASE [smartlife-1203]
ADD FILE 
(
    NAME = Test'+cast(@i as nvarchar(10))+'data2,
    FILENAME ='''+@filePath+cast(@i as nvarchar(10))+'dat2.mdf'',
    SIZE = 5MB,
    MAXSIZE = 1000MB,
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

CREATE TABLE [dbo].[Bas_ResidentBaseInfo](
	[ResidentId] [uniqueidentifier] NOT NULL,
	[Id] [int] NOT NULL,
	[CheckInTime] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[OperatedBy] [uniqueidentifier] NULL,
	[OperatedOn] [datetime] NULL,
	[DataSource] [char](5) NULL,
	[ResidentName] [nvarchar](20) NOT NULL,
	[ResidentStatus] [tinyint] NOT NULL,
	[ResidentBizId] [char](5) NULL,
	[IDNo] [char](18) NOT NULL,
	[Gender] [char](1) NOT NULL,
	[NativePlace] [char](5) NULL,
	[HouseholdRegister] [char](5) NULL,
	[EducationLevel] [char](5) NULL,
	[Marriage] [char](5) NULL,
	[LivingStatus] [char](5) NULL,
	[HousingStatus] [char](5) NULL,
	[IncomeStatus] [char](5) NULL,
	[PlaceOfHouseholdRegister] [nvarchar](200) NULL,
	[ResidentialAddress] [nvarchar](200) NULL,
	[AreaId] [char](5) NOT NULL,
	[AreaId2] [varchar](40) NOT NULL,
	[AreaId3] [varchar](40) NULL,
	[PostCode] [char](6) NULL,
	[Tel] [varchar](30) NULL,
	[Mobile] [varchar](20) NULL,
	[Remark] [nvarchar](400) NULL,
	[InputCode1] [varchar](30) NULL,
	[InputCode2] [varchar](30) NULL
) on OldManBaseInfoPS1 (AreaId)

CREATE TABLE [dbo].[Bas_ResidentFamilyInfo](
	[Id] [int] NOT NULL,
	[CheckInTime] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[OperatedBy] [uniqueidentifier] NULL,
	[OperatedOn] [datetime] NULL,
	[DataSource] [char](5) NULL,
	[FamilyId] [int] NOT NULL,
	[PartitionId]  AS ([FamilyId]%(30)) PERSISTED,
	[ResidentIdOfA] [uniqueidentifier] NOT NULL,
	[ResidentIdOfB] [uniqueidentifier] NOT NULL,
	[AToB] [char](5) NOT NULL,
	[BToA] [char](5) NOT NULL
) on  OldManFamilyInfoPS1(PartitionId)

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
