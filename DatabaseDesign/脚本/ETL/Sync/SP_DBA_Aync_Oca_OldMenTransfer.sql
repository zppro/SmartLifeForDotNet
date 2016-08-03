/******************************************************************************/
/*SP_DBA_Aync_Oca_OldManTransfer */
/*function :老人们批量地迁移居住区域的数据库操作 */
/******************************************************************************/	
		create procedure 	[dbo].[SP_DBA_Aync_Oca_OldMenTransfer]
				as 
			begin
									
						DECLARE  @IdNO char(18),@NewAreadId char(5),@OldAreaId char(5)
						DECLARE id_cursor CURSOR FOR 
						select distinct b.IDNo ,a.areaid NewAreadId,b.areaid OldAreaId from 
						[SmartLife-1203].dbo.bas_residentbaseinfo a,
						[Smartlife-120300].dbo.Oca_OldManBaseInfo b
						where a.idno=b.idno and a.areaid<>b.areaid and a.idno not like '9999%';

						OPEN id_cursor
						 
						FETCH NEXT FROM id_cursor 
						INTO  @IdNO,@NewAreadId,@OldAreaId
						 
						WHILE @@FETCH_STATUS = 0
						 BEGIN

						 /**********/
						 print @IdNO+@NewAreadId+@OldAreaId
                                                  exec 	SP_DBA_Aync_Oca_OldManTransfer @idno=@IdNO,@oldareaid=@OldAreaId,@newareaid=@NewAreadId	

						 FETCH NEXT FROM id_cursor 
							INTO  @IdNO,@NewAreadId,@OldAreaId
						 END 
						CLOSE id_cursor
						 DEALLOCATE id_cursor
			end