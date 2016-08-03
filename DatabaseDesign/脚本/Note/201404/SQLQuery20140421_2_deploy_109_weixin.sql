CREATE TABLE [dbo].[Sys_Dictionary](
	[DictionaryId] [char](5) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[CodeClass] [char](5) NULL,
	[DictionaryName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](400) NULL,
	[CodeRule] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Sys_Dictionary] PRIMARY KEY CLUSTERED 
(
	[DictionaryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dictionary', @level2type=N'COLUMN',@level2name=N'DictionaryId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dictionary', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dictionary', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代码分类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dictionary', @level2type=N'COLUMN',@level2name=N'CodeClass'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dictionary', @level2type=N'COLUMN',@level2name=N'DictionaryName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dictionary', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码规则' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dictionary', @level2type=N'COLUMN',@level2name=N'CodeRule'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统_字典' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dictionary'
GO

ALTER TABLE [dbo].[Sys_Dictionary] ADD  DEFAULT ('1') FOR [Status]
GO

--------------------------------------=================================================================
CREATE TABLE [dbo].[Sys_DictionaryItem](
	[DictionaryId] [char](5) NOT NULL,
	[ItemId] [char](5) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[ItemCode] [varchar](100) NULL,
	[ItemName] [nvarchar](100) NULL,
	[Description] [varchar](400) NULL,
	[ParentId] [char](5) NULL,
	[Levels] [tinyint] NOT NULL,
	[EndFlag] [tinyint] NOT NULL,
	[SystemFlag] [tinyint] NOT NULL,
	[OrderNo] [int] NOT NULL,
	[InputCode1] [varchar](30) NULL,
	[InputCode2] [varchar](30) NULL,
 CONSTRAINT [PK_Sys_DictionaryItem] PRIMARY KEY CLUSTERED 
(
	[DictionaryId] ASC,
	[ItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictionaryItem', @level2type=N'COLUMN',@level2name=N'DictionaryId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典项目编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictionaryItem', @level2type=N'COLUMN',@level2name=N'ItemId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictionaryItem', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictionaryItem', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictionaryItem', @level2type=N'COLUMN',@level2name=N'ItemCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictionaryItem', @level2type=N'COLUMN',@level2name=N'ItemName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictionaryItem', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictionaryItem', @level2type=N'COLUMN',@level2name=N'ParentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'级次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictionaryItem', @level2type=N'COLUMN',@level2name=N'Levels'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'末级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictionaryItem', @level2type=N'COLUMN',@level2name=N'EndFlag'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'此代码是系统代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictionaryItem', @level2type=N'COLUMN',@level2name=N'SystemFlag'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictionaryItem', @level2type=N'COLUMN',@level2name=N'OrderNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'拼音码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictionaryItem', @level2type=N'COLUMN',@level2name=N'InputCode1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'五笔码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictionaryItem', @level2type=N'COLUMN',@level2name=N'InputCode2'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统_字典项目' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictionaryItem'
GO

ALTER TABLE [dbo].[Sys_DictionaryItem] ADD  DEFAULT ('1') FOR [Status]
GO

ALTER TABLE [dbo].[Sys_DictionaryItem] ADD  DEFAULT ('1') FOR [Levels]
GO

ALTER TABLE [dbo].[Sys_DictionaryItem] ADD  DEFAULT ('1') FOR [EndFlag]
GO

ALTER TABLE [dbo].[Sys_DictionaryItem] ADD  DEFAULT ('1') FOR [SystemFlag]
GO

ALTER TABLE [dbo].[Sys_DictionaryItem] ADD  DEFAULT ('1') FOR [OrderNo]
GO

-----------------------------------=======================================
delete from Sys_Dictionary
insert Sys_Dictionary(DictionaryId,Status,CodeClass,DictionaryName,Description,CodeRule) values('11012',1,'00012','生活服务项目A(大类)',null,'2')
insert Sys_Dictionary(DictionaryId,Status,CodeClass,DictionaryName,Description,CodeRule) values('11013',1,'00012','生活服务项目B(小类)',null,'5')


insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11012','00001',1,'01','政府购买服务',null,null,1,1,1,1,'ZFGMFW','GYMNET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11012','00002',1,'02','精神关怀服务',null,null,1,1,1,2,'JSGHFW','OPUNET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11012','00003',1,'03','生活服务',null,null,1,1,1,3,'SHFW','TIET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11012','00004',1,'04','咨询服务',null,null,1,1,1,4,'ZXFW','UYET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11012','00005',1,'05','社区',null,null,1,1,1,5,'SQFW','PAET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11012','00006',1,'06','其他服务',null,null,1,1,1,6,'QTFW','AWET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','01001',1,'01001','家政清洁',null,null,1,1,1,1,'JZQJ','PGII')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','01002',1,'01002','水电家电维修',null,null,1,1,1,2,'SDJDWX','IJPJXW')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','01003',1,'01003','医疗保健服务',null,null,1,1,1,3,'YLBJFW','AUWWET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','02001',1,'02001','建议',null,null,1,1,1,1,'JY','VY')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','02002',1,'02002','投诉',null,null,1,1,1,2,'TS','RY')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','02003',1,'02003','举报',null,null,1,1,1,3,'JB','IR')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','02004',1,'02004','感谢',null,null,1,1,1,4,'GX','DY')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','02005',1,'02005','心理疏导',null,null,1,1,1,5,'XLSD','NGNN')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','02006',1,'02006','读报谈心',null,null,1,1,1,6,'DBTX','YRYN')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','02007',1,'02007','其他精神关怀服务',null,null,1,1,1,7,'QTJSGHFW','AWOPUNET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','03001',1,'03001','家政清洁',null,null,1,1,1,1,'JZQJ','PGII')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','03002',1,'03002','家电安装维修',null,null,1,1,1,2,'JDAZWX','PJPUXW')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','03003',1,'03003','物业安装维修',null,null,1,1,1,3,'WYAZWX','TOPUXW')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','03004',1,'03004','水电安装维修',null,null,1,1,1,4,'SDAZWX','IJPUXW')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','03005',1,'03005','疏通服务',null,null,1,1,1,5,'STFW','NCET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','03006',1,'03006','搬家搬运',null,null,1,1,1,6,'BJBY','RPRF')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','03007',1,'03007','个人洗护',null,null,1,1,1,7,'GRXH','WWIR')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','03008',1,'03008','生活照料服务',null,null,1,1,1,8,'SHZLFW','TIJOET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','03009',1,'03009','代办服务',null,null,1,1,1,9,'DBFW','WLET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','03010',1,'03010','配送服务-水气饭票日用品',null,null,1,1,1,10,'PSFW-SQFPRYP','SUET-IRQSJEK')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','03011',1,'03011','预约服务',null,null,1,1,1,11,'YYFW','CXET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','03012',1,'03012','医疗保健服务',null,null,1,1,1,12,'YLBJFW','AUWWET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','03013',1,'03013','其他生活服务',null,null,1,1,1,13,'QTSHFW','AWTIET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','04001',1,'04001','医疗咨询',null,null,1,1,1,1,'YLZX','AUUY')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','04002',1,'04002','法律咨询',null,null,1,1,1,2,'FLZX','ITUY')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','04003',1,'04003','政策咨询',null,null,1,1,1,3,'ZCZX','GTUY')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','04004',1,'04004','一般咨询',null,null,1,1,1,4,'YBZX','GTUY')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','05001',1,'05001','助老服务',null,null,1,1,1,1,'ZLFW','EFET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','05002',1,'05002','社区卫生服务中心',null,null,1,1,1,2,'SQWSFWZX','PABTETKN')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','05003',1,'05003','社区警务室',null,null,1,1,1,3,'SQJWS','PAATP')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','05004',1,'05004','老年食堂',null,null,1,1,1,4,'LNST','FRWI')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','05005',1,'05005','社区老年活动中心',null,null,1,1,1,5,'SQLNHDZX','PAFRIFKN')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','05006',1,'05006','社区志愿者服务',null,null,1,1,1,6,'SQZYZFW','PAFDFET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','05007',1,'05007','社区司法援助服务',null,null,1,1,1,7,'SQSFYZFW','PANIREET')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','06001',1,'06001','疑难',null,null,1,1,1,1,'YN','XC')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','06002',1,'06002','测试',null,null,1,1,1,2,'CS','IY')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','06003',1,'06003','误拨',null,null,1,1,1,3,'WB','YR')