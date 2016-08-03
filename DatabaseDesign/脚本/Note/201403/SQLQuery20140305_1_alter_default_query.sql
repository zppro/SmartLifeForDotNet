select DB_NAME() as DatabaseName,user_name() as UserName,
SUBSTRING(RecoverScript,CHARINDEX('PROCEDURE',RecoverScript)+10,len(RecoverScript)-CHARINDEX('PROCEDURE',RecoverScript)-9) as TableName,
'Procedure' ObjectType,
'create' ChangeType,
--cast(Id as nvarchar(32))+'_'+cast(VersionId as nvarchar(32)) 
VersionId,
 '3' as FunctionId from dbo.Dey_Script where Type='P';
 
 select  DB_NAME() as DatabaseName,user_name() as UserName,
--SUBSTRING(UpdateScript,CHARINDEX(']',UpdateScript)+2,
(case CHARINDEX('ADD ',UpdateScript) when 0 then CHARINDEX('(',UpdateScript) 
else CHARINDEX('ADD ',UpdateScript) end),
CHARINDEX(']',UpdateScript)-2 as TableName,
'table' ObjectType,
SUBSTRING(UpdateScript,0,CHARINDEX('table',UpdateScript)) as ChangeType,
--cast(Id as nvarchar(32))+'_'+cast(VersionId as nvarchar(32)) 
VersionId,
 '3' as FunctionId from dbo.Dey_Script where Type='T'
 
 -------------===============================================================
 select * from sys.tables where Name='Dey_Script' 
 select * from dbo.Cfg_Object;
 select name,xusertype from sys.systypes;
 
 select * from dbo.Cfg_ObjectColumnDetail where ChangeType='alter' and ID not in(
 select id from dbo.Cfg_ObjectColumnDetail where ChangeType='alter' and
  systemtypeiddest=systemtypeidsrc and MaxLengthDest=maxlengthsrc and isnulldest=isnullsrc
  --and DefaultDefinitionDest=DefaultDefinitionDest
  and isnull(DefaultDefinitionDest,'9')=isnull(DefaultDefinitionSrc,'9')
  );
 
 select isnull(DefaultDefinitionDest,'9'),isnull(DefaultDefinitionDest,'9'),
 tablename,columnname from dbo.Cfg_ObjectColumnDetail where isnull(DefaultDefinitionDest,'9')=isnull(DefaultDefinitionDest,'9')
 
 select * from dbo.Dey_Script  where objectname='Oca_ServiceTrackLog' order by versionid;
 
 
 alter table Oca_ServiceTrackLog drop constraints dk_Oca_ServiceTrackLogStatus; 
 alter table Oca_ServiceTrackLog add constraints dk_Oca_ServiceTrackLogStatus  default ('1') for Status ;
   
        select replace(REPLACE(a.DefaultDefinitionSrc,'(',''),')','')
        ------------
        select  DB_NAME() as DatabaseName,user_name() as UserName,
        e.TableName,e.ColumnName,e.SystemTypeId,e.MaxLength,
        e.IsNullable,'column' ObjectType,'add' ChangeType,3 VersionId,2 FunctionId
from (
      select b.Name  TableName,
             a.name ColumnName,a.system_type_id  SystemTypeId,a.max_length MaxLength ,a.is_nullable IsNullable 
      from remote_dbo.[smartlife-120300].sys.columns a 
           right join remote_dbo.[smartlife-120300].sys.tables b 
	   on b.object_id=a.object_id and b.object_id>0 
     ) e 
     /*,
     (
        select b.Name TableName
        from sys.columns a inner join sys.tables b on b.object_id=a.object_id and b.name='Oca_CallRecord'
        and b.object_id>0 
    ) f*/
where 
e.TableName+e.ColumnName not in
   ( 
      select b.Name+a.Name ColumnName
      from sys.columns a 
           inner join sys.tables b 
	   on b.object_id=a.object_id and b.object_id>0 
    ) --and e.TableName='Oca_CallRecord'
    and  
    e.TableName in ( select b.Name TableName
        from sys.columns a inner join sys.tables b on b.object_id=a.object_id and b.name='Oa_CallRecord'
        and b.object_id>0 
        
        ) ;
 ------------------------================================================       
        ALTER TABLE Oca_ServiceTrackLog
 drop constraint dk_Oca_ServiceTrackLog_status;
alter table Oca_ServiceTrackLog	  add  constraint  dk_Oca_ServiceTrackLog_status   default '2' for Status;


        select * from dbo.sysobjects
        select * from dbo.syscomments
        select a.name,a.object_id,a.parent_column_id,a.parent_object_id,a.definition,
        (select name from sys.all_objects where object_id=a.parent_object_id) table_name ,
        (select name from sys.all_columns  where default_object_id=a.object_id) column_name
        from sys.default_constraints  a where a.parent_object_id='430624577';
        
        select OBJECT_ID from sys.all_objects where name='Dey_Script';626101271
        select name from sys.all_columns  where default_object_id<>0  object_id=626101271 
        (select name from sys.all_objects where object_id=a.parent_object_id) table_name
           (select name from sys.all_columns  where default_object_id=a.object_id) column_name
           
           
           select a.definition from sys.default_constraints  a 
           where a.object_id=(select default_object_id from sys.all_columns where name='VersionIdTo')
           and a.parent_object_id=(select object_id from sys.all_objects where name='Cfg_Cnfigure') 
           
           select object_id from sys.all_objects where name='Cfg_Configure'
           select OBJECT_ID from sys.tables where name='Cfg_Configure'
           
           select default_object_id from sys.all_columns where name='VersionIdTo'
           
            select default_object_id from sys.columns where name='VersionIdTo'
            
            

           select * from Cfg_Object where ObjectType='Procedure'
           
           
           
           -----------------========================
           select 3 VersionId,
       TableName objectName,
       'alter table '+TableName+' ' +ChangeType+ ' '+case ChangeType when 'alter' then ' column' else '' end 
          +' '+a.ColumnName+' ' +
           (select  Name from sys.sysTypes where xuserType=a.SystemTypeIdDest )+
           case a.SystemTypeIdDest when 167 then replace('('+CAST (a.MaxLengthDest as nvarchar(32))+')','-1','max')
            when 175 then '('+CAST (a.MaxLengthDest as nvarchar(32))+')'
            when 231 then  '('+replace(left(cast(a.MaxLengthDest/2 as nvarchar(32)),1),'0','max')+
                    SUBSTRING(cast(a.MaxLengthDest/2 as nvarchar(32)),2,LEN(cast(a.MaxLengthDest/2 as nvarchar(32))))+')' else '' end +
		    case isnull(a.DefaultDefinitionDest,'9') when '9'  then '' else  ' default '+ a.DefaultDefinitionDest end +
           ' ' +case a.IsNullDest when 1 then ' null ' when 0 then ' not null ' else '' end +' ;'    UpdateScript,

       'alter table '+TableName+' '+case ChangeType when 'alter' then ' alter column' else ' drop column ' end 
                  +' '+a.ColumnName+' ' +
               (select  Name from sys.sysTypes where xuserType=a.SystemTypeIdSrc )+
                  case a.SystemTypeIdSrc 
	           when 167 then replace('('+CAST (a.MaxLengthSrc as nvarchar(32))+')','-1','max')
                   when 175 then '('+CAST (a.MaxLengthSrc as nvarchar(32))+')'
                   when 231 then  '('+replace(left(cast(a.MaxLengthSrc/2 as nvarchar(32)),1),'0','max')+
                        SUBSTRING(cast(a.MaxLengthSrc/2 as nvarchar(32)),2,LEN(cast(a.MaxLengthSrc/2 as nvarchar(32))))+')' else '' end +
		        case isnull(a.DefaultDefinitionSrc,'9') when '9'  then '' else  ' default '+ a.DefaultDefinitionSrc end +
                       ' ' +case a.IsNullSrc when 1 then ' null ' when 0 then ' not null ' else '' end +' ;'  RecoverScript,
        'select 1' UpdateValIdateScript,
        'select 1' RecoverValIdateScript,
        'T' Type
from Cfg_ObjectColumnDetail a 
where  ChangeType='alter';



/**************************************************************/
/* SP_Cfg_GetColumnDetailForAddColumn                         */
/*  在源库，不在目的库的表的字段的详细信息  add column        */
/*  @source_dbName 源数据库名，@dest_dbName 目的数据库名      */
/**************************************************************/

PRINT 'source_db_name $(source_db_name) ,sync_type $(sync_type)'
PRINT 'Creating procedure SP_Cfg_GetColumnDetailForAddColumn ...'

IF (EXISTS (SELECT *
            FROM dbo.sysobjects
            WHERE (name = N'SP_Cfg_GetColumnDetailForAddColumn')
              AND (type = 'P')))
  DROP PROCEDURE SP_Cfg_GetColumnDetailForAddColumn
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[SP_Cfg_GetColumnDetailForAddColumn]
@source_dbName nvarchar(100),
@dest_dbName   nvarchar(100)
 AS
 BEGIN
DECLARE  @sql_str nvarchar(max) 
      SET NOCOUNT ON
     
        IF (EXISTS (select * from Cfg_ObjectColumnDetail where ChangeType='add' and  ObjectType='column'))
             BEGIN
                       
                delete from  Cfg_ObjectColumnDetail where ChangeType='add' and  ObjectType='column';
             END
       IF (NOT EXISTS (select * from Cfg_ObjectColumnDetail where ChangeType='add' and  ObjectType='column'))
             BEGIN
      set @sql_str='  insert into dbo.Cfg_ObjectColumnDetail (
           DatabaseName,UserName,TableName,ColumnName,
	   SystemTypeIdDest,MaxLengthDest,IsNullDest,DefaultDefinitionDest,
	   ObjectType,ChangeType,VersionId,FunctionId)
            select  DB_NAME() as DatabaseName,user_name() as UserName, e.TableName,e.ColumnName,
	e.SystemTypeId SystemTypeIdDest,e.MaxLength MaxLengthDest,
	e.IsNullable IsNullableDest,e.DefaultDefinition DefaultDefinitionDest,
	''column'' ObjectType,''add'' ChangeType,3 VersionId,2 FunctionId
          from (
             select b.Name  TableName,  a.name ColumnName,
	     a.system_type_id    SystemTypeId,
	     a.max_length        MaxLength ,
	     a.is_nullable       IsNullable,  
	     ( select dc.definition from sys.default_constraints  dc 
                where dc.object_id=a.default_object_id
               and dc.parent_object_id=b.object_id) DefaultDefinition

           from '+@source_dbName+'.sys.columns a 
           right join '+@source_dbName+'.sys.tables b 
	   on b.object_id=a.object_id and b.object_id>0 
         ) e
           where e.TableName+e.ColumnName not in
            ( 
                 select b.Name+a.Name ColumnName
                 from '+@dest_dbName+'.sys.columns a 
                  inner join '+@dest_dbName+'.sys.tables b 
	          on b.object_id=a.object_id and b.object_id>0 
             ) 
              and  e.TableName in 
             (
                  select b.Name TableName
                   from '+@dest_dbName+'.sys.columns a inner join '+@dest_dbName+'.sys.tables b on b.object_id=a.object_id
                  and b.object_id>0 
              );'
        exec sp_executesql @sql_str 
          END

 END
GO


-------------------
select 3 VersionId,
       TableName objectName,
        'alter table '+TableName+' drop constraints dk_' +TableName+a.ColumnName+ '; '+
       'alter table '+TableName+' add constraints dk_' +TableName+a.ColumnName+ ' '+
	 case isnull(a.DefaultDefinitionDest,'9') when '9'  then '' else  ' default '+ a.DefaultDefinitionDest end +
           ' for ' +a.ColumnName +' ;'    UpdateScript,

       'alter table '+TableName+' '+case ChangeType when 'alter' then ' alter column' else ' drop column ' end 
                  +' '+a.ColumnName+' ' +
               (select  Name from sys.sysTypes where xuserType=a.SystemTypeIdSrc )+
                  case a.SystemTypeIdSrc 
	           when 167 then replace('('+CAST (a.MaxLengthSrc as nvarchar(32))+')','-1','max')
                   when 175 then '('+CAST (a.MaxLengthSrc as nvarchar(32))+')'
                   when 231 then  '('+replace(left(cast(a.MaxLengthSrc/2 as nvarchar(32)),1),'0','max')+
                        SUBSTRING(cast(a.MaxLengthSrc/2 as nvarchar(32)),2,LEN(cast(a.MaxLengthSrc/2 as nvarchar(32))))+')' else '' end +
		        case isnull(a.DefaultDefinitionSrc,'9') when '9'  then '' else  ' default '+ a.DefaultDefinitionSrc end +
                       ' ' +case a.IsNullSrc when 1 then ' null ' when 0 then ' not null ' else '' end +' ;'  RecoverScript,
        'select 1' UpdateValIdateScript,
        'select 1' RecoverValIdateScript,
        'T' Type
from Cfg_ObjectColumnDetail a 
where  ChangeType='alter' and (isnull(DefaultDefinitionDest,'9')<>isnull(DefaultDefinitionSrc,'9'))


