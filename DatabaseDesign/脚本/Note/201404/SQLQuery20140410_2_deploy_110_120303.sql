select * from Oca_OldManBaseInfo where Mobile like '13819148144%'
select * from Oca_OldManBaseInfo where mobile='13819148144'
13819148144 
select CallNo,LEN(CallNo) from Oca_OldManConfigInfo where len(CallNo)=12 
 like '13819148144%'

select * from [IPCC-1203].dbo.IPCC_CallRecord
where Caller='13819148144'

select * from [IPCC-1203].dbo.IPCC_Caller where caller like '13819148144%'

select * from Temp_Mobile where F2='13819148144	'

select * from Temp_ExcelSource where Tel='13819148144'
select * from Temp_ExcelSource where mobile='13819148144'

select * from Oca_OldManBaseInfo where oldmanname=N'老人(13819148144	)'
select Mobile,LEN(mobile),SUBSTRING(mobile,1,11) from Oca_OldManBaseInfo where IDNo='330102193001011234' 
and len(mobile)=12
order by Mobile

13429188644 老人(13429180494 ) 

select * from Oca_CallService where Caller like '13819148144%'
----------------=======================================================
select * into Oca_OldManBaseInfoOld0410 from Oca_OldManBaseInfo
select * into Oca_OldManConfigInfoOld0410 from Oca_OldManConfigInfo
update Oca_OldManBaseInfo set Mobile=SUBSTRING(mobile,1,11)
where IDNo='330102193001011234' 
and len(mobile)=12;

--select CallNo,LEN(CallNo) from Oca_OldManConfigInfo where len(CallNo)=12 
update Oca_OldManConfigInfo 
set CallNo=SUBSTRING(callno,1,11)
where len(CallNo)=12 ;
----------------------------------===========================================
select * from sys.tables

exec SP_DBA_PartitionCreateTable @databasename='Smartlife-1203'
exec SP_DBA_PartitionImportData @databasename='Smartlife-1203'

select * from sys.partitions
where object_id in (select object_id from sys.tables where name='bas_residentbaseinfo')