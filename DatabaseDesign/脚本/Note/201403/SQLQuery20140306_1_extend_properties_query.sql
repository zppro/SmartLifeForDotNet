select * from cfg_object;
select * from Dey_Script where TYPE='M';
 select * from Cfg_TableColumnComment
------------------==========取表及字段的注释信息，存入配置库。
insert into Cfg_TableColumnComment (DatabaseName,UserName,TableName,ColumnName,
BeforeComment,BeforeLength,BeforeModifyDate,Type)
select DB_NAME() as DatabaseName,user_name() as UserName,
(select name from [SmartLife-120300].sys.tables where object_id=a.major_id) tablename,
(select name from [SmartLife-120300].sys.columns where object_id=a.major_id and column_id=a.minor_id) columnname,
cast(value as nvarchar(max))    BeforeComment,
LEN(cast(value as nvarchar(3200))) BeforeLength,
GETDATE() BeforModifyDate,
a.minor_id Type 
 from [SmartLife-120300].sys.extended_properties a where a.name='MS_Description';
----------------------===================end
 


--------------------====================***************
insert into Dey_Script (VersionId,objectName,UpdateScript,RecoverScript,UpdateValIdateScript,RecoverValIdateScript,Type)
select 3 VersionId,
      a.TableName objectName,
      'EXEC sys.sp_addextendedproperty @name=''MS_Description'', @value='''+a.BeforeComment+''' , 
@level0type=''SCHEMA'',@level0name='''+a.UserName+''', 
@level1type=''TABLE'',@level1name='''+a.TableName+''', 
@level2type=''COLUMN'',@level2name='''+a.ColumnName +'''
GO'  UpdateScript,'' RecoverScript,
      'select 1' UpdateValIdateScript,
      'select 1' RecoverValIdateScript,
      'M' Type
 from Cfg_TableColumnComment a where  a.Type<>0 and a.databasename='smartlife-120300'
 and a.tablename+a.columnname+a.beforecomment not in(
 select ab.tablename+ab.columnname+ab.beforecomment  tcb
       from cfg_tablecolumncomment ab ,cfg_tablecolumncomment bb
       where ab.databasename='smartlife-120300' and bb.databasename='smartlife-120301'
        and ab.tablename=bb.tablename and ab.columnname=bb.columnname and ab.beforecomment=bb.beforecomment
 );
 
 insert into Dey_Script (VersionId,objectName,UpdateScript,RecoverScript,UpdateValIdateScript,RecoverValIdateScript,Type)
select 3 VersionId,
      a.TableName objectName,
      'EXEC sys.sp_addextendedproperty @name=''MS_Description'', @value='''+a.BeforeComment+''' , 
@level0type=''SCHEMA'',@level0name='''+a.UserName+''', 
@level1type=''TABLE'',@level1name='''+a.TableName+'''
GO'  UpdateScript,'' RecoverScript,
      'select 1' UpdateValIdateScript,
      'select 1' RecoverValIdateScript,
      'M' Type
 from Cfg_TableColumnComment a where  a.Type=0 and a.databasename='smartlife-120300'
 and a.tablename+a.beforecomment not in(
 select ab.tablename+ab.beforecomment  tcb
       from cfg_tablecolumncomment ab ,cfg_tablecolumncomment bb
       where ab.databasename='smartlife-120300' and bb.databasename='smartlife-120301'
        and ab.tablename=bb.tablename  and ab.beforecomment=bb.beforecomment and ab.type=0
 );
--------------------------==================****************** end 
 
 EXEC sys.sp_addextendedproperty @name='MS_Description', @value='Id' ,   @level0type='SCHEMA',@level0name='dbo',  
  @level1type='TABLE',@level1name='Pub_ReminderObject',   @level2type='COLUMN',@level2name='Id'  
 GO
 
 EXEC sys.sp_addextendedproperty @name='MS_Description', @value='公共_提醒对象' ,   @level0type='SCHEMA',@level0name='dbo',   @level1type='TABLE',@level1name='Pub_ReminderObject'  
 GO
 
 select * from  Cfg_ObjectColumnDetail where ChangeType='add' and  ObjectType='column';
  select * from  Cfg_ObjectColumnDetail where ChangeType='alter' and  ObjectType='column';
    select * from  Cfg_ObjectColumnDetail where ChangeType='drop' and  ObjectType='column';
 
 ---------------调用方法如下所示：
 exec dbo.SP_Cfg_GetColumnDetailForAlterColumn @source_DbName='[smartlife-120300]',@dest_DbName='[smartlife-120301]'
 
 exec dbo.SP_Cfg_GetColumnDetailForDropColumn @source_DbName='[smartlife-120300]',@dest_DbName='[smartlife-120301]'
 
 exec dbo.SP_Cfg_GetTableColumnComment @source_DbName='remote_dbo.[smartlife-120300]',@databasename='remote_dbo.smartlife-120300'
 exec dbo.SP_Cfg_GetTableColumnComment @source_DbName='[smartlife-120300]',@databasename='smartlife-120300'
 exec dbo.SP_Dey_AddTableColumnCommentScript  @source_DbName='smartlife-120300',@dest_DbName='smartlife-120301'
 --------------====================
 
 
   select ''''+replace(REPLACE('remote_dbo.[smartlife-120300]','[',''),']','')+'''' as data
   select * from  Cfg_TableColumnComment where DatabaseName='remote_dbo.smartlife-120300'
 select * from cfg_tablecolumncomment;
 select replace(REPLACE('[smartlife-120300]','[',''''),']','''')
                              insert into dbo.Cfg_ObjectColumnDetail (            DatabaseName,UserName,TableName,ColumnName,     SystemTypeIdDest,MaxLengthDest,IsNullDest,DefaultDefinitionDest,            SystemTypeIdSrc,MaxLengthSrc,IsNullSrc,DefaultDefinitionSrc,     ObjectType,ChangeType,VersionId,FunctionId     )  select  DB_NAME() as DatabaseName,user_name() as UserName, m.TableName,m.ColumnName,   m.SystemTypeId SystemTypeIdDest,m.MaxLength MaxLengthDest,          m.IsNullable  IsNullableDest,m.DefaultDefinition DefaultDefinitionDest,   n.SystemTypeId SystemTypeIdSrc,n.MaxLength MaxLengthSrc,          n.IsNullable IsNullableSrc,n.DefaultDefinition DefaultDefinitionDest,   'column' ObjectType,'alter' ChangeType,3 VersionId,2 FunctionId  from (          select b.Name  TableName, a.name ColumnName,          a.system_type_id   SystemTypeId,          a.max_length       MaxLength ,          a.is_nullable      IsNullable ,         ( select dc.definition from [smartlife-120300].sys.default_constraints  dc                   where dc.object_id=a.default_object_id                 and dc.parent_object_id=b.object_id) DefaultDefinition          from [smartlife-120300].sys.columns a         right join [smartlife-120300].sys.tables b              on b.object_id=a.object_id  and b.object_id>0       ) m,       (           select b.Name+a.name      ColumnName,           a.system_type_id   SystemTypeId,    a.max_length       MaxLength ,    a.is_nullable      IsNullable,                 ( select dc.definition from [smartlife-120302].sys.default_constraints  dc                    where dc.object_id=a.default_object_id                   and dc.parent_object_id=b.object_id) DefaultDefinition           from [smartlife-120302].sys.columns a           right join [smartlife-120302].sys.tables b               on b.object_id=a.object_id  and b.object_id>0         ) n      where m.TableName+m.ColumnName  =n.ColumnName        and m.TableName+m.ColumnName not in (                                          select e.TableName+e.ColumnName pri_key                                          from (                                                select b.Name  TableName,                                                 a.name ColumnName,a.system_type_id  SystemTypeId,a.max_length MaxLength ,a.is_nullable IsNullable,               ( select dc.definition from [smartlife-120300].sys.default_constraints  dc                                                     where dc.object_id=a.default_object_id                                                   and dc.parent_object_id=b.object_id) DefaultDefinition                                                from [smartlife-120300].sys.columns a right join [smartlife-120300].sys.tables b on b.object_id=a.object_id                                                 and b.object_id>0              ) e                                               inner join                                              (                                                 select b.Name  TableName,                                                 a.name ColumnName,a.system_type_id  SystemTypeId,a.max_length MaxLength ,a.is_nullable IsNullable ,               ( select dc.definition from [smartlife-120302].sys.default_constraints  dc                                                       where dc.object_id=a.default_object_id                                                     and dc.parent_object_id=b.object_id) DefaultDefinition                                                 from [smartlife-120302].sys.columns a                      right join [smartlife-120302].sys.tables b on b.object_id=a.object_id                                                  and b.object_id>0             ) f                                               on                                                 e.TableName=f.TableName and e.ColumnName=f.ColumnName and e.SystemTypeId=f.SystemTypeId               and e.MaxLength=f.MaxLength  and e.IsNullable=f.IsNullable              and isnull                                                                                insert into dbo.Cfg_ObjectColumnDetail (            DatabaseName,UserName,TableName,ColumnName,     SystemTypeIdDest,MaxLengthDest,IsNullDest,DefaultDefinitionDest,            SystemTypeIdSrc,MaxLengthSrc,IsNullSrc,DefaultDefinitionSrc,     ObjectType,ChangeType,VersionId,FunctionId     )  select  DB_NAME() as DatabaseName,user_name() as UserName, m.TableName,m.ColumnName,   m.SystemTypeId SystemTypeIdDest,m.MaxLength MaxLengthDest,          m.IsNullable  IsNullableDest,m.DefaultDefinition DefaultDefinitionDest,   n.SystemTypeId SystemTypeIdSrc,n.MaxLength MaxLengthSrc,          n.IsNullable IsNullableSrc,n.DefaultDefinition DefaultDefinitionDest,   'column' ObjectType,'alter' ChangeType,3 VersionId,2 FunctionId  from (          select b.Name  TableName, a.name ColumnName,          a.system_type_id   SystemTypeId,          a.max_length       MaxLength ,          a.is_nullable      IsNullable ,         ( select dc.definition from [smartlife-120300].sys.default_constraints  dc                   where dc.object_id=a.default_object_id                 and dc.parent_object_id=b.object_id) DefaultDefinition          from [smartlife-120300].sys.columns a         right join [smartlife-120300].sys.tables b              on b.object_id=a.object_id  and b.object_id>0       ) m,       (           select b.Name+a.name      ColumnName,           a.system_type_id   SystemTypeId,    a.max_length       MaxLength ,    a.is_nullable      IsNullable,                 ( select dc.definition from [smartlife-120302].sys.default_constraints  dc                    where dc.object_id=a.default_object_id                   and dc.parent_object_id=b.object_id) DefaultDefinition           from [smartlife-120302].sys.columns a           right join [smartlife-120302].sys.tables b               on b.object_id=a.object_id  and b.object_id>0         ) n      where m.TableName+m.ColumnName  =n.ColumnName        and m.TableName+m.ColumnName not in (                                          select e.TableName+e.ColumnName pri_key                                          from (                                                select b.Name  TableName,                                                 a.name ColumnName,a.system_type_id  SystemTypeId,a.max_length MaxLength ,a.is_nullable IsNullable,               ( select dc.definition from [smartlife-120300].sys.default_constraints  dc                                                     where dc.object_id=a.default_object_id                                                   and dc.parent_object_id=b.object_id) DefaultDefinition                                                from [smartlife-120300].sys.columns a right join [smartlife-120300].sys.tables b on b.object_id=a.object_id                                                 and b.object_id>0              ) e                                               inner join                                              (                                                 select b.Name  TableName,                                                 a.name ColumnName,a.system_type_id  SystemTypeId,a.max_length MaxLength ,a.is_nullable IsNullable ,               ( select dc.definition from [smartlife-120302].sys.default_constraints  dc                                                       where dc.object_id=a.default_object_id                                                     and dc.parent_object_id=b.object_id) DefaultDefinition                                                 from [smartlife-120302].sys.columns a                      right join [smartlife-120302].sys.tables b on b.object_id=a.object_id                                                  and b.object_id>0             ) f                                               on                                                 e.TableName=f.TableName and e.ColumnName=f.ColumnName and e.SystemTypeId=f.SystemTypeId               and e.MaxLength=f.MaxLength  and e.IsNullable=f.IsNullable              and isnull
                              
      select LEN('insert into dbo.Cfg_ObjectColumnDetail (            DatabaseName,UserName,TableName,ColumnName,     SystemTypeIdDest,MaxLengthDest,IsNullDest,DefaultDefinitionDest,            SystemTypeIdSrc,MaxLengthSrc,IsNullSrc,DefaultDefinitionSrc,     ObjectType,ChangeType,VersionId,FunctionId     )  select  DB_NAME() as DatabaseName,user_name() as UserName, m.TableName,m.ColumnName,   m.SystemTypeId SystemTypeIdDest,m.MaxLength MaxLengthDest,          m.IsNullable  IsNullableDest,m.DefaultDefinition DefaultDefinitionDest,   n.SystemTypeId SystemTypeIdSrc,n.MaxLength MaxLengthSrc,          n.IsNullable IsNullableSrc,n.DefaultDefinition DefaultDefinitionDest,   ''column'' ObjectType,''alter'' ChangeType,3 VersionId,2 FunctionId  from (    select b.Name  TableName, a.name ColumnName,          a.system_type_id   SystemTypeId,          a.max_length       MaxLength ,          a.is_nullable      IsNullable ,         ( select dc.definition from [smartlife-120300].sys.default_constraints  dc                   where dc.object_id=a.default_object_id                 and dc.parent_object_id=b.object_id) DefaultDefinition          from [smartlife-120300].sys.columns a         right join [smartlife-120300].sys.tables b              on b.object_id=a.object_id  and b.object_id>0       ) m,       (   select b.Name+a.name      ColumnName,           a.system_type_id   SystemTypeId,    a.max_length       MaxLength ,    a.is_nullable      IsNullable,                 ( select dc.definition from [smartlife-120302].sys.default_constraints  dc                    where dc.object_id=a.default_object_id                   and dc.parent_object_id=b.object_id) DefaultDefinition           from [smartlife-120302].sys.columns a           right join [smartlife-120302].sys.tables b               on b.object_id=a.object_id  and b.object_id>0         ) n      where m.TableName+m.ColumnName  =n.ColumnName        and m.TableName+m.ColumnName not in (select e.TableName+e.ColumnName pri_key                                           from (                                                select b.Name  TableName,                                                 a.name ColumnName,a.system_type_id  SystemTypeId,a.max_length MaxLength ,a.is_nullable IsNullable,               ( select dc.definition from [smartlife-120300].sys.default_constraints  dc                                                     where dc.object_id=a.default_object_id                                                   and dc.parent_object_id=b.object_id) DefaultDefinition                                                from [smartlife-120300].sys.columns a right join [smartlife-120300].sys.tables b on b.object_id=a.object_id                                                 and b.object_id>0              ) e                                               inner join                                              (                                                 select b.Name  TableName,                                                 a.name ColumnName,a.system_type_id  SystemTypeId,a.max_length MaxLength ,a.is_nullable IsNullable ,               ( select dc.definition from [smartlife-120302].sys.default_constraints  dc                                                       where dc.object_id=a.default_object_id                                                     and dc.parent_object_id=b.object_id) DefaultDefinition                                                 from [smartlife-120302].sys.columns a                      right join [smartlife-120302].sys.tables b on b.object_id=a.object_id                                                  and b.object_id>0             ) f                                             on                                           e.TableName=f.TableName and e.ColumnName=f.ColumnName and e.SystemTypeId=f.SystemTypeId               and e.MaxLength=f.MaxLength  and e.IsNullable=f.IsNullable              and isnull(e.DefaultDefinition,''9'')=isnull(f.DefaultDefinition,''9'') );')
      
       select ab.tablename+ab.columnname+ab.beforecomment  tcb
       from cfg_tablecolumncomment ab ,cfg_tablecolumncomment bb
       where ab.databasename='smartlife-120300' and bb.databasename='smartlife-120301'
        and ab.tablename=bb.tablename and ab.columnname=bb.columnname and ab.beforecomment=bb.beforecomment;
        
        
                                insert into Cfg_TableColumnComment (DatabaseName,UserName,TableName,ColumnName,                            
                                       BeforeComment,BeforeLength,BeforeModifyDate,Type)                      
                                         select 'remote_dbo.smartlife-120300' as DatabaseName,user_name() as UserName,               
                                                      (select name from remote_dbo.[smartlife-120300].sys.tables 
                                                      where object_id=a.major_id) tablename,                           
                                                       (select name from remote_dbo.[smartlife-120300].sys.columns 
                                                       where object_id=a.major_id and column_id=a.minor_id) columnname,                             cast(value as nvarchar(max))    BeforeComment,                             LEN(cast(value as nvarchar(3200))) BeforeLength,                             GETDATE() BeforModifyDate,                             a.minor_id Type                              
                                from remote_dbo.[smartlife-120300].sys.extended_properties a where a.name='MS_Description'; 
                                
                           alter table cfg_tablecolumncomment alter column     DatabaseName           nvarchar(60)     not null
                           
                         insert into Dey_Script (VersionId,objectName,UpdateScript,RecoverScript,UpdateValIdateScript,RecoverValIdateScript,Type)  
                             select 3 VersionId,
                        a.TableName objectName,
                       'EXEC sys.sp_addextendedproperty @name=''MS_Description'', @value='''+a.BeforeComment+''' , 
                       @level0type=''SCHEMA'',@level0name='''+a.UserName+''', 
                       @level1type=''TABLE'',@level1name='''+a.TableName+''', 
                       @level2type=''COLUMN'',@level2name='''+a.ColumnName +'''
                      GO'  UpdateScript,
		      '' RecoverScript,
                      'select 1' UpdateValIdateScript,
                      'select 1' RecoverValIdateScript,
                      'M' Type
                 from Cfg_TableColumnComment  a
		 where  a.Type<>0 and a.databasename='smartlife-120300'
                      and a.tablename+a.columnname+a.beforecomment not in(
                      select ab.tablename+ab.columnname+ab.beforecomment  tcb
                      from cfg_tablecolumncomment ab ,cfg_tablecolumncomment bb
                       where ab.databasename='smartlife-120300' and bb.databasename='smartlife-120301'
                      and ab.tablename=bb.tablename and ab.columnname=bb.columnname and ab.beforecomment=bb.beforecomment
                     );