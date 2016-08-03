/*******************************************************************************/
/* SP_DBA_UpdateTableColumnRule2 */
/* 更新表的字段的数据，以规则二进行映射 */
/*  */
/*******************************************************************************/
create  procedure  [dbo].[SP_DBA_UpdateTableColumnRule2]
@SrcTableName               varchar(100),
@SrcTableColumnName         varchar(100),
@SrcTableJoinColumnName     varchar(100),
@UpdateTableName            varchar(100),
@UpdateTableColumnName      varchar(100),
@UpdateTableJoinColumnName  varchar(100)
as
declare @sqlStr nvarchar(4000)
begin

 set @sqlStr=' update dbo.'+@UpdateTableName+'
set '+@UpdateTableColumnName+'=(case ISNULL(b.f6,''0'') when ''0'' then 0 else 1 end)
from dbo.'+@UpdateTableName+' a,dbo.'+@SrcTableName+' b
where a.idno=b.f4;'
exec sp_executesql @sqlStr
end

exec SP_DBA_UpdateTableColumnRule2 @Srctablename='' ,@SrcTableColumnName='',
     @SrcTableJoinColumnName='',@UpdateTableName='',@UpdateTableColumnName='',
     @UpdateTableJoinColumnName='' HealthInsuranceFlag

select * from dbo.cfg_bridge;
select * from Dey_Script;

/*******************************************************************************/
/* SP_DBA_GetTableColumnDetailForCreate */
/* 取表的元数据 */
/*  */
/*******************************************************************************/
alter procedure SP_DBA_GetTableColumnDetailForCreate
@DatabaseName nvarchar(60),
@TableName  nvarchar(50)
as 
declare @sqlStr nvarchar(4000)
begin
      set @sqlStr='   insert into Cfg_TableColumnDetailForCreate (DatabaseName,UserName,TableName,ColumnName,SystemTypeIdDest,MaxLengthDest,
         IsNullDest,IdentityDef,ChangeType,CheckInTime)
         select '''+@DatabaseName+''' databasename,b.TABLE_SCHEMA,b.tablename,b.column_name,b.DATA_TYPE,
         b.CHARACTER_OCTET_LENGTH,b.IS_NULLABLE,b.identitycolumn,''create'' changetype,GETDATE() checkintime
        from 
        ( 
           select a.ordinal_position,a.table_schema,a.table_name as tablename,a.column_name,
           a.data_type,a.character_octet_length,a.is_nullable,d.column_name identitycolumn
          from (select * from '+@DatabaseName+'.INFORMATION_SCHEMA.COLUMNS where table_name ='''+@TableName+'''
         ) a 
        left join (
          
	       select user_name() constraint_schema,a.name table_name,b.name column_name
              from '+@DatabaseName+'.sys.tables  a,
	           '+@DatabaseName+'.sys.identity_columns b
               where a.object_id =b.object_id
          )d
         on  a.TABLE_SCHEMA = d.constraint_schema   and a.column_name = d.column_name and a.TABLE_NAME=d.TABLE_NAME) b
         '
        exec sp_executesql @sqlStr
end 

exec SP_DBA_GetTableColumnDetailForCreate @databasename='[Smartlife-120303]' ,@tablename='Oca_CC_Ext'

select * 
from  Cfg_TableColumnDetailForCreate;


/*******************************************************************************/
/* SP_DBA_MergeColumnDefinition */
/* 转化合成列定义的片段 */
/* 指定需要处理的表：参数 @tablename */
/* 前提条件：Cfg_TableColumnDetailForCreate   结果保存：temp_callservice  */
/*******************************************************************************/
create procedure SP_DBA_MergeColumnDefinition
@TableName  nvarchar(50)
as 
begin
          insert  into temp_callservice
          select a.tablename as tablename,a.columnname+' '+a.SystemTypeIdDest+
           case a.SystemTypeIdDest when 'varchar' then replace('('+CAST (a.MaxLengthDest as nvarchar(32))+')','-1','max')
	                    when 'varbinary' then replace('('+CAST (a.MaxLengthDest as nvarchar(32))+')','-1','max')
                            when 'char' then '('+CAST (a.MaxLengthDest as nvarchar(32))+')'
                            when 'nvarchar' then  '('+replace(left(cast(a.MaxLengthDest/2 as nvarchar(32)),1),'0','max')+
           SUBSTRING(cast(a.MaxLengthDest/2 as nvarchar(32)),2,LEN(cast(a.MaxLengthDest/2 as nvarchar(32))))+')' else '' end +
          ' ' +case a.IsNullDest when 'NO' then ' not null' when 'YES' then ' null' else '' end +
          ' '+case  when a.IdentityDef IS NULL then '' 
          else '  identity (1,1) ' end +',' 
          as columname 
          from 
             (select * from Cfg_TableColumnDetailForCreate where tablename =@TableName
              ) a 
 end        
 
exec   SP_DBA_MergeColumnDefinition @tablename='Oca_CC_Ext'
select * 
from temp_callservice 
where tablename='Oca_CC_Ext'