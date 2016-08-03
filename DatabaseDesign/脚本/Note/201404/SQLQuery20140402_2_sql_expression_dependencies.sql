select OldManId,FamilyMemberId
from dbo.oca_oldmanfamilyinfo;

select gender,2014-SUBSTRING(idno,7,4),idno
from dbo.Oca_OldManBaseInfo
where IDNo<>'999999999999999999'

where OldManId='2C30E62E-1E8F-4B9D-B89E-3ADB396FE50B'

select gender,2014-SUBSTRING(idno,7,4),idno
from dbo.Oca_FamilyMember
where  LEN(idno)=18 and IDNo <>'' 

FamilyMemberId='1BD9040F-A38D-42BE-97E2-BE5B8060E9B7';

select * 
from  Sys_DictionaryItem
where DictionaryId='11003';

select gender,case substring(idno,17,1)%2 when 1 then 'M' else 'F' end,familymembername,idno
from  dbo.Oca_FamilyMember
where LEN(idno)=18 and IDNo <>'' and Gender ='N'

update dbo.Oca_FamilyMember
set  gender=case substring(idno,17,1)%2 when 1 then 'M' else 'F' end
where LEN(idno)=18 and IDNo <>'' and Gender ='N'
-------------------------------------------------------------

select a.OldManId,a.FamilyMemberId,b.Gender,c.Gender,b.age-c.age
from dbo.oca_oldmanfamilyinfo a left join 
(
select gender,2014-SUBSTRING(idno,7,4) age,oldmanid
from dbo.Oca_OldManBaseInfo
where IDNo<>'999999999999999999') b on a.OldManId=b.OldManId

 left join (select gender,2014-SUBSTRING(idno,7,4)age,FamilyMemberId
from dbo.Oca_FamilyMember
where  LEN(idno)=18 and IDNo <>'' ) c on a.FamilyMemberId=c.FamilyMemberId


insert into Bas_ResidentFamilyInfo (ID,checkintime,status,datasource,familyid,residentidofa,
residentidofb,atob,  btoa)
select ID,checkintime,status,datasource,id familyid,oldmanid residentidofa,
FamilyMemberId residentidofb,'00003' atob,'00001'  btoa
from [Smartlife-120303].dbo.oca_oldmanfamilyinfo

select * from sys.partitions
exec SP_Tol_UspOutputData @tablename='Sys_Menu'


select a.name,dbo.joinstr(b.name) columnname
                      from   sys.tables a,
                             sys.all_columns b
                       where a.object_id=b.object_id and b.name<>'id'
                     group by a.name
                     
  create table Cfg_TableNameForLoadData
(
 Id int identity(1,1) not null,
 TableName varchar(60) not null,
 Comment  nvarchar(60) not null
)

select name from sys.tables where name like 'Sys%'


insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_PlatformApplication','系统_平台应用');
insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_Tree','系统_树');
insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_Behavior','系统_行为');
insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_MenuBehavior','系统_菜单行为');
insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_Dictionary','系统_字典');
insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_DictionaryItem','系统_字典项目');
insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_TreeItem','系统_树项目');
insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_DictionaryItemExtend','系统_字典项目扩展');
insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_ApplicationAccessPoint','系统_应用接入点');
insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_Server','系统_服务器');
insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_ApplicationAccess','系统_应用接入');
insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_WhiteList','系统_白名单');
insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_ApplicationAccessibility','系统_接口应用访问性');
insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_Platform','系统_平台');
insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_Application','系统_应用');
insert into Cfg_TableNameForLoadData (TableName,Comment)values ('Sys_Menu','系统_菜单');

select tablename,comment from Cfg_TableNameForLoadData

declare @str varchar(1000)
set @str='r.[Smartlife-120300]'
select substring(@str,CHARINDEX('.',@str,0)+1,LEN(@str))


USE tempdb;
GO
CREATE PROCEDURE dbo.Y AS
SELECT * FROM sys.objects
GO
CREATE PROCEDURE dbo.X as
    EXEC dbo.Y;
GO
 


SELECT ( select name from sys.all_objects where object_id=a.referencing_id),
         referenced_entity_name
FROM sys.sql_expression_dependencies a

select definition 
from sys.check_constraints  
