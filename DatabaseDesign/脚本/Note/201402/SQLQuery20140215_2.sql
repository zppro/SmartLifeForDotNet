USE [SmartLife-120301]
GO

DECLARE	@return_value int,
		@ErrorCode int,
		@ErrorMessage nvarchar(500)

EXEC	@return_value = [dbo].[SP_SIM_Disposal_CallServiceAndWorkOrder]
		@ErrorCode = @ErrorCode OUTPUT,
		@ErrorMessage = @ErrorMessage OUTPUT
GO
