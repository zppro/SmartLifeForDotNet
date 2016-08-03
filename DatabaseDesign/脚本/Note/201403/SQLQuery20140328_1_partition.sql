select * from cfg_bridge where IsSourceDb=0;
alter table cfg_bridge add Port varchar(5) null;
alter table cfg_bridge add IsSourceDb  bit null;
update Cfg_Bridge set port='10017',IsSourceDb=0
where NodeIp='115.236.175.110' and DatabaseName<>'smartlife-120300'

update Cfg_Bridge set port='10017',IsSourceDb=1
where NodeIp='115.236.175.110' and DatabaseName='smartlife-120300'

select * from Leblue-Configuration where [DatabaseName]= 'smartlife-120303'

select * 
from sys.partition_functions;

----------------------------------------=================================1.加文件组
declare @i int,@str nvarchar(4000)
set @i=0
set @str=''
while (select @i)<41
begin
set @i=@i+1
print @i
set @str='alter database [smartlife-1203] add filegroup test'+cast(@i as nvarchar(10))+'fg;'
exec sp_executesql @str
end
------------------------------------====================================
select *
from sys.filegroups 

go
----------------------------------------------------------===========2.加文件
declare @i int,@str nvarchar(4000)
set @i=0
set @str=''
while (select @i)<40
begin
set @i=@i+1
print @i
set @str='ALTER DATABASE [smartlife-1203]
ADD FILE 
(
    NAME = Test'+cast(@i as nvarchar(10))+'data2,
    FILENAME =''C:\Program Files\Microsoft SQL Server\100\tt'+cast(@i as nvarchar(10))+'dat2.mdf'',
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
) TO FILEGROUP test'+cast(@i as nvarchar(10))+'fg
;'
exec sp_executesql @str
end
go
----------------------------------------======================================

declare @i int,@str nvarchar(4000)
set @i=0
set @str=''
while (select @i)<30
begin
set @i=@i+1
--print 'test'+cast(@i as nvarchar(10))+'fg,'
print cast(@i as nvarchar(10))+','
end
alter database [smartlife-120303] add filegroup test21fg
ALTER DATABASE [smartlife-120303]
ADD FILE 
(
    NAME = Test21data2,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\100\tt21dat2.mdf',
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
) TO FILEGROUP test21fg
;

------------------------------------------------=========================3 加分区函数
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
-------------------------------------====================================== 4. 加分区模式
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
----------------------------=================================================

CREATE PARTITION SCHEME myRangePS1
    AS PARTITION myRangePF1
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


drop table Oca_OldManBaseInfoPartition;
CREATE TABLE PartitionTable (col1 int, col2 char(10))
    ON myRangePS1 (col1) ;

--------------------------------------------------------------=============================5 创建分区表

drop table [dbo].[Bas_ResidentBaseInfo]
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
) ON OldManBaseInfoPS1 (areaid) 

GO

drop table Bas_ResidentFamilyInfo;
CREATE TABLE [dbo].[Bas_ResidentFamilyInfo](
	[Id] [int] NOT NULL,
	[CheckInTime] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[OperatedBy] [uniqueidentifier] NULL,
	[OperatedOn] [datetime] NULL,
	[DataSource] [char](5) NULL,
	[FamilyId] [int] NOT NULL,
	 PartitionId  as FamilyId%30 persisted, 
	[ResidentIdOfA] [uniqueidentifier] NOT NULL,
	[ResidentIdOfB] [uniqueidentifier] NOT NULL,
	[AToB] [char](5) NOT NULL,
	[BToA] [char](5) NOT NULL
) ON OldManFamilyInfoPS1 (PartitionId) 

GO
--------------------------------------------------------------==============================
CREATE TABLE [dbo].[Oca_OldManBaseInfoPartition](
	[OldManId] [uniqueidentifier] NOT NULL,
	[Id] [int]  NOT NULL,
	[CheckInTime] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[OperatedBy] [uniqueidentifier] NULL,
	[OperatedOn] [datetime] NULL,
	[DataSource] [char](5) NULL,
	[OldManName] [nvarchar](20) NOT NULL,
	[Gender] [char](1) NOT NULL,
	[Birthday] [datetime] NULL,
	[IDNo] [char](18) NOT NULL,
	[HealthInsuranceFlag] [tinyint] NOT NULL,
	[HealthInsuranceNumber] [varchar](50) NULL,
	[SocialInsuranceFlag] [tinyint] NOT NULL,
	[SocialInsuranceNumber] [varchar](50) NULL,
	[LivingState] [char](5) NULL,
	[OldManIdentity] [char](5) NULL,
	[AreaId] [char](5) NULL,
	[AreaId2] [varchar](40) NULL,
	[AreaId3] [varchar](40) NULL,
	[Address] [nvarchar](200) NULL,
	[LongitudeS] [varchar](20) NULL,
	[LatitudeS] [varchar](20) NULL,
	[PostCode] [char](6) NULL,
	[Tel] [varchar](30) NULL,
	[Mobile] [varchar](20) NULL,
	[Remark] [nvarchar](400) NULL,
	[InputCode1] [varchar](30) NULL,
	[InputCode2] [varchar](30) NULL,
 CONSTRAINT [PK_Oca_OldManBaseInfoPartition] PRIMARY KEY CLUSTERED 
(
	[OldManId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [Primary]
) ON myRangePS1 (id) 

SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF

CREATE  INDEX [Oca_OldManBaseInfoPartition_id] ON [dbo].[Oca_OldManBaseInfoPartition] 
(
    [id] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON  myRangePS1 (id) 

drop index PK_Oca_OldManBaseInfoPartition on [dbo].[Oca_OldManBaseInfoPartition] ;

CREATE CLUSTERED INDEX [Oca_OldManBaseInfoPartition_id] ON [dbo].[Oca_OldManBaseInfoPartition] 
(
    [id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, 
DROP_EXISTING = ON, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON  myRangePS1 (id) 
-------------------------=================================================================

set statistics profile on
select ID%20,count(idno) 
from Oca_OldManBaseInfo
group by ID%20;

select  SUBSTRING(idno,1,6) ,COUNT(*)
from Oca_OldManBaseInfo
group by SUBSTRING(idno,1,6)

select * from Sys_DictionaryItem where DictionaryId='00005' and ParentId='00112';

delete from Sys_DictionaryItem where DictionaryId='00005' and ParentId='00112';
insert into Sys_DictionaryItem
select * from [Smartlife-120397].dbo.Sys_DictionaryItem where DictionaryId='00005' and ParentId='00112'

select a.ItemId,b.ItemId,a.ItemName,b.ItemName,a.Status,b.Status,a.Description,b.Description
,a.endflag,b.EndFlag,a.SystemFlag,b.SystemFlag,a.OrderNo,b.OrderNo,a.Levels,b.Levels,a.InputCode1,b.InputCode1
,a.InputCode2,b.InputCode2
from (select * from Sys_DictionaryItem where DictionaryId='00005' and ParentId='00112')a,
(select * from [Smartlife-120397].dbo.Sys_DictionaryItem where DictionaryId='00005' and ParentId='00112') b
where a.ItemId=b.itemid

--select * into Sys_DictionaryItemOld0328 from remote_dbo.[smartlife-120396].dbo.Sys_DictionaryItemOld0328


select   dbo.joinStr(name)
from    sys.all_columns 
where  object_id   in 
            (   select  object_id 
                from    sys.tables 
               where  name='Oca_OldManBaseInfo'
             )

insert into Oca_OldManBaseInfoPartition(OldManId,Id,CheckInTime,Status,OperatedBy,OperatedOn,DataSource,OldManName,Gender,Birthday,IDNo,HealthInsuranceFlag,HealthInsuranceNumber,SocialInsuranceFlag,SocialInsuranceNumber,LivingState,OldManIdentity,AreaId,AreaId2,AreaId3,Address,LongitudeS,LatitudeS,PostCode,Tel,Mobile,Remark,InputCode1,InputCode2)
select OldManId,Id%20+1 id,CheckInTime,Status,OperatedBy,OperatedOn,DataSource,OldManName,Gender,Birthday,IDNo,HealthInsuranceFlag,HealthInsuranceNumber,SocialInsuranceFlag,SocialInsuranceNumber,LivingState,OldManIdentity,AreaId,AreaId2,AreaId3,Address,LongitudeS,LatitudeS,PostCode,Tel,Mobile,Remark,InputCode1,InputCode2
from Oca_OldManBaseInfo


select * 
from Oca_OldManBaseInfo
where idno='330125192811210422';

select * 
from Oca_OldManBaseInfoPartition
where 
idno='330125192811210422' and ID=18 and 
$Partition.myRangePF1 (id)=18;

select * 
from Oca_OldManBaseInfo
where id%20+1=18

exec SP_Cfg_GetFunctionDefinition @source_dbName='[Smartlife-120300]',@dest_dbName='[Smartlife-120301]'

select * from [SmartLife-120300].dbo.Oca_OldManBaseInfo

select * from cfg_objectversionhistory where type='F'

select ID,idno,oldmanid
 from Oca_OldManBaseInfo


CREATE TABLE [dbo].[foo_year_source_partition_test](
 [detail_date] [datetime] NULL,
 [source_key] [int] NULL,
 [year_source]  AS ((CONVERT([char](2),right(datepart(year,[detail_date]),2),0)+'-') +right('0000'+CONVERT([varchar](3),[source_key],0),(3))) PERSISTED,
 [ys_id] int identity (1,1)
) ON zzYearSourcePScheme(year_source)
go

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

