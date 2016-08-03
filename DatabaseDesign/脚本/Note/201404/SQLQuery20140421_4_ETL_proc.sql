EXEC master..xp_cmdshell 'BCP "SELECT TOP 20 * FROM [leblue-etl].dbo.cfg_etl_procedure" 
queryout c:\currency2.txt -c -U"sa" -P"123456" ' 


EXEC master..xp_cmdshell 'BCP "SELECT TOP 20 * FROM [Smartlife-120300].dbo.oca_oldmanbaseinfo" 
queryout e:\oldmanbaseinfo0421.txt -c -U"dbo" -P"leblue@2014" -S"115.236.175.110,10017" ' 

go
------------------------===================================================
/************************************************************************/
/* SP_ETL_ExpData */
/* 按照单列的约束对数据进行检查 */
/************************************************************************/
create procedure SP_ETL_ExpData
@tablename nvarchar(100)
as 
begin
EXEC master..xp_cmdshell 'BCP "SELECT TOP 20 * FROM [Smartlife-120300].dbo.oca_oldmanbaseinfo" queryout e:\oldmanbaseinfo0421.txt -c -U"sa" -P"1,leblue@2013" -S"115.236.175.110,10017" '
select * into oca_step1oldmanbaseinfo 
from remote_dbo.[Smartlife-120300].dbo.oca_oldmanbaseinfo
where 1=0

EXEC master..xp_cmdshell 'BCP oca_step1oldmanbaseinfo in e:\oldmanbaseinfo0421.txt -c -U"sa" -P"123456" -d"Leblue-Etl"' 
end

select * from oca_step1oldmanbaseinfo;

go
/************************************************************************/
/* SP_ETL_CheckColumn */
/* 按照单列的约束对数据进行检查 */
/************************************************************************/
alter procedure SP_ETL_CheckColumn
@TableName nvarchar(100)='',
@ColumnName nvarchar(100)=''
as 
begin
select 'print checkcolumn finished'
end

/************************************************************************/
/* SP_ETL_CheckStructure */
/* 按照结构关系对数据进行检查 */
/************************************************************************/
alter procedure SP_ETL_CheckStructure
@TableName nvarchar(100)='',
@ColumnName nvarchar(100)=''
as 
begin
select 'print checkStructure finished'
end

/************************************************************************/
/* SP_ETL_CheckBusiness */
/* 按照业务规则对数据进行检查 */
/************************************************************************/
alter procedure SP_ETL_CheckBusiness
@TableName nvarchar(100)='',
@ColumnName nvarchar(100)=''
as 
begin
select 'print checkBusiness finished'
end

/************************************************************************/
/* SP_ETL_CheckDataQuality */
/* 对数据进行检查 */
/************************************************************************/
alter procedure SP_ETL_CheckDataQuality
as 
begin
    
 exec  sp_etl_checkcolumn
 exec    sp_etl_checkStructure
 exec    SP_ETL_CheckBusiness

end

/************************************************************************/
/* SP_ETL_CleanData */
/* 对数据进行清洗 */
/************************************************************************/
create procedure SP_ETL_CleanData
as 
begin
 exec   SP_ETL_Elementing
 exec	SP_ETL_Standarding
 exec	SP_ETL_Verify
 exec	SP_ETL_Matching
 exec	SP_ETL_Family
 exec	SP_ETL_Documenting
   
   
end

/************************************************************************/
/* SP_ETL_Elementing */
/* 对数据进行清洗,步骤一元素化 */
/************************************************************************/
create procedure SP_ETL_Elementing
as 
begin
   select 'print elementing finished'
end
/************************************************************************/
/* SP_ETL_Standarding */
/* 对数据进行清洗，步骤二规范化 */
/************************************************************************/
create procedure SP_ETL_Standarding
as 
begin
   select 'standarding finished'
end
/************************************************************************/
/* SP_ETL_Verify */
/* 对数据进行清洗，步骤三验证 */
/************************************************************************/
create procedure SP_ETL_Verify
as 
begin
   select 'verify finished'
end
/************************************************************************/
/* SP_ETL_Matching */
/* 对数据进行清洗 步骤四 匹配*/
/************************************************************************/
create procedure SP_ETL_Matching
as 
begin
   select 'matching finished'
end
/************************************************************************/
/* SP_ETL_Family */
/* 对数据进行清洗 步骤五分组*/
/************************************************************************/
create procedure SP_ETL_Family
as 
begin
   select 'familied finished'
end
/************************************************************************/
/* SP_ETL_Documenting */
/* 对数据进行清洗 步骤六文档化*/
/************************************************************************/
create procedure SP_ETL_Documenting
as 
begin
   select 'documenting finished'
end


select * from Sys_TreeItem;
exec SP_Tol_UspOutputData @tablename='Sys_TreeItem'

exec SP_Dey_OutputData @tablename='Sys_TreeItem'

insert Sys_TreeItem(TreeId,ItemId,Status,ItemCode,ItemName,GenerateMode,GenerateContent,PrefixId,Description,OrderNo)
 values('00008','10008',1,'01','OldCare','00002','select DictionaryId as Id,DictionaryId as Code,DictionaryName 
as Name,'_' as PId,0 as Levels,cast(0 as bit) as IsParent,CodeClass  from sys_dictionary where Status=1','null','',1)

insert Sys_TreeItem(TreeId,ItemId,Status,ItemCode,ItemName,GenerateMode,GenerateContent,PrefixId,Description,OrderNo) 
values('00010','10010',1,'1','1','00002','with CTE_Sys_Dictionary_00005 as (    select cast(a.ItemId as varchar(40)) 
as Id,a.ItemCode as Code,a.ItemName as Name,cast('_    ' as varchar(40)) as PId,a.Levels-1 as Levels,    a.EndFlag,
a.OrderNo    from Sys_DictionaryItem a where a.Status=1 and a.DictionaryId='$DictionaryId$' and a.ItemId = '$ItemId$'   
 union all    select cast(a.ItemId as varchar(40)) as Id,a.ItemCode as Code,a.ItemName as Name,cast(isnull(a.ParentId,'_') 
 as varchar(40)) as PId,a.Levels-1 as Levels,a.EndFlag,a.OrderNo    from Sys_DictionaryItem a inner join 
 CTE_Sys_Dictionary_00005 b on a.ParentId = b.Id where a.Status=1    union all    select cast(a.AreaId as varchar(40))
  as Id,a.AreaCode as Code,a.AreaName as Name,cast(isnull(a.ParentId,'_') as varchar(40)) as PId,a.Levels-1 as Levels,
  a.EndFlag,a.OrderNo    from pub_Area a inner join CTE_Sys_Dictionary_00005 b on a.ParentId = b.Id where a.Status = 1  ) 
  
  
  '
  
  insert Sys_TreeItem(TreeId,ItemId,Status,ItemCode,ItemName,GenerateMode,GenerateContent,PrefixId,Description,OrderNo) 
  values('00006','10006',1,'02','菜单行为','00002','select a.MenuId+''-''+a.BehaviorCode as Id,c.BehaviorName as Name,
  a.MenuId as PId, b.Levels+1 as Levels,cast(0 as bit) as IsParent   ,case when d.GroupId is not null then cast(1 as bit)
   else cast(0 as bit) end Checked,  isnull(d.GroupId,e.GroupId)  from Sys_MenuBehavior a inner join Sys_Menu b on a.MenuId 
   = b.MenuId inner join Sys_Behavior c  on a.BehaviorCode = c.BehaviorCode cross join Pub_Group e left join Pub_GroupPermit 
   d  on e.GroupId = d.GroupId and a.MenuId+''-''+a.BehaviorCode = d.ResourceId  where b.Status = 1 and c.Status = 1 and 
   e.Status =1','null','',2)
   select m.* from CTE_Sys_Dictionary_00005 m','null','',1)
   
   
   select 'insert Sys_TreeItem(TreeId,ItemId,Status,ItemCode,ItemName,GenerateMode,GenerateContent,PrefixId,Description,OrderNo) values('+''''+isnull(replace(TreeId,'''',''''''),'null')+''''+','+''''+isnull(replace(ItemId,''',''''),'null')+''''+','+isnull(cast(Status as varchar(10)),'null')+','+''''+isnull(replace(ItemCode,''',''''),'null')+''''+','+''''+isnull(replace(ItemName,''',''''),'null')+''''+','+''''+isnull(replace(GenerateMode,''',''''),'null')+''''+','+''''+isnull(replace(GenerateContent,''',''''),'null')+''''+','+''''+isnull(replace(PrefixId,''',''''),'null')+''''+','+''''+isnull(replace(Description,''',''''),'null')+''''+','+isnull(cast(OrderNo as varchar(30)),'null')+')' updatescript,'' targetscript  from Sys_TreeItem
   
   select * from dbo.Sys_Dictionary where DictionaryId between 11012 and 11013;
   
   select * from Sys_Dictionaryitem 
   where DictionaryId='11012' or DictionaryId='11013'
   
   exec SP_Tol_UspOutputDataEx @tablename='Sys_Dictionary',@whereclause=' where DictionaryId between 11012 and 11013'

exec SP_Dey_OutputData @tablename='Sys_Dictionaryitem',@where='where DictionaryId=''11012'' or DictionaryId=''11013'''

select  a.updatescript,replace(a.updatescript,'''null''','null')
from (
select 'insert Sys_Dictionary(DictionaryId,Status,CodeClass,DictionaryName,Description,CodeRule) values('+''''+
isnull(replace(DictionaryId,'''',''''''),'null')+''''+','+isnull(cast(Status as varchar(10)),'null')+','+''''+
isnull(replace(CodeClass,'''',''''''),'null')+''''+','+''''+isnull(replace(DictionaryName,'''',''''''),'null')+''''+','+''''+
isnull(replace(Description,'''',''''''),'null')+''''+','+''''+isnull(replace(CodeRule,'''',''''''),'null')+''''+')' updatescript,
'' targetscript  from Sys_Dictionary  where DictionaryId between 11012 and 11013) a

insert Sys_Dictionary(DictionaryId,Status,CodeClass,DictionaryName,Description,CodeRule) values('11013',1,'00012','生活服务项目B(小类)','null','5')


select 'insert Sys_Dictionary(DictionaryId,Status,CodeClass,DictionaryName,Description,CodeRule) values('
+''''+isnull(DictionaryId,'null')+''''+','+isnull(cast(Status as varchar(10)),'null')+','+''''+
isnull(CodeClass,'null')+''''+','+''''+isnull(DictionaryName,'null')+''''+','+''''+
isnull(Description,'null')+''''+','+''''+isnull(CodeRule,'null')+''''+')' updatescript,'' targetscript  
from Sys_Dictionary  where DictionaryId between 11012 and 11013


insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11012','00002',1,'02','精神关怀服务','null','null ',1,1,1,2,'null','null')


insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11012','00005',1,'05','社区服务','null',null,1,1,1,5,'null','null')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','03006',1,'03006','搬家搬运','null',null,1,1,1,6,'null','null')
insert Sys_Dictionaryitem(DictionaryId,ItemId,Status,ItemCode,ItemName,Description,ParentId,Levels,EndFlag,SystemFlag,OrderNo,InputCode1,InputCode2) values('11013','03006',1,'03006','搬家搬运','null','null ',1,1,1,6,'null','null')

select * from sys.types;

select * from sys.syslockinfo