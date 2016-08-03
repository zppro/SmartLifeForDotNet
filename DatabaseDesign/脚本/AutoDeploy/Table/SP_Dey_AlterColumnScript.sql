USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Dey_AlterColumnScript]    Script Date: 05/13/2014 18:14:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/***************************************************************************/
/* SP_Dey_AlterColumnScript                                                  */
/* 生成修改表的字段的脚本存入数据库,包括修改字段的类型，长度，是否为空。   */
/***************************************************************************/


CREATE    PROCEDURE [dbo].[SP_Dey_AlterColumnScript]
 AS
 BEGIN
      SET NOCOUNT ON
     
         IF (EXISTS (select * from Dey_Script where Type='N'))
             BEGIN
               delete from Dey_Script where Type='N';        
             END
        IF (NOT EXISTS (select * from Dey_Script where Type='N'))
             BEGIN
                  insert into Dey_Script (VersionId,objectName,UpdateScript,RecoverScript,UpdateValIdateScript,RecoverValIdateScript,Type)
                  select 3 VersionId,
                         TableName objectName,
                       'alter table '+TableName+' ' +ChangeType+ ' '+case ChangeType when 'alter' then ' column' else '' end 
                            +' '+a.ColumnName+' ' +
                       (select  Name from sys.sysTypes where xuserType=a.SystemTypeIdDest )+
                               case a.SystemTypeIdDest when 167 then replace('('+CAST (a.MaxLengthDest as nvarchar(32))+')','-1','max')
			                               when 165 then replace('('+CAST (a.MaxLengthDest as nvarchar(32))+')','-1','max')
                                                       when 175 then '('+CAST (a.MaxLengthDest as nvarchar(32))+')'
                                                       when 231 then  '('+replace(left(cast(a.MaxLengthDest/2 as nvarchar(32)),1),'0','max')+
                                                   SUBSTRING(cast(a.MaxLengthDest/2 as nvarchar(32)),2,LEN(cast(a.MaxLengthDest/2 as nvarchar(32))))+')' else '' end +
		              case isnull(a.DefaultDefinitionDest,'9') when '9'  then '' else  ' default '+ a.DefaultDefinitionDest end +
                             ' ' +case a.IsNullDest when 1 then ' null ' when 0 then ' not null ' else '' end +' ;'   
			UpdateScript,

                        'alter table '+TableName+' '+case ChangeType when 'alter' then ' alter column' else ' drop column ' end 
                                +' '+a.ColumnName+' ' +
                           (select  Name from sys.sysTypes where xuserType=a.SystemTypeIdSrc )+
                                  case a.SystemTypeIdSrc 
	                                       when 167 then replace('('+CAST (a.MaxLengthSrc as nvarchar(32))+')','-1','max')
					       when 165 then replace('('+CAST (a.MaxLengthSrc as nvarchar(32))+')','-1','max')
                                               when 175 then '('+CAST (a.MaxLengthSrc as nvarchar(32))+')'
                                               when 231 then  '('+replace(left(cast(a.MaxLengthSrc/2 as nvarchar(32)),1),'0','max')+
                                              SUBSTRING(cast(a.MaxLengthSrc/2 as nvarchar(32)),2,LEN(cast(a.MaxLengthSrc/2 as nvarchar(32))))+')' else '' end +
		                              ' ' +case a.IsNullSrc when 1 then ' null ' when 0 then ' not null ' else '' end +' ;'  
			RecoverScript,
                       'select 1' UpdateValIdateScript,
                       'select 1' RecoverValIdateScript,
                       'N' Type
                from Cfg_ObjectColumnDetail a 
                where  ChangeType='alter' and ((systemtypeiddest<>systemtypeidsrc) 
                                              or ( MaxLengthDest<>maxlengthsrc) 
			                      or ( isnulldest<>isnullsrc));     
             END
 END

GO

