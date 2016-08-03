USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Dey_AddColumnScript]    Script Date: 05/13/2014 18:13:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************/
/* SP_Dey_AddColumnScript                                     */
/*  生成修改表添加字段的脚本                                  */
/**************************************************************/


CREATE    PROCEDURE [dbo].[SP_Dey_AddColumnScript]
 AS
 BEGIN
      SET NOCOUNT ON
     
        IF (EXISTS (select * from Dey_Script where Type='A'))
             BEGIN
               delete from Dey_Script where Type='A';        
             END
        IF (NOT EXISTS (select * from Dey_Script where Type='A'))
             BEGIN
                 insert into Dey_Script (VersionId,objectName,UpdateScript,RecoverScript,UpdateValIdateScript,RecoverValIdateScript,Type)
                select 3 VersionId,
                       TableName objectName,
                       '
                       if(not exists (select * from sys.all_columns where Name='''+a.ColumnName+''' and object_id =
                        (select object_id from sys.tables where Name='''+TableName+''')))
                         begin 
                               alter table '+TableName+' ' +ChangeType+ ' '+case ChangeType when 'alter' then ' column' else '' end 
                               +' '+a.ColumnName+' ' +
                              (select  Name from sys.sysTypes where xuserType=a.SystemTypeIdDest )+
                              case a.SystemTypeIdDest when 167 then replace('('+CAST (a.MaxLengthDest as nvarchar(32))+')','-1','max')
                                    when 175 then '('+CAST (a.MaxLengthDest as nvarchar(32))+')'
                                    when 231 then  '('+replace(left(cast(a.MaxLengthDest/2 as nvarchar(32)),1),'0','max')+                   
				    SUBSTRING(cast(a.MaxLengthDest/2 as nvarchar(32)),2,LEN(cast(a.MaxLengthDest/2 as nvarchar(32))))+')' else '' end +
		              case isnull(a.DefaultDefinitionDest,'9') when '9'  then '' else  ' default '+ a.DefaultDefinitionDest end +
                             ' ' +case a.IsNullDest when 1 then ' null ' when 0 then ' not null ' else '' end +' ;
                         end
                         go  
                      ' UpdateScript,
                      '
                     if(exists (select * from sys.all_columns where Name='''+a.ColumnName+''' and object_id =
                       (select object_id from sys.tables where Name='''+TableName+''')))
                        begin 
                            alter table '+TableName+' '+case ChangeType when 'alter' then ' alter column' else ' drop column ' end 
                            +' '+a.ColumnName+' ;
                        end
                        go
                    '   RecoverScript,
                     'select 1' UpdateValIdateScript,
                    'select 1' RecoverValIdateScript,
                    'A' Type
from Cfg_ObjectColumnDetail a 
where ChangeType ='add';      
             END
 END

GO

