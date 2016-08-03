
        select  b.columname+'' 
        from 
        ( 
           select a.ordinal_position,a.table_schema+'.'+a.table_name as tablename,a.column_name+' '+a.data_type+
           case a.DATA_TYPE when 'varchar' then replace('('+CAST (a.character_octet_length as nvarchar(32))+')','-1','max')
	                    when 'varbinary' then replace('('+CAST (a.character_octet_length as nvarchar(32))+')','-1','max')
                            when 'char' then '('+CAST (a.character_octet_length as nvarchar(32))+')'
                            when 'nvarchar' then  '('+replace(left(cast(a.CHARACTER_OCTET_LENGTH/2 as nvarchar(32)),1),'0','max')+
           SUBSTRING(cast(a.CHARACTER_OCTET_LENGTH/2 as nvarchar(32)),2,LEN(cast(a.CHARACTER_OCTET_LENGTH/2 as nvarchar(32))))+')' else '' end +
          ' ' +case a.is_nullable when 'NO' then ' not null' when 'YES' then ' null' else '' end +' '+case  when d.COLUMN_NAME IS NULL then '' 
          else '  identity (1,1) ' end +',' as columname
          from (select * from [smartlife-120300].INFORMATION_SCHEMA.COLUMNS where table_name ='Oca_CallService'
         ) a 
        left join (
             -- select constraint_schema, table_name,column_name from $(source_db_name).INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
             -- where upper(constraint_name) like 'PK%' 

	       select user_name() constraint_schema,a.name table_name,b.name column_name
              from [smartlife-120300].sys.tables  a,
	           [smartlife-120300].sys.identity_columns b
               where a.object_id =b.object_id
          )d
         on  a.TABLE_SCHEMA = d.constraint_schema   and a.column_name = d.column_name and a.TABLE_NAME=d.TABLE_NAME) b
         
         -------------------------------------------
         create table Cfg_TableColumnDetailForCreate
(
Id             int Identity(1,1)  not null,
DatabaseName  nvarchar(60)        null,
UserName      nvarchar(30)       not null,
TableName     nvarchar(60)       not null,
ColumnName        varchar(60)        null,
SystemTypeIdDest  varchar(30)        null,
MaxLengthDest     int                null,
IsNullDest        varchar(10)        null,
DefaultDefinitionDest  nvarchar(max) null,
IdentityDef     varchar(10)          null,
ChangeType    nvarchar(20)       not null,
VersionId     int                 null,
FunctionId    int                 null,
CheckInTime   datetime           default (getdate()) not null
)

create procedure SP_DBA_GetTableColumnDetailForCreate
@DatabaseName varchar(60),
@TableName  varchar(50)
as 
begin
         insert into Cfg_TableColumnDetailForCreate (DatabaseName,UserName,TableName,ColumnName,SystemTypeIdDest,MaxLengthDest,
         IsNullDest,IdentityDef,ChangeType,CheckInTime)
         select 'rmo.[smartlife-120300]' databasename,b.TABLE_SCHEMA,b.tablename,b.column_name,b.DATA_TYPE,
         b.CHARACTER_OCTET_LENGTH,b.IS_NULLABLE,b.identitycolumn,'create' changetype,GETDATE() checkintime
        from 
        ( 
           select a.ordinal_position,a.table_schema,a.table_name as tablename,a.column_name,
           a.data_type,a.character_octet_length,a.is_nullable,d.column_name identitycolumn
          from (select * from [smartlife-120300].INFORMATION_SCHEMA.COLUMNS where table_name ='Oca_CallService'
         ) a 
        left join (
          
	       select user_name() constraint_schema,a.name table_name,b.name column_name
              from [smartlife-120300].sys.tables  a,
	           [smartlife-120300].sys.identity_columns b
               where a.object_id =b.object_id
          )d
         on  a.TABLE_SCHEMA = d.constraint_schema   and a.column_name = d.column_name and a.TABLE_NAME=d.TABLE_NAME) b
end         
         
         
         select   dbo.joinStr(name)
from    sys.all_columns 
where  object_id   in 
            (   select  object_id 
                from    sys.tables 
               where  name='Cfg_TableColumnDetailForCreate'
             )

alter table Cfg_TableColumnDetailForCreate alter column DatabaseName  nvarchar(60)        null;

select * from Cfg_TableColumnDetailForCreate;


    select a.tablename as tablename,a.columnname+' '+a.SystemTypeIdDest+
           case a.SystemTypeIdDest when 'varchar' then replace('('+CAST (a.MaxLengthDest as nvarchar(32))+')','-1','max')
	                    when 'varbinary' then replace('('+CAST (a.MaxLengthDest as nvarchar(32))+')','-1','max')
                            when 'char' then '('+CAST (a.MaxLengthDest as nvarchar(32))+')'
                            when 'nvarchar' then  '('+replace(left(cast(a.MaxLengthDest/2 as nvarchar(32)),1),'0','max')+
           SUBSTRING(cast(a.MaxLengthDest/2 as nvarchar(32)),2,LEN(cast(a.MaxLengthDest/2 as nvarchar(32))))+')' else '' end +
          ' ' +case a.IsNullDest when 'NO' then ' not null' when 'YES' then ' null' else '' end +
          ' '+case  when a.IdentityDef IS NULL then '' 
          else '  identity (1,1) ' end +',' 
          as columname into temp_callservice
          from (select * from Cfg_TableColumnDetailForCreate where tablename ='Oca_CallService'
         ) a 
         
         select * from temp_callservice;
         
         DECLARE  @ProcessTrackingTableName varchar(4000)
         set @ProcessTrackingTableName=''
         select @ProcessTrackingTableName =@ProcessTrackingTableName+ b.columname+'' 
        from temp_callservice b 
        where tablename='Oca_CallService' 
        select @ProcessTrackingTableName
        
        
        CallServiceId uniqueidentifier  not null ,Id int  not null   identity (1,1) ,CheckInTime datetime  not null ,
        Status tinyint  not null ,OperatedBy uniqueidentifier  null ,OperatedOn datetime  null ,DataSource char(5)  null ,
        AreaId char(5)  null ,AreaId2 varchar(40)  null ,AreaId3 varchar(40)  null ,Content nvarchar(500)  null ,
        LongitudeS varchar(20)  null ,LatitudeS varchar(20)  null ,CallSeconds int  null ,DoStatus tinyint  not null ,
        DoResults nvarchar(1000)  null ,OldManId uniqueidentifier  not null ,ServiceQueueId uniqueidentifier  not null ,
        ServiceQueueNo varchar(30)  not null ,ServiceExtId uniqueidentifier  null ,ServiceExtNo varchar(20)  null ,
        ServiceCatalog char(5)  not null ,AbandonFlag tinyint  not null ,DialBackCount int  not null ,
        AbandonOn datetime  null ,LastDialBackOn datetime  null ,ServiceQueueIdOld uniqueidentifier  null ,
        ServiceQueueNoOld varchar(30)  null ,