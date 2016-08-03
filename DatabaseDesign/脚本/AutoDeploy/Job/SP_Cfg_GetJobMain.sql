USE [Leblue-Configuration]
GO

/****** Object:  StoredProcedure [dbo].[SP_Cfg_GetJobMain]    Script Date: 05/13/2014 09:25:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************/
/*  SP_Cfg_GetJobMain  */
/*  取得作业的信息存入数据库 入口程序 */
/*  参数两个,@source_dbName源数据库所在的机器,    
    @dest_dbName 目标数据库所在的机器  两个参数都有默认值。
    @source_dbName 默认时为存储过程所在的本机
    @dest_dbName默认时为 不比较目标数据库*/
/***********************************************************************/
CREATE procedure [dbo].[SP_Cfg_GetJobMain]
@source_dbName nvarchar(100)='',
@dest_dbName nvarchar(100)='' 
as
begin

    ----------获得作业的信息
	exec SP_Cfg_GetJob @source_dbName=@source_dbName,@dest_dbName=@dest_dbName

    ----------获得作业步骤的信息
	exec SP_Cfg_GetJobSteps @source_dbName=@source_dbName,@dest_dbName=@dest_dbName

    ----------获得作业计划的信息
	exec SP_Cfg_GetJobSchedule @source_dbName=@source_dbName,@dest_dbName=@dest_dbName

    ----------获得作业所在的服务器的信息
	exec SP_Cfg_GetJobServer @source_dbName=@source_dbName,@dest_dbName=@dest_dbName
end
GO

