USE [SmartLife-120300]
GO

/****** Object:  StoredProcedure [dbo].[SP_DBA_ImportOldManBaseInfoFromEvaluated]    Script Date: 05/26/2014 11:07:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**********************************************************/
/* SP_DBA_ImportOldManBaseInfoFromEvaluated           	  */
/* 导入老人信息                                           */
/* 前提条件是 dbo.Eva_EvaluatedBaseInfo表存在             */
/**********************************************************/
ALTER procedure [dbo].[SP_DBA_ImportOldManBaseInfoFromEvaluated]
(
@ErrorCode int output,
@ErrorMessage as nvarchar(500) output
)
as 
BEGIN
	set @ErrorCode = 0;
	declare @placeHolderForNull char(4)
	set @placeHolderForNull = 'Null'
	
    declare @SourceDatabaseName  varchar(35)
    set @SourceDatabaseName='['+DB_NAME()+']'
    
    declare @InputParams varchar(max) 
	set @InputParams = '@SourceDatabaseName='+isnull(@SourceDatabaseName,@placeHolderForNull) 
    
	--print @InputParams
	BEGIN TRY
	
		BEGIN TRAN;  
		
		declare @str_sql nvarchar(3000)
		set @str_sql='
		insert into dbo.Oca_OldManBaseInfo(OldManId,CheckInTime,Status,OperatedBy,OperatedOn,DataSource,
		OldManName,IDNo,Gender,HealthInsuranceFlag,SocialInsuranceFlag,
		AreaId,AreaId2,AreaId3,
		Address,PostCode,Tel,Mobile,Remark,InputCode1,InputCode2)
		
		select  distinct  a.ResidentId,GETDATE() CheckInTime,a.Status,a.OperatedBy,a.OperatedOn,''00003'' DataSource,a.
		ResidentName,a.IDNo,a.Gender,''0'' HealthInsuranceFlag,''0'' SocialInsuranceFlag,
		a.AreaId,a.AreaId2,a.AreaId3,a.
		ResidentialAddress,a.PostCode,a.Tel,a.Mobile,a.Remark,a.InputCode1,a.InputCode2
		
		from '+@SourceDatabaseName+'.dbo.Eva_EvaluatedBaseInfo          a
		inner join '+@SourceDatabaseName+'.dbo.Eva_EvaluatedRequisition b on a.ResidentId=b.residentid
        inner join '+@SourceDatabaseName+'.dbo.Eva_EvaluatedReport      c on b.RequisitionId=c.RequisitionId
        where 
         c.ServeItemType=''00001'' and c.doStatus=4 
	      and a.idno not in (select idno from dbo.Oca_OldManBaseInfo);'
        print @str_sql;
        exec sp_executesql @str_sql
        
        	set @str_sql='
        	with Cte_Oca_OldManConfigInfo(OldManId,CheckInTime,OperatedBy,OperatedOn,DataSource,CallNo,LocateFlag,GovTurnkeyFlag,AssessLevel,selectcheckintime)
	as(
	select a.ResidentId,getdate() CheckInTime,a.OperatedBy,a.OperatedOn,''00003'' DataSource,
	b.Mobile callno,0 locateflag,0 govturnkeyflag ,c.AssessLevel assesslevel
		,c.CheckInTime selectcheckintime
		from '+@SourceDatabaseName+'.dbo.Eva_EvaluatedBaseInfo          a
		inner join '+@SourceDatabaseName+'.dbo.Eva_EvaluatedRequisition b on a.ResidentId=b.residentid
        inner join '+@SourceDatabaseName+'.dbo.Eva_EvaluatedReport      c on b.RequisitionId=c.RequisitionId
        where 
         c.ServeItemType=''00001'' and c.doStatus=4  and c.AssessLevel is not null
	      and a.idno not in (select idno from dbo.Oca_OldManBaseInfo where OldManId in
	      (select OldManId from Oca_OldManConfigInfo))
	      )
	      insert into Oca_OldManConfigInfo(OldManId,CheckInTime,OperatedBy,OperatedOn,DataSource,CallNo,LocateFlag,GovTurnkeyFlag,AssessLevel)
	      select OldManId,CheckInTime,OperatedBy,OperatedOn,DataSource,CallNo,LocateFlag,GovTurnkeyFlag,AssessLevel from Cte_Oca_OldManConfigInfo 
	      where OldManId in(        select OldManId from Cte_Oca_OldManConfigInfo group by OldManId)
	      and selectcheckintime in (  select max(selectcheckintime) selectcheckintime from Cte_Oca_OldManConfigInfo group by OldManId)
	      ;'
        print @str_sql;
        exec sp_executesql @str_sql
        
        commit TRAN; 
        RETURN 0 -- Success!!!
        
     END TRY
     BEGIN CATCH
		 IF @@TRANCOUNT > 0
		 BEGIN
			SET @ErrorCode = ERROR_NUMBER()
			SET @ErrorMessage = N'错误代码:'+cast(@ErrorCode as varchar(20))+N',错误消息:' + ERROR_MESSAGE()+ N',来源:'+ERROR_PROCEDURE()+ N',行号:' + cast(ERROR_LINE() as varchar(5))  +N',输入参数:'+ @InputParams;
			ROLLBACK TRAN
			if(@ErrorCode < 50000 or @ErrorCode >= 60000)
				begin
					RAISERROR @ErrorCode @ErrorMessage
				end
			
			RETURN -1  -- Failure ???
		 END 
	 END CATCH
END 