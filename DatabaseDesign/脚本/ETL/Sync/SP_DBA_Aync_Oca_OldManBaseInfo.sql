/*******************************************************************************************/
/* SP_DBA_Aync_Oca_OldManBaseInfo                                                          */
/* 把1203库的居民信息 同步到业务库的老人基本信息表和评估表                                 */
/* @Phase   1 ：审批前， 2：审批后*/
/*******************************************************************************************/
create procedure [dbo].[SP_DBA_Aync_Oca_OldManBaseInfo]
@Phase int
as
begin

	if (@Phase=1)
	   begin
		exec  SP_DBA_Aync_Oca_OldManBaseInfo_Phase1
	   end 
	   
     if (@Phase=2)
	   begin
	   
		exec   SP_DBA_Aync_Oca_OldManBaseInfo_Phase2
	   end   
        
end
