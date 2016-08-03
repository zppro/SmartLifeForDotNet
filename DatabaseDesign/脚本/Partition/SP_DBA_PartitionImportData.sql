/***********************************************************************/
/*   SP_DBA_PartitionImportData            */
/*  导入分区表的数据   */
/***********************************************************************/
create procedure SP_DBA_PartitionImportData
@databasename varchar(100)
as
begin
		insert into  Bas_ResidentBaseInfo(   id,checkintime, ResidentId,Status,OperatedBy,OperatedOn,DataSource,ResidentName,
		ResidentStatus,IDNo,Gender,AreaId,AreaId2)
		select ID,CheckInTime,
		OldManId,Status,OperatedBy,OperatedOn,DataSource,OldManName,
		'00001' ResidentStatus,IDNo,Gender,AreaId,AreaId2
		from [smartlife-120303].dbo.Oca_OldManBaseInfo 
		where AreaId2 is not  null;

		insert into  Bas_ResidentBaseInfo(   id,checkintime, ResidentId,Status,OperatedBy,OperatedOn,DataSource,ResidentName,
		ResidentStatus,IDNo,Gender,AreaId,AreaId2)
		select ID,CheckInTime,
		OldManId,Status,OperatedBy,OperatedOn,DataSource,OldManName,
		'00001' ResidentStatus,IDNo,Gender,AreaId,AreaId2
		from [smartlife-120304].dbo.Oca_OldManBaseInfo 
		where AreaId2 is not  null;

end 