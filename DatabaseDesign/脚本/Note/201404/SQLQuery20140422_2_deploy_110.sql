exec SP_Tol_UspOutputDataEx @tablename='Sys_tree',@whereclause='where treeid in(''00001'')'
select * 
from Sys_Tree where treeid in('00001');

select * from Pub_UserArea;


select a.*,b.UserId from Oca_OldManBaseInfo a,Pub_UserArea b
where (a.AreaId2=b.AreaId or a.AreaId3=b.AreaId or (a.AreaId2 is null and a.AreaId3 is null))
and Status=1 and a.AreaId='99999' and UserId='AC41BAC1-9720-4C41-AAF5-C4D6B85E706E'


select a.*,b.UserId from Oca_OldManBaseInfo a,Pub_UserArea b
where a.AreaId2=b.AreaId 
union
select a.*,b.UserId from Oca_OldManBaseInfo a,Pub_UserArea b
where a.AreaId3=b.AreaId

select * 
from Oca_OldManBaseInfo 
where userid=''

go



 use [SmartLife-120300]
--增加字段[对象编号]ObjectId
IF not EXISTS(SELECT * FROM  DBO.SysColumns WHERE ID = Object_ID('Cer_Redirect') AND Name='ObjectId')
BEGIN
      ALTER TABLE Cer_Redirect ADD ObjectId varchar(10)  NOT NULL default  '120300'
      EXEC sp_addextendedproperty N'MS_Description',N'对象编号',N'user', N'dbo', N'table', N'Cer_Redirect', N'column', N'ObjectId'

End
GO
 
use [smartlife-120301]
--增加字段[对象编号]ObjectId
IF not EXISTS(SELECT * FROM  DBO.SysColumns WHERE ID = Object_ID('Cer_Redirect') AND Name='ObjectId')
BEGIN
      ALTER TABLE Cer_Redirect ADD ObjectId varchar(10)  NOT NULL default  '120301'
      EXEC sp_addextendedproperty N'MS_Description',N'对象编号',N'user', N'dbo', N'table', N'Cer_Redirect', N'column', N'ObjectId'

End
GO
 
use [smartlife-120302]
--增加字段[对象编号]ObjectId
IF not EXISTS(SELECT * FROM  DBO.SysColumns WHERE ID = Object_ID('Cer_Redirect') AND Name='ObjectId')
BEGIN
      ALTER TABLE Cer_Redirect ADD ObjectId varchar(10)  NOT NULL default  '120302'
      EXEC sp_addextendedproperty N'MS_Description',N'对象编号',N'user', N'dbo', N'table', N'Cer_Redirect', N'column', N'ObjectId'

End
GO
 
use [smartlife-120303]
--增加字段[对象编号]ObjectId
IF not EXISTS(SELECT * FROM  DBO.SysColumns WHERE ID = Object_ID('Cer_Redirect') AND Name='ObjectId')
BEGIN
      ALTER TABLE Cer_Redirect ADD ObjectId varchar(10)  NOT NULL default  '120303'
      EXEC sp_addextendedproperty N'MS_Description',N'对象编号',N'user', N'dbo', N'table', N'Cer_Redirect', N'column', N'ObjectId'

End
GO
 
use [smartlife-120304]
--增加字段[对象编号]ObjectId
IF not EXISTS(SELECT * FROM  DBO.SysColumns WHERE ID = Object_ID('Cer_Redirect') AND Name='ObjectId')
BEGIN
      ALTER TABLE Cer_Redirect ADD ObjectId varchar(10)  NOT NULL default  '120304'
      EXEC sp_addextendedproperty N'MS_Description',N'对象编号',N'user', N'dbo', N'table', N'Cer_Redirect', N'column', N'ObjectId'

End
GO
 
use [smartlife-120305]
--增加字段[对象编号]ObjectId
IF not EXISTS(SELECT * FROM  DBO.SysColumns WHERE ID = Object_ID('Cer_Redirect') AND Name='ObjectId')
BEGIN
      ALTER TABLE Cer_Redirect ADD ObjectId varchar(10)  NOT NULL default  '120305'
      EXEC sp_addextendedproperty N'MS_Description',N'对象编号',N'user', N'dbo', N'table', N'Cer_Redirect', N'column', N'ObjectId'

End
GO
 
use [smartlife-120306]
--增加字段[对象编号]ObjectId
IF not EXISTS(SELECT * FROM  DBO.SysColumns WHERE ID = Object_ID('Cer_Redirect') AND Name='ObjectId')
BEGIN
      ALTER TABLE Cer_Redirect ADD ObjectId varchar(10)  NOT NULL default  '120306'
      EXEC sp_addextendedproperty N'MS_Description',N'对象编号',N'user', N'dbo', N'table', N'Cer_Redirect', N'column', N'ObjectId'

End
GO
 
use [smartlife-120307]
--增加字段[对象编号]ObjectId
IF not EXISTS(SELECT * FROM  DBO.SysColumns WHERE ID = Object_ID('Cer_Redirect') AND Name='ObjectId')
BEGIN
      ALTER TABLE Cer_Redirect ADD ObjectId varchar(10)  NOT NULL default  '120307'
      EXEC sp_addextendedproperty N'MS_Description',N'对象编号',N'user', N'dbo', N'table', N'Cer_Redirect', N'column', N'ObjectId'

End
GO
 
use [smartlife-120311]
--增加字段[对象编号]ObjectId
IF not EXISTS(SELECT * FROM  DBO.SysColumns WHERE ID = Object_ID('Cer_Redirect') AND Name='ObjectId')
BEGIN
      ALTER TABLE Cer_Redirect ADD ObjectId varchar(10)  NOT NULL default  '120311'
      EXEC sp_addextendedproperty N'MS_Description',N'对象编号',N'user', N'dbo', N'table', N'Cer_Redirect', N'column', N'ObjectId'

End
GO
 
use [Smartlife-120395]
--增加字段[对象编号]ObjectId
IF not EXISTS(SELECT * FROM  DBO.SysColumns WHERE ID = Object_ID('Cer_Redirect') AND Name='ObjectId')
BEGIN
      ALTER TABLE Cer_Redirect ADD ObjectId varchar(10)  NOT NULL default  '120395'
      EXEC sp_addextendedproperty N'MS_Description',N'对象编号',N'user', N'dbo', N'table', N'Cer_Redirect', N'column', N'ObjectId'

End
GO
 
use [smartlife-120396]
--增加字段[对象编号]ObjectId
IF not EXISTS(SELECT * FROM  DBO.SysColumns WHERE ID = Object_ID('Cer_Redirect') AND Name='ObjectId')
BEGIN
      ALTER TABLE Cer_Redirect ADD ObjectId varchar(10)  NOT NULL default  '120396'
      EXEC sp_addextendedproperty N'MS_Description',N'对象编号',N'user', N'dbo', N'table', N'Cer_Redirect', N'column', N'ObjectId'

End
GO
 
use [smartlife-120397]
--增加字段[对象编号]ObjectId
IF not EXISTS(SELECT * FROM  DBO.SysColumns WHERE ID = Object_ID('Cer_Redirect') AND Name='ObjectId')
BEGIN
      ALTER TABLE Cer_Redirect ADD ObjectId varchar(10)  NOT NULL default  '120397'
      EXEC sp_addextendedproperty N'MS_Description',N'对象编号',N'user', N'dbo', N'table', N'Cer_Redirect', N'column', N'ObjectId'

End
GO
 
use [smartlife-120398]
--增加字段[对象编号]ObjectId
IF not EXISTS(SELECT * FROM  DBO.SysColumns WHERE ID = Object_ID('Cer_Redirect') AND Name='ObjectId')
BEGIN
      ALTER TABLE Cer_Redirect ADD ObjectId varchar(10)  NOT NULL default  '120398'
      EXEC sp_addextendedproperty N'MS_Description',N'对象编号',N'user', N'dbo', N'table', N'Cer_Redirect', N'column', N'ObjectId'

End
GO
 
use [smartlife-120399]
--增加字段[对象编号]ObjectId
IF not EXISTS(SELECT * FROM  DBO.SysColumns WHERE ID = Object_ID('Cer_Redirect') AND Name='ObjectId')
BEGIN
      ALTER TABLE Cer_Redirect ADD ObjectId varchar(10)  NOT NULL default  '120399'
      EXEC sp_addextendedproperty N'MS_Description',N'对象编号',N'user', N'dbo', N'table', N'Cer_Redirect', N'column', N'ObjectId'

End
GO
