exec SP_Dey_CreateTableScriptProc @source_dbName='[Smartlife-120300]' ,@dest_dbName='[Smartlife-120303]'

select *
from Cfg_ObjectNameForCreate where TYPE='table';

select * from Cfg_TableColumnDetailForCreate 
 where CONVERT(varchar(16),checkintime,120)=
 (
		select max(CONVERT(varchar(16),checkintime,120)) 
		from Cfg_TableColumnDetailForCreate 
)
--where 
group by CONVER (varchar(17),checkintime,120);

 select *
 from temp_callservice where tablename='t_deploy_object_column_detail';
 
 select * 
 from Cfg_ObjectPrimaryKey;
 
 select * 
 from  Dey_Script
 where TYPE='V';
 
 delete from  Dey_Script
 where TYPE='T';
 declare @sql nvarchar(2000) ,  @ProcessTrackingTableName varchar(4000)
        set @ProcessTrackingTableName=''
   select --dbo.joinStr(columname ),
   tablename,columname
                   from temp_callservice
                   where tablename='t_deploy_object' 
                   group by tablename
     select  @ProcessTrackingTableName             
                    @TableName;
 
                IF (OBJECT_ID(N'dbo.t_deploy_object', 'U') IS NULL)                BEGIN                     PRINT ''                     PRINT 'Creating table t_deploy_object ...'                     create table [dbo].t_deploy_object (id int  not null   identity (1,1) ,database_name nvarchar(20)  not null ,user_name nvarchar(30)  not null ,table_name nvarchar(60)  not null ,object_type nvarchar(20)  not null ,change_type nvarchar(20)  not null ,version_id int  not null ,function_id int  not null ,id int  not null   identity (1,1) ,database_name nvarchar(20)  not null ,user_name nvarchar(30)  not null ,table_name nvarchar(60)  not null ,object_type nvarchar(20)  not null ,change_type nvarchar(20)  not null ,version_id int  not null ,function_id int  not null ,id int  not null   identity (1,1) ,database_name nvarchar(20)  not null ,user_name nvarchar(30)  not null ,table_name nvarchar(60)  not null ,object_type nvarchar(20)  not null ,change_type nvarchar(20)  not null ,version_id int  not null ,function_id int  not null ,id int  not null   identity (1,1) ,database_name nvarchar(20)  not null ,user_name nvarchar(30)  not null ,table_name nvarchar(60)  not null ,object_type nvarchar(20)  not null ,change_type nvarchar(20)  not null ,version_id int  not null ,function_id int  not null ,id int  not null   identity (1,1) ,database_name nvarchar(20)  not null ,user_name nvarchar(30)  not null ,table_name nvarchar(60)  not null ,object_type nvarchar(20)  not null ,change_type nvarchar(20)  not null ,version_id int  not null ,function_id int  not null ,id int  not null   identity (1,1) ,database_name nvarchar(20)  not null ,user_name nvarchar(30)  not null ,table_name nvarchar(60)  not null ,object_type nvarchar(20)  not null ,change_type nvarchar(20)  not null ,version_id int  not null ,function_id int  not null ,id int  not null   identity (1,1) ,database_name nvarchar(20)  not null ,user_name nvarchar(30)  not null ,table_name nvarchar(60)  not null ,object_type nvarchar(20)  not null ,change_type nvarchar(20)  not null ,version_id int  not null ,function_id int  not null ,id int  not null   identity (1,1) ,database_name nvarchar(20)  not null ,user_name nvarchar(30)  not null ,table_name nvarchar(60)  not null ,object_type nvarchar(20)  not null ,change_type nvarchar(20)  not null ,version_id int  not null ,function_id int  not null ,id int  not null   identity (1,1) ,database_name nvarchar(20)  not null ,user_name nvarchar(30)  not null ,table_name nvarchar(60)  not null ,object_type nvarchar(20)  not null ,change_type nvarchar(20)  not null ,version_id int  not null ,function_id int  not null ,id int  not null   identity (1,1) ,database_name nvarchar(20)  not null ,user_name nvarchar(30)  not null ,table_name nvarchar(60)  not null ,object_type nvarchar(20)  not null ,change_type nvarchar(20)  not null ,version_id int  not null ,function_id int  not null ,id int  not null   identity (1,1) ,database_name nvarchar(20)  not null ,user_name nvarchar(30)  not null ,table_name nvarchar(60)  not null ,object_type nvarchar(20)  not null ,change_type nvarchar(20)  not null ,version_id int  not null ,function_id int  not null ,id int  not null   identity (1,1) ,database_name nvarchar(20)  not null ,user_name nvarchar(30)  not null ,table_name nvarchar(60)  not null ,object_type nvarchar(20)  not null ,change_type nvarchar(20)  not null ,version_id int  not null ,function_id int  not null ,id int  not null   identity (1,1) ,database_name nvarchar(20)  not null ,user_name nvarchar(30)  not null ,table_name nvarchar(60)  not null ,object_type nvarchar(20)  not null ,change_type nvarchar(20)  not null ,version_id int  not null ,function_id int  not null ,id int  not null   identity (1,1) ,database_name nvarchar(20)  not null ,user_name nvarchar(30)  not null ,table_name nvarchar(60)  not null ,object_type nvarchar(20)  not null ,change_type nvarchar(20)  not null ,version_id int  not null ,function_id int  not null ,id int  not null   identi
                
                                IF (OBJECT_ID(N'dbo.t_deploy_orderformat', 'U') IS NULL)                BEGIN                     PRINT ''                     PRINT 'Creating table t_deploy_orderformat ...'                     create table [dbo].t_deploy_orderformat (id int  not null   identity (1,1) ,function_id int  not null ,function_name nvarchar(100)  null ,order_time datetime  null ,owner_person nvarchar(20)  null ,response_person nvarchar(20)  null ,do_person nvarchar(20)  null ,start_time datetime  null ,finish_time datetime  null ,level int  null ,name nvarchar(30)  null ,content nvarchar(100)  null ,id int  not null   identity (1,1) ,function_id int  not null ,function_name nvarchar(100)  null ,order_time datetime  null ,owner_person nvarchar(20)  null ,response_person nvarchar(20)  null ,do_person nvarchar(20)  null ,start_time datetime  null ,finish_time datetime  null ,level int  null ,name nvarchar(30)  null ,content nvarchar(100)  null ,id int  not null   identity (1,1) ,function_id int  not null ,function_name nvarchar(100)  null ,order_time datetime  null ,owner_person nvarchar(20)  null ,response_person nvarchar(20)  null ,do_person nvarchar(20)  null ,start_time datetime  null ,finish_time datetime  null ,level int  null ,name nvarchar(30)  null ,content nvarchar(100)  null ,id int  not null   identity (1,1) ,function_id int  not null ,function_name nvarchar(100)  null ,order_time datetime  null ,owner_person nvarchar(20)  null ,response_person nvarchar(20)  null ,do_person nvarchar(20)  null ,start_time datetime  null ,finish_time datetime  null ,level int  null ,name nvarchar(30)  null ,content nvarchar(100)  null ,id int  not null   identity (1,1) ,function_id int  not null ,function_name nvarchar(100)  null ,order_time datetime  null ,owner_person nvarchar(20)  null ,response_person nvarchar(20)  null ,do_person nvarchar(20)  null ,start_time datetime  null ,finish_time datetime  null ,level int  null ,name nvarchar(30)  null ,content nvarchar(100)  null ,id int  not null   identity (1,1) ,function_id int  not null ,function_name nvarchar(100)  null ,order_time datetime  null ,owner_person nvarchar(20)  null ,response_person nvarchar(20)  null ,do_person nvarchar(20)  null ,start_time datetime  null ,finish_time datetime  null ,level int  null ,name nvarchar(30)  null ,content nvarchar(100)  null ,id int  not null   identity (1,1) ,function_id int  not null ,function_name nvarchar(100)  null ,order_time datetime  null ,owner_person nvarchar(20)  null ,response_person nvarchar(20)  null ,do_person nvarchar(20)  null ,start_time datetime  null ,finish_time datetime  null ,level int  null ,name nvarchar(30)  null ,content nvarchar(100)  null ,id int  not null   identity (1,1) ,function_id int  not null ,function_name nvarchar(100)  null ,order_time datetime  null ,owner_person nvarchar(20)  null ,response_person nvarchar(20)  null ,do_person nvarchar(20)  null ,start_time datetime  null ,finish_time datetime  null ,level int  null ,name nvarchar(30)  null ,content nvarchar(100)  null ,id int  not null   identity (1,1) ,function_id int  not null ,function_name nvarchar(100)  null ,order_time datetime  null ,owner_person nvarchar(20)  null ,response_person nvarchar(20)  null ,do_person nvarchar(20)  null ,start_time datetime  null ,finish_time datetime  null ,level int  null ,name nvarchar(30)  null ,content nvarchar(100)  null ,id int  not null   identity (1,1) ,function_id int  not null ,function_name nvarchar(100)  null ,order_time datetime  null ,owner_person nvarchar(20)  null ,response_person nvarchar(20)  null ,do_person nvarchar(20)  null ,start_time datetime  null ,finish_time datetime  null ,level int  null ,name nvarchar(30)  null ,content nvarchar(100)  null ,id int  not null   identity (1,1) ,function_id int  not null ,function_name nvarchar(100)  null ,order_time datetime  null ,owner_person nvarchar(20)  null ,response_person nvarchar(20)  null ,do_person nvarchar(20)  null ,start_time datetime  null ,finish_t
declare @sql nvarchar(2000) 
declare @primarykey varchar(1000)
declare @table_name varchar(20) 
set @table_name='sysdiagrams' 
set @sql='select @count=''CONSTRAINT  PK_'+@table_name+'  PRIMARY KEY CLUSTERED  (''+columnname+''),'' from Cfg_ObjectPrimaryKey
               where ObjectName='''+@table_name +''''
--  print @sql
exec sp_executesql @sql, N'@count varchar(1000) out', @primarykey out 
--select @count=columnname from Cfg_ObjectPrimaryKey where ObjectName='sysdiagrams'
       
       select DB_NAME() as DatabaseName,user_name() as UserName,m.name OjbectName,
                m.definition  BeforeDefinition,m.length BeforeLength,m.modify_date BeforeModifyDate,
                 isnull(n.definition,-1)  AfterDefinition ,isnull(n.length,-1) AfterLength ,
                 isnull(n.modify_date,GETDATE()) AfterModifyDate  ,'V' type
                from     
                (select a.name,b.definition,LEN(b.definition) length,a.modify_date
                from [Smartlife-120300].sys.all_objects a,[Smartlife-120300].sys.all_sql_modules b 
                where a.Type in ('V') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>40)m
                left join 
                (select a.name,b.definition,LEN(b.definition) length,a.modify_date
                from [Smartlife-120301].sys.all_objects a,[Smartlife-120301].sys.all_sql_modules b 
                where a.Type in ('V') and a.object_id>0 and a.object_id=b.object_id and  len(b.definition)>40) n
                on m.name=n.name
                
               
               select * from temp_mobile;
               
            exec   SP_DBA_ImportOldManInfoOnlyMobile
            
            select top 400 * from Oca_OldManBaseInfo order by CheckInTime desc;
             select top 400 * from Oca_OldManConfigInfo order by CheckInTime desc;
         
             
           SELECT SO.NAME object_name,
            sc.name,SM.TEXT
          FROM remote_dbo.[smartlife-120300].dbo.sysobjects SO INNER JOIN remote_dbo.[smartlife-120300].dbo.syscolumns SC ON SO.id = SC.id   
            inner JOIN remote_dbo.[smartlife-120300].dbo.syscomments SM ON SC.cdefault = SM.id   
         WHERE SO.xtype = 'U' -- and SO.name in (select objectname from dey_script where type='T')
            and SO.NAME not in(select TableName from Cfg_Object where ObjectType='table' and ChangeType='not')
        ORDER BY SO.[name], SC.colid 
                   
                   
                   -----------------------------------------------------------
                                 select '[Smartlife-120300]'  as  databasename,(select name from sys.schemas where SCHEMA_ID= m.schema_id) userName,
                           isnull((select name from [Smartlife-120300].sys.tables where object_id=m.parent_object_id ),
                          (select name from [Smartlife-120300].sys.table_types where parent_object_id=m.parent_object_id)
                           ) objectname,
                           dbo.joinStr(ac.name) columnname
                           , isnull((select object_id%10 from [Smartlife-120300].sys.tables where object_id=m.parent_object_id ),255) type
                        from    [Smartlife-120300].sys.key_constraints m ,
                                [Smartlife-120300].sys.index_columns ic,
                                [Smartlife-120300].sys.all_columns ac
                        where   m.type='PK' 
                                and m.parent_object_id=ic.object_id --and m.parent_object_id='217767833'
                                and m.parent_object_id=ac.object_id and m.unique_index_id=ic.index_id
                                and ic.column_id=ac.column_id 
                                group by m.schema_id,m.parent_object_id;
                                
                                select object_id  
                                from [SmartLife-120300].sys.tables 
                                where name='sysdiagrams'
                                select * from [Smartlife-120300].sys.key_constraints m where m.parent_object_id='217767833' 
                                unique_index_id
                                
                                265768004,281768061
                                
                                select *index_id from [Smartlife-120300].sys.index_columns where object_id='217767833';
                                
                                exec SP_Cfg_GetObjectPrimaryKey @source_dbName='[Smartlife-120300]'
 
 
  exec SP_Dey_CreateTableScriptProc @source_dbName='[Smartlife-120300]' ,@dest_dbName='[Smartlife-120303]'
		 
		 exec SP_Cfg_GetColumnDetailForAddColumn @source_dbName='[Smartlife-120300]' ,@dest_dbName='[Smartlife-120303]'
		 exec SP_Dey_AddColumnScript

		 exec SP_Cfg_GetColumnDetailForAlterColumn @source_dbName='[Smartlife-120300]' ,@dest_dbName='[Smartlife-120303]'
		 exec SP_Dey_AlterColumnScript
		 
		 exec SP_Cfg_GetTableColumnComment @source_dbName='[Smartlife-120300]'
		 exec SP_Cfg_GetTableColumnComment @source_dbName='[Smartlife-120303]'
		 exec SP_Dey_AddTableColumnCommentScript @source_dbName='[Smartlife-120300]' ,@dest_dbName='[Smartlife-120303]'
/**************************************************************************************/
/*  SP_Dey_CreateTableScriptMain */
/*  生成创建表的脚本 入口调用程序 */
/*  @source_dbName 源数据库名，@dest_dbName 目的数据库名      */
/**************************************************************************************/      
alter procedure SP_Dey_CreateTableScriptMain   
@source_dbName nvarchar(100),
@dest_dbName   nvarchar(100),
@log_Information nvarchar(max) output 
as   
begin         
         set @log_Information=''                 
	     exec SP_Dey_CreateTableScriptProc @source_dbName=@source_dbName ,@dest_dbName=@dest_dbName
		 print 'step1 success'
		 set @log_Information=@log_Information+'SP_Dey_CreateTableScriptProc 执行成功'+'@';
		 exec SP_Cfg_GetColumnDetailForAddColumn @source_dbName=@source_dbName ,@dest_dbName=@dest_dbName
		 set @log_Information=@log_Information+'SP_Cfg_GetColumnDetailForAddColumn 执行成功'+'@';
		 exec SP_Dey_AddColumnScript
		 set @log_Information=@log_Information+'SP_Dey_AddColumnScript 执行成功'+'@';
		 

		 exec SP_Cfg_GetColumnDetailForAlterColumn @source_dbName=@source_dbName ,@dest_dbName=@dest_dbName
		 set @log_Information=@log_Information+'SP_Cfg_GetColumnDetailForAlterColumn 执行成功'+'@';
		 
		 exec SP_Dey_AlterColumnScript
		 set @log_Information=@log_Information+'SP_Dey_AlterColumnScript 执行成功'+'@';
		 
		 exec SP_Cfg_GetTableColumnComment @source_dbName=@source_dbName
		 set @log_Information=@log_Information+'SP_Cfg_GetTableColumnComment1 执行成功'+'@';
		 
		 exec SP_Cfg_GetTableColumnComment @source_dbName=@dest_dbName
		  set @log_Information=@log_Information+'SP_Cfg_GetTableColumnComment2 执行成功'+'@';
		 
		 exec SP_Dey_AddTableColumnCommentScript @source_dbName=@source_dbName ,@dest_dbName=@dest_dbName
		  set @log_Information=@log_Information+'SP_Dey_AddTableColumnCommentScript 执行成功'+'@';
		 
		 exec SP_Dey_AlterColumnSetDefaultScript
		  set @log_Information=@log_Information+'SP_Dey_AlterColumnSetDefaultScript 执行成功'+'@';	
 end
 
 
   exec SP_Dey_CreateTableScriptMain @source_dbName='[Smartlife-120300]' ,@dest_dbName='[Smartlife-120303]'
   
   exec SP_Cfg_GetViewDefinition    @source_dbName='[Smartlife-120300]' ,@dest_dbName='[Smartlife-120303]'
   exec SP_Dey_CreateViewScriptProc 
   
    select BeforeDefinition,LEN(beforedefinition) 
    from   cfg_objectversionhistory a
  where type='V'
      ------------------------------------=======================120304          
                select * from  Pub_ServiceStation;  
                select * from Sys_DictionaryItem where ItemName='拱墅区'    
                select * from Pub_Area;        