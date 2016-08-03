/******************************************************************************/
/*SP_DBA_Aync_Oca_OldManTransfer */
/*function :老人迁移居住区域的数据库操作 */
/*@IdNo 老人身份证 */
/*@OldAreaId 旧的区域的编号,@NewAreaId 新的区域的编号 */
/******************************************************************************/
		create procedure 	[dbo].[SP_DBA_Aync_Oca_OldManTransfer]
					@IdNo       char(18),
					@OldAreaId  char(5),
					@NewAreaId  char(5)
					as 
			begin
					declare @str nvarchar(max),@OldDatabaseName nvarchar(max),@NewDatabaseName nvarchar(max)
					
					select @OldDatabaseName=databasename from Cfg_DatabaseAreaIdMap where areaid=@OldAreaId;
					set @str='update '+@OldDatabaseName+'.dbo.Oca_OldManBaseInfo set status=3 , OperatedOn=getdate()
						where idno='''+@IdNo+''''
					print '--'+@str
					exec sp_executesql @str	
					select @NewDatabaseName=databasename from Cfg_DatabaseAreaIdMap where areaid=@NewAreaId;
					
					/*set @str='select count(*) from '+@NewDatabaseName+'.dbo.Oca_OldManBaseInfo 
						where idno='+@IdNo*/
						
					set @str='insert into '+@NewDatabaseName+'.dbo.Oca_OldManBaseInfo (OldManId,CheckInTime,Status,OperatedBy,OperatedOn,DataSource,
					 OldManName,Gender,Birthday,IDNo,HealthInsuranceFlag,HealthInsuranceNumber,SocialInsuranceFlag,
					 SocialInsuranceNumber,LivingState,OldManIdentity,AreaId,AreaId2,AreaId3,Address,LongitudeS,
					 LatitudeS,PostCode,Tel,Mobile,Remark,InputCode1,InputCode2)
						select OldManId,CheckInTime,Status,OperatedBy,OperatedOn,DataSource,
					 OldManName,Gender,Birthday,IDNo,HealthInsuranceFlag,HealthInsuranceNumber,SocialInsuranceFlag,
					 SocialInsuranceNumber,LivingState,OldManIdentity,AreaId,AreaId2,AreaId3,Address,LongitudeS,
					 LatitudeS,PostCode,Tel,Mobile,Remark,InputCode1,InputCode2
						from '+@OldDatabaseName+'.dbo.Oca_OldManBaseInfo
						 where IDNo not in(select IDNo from '+@NewDatabaseName+'.dbo.Oca_OldManBaseInfo)
							  and idno='''+@IdNo+''' and Status=3'
					print '--'+@str	
						exec sp_executesql @str	
					set @str='update '+@NewDatabaseName+'.dbo.Oca_OldManBaseInfo set status=1, OperatedOn=getdate()
						where idno='''+@IdNo+ ''' and status<>1;'
					print '--'+@str
						exec sp_executesql @str	
		end