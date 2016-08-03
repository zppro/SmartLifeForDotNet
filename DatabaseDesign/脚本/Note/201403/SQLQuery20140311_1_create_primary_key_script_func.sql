select right(convert(varchar(30),getdate(),113),3)*3731%1000000,
rand(right(convert(varchar(30),getdate(),113),3))

select dbo.func_tol_getrandom()

-----------------------------------------------------------------

--@标值函数：FUNC_Tol_GetRandom  获取 六位的随机数。

alter  FUNCTION [dbo].[FUNC_Tol_GetRandom] 
( 
)
RETURNS varchar(6)
AS
BEGIN
  
     begin
	        declare @Ret varchar(6)	
	        select @Ret =right(convert(varchar(30),getdate(),113),3)*3731%900000+100000	
	 end
	 return @Ret
END


select  DB_NAME() as DatabaseName,
        user_name() as UserName,
        ObjectName TableName,
        'table' ObjectType,
        'create' ChangeType,
        VersionId,
        '3' as FunctionId 
from dbo.Dey_Script where Type='T'

select dbo.func_tol_getObjectdll('Oca_OldManBaseInfo','table')
DECLARE  @table_name nvarchar(50),@ProcessTrackingTableName varchar(4000),@primarykey varchar(4000),@sql_ret varchar(4000)

  select @table_name ='oca_callservice'
         set @ProcessTrackingTableName=''
         select @ProcessTrackingTableName =@ProcessTrackingTableName+ c.columnname+'' 
         from 
          ( 
                select b.ColumnName+'  '+(select a.name from sys.types  a where a.user_type_id=b.systemtypeiddest)+
                       case  b.systemtypeiddest when 167 then replace('('+CAST (b.MaxLengthDest as nvarchar(32))+')','-1','max')
                                                when 165 then replace('('+CAST (b.MaxLengthDest as nvarchar(32))+')','-1','max')
                        when 175 then '('+CAST (b.MaxLengthDest as nvarchar(32))+')' 
                         when 231 then  '('+replace(left(cast(b.MaxLengthDest/2 as nvarchar(32)),1),'0','max')+
                         SUBSTRING(cast(b.MaxLengthDest/2 as nvarchar(32)),2,LEN(cast(b.MaxLengthDest/2 as nvarchar(32))))+')' else '' end +' '
                         +case b.ISNULLdest when 0 then ' not null' when 1 then ' null' else '' end +' ,' as columnname 
                          from Cfg_ObjectColumnDetail b where TableName=@table_name
           ) c
          -- select @ProcessTrackingTableName
           
           select @primarykey=columnname  from Cfg_ObjectPrimaryKey  where objectname='oca_callservice' 
            set @primarykey='  PRIMARY KEY CLUSTERED  ('+isnull(@primarykey,'')+'),'
             set @ProcessTrackingTableName =@ProcessTrackingTableName+@primarykey
           set  @sql_ret='create table [dbo].'+@table_name+' ('+left(@ProcessTrackingTableName,LEN(@ProcessTrackingTableName)-1)+');'
           print @sql_ret
            
           select TableName,ColumnName,systemtypeiddest,MaxLengthDest,ISNULLdest 
from Cfg_ObjectColumnDetail where tablename='oca_callservice'
[smartlife-120300]
select columnname from Cfg_ObjectPrimaryKey where objectname='oca_callservice' 
                       insert into Cfg_ObjectPrimaryKey (databasename,username,objectname,columnname,type)
                        select 'smartlife-120300' databasename,(select name from sys.schemas where SCHEMA_ID= m.schema_id) userName,
                           isnull((select name from [smartlife-120300].sys.tables where object_id=m.parent_object_id ),
                          (select name from [smartlife-120300].sys.table_types where parent_object_id=m.parent_object_id)
                           ) objectname,
                           dbo.joinStr(ac.name) columnname
                           , isnull((select object_id%10 from [smartlife-120300].sys.tables where object_id=m.parent_object_id ),-1) type
                        from    [smartlife-120300].sys.key_constraints m ,
                                [smartlife-120300].sys.index_columns ic,
                                [smartlife-120300].sys.all_columns ac
                        where   m.type='PK' --and 
                        --m.parent_object_id=
                        --(select object_id from [smartlife-120300].sys.tables where name='oca_callservice' )
                                and m.parent_object_id=ic.object_id 
                                and m.parent_object_id=ac.object_id 
                                and ic.index_column_id=ac.column_id 
                                group by m.schema_id,m.parent_object_id
                                
                                --在Sql Server中执行这段代码可以开启CLR
exec sp_configure 'show advanced options', '1';
go
reconfigure;
go
exec sp_configure 'clr enabled', '1'
go
reconfigure;
exec sp_configure 'show advanced options', '1';
go 


create table Cfg_ObjectPrimaryKey
(
DatabaseName           nvarchar(60)     not null,
UserName               nvarchar(30)     not null,
ObjectName	        Varchar(60)	Not null,
ColumnName              Varchar(60)         null,
BeforeModifyDate	DateTime	    null,
AfterModifyDate  	DateTime	    null,
Type                    tinyint         not null default('0')
)


alter function Func_Tol_GetObjectDll
(
@ObjectName nvarchar(50),
@ObjectType nvarchar(50)
)
RETURNS varchar(6000)
AS
BEGIN
DECLARE  @table_name nvarchar(50),@ProcessTrackingTableName varchar(4000),@primarykey varchar(4000),@sql_ret varchar(4000)

  select @table_name ='oca_callservice'
         set @ProcessTrackingTableName=''
         select @ProcessTrackingTableName =@ProcessTrackingTableName+ c.columnname+'' 
         from 
          ( 
                select b.ColumnName+'  '+(select a.name from sys.types  a where a.user_type_id=b.systemtypeiddest)+
                       case  b.systemtypeiddest when 167 then replace('('+CAST (b.MaxLengthDest as nvarchar(32))+')','-1','max')
                                                when 165 then replace('('+CAST (b.MaxLengthDest as nvarchar(32))+')','-1','max')
                        when 175 then '('+CAST (b.MaxLengthDest as nvarchar(32))+')' 
                         when 231 then  '('+replace(left(cast(b.MaxLengthDest/2 as nvarchar(32)),1),'0','max')+
                         SUBSTRING(cast(b.MaxLengthDest/2 as nvarchar(32)),2,LEN(cast(b.MaxLengthDest/2 as nvarchar(32))))+')' else '' end +' '
                         +case b.ISNULLdest when 0 then ' not null' when 1 then ' null' else '' end +' ,' as columnname 
                          from Cfg_ObjectColumnDetail b where TableName=@ObjectName
           ) c
          -- select @ProcessTrackingTableName
           
           select @primarykey=columnname  from Cfg_ObjectPrimaryKey  where objectname=@ObjectName
            set @primarykey='  PRIMARY KEY CLUSTERED  ('+isnull(@primarykey,'')+'),'
             set @ProcessTrackingTableName =@ProcessTrackingTableName+@primarykey
           set  @sql_ret='create table [dbo].'+@ObjectName+' ('+left(@ProcessTrackingTableName,LEN(@ProcessTrackingTableName)-1)+');'
         --  print @sql_ret
         return @sql_ret
END
