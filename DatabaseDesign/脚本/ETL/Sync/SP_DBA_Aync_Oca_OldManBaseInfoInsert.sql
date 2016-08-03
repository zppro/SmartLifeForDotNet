		/****************************************************************************/
		/*SP_DBA_Aync_Oca_OldManBaseInfoInsert                                      */
		/* function: 插入老人基本信息数据                                           */
		/* @RefreshTime 参数是上次刷新时间 比此时刻 更晚更新的记录才被加到数据库    */
		/****************************************************************************/						
					create procedure SP_DBA_Aync_Oca_OldManBaseInfoInsert
					@RefreshTime datetime
					as 
					begin
					       
					        DECLARE @str nvarchar(max)
							DECLARE  @DatabaseName varchar(max),@AreaId char(5)
						DECLARE id_cursor CURSOR FOR 
							select databasename,areaid from Cfg_DatabaseAreaIdMap where areaid<'01190';

						OPEN id_cursor
						 
						FETCH NEXT FROM id_cursor 
						INTO  @DatabaseName,@AreaId
						 
						WHILE @@FETCH_STATUS = 0
						 BEGIN

						 /**********/
						 set @str='insert into '+@DatabaseName+'.dbo.Oca_OldManBaseInfo (OldManId,CheckInTime,Status,OperatedBy,OperatedOn,DataSource,
						 OldManName,Gender,Birthday,IDNo,HealthInsuranceFlag,HealthInsuranceNumber,SocialInsuranceFlag,
						 SocialInsuranceNumber,LivingState,OldManIdentity,AreaId,AreaId2,AreaId3,Address,LongitudeS,
						 LatitudeS,PostCode,Tel,Mobile,Remark,InputCode1,InputCode2)
							select residentid OldManId,getdate() CheckInTime,Status,OperatedBy,OperatedOn,DataSource,residentname OldManName,Gender,null Birthday,
							   IDNo,0 HealthInsuranceFlag,null HealthInsuranceNumber,0 SocialInsuranceFlag,null SocialInsuranceNumber,
							 LivingStatus  LivingState,null OldManIdentity,AreaId,AreaId2,AreaId3,ResidentialAddress Address,null LongitudeS,null LatitudeS,
							 PostCode,Tel,Mobile,Remark,InputCode1,InputCode2
							from [SmartLife-1203].dbo.bas_residentbaseinfo
							 where IDNo not in(select IDNo from '+@DatabaseName+'.dbo.Oca_OldManBaseInfo)
								  and AreaId='''+@AreaId+''' and Status=1 and (OperatedOn >'''+convert(varchar(20),isnull(@refreshtime,GETDATE()),120)+''' or CheckInTime >'''+convert(varchar(20),isnull(@refreshtime,GETDATE()),120)+''');' 

                          print '--'+@str
                          --exec sp_executesql @str
						 FETCH NEXT FROM id_cursor 
							INTO  @DatabaseName,@AreaId
						 END 
						CLOSE id_cursor
						 DEALLOCATE id_cursor
						 
							    insert into [Smartlife-120300].dbo.Oca_OldManBaseInfo (OldManId,CheckInTime,Status,OperatedBy,OperatedOn,DataSource,
         OldManName,Gender,Birthday,IDNo,HealthInsuranceFlag,HealthInsuranceNumber,SocialInsuranceFlag,
         SocialInsuranceNumber,LivingState,OldManIdentity,AreaId,AreaId2,AreaId3,Address,LongitudeS,
         LatitudeS,PostCode,Tel,Mobile,Remark,InputCode1,InputCode2)
		    select residentid OldManId,getdate() CheckInTime,Status,OperatedBy,OperatedOn,DataSource,residentname OldManName,Gender,null Birthday,
               IDNo,0 HealthInsuranceFlag,null HealthInsuranceNumber,0 SocialInsuranceFlag,null SocialInsuranceNumber,
             LivingStatus  LivingState,null OldManIdentity,AreaId,AreaId2,AreaId3,ResidentialAddress Address,null LongitudeS,null LatitudeS,
             PostCode,Tel,Mobile,Remark,InputCode1,InputCode2
		    from [SmartLife-1203].dbo.bas_residentbaseinfo
		     where IDNo not in(select IDNo from [Smartlife-120300].dbo.Oca_OldManBaseInfo)
		          and AreaId='99999' and Status=1 and (OperatedOn >@refreshtime or CheckInTime >@refreshtime);
		          
		       
					end
		