/**************************************************************************************/
/*  SP_Dey_CreateTypeScriptProc */
/*  生成创建类型的脚本  */
/*  @source_dbName 源数据库名，@dest_dbName 目的数据库名      */
/**************************************************************************************/  
alter   PROCEDURE [dbo].[SP_Dey_CreateTypeScriptProc]
@source_dbName nvarchar(100),
@dest_dbName   nvarchar(100)
 AS
 BEGIN
SET NOCOUNT ON
 
DECLARE  @table_name nvarchar(50),@ProcessTrackingTableName varchar(4000),@primarykey varchar(4000),@object_type_id  int
,@sql nvarchar(4000)
     PRINT '-------- make create type script --------'
     set @ProcessTrackingTableName=''
     set @primarykey=''
  exec SP_Cfg_GetObjectNameForCreate @source_dbName=@source_dbName,@dest_dbName=@dest_dbName,@type='type'

   IF (EXISTS (select * from  Dey_Script  where TYPE='Y' ))
             BEGIN
              delete from  Dey_Script  where TYPE='Y';
             END
DECLARE create_table_cursor CURSOR FOR 
select ObjectName tablename from Cfg_ObjectNameForCreate where TYPE='type';
--select name from remote_dbo.[smartlife-120300].sys.table_types  order by name desc ;
--select name from [$(source_db_name)].sys.objects where type='TT' order by name desc ;
     OPEN create_table_cursor
 
     FETCH NEXT FROM create_table_cursor 
     INTO  @table_name
 
     WHILE @@FETCH_STATUS = 0
      BEGIN
  
        
       --  set @sql='select @object_type_id=type_table_object_id from '+@source_dbName+'.sys.table_types where name='''+@table_name +''''
       --   exec sp_executesql @sql, N'@object_type_id int out', @object_type_id out 
         
         set @ProcessTrackingTableName=''
       /*  select @ProcessTrackingTableName =@ProcessTrackingTableName+ c.columnname+'' 
         from 
          ( 
                select b.name+'  '+(select a.name from remote_dbo.[smartlife-120300].sys.types  a where a.user_type_id=b.user_type_id)+
                       case  b.user_type_id when 167 then replace('('+CAST (b.max_length as nvarchar(32))+')','-1','max')
                        when 175 then '('+CAST (b.max_length as nvarchar(32))+')'
                         when 231 then  '('+replace(left(cast(b.max_length/2 as nvarchar(32)),1),'0','max')+
                         SUBSTRING(cast(b.max_length/2 as nvarchar(32)),2,LEN(cast(b.max_length/2 as nvarchar(32))))+')' else '' end +' '
                         +case b.is_nullable when 0 then ' not null' when 1 then ' null' else '' end +' ,' as columnname 
                          from remote_dbo.[smartlife-120300].sys.all_columns b where object_id=@object_type_id
           ) c*/
           
             exec SP_DBA_GetTypeColumnDetailForCreate @databasename='[Smartlife-120300]',@TypeName='JSONHierarchy'
             exec SP_DBA_MergeTypeColumnDefinition @TypeName='JSONHierarchy'
            set @ProcessTrackingTableName=dbo.Func_Tol_ContactColumn('JSONHierarchy')

                 set @primarykey=''
            if (EXISTS(   select * from  Cfg_ObjectPrimaryKey where TYPE=255 and ObjectName='JSONHierarchy'))
           begin
          
				   set @sql='select @count=''CONSTRAINT  PK_'+@table_name+'  PRIMARY KEY CLUSTERED  (''+columnname+''),'' from Cfg_ObjectPrimaryKey
					   where ObjectName='''+@table_name +''''
						 print @sql
					   exec sp_executesql @sql, N'@count varchar(4000) out', @primarykey out 
            end  
            /* 
           if (EXISTS ( select  m.object_id,m.schema_id,m.parent_object_id,ac.name+',' columnname
                        from    remote_dbo.[smartlife-120300].sys.key_constraints m ,
                                remote_dbo.[smartlife-120300].sys.index_columns ic,remote_dbo.[smartlife-120300].sys.all_columns ac
                        where   m.type='PK' and m.parent_object_id=
                        (select type_table_object_id from remote_dbo.[smartlife-120300].sys.table_types 
                          where name=@table_name 
                         )
                                and m.parent_object_id=ic.object_id and m.parent_object_id=ac.object_id and ic.index_column_id=ac.column_id 
                       )
               )
               BEGIN
                         select @primarykey=@primarykey+ e.columnname+' '
                         from ( 
                                select m.object_id,m.schema_id,m.parent_object_id,ac.name+',' columnname
                                from   remote_dbo.[smartlife-120300].sys.key_constraints m ,
                                       remote_dbo.[smartlife-120300].sys.index_columns ic,remote_dbo.[smartlife-120300].sys.all_columns ac
                                where  m.type='PK' and m.parent_object_id=(select type_table_object_id from remote_dbo.[smartlife-120300].sys.table_types where name=@table_name )
                                       and m.parent_object_id=ic.object_id and m.parent_object_id=ac.object_id and ic.index_column_id=ac.column_id 
                               ) e

                          set @primarykey='  PRIMARY KEY CLUSTERED  ('+left(@primarykey,LEN(@primarykey)-1)+'),'
               END*/

               set @ProcessTrackingTableName =@ProcessTrackingTableName+isnull(@primarykey,'')

            IF (NOT EXISTS (select * from Cfg_Object where ChangeType='create' and TableName=@table_name))
                      insert into Dey_Script (VersionId,ObjectName,UpdateScript,RecoverScript,UpdateValidateScript,RecoverValidateScript,type) 
                            select 3  version_id,@table_name  object_name,
                              '
                             if (not exists(select * from sys.table_types where name='''+@table_name+'''))
                                    begin  
                                           PRINT ''''
                                           PRINT ''Creating type '+@table_name+' ...''
                                            create type  [dbo].'+@table_name+' as table ('+left(@ProcessTrackingTableName,LEN(@ProcessTrackingTableName)-1)+');
                                     end
                             go'  update_script,
                            'drop type [dbo].'+@table_name   recover_script,
                            'select COUNT(object_id) from sys.table_types where name='''+@table_name+''';  
                             go'   update_validate_script,
                           'select COUNT(object_id) from sys.table_types where name='''+@table_name+''';  
                            go'   recover_validate_script,'Y' type
 
       
             FETCH NEXT FROM create_table_cursor 
             INTO  @table_name
         END 
        CLOSE create_table_cursor
     DEALLOCATE create_table_cursor
 END
