insert into temp_excelsource (oldmanid,status,oldmanname,idno,
healthinsuranceFlag,socialinsuranceFlag,areaid,areaid2,areaid3,tel,address,remark)
select newid() oldmanid ,1 status ,a.name,a.idno,0 healthinsuranceFlag,0 socialinsuranceFlag,'01189' areaid,
(select areaid from pub_area where areaname=a.areadid2) areaid2,
(select areaid from pub_area where areaname=a.areaid3+'ÉçÇø') areaid3,
cast(CAST(a.tel as bigint ) as varchar(30)) tel,
a.address,a.remark
from 
(
select f1 name,f4 idno,f10 address,f12 tel,f16 areadid2,f17 areaid3,f24 remark from sheet1$ where f4 is not null and (len(f4)=18 or len(f4)=15)  union
select f1 name,f4 idno,f10 address,f12 tel,f16 areadid2,f17 areaid3,f24 remark from sheet2$ where f4 is not null and (len(f4)=18 or len(f4)=15) union
select f1 name,f4 idno,f10 address,f12 tel,f16 areadid2,f17 areaid3,f24 remark from sheet3$ where f4 is not null and (len(f4)=18 or len(f4)=15) union
select f1 name,f4 idno,f10 address,f12 tel,f16 areadid2,f17 areaid3,f24 remark from sheet4$ where f4 is not null and (len(f4)=18 or len(f4)=15) union
select f1 name,f4 idno,f10 address,f12 tel,f16 areadid2,f17 areaid3,f24 remark from sheet5$ where f4 is not null and (len(f4)=18 or len(f4)=15) union
select f1 name,f4 idno,f10 address,f12 tel,f16 areadid2,f17 areaid3,f24 remark from sheet6$ where f4 is not null and (len(f4)=18 or len(f4)=15)
) a;

update temp_excelsource set tel=cast(CAST(a.tel as bigint ) as varchar(30))
from  temp_excelsource b,(
select f1 name,f4 idno,f10 address,f12 tel,f16 areadid2,f17 areaid3,f24 remark from sheet1$ where f4 is not null and (len(f4)=18 or len(f4)=15)  union
select f1 name,f4 idno,f10 address,f12 tel,f16 areadid2,f17 areaid3,f24 remark from sheet2$ where f4 is not null and (len(f4)=18 or len(f4)=15) union
select f1 name,f4 idno,f10 address,f12 tel,f16 areadid2,f17 areaid3,f24 remark from sheet3$ where f4 is not null and (len(f4)=18 or len(f4)=15) union
select f1 name,f4 idno,f10 address,f12 tel,f16 areadid2,f17 areaid3,f24 remark from sheet4$ where f4 is not null and (len(f4)=18 or len(f4)=15) union
select f1 name,f4 idno,f10 address,f12 tel,f16 areadid2,f17 areaid3,f24 remark from sheet5$ where f4 is not null and (len(f4)=18 or len(f4)=15) union
select f1 name,f4 idno,f10 address,f12 tel,f16 areadid2,f17 areaid3,f24 remark from sheet6$ where f4 is not null and (len(f4)=18 or len(f4)=15)
) a
where a.idno=b.idno

update temp_excelsource set datasource='00003',gender='M'

select * from temp_excelsource;
exec SP_DBA_ImportOldManBaseInfo

select * from remote_dbo.[smartlife-120301].dbo.Oca_OldManBaseInfo;
CREATE TABLE [dbo].[Temp_ExcelSource](
	[OldManId] [uniqueidentifier] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CheckInTime] [datetime] NULL,
	[Status] [tinyint] NULL,
	[OperatedBy] [uniqueidentifier] NULL,
	[OperatedOn] [datetime] NULL,
	[DataSource] [char](5) NULL,
	[OldManName] [nvarchar](20) NULL,
	[Gender] [char](1) NULL,
	[Birthday] [datetime] NULL,
	[IDNo] [char](18) NULL,
	[HealthInsuranceFlag] [tinyint] NULL,
	[HealthInsuranceNumber] [varchar](50) NULL,
	[SocialInsuranceFlag] [tinyint] NULL,
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
 CONSTRAINT [PK_ExcelSource] PRIMARY KEY CLUSTERED 
(
	[OldManId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Temp_ExcelSource] ADD  CONSTRAINT [DF__Oca___Check__35DCF99B]  DEFAULT (getdate()) FOR [CheckInTime]
GO

ALTER TABLE [dbo].[Temp_ExcelSource] ADD  CONSTRAINT [DF__Oca___Statu__36D11DD4]  DEFAULT ('1') FOR [Status]
GO

select * from Pub_Area;
drop table Pub_Area;

select * into Pub_Area from remote_dbo.[Smartlife-120301].dbo.Pub_Area;