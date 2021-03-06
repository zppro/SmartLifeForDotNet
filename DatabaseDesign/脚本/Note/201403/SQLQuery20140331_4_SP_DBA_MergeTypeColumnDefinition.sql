/*******************************************************************************/
/* SP_DBA_MergeTypeColumnDefinition */
/* 转化合成 表类型的列定义的片段 */
/*  */
/*******************************************************************************/
create procedure [dbo].[SP_DBA_MergeTypeColumnDefinition]
@TypeName  nvarchar(50)
as 
begin

    IF (EXISTS (select * from  temp_callservice where tablename=@TypeName))
             BEGIN
                       
              delete from  temp_callservice where tablename=@TypeName;
             END

          insert  into temp_callservice
        select a.tablename as tablename,a.columnname+' '+a.SystemTypeIdDest+
           case a.SystemTypeIdDest when 'varchar' then replace('('+CAST (a.MaxLengthDest as nvarchar(32))+')','-1','max')
	                    when 'varbinary' then replace('('+CAST (a.MaxLengthDest as nvarchar(32))+')','-1','max')
                            when 'char' then '('+CAST (a.MaxLengthDest as nvarchar(32))+')'
                            when 'nvarchar' then  '('+replace(left(cast(a.MaxLengthDest/2 as nvarchar(32)),1),'0','max')+
           SUBSTRING(cast(a.MaxLengthDest/2 as nvarchar(32)),2,LEN(cast(a.MaxLengthDest/2 as nvarchar(32))))+')' else '' end +
          ' ' +case a.IsNullDest when 0 then ' not null' when 1 then ' null' else '' end 
         +',' 
          as columname 
          from 
             (--select * from Cfg_TableColumnDetailForCreate where tablename =@TableName
                 select * from Cfg_TypeColumnDetailForCreate 
				 where tablename =@TypeName and CONVERT(varchar(16),checkintime,120)=
				 (
						select max(CONVERT(varchar(16),checkintime,120)) 
						from Cfg_TypeColumnDetailForCreate 
				 )
              ) a 
              
     end         
     -------------------------------------
     
    select  dbo.Func_Tol_ContactColumn('JSONHierarchy')
    
   select * from  Cfg_ObjectPrimaryKey where TYPE=255 and ObjectName='JSONHierarchy'