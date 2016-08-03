create table Oca_OldMan
(
OldManId  uniqueidentifier not null,
Name      nvarchar(64)     not null,
IDNo      char(18)         not null,
Gender    char(18)         not null,
UrlHead   varchar(255)     null,
CheckInTime Datetime       not null,
Status    TinyInt   default(1)   not null,
Mobile    varchar(30)      null,
Remark    Nvarchar(400)    null,
 CONSTRAINT [PK_Oca_OldMan] PRIMARY KEY NONCLUSTERED 
(
	[OldManId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;

insert into Oca_OldMan(OldManId,Name,IDNo,Gender,CheckInTime,Status,Mobile,Remark)
select ResidentId OldManId,ResidentName Name,IDNo,Gender,CheckInTime,Status,Mobile,Remark
from Bas_ResidentBaseInfo where len(ResidentName)<=3 and len(residentname)>1 and residentname not like '%测试%'
  
set
select * from Oca_OldMan


EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'OldManId'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'老人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'Name'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'老人身份证' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'IDNo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'老人性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'Gender'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'老人头像路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'UrlHead'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据进入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'CheckInTime'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态:1有效,0无效' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'Status'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'老人联系号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'Mobile'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Oca_OldMan', @level2type=N'COLUMN',@level2name=N'Remark'

