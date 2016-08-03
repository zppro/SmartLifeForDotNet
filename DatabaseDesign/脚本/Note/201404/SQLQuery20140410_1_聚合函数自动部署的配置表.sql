select * 
from sys.databases;

master
go
select @@servername;
select serverproperty('wangwei');

sp_dropserver 'MEIY'
go
sp_addserver 'wangwei','local'
 
select * from sys.all_parameters
where object_id in (select object_id from sys.assembly_modules where assembly_class='JoinStrEx');------参数

select name,assembly_id from sys.assemblies;----------程序集
select assembly_id,name filename,file_id fileid,content,LEN(content) length from sys.assembly_files;  -----生成脚本的查询视图

select a.name,a.assembly_id,b.filename,b.fileid,b.content,b.length
from 
(select name,assembly_id 
 from sys.assemblies) a,
(select assembly_id,name filename,file_id fileid,content,LEN(content) length 
  from sys.assembly_files) b
where a.assembly_id=b.assembly_id

select * from sys.assembly_modules;  ---聚合函数名称
select * from sys.assembly_references;  
select * from sys.assembly_types;

drop table cfg_assemblies;
create table Cfg_Assemblies
(
 Name                nvarchar(128)  null,
 AssemblyId          int            null,
 FileName            nvarchar(260)  null,
 FileId              int            null,
 Content             varbinary(max) null,
 Length              int            null
 );
 
 drop table cfg_aggregateFunction;
 create table Cfg_AggregateFunction
 (
 ObjectId      int            null,
 AssemblyId    int            null,
 AssemblyClass nvarchar(128)  null,
 ParameterName nvarchar(128)  null,
 UserTypeId    int            null,
 TypeName      varchar(20)    null,
 AssemblyName  varchar(20)    null,
 ParamLength  int null
 );
 alter table Cfg_AggregateFunction add AssemblyName  varchar(20)    null
 alter table Cfg_AggregateFunction add  ParamLength  int null;
 
 
 insert into Cfg_AggregateFunction (ObjectId,AssemblyId,AssemblyClass,ParameterName,UserTypeId,ParamLength,TypeName,AssemblyName)
 select a.object_id,a.assembly_id,a.assembly_class,b.name paramterName,b.user_type_id,
 case b.user_type_id when 231 then b.max_length/2 else b.max_length end  ParamLength,
      (select name from sys.types where user_type_id=b.user_type_id) TypeName,
      (select name  from sys.assemblies where assembly_id=a.assembly_id) AssemblyName
 from (
 select * from sys.assembly_modules) a,
 (select * from sys.all_parameters)b
 where a.object_id=b.object_id and b.name<>''  and a.assembly_class not in 
 (
  select distinct assembly_class
 from 
 sys.assembly_modules
 
 )
 
 select * from sys.types;
 
 insert into Cfg_Assemblies (Name,AssemblyId,FileName,FileId,Content,Length)
 select c.name,c.assembly_id,c.filename,c.fileid,c.content,c.length
 from 
 (
		 select a.name,a.assembly_id,b.filename,b.fileid,b.content,b.length
		from 
		(select name,assembly_id 
		 from [Smartlife-120300].sys.assemblies) a,
		(select assembly_id,name filename,file_id fileid,content,LEN(content) length 
		  from [Smartlife-120300].sys.assembly_files) b
		where a.assembly_id=b.assembly_id
) c

where c.name+c.filename not in 
(
         select a.name+b.filename
		from 
		(select name,assembly_id 
		 from [Smartlife-1203].sys.assemblies) a,
		(select assembly_id,name filename,file_id fileid,content,LEN(content) length 
		  from [Smartlife-1203].sys.assembly_files) b
		where a.assembly_id=b.assembly_id
) 

exec dbo.SP_Cfg_GetAggregateFunctionDefinition @source_dbName='[Smartlife-120300]',@dest_dbName='[smartlife_test]'

select * from Cfg_Assemblies;
select * from Cfg_AggregateFunction;