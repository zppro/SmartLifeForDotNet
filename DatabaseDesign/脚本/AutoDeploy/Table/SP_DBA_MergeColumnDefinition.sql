USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_DBA_MergeColumnDefinition]    Script Date: 05/13/2014 18:12:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************************************************/
/* SP_DBA_MergeColumnDefinition */
/* 转化合成列定义的片段 */
/*  */
/*******************************************************************************/
CREATE procedure [dbo].[SP_DBA_MergeColumnDefinition]
@TableName  nvarchar(50)
as 
begin

    IF (EXISTS (select * from  temp_callservice where tablename=@Tablename))
             BEGIN
                       
              delete from  temp_callservice where tablename=@Tablename;
             END

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
             (--select * from Cfg_TableColumnDetailForCreate where tablename =@TableName
                 select * from Cfg_TableColumnDetailForCreate 
				 where tablename =@TableName and CONVERT(varchar(16),checkintime,120)=
				 (
						select max(CONVERT(varchar(16),checkintime,120)) 
						from Cfg_TableColumnDetailForCreate 
				 )
              ) a 
 end 
GO

