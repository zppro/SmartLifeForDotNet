create database [Leblue-Etl]
create table Out_DirtyData
(
 TableName varchar(60) null,
 Id         int null,
 ColumnName varchar(60)null,
 Reason nvarchar(200) null,
 Remark nvarchar(200) null,
 SourceDb varchar(60) null
);

CREATE TABLE [dbo].[Pip_SourceOldManBaseInfo](
	[OldManId] [uniqueidentifier] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
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
	[InputCode2] [varchar](30) NULL
	,SourceDb varchar(60) null
	);
	
	create table Pip_DestiOldManBaseInfo
   ([OldManId] [uniqueidentifier] NOT NULL,
	[Id]    [int] IDENTITY(1,1) NOT NULL,
	[CheckInTime] [datetime] NOT NULL,
    FamilyName     nvarchar(10) null,
    LastName    nvarchar(10) null,
    [OldManName] [nvarchar](20) NOT NULL,
	[Gender] [char](1) NOT NULL,	
	[IDNo] [char](18) NOT NULL,
    IdAreaId  int null,
    IdBirthday int null,
    IdRandom  int null,
    SourceDb varchar(60) null);
    
    go
    create procedure SP_Etl_ExpData
    @tablename varchar(50),
    @beginTime datetime,
    @endTime datetime
    as 
    begin
    insert into Pip_SourceOldManBaseInfo
    (OldManId,Id,CheckInTime,Status,OperatedBy,OperatedOn,DataSource,OldManName,Gender,Birthday,IDNo,
HealthInsuranceFlag,HealthInsuranceNumber,SocialInsuranceFlag,SocialInsuranceNumber,LivingState,
OldManIdentity,AreaId,AreaId2,AreaId3,Address,LongitudeS,LatitudeS,PostCode,Tel,Mobile,Remark,
InputCode1,InputCode2,sourcedb)
    select OldManId,Id,CheckInTime,Status,OperatedBy,OperatedOn,DataSource,OldManName,Gender,Birthday,IDNo,
HealthInsuranceFlag,HealthInsuranceNumber,SocialInsuranceFlag,SocialInsuranceNumber,LivingState,
OldManIdentity,AreaId,AreaId2,AreaId3,Address,LongitudeS,LatitudeS,PostCode,Tel,Mobile,Remark,
InputCode1,InputCode2,'120303' sourcedb
    from [smartlife-120303].dbo.Oca_OldManBaseInfo
    where checkintime  between @beginTime  and @endTime;
    end