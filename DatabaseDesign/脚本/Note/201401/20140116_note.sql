copy nul %date:~0,10%.log 

 echo 文本文件内容。。内容。。>file_name_%date:~0,4%%date:~5,2%%date:~8,2%.txt 

set filename="%date:~0,10% %time:~0,2%-%time:~3,2%.log" D:\>copy nul %filename% 已复制 1 个文件。
set date_mark="%date:~0,4%%date:~5,2%%date:~8,2%"
echo %filename%

alter table [dbo].Cer_DeployNode ADD DEFAULT (getdate()) for CheckInTime;
go

SELECT ST.[name] AS "Table Name", SC.[name] AS "Column Name", SD.definition AS "Default Value", SD.[name] AS "Constraint Name"   FROM sys.tables ST INNER JOIN sys.syscolumns SC ON ST.[object_id] = SC.[id]   INNER JOIN sys.default_constraints SD ON ST.[object_id] = SD.[parent_object_id] AND SC.colid = SD.parent_column_id   ORDER BY ST.[name], SC.colid  

select name FROM sysobjects A 
JOIN sysconstraints sc ON A.id = sc.constid 
WHERE object_name(A.parent_obj) = 'Oca_MerchantServePeriod' AND A.xtype = "D" 
AND sc.colid =(SELECT colid FROM syscolumns WHERE id = object_id('Oca_MerchantServePeriod') AND name = 'CheckInTime')