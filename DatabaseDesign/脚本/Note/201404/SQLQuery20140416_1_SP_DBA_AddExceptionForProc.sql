--------------------------===============
	create procedure SP_DBA_AddExceptionForProc
	as 
	begin
	select stuff(m.targetscript,CHARINDEX(m.parametername,m.targetscript,1)-2,0,(case m.parametername when 'as ' then '@ErrorCode int output,
@ErrorMessage as nvarchar(500) output' else '@ErrorCode int output,
@ErrorMessage as nvarchar(500) output,' end )) ProcedureScript
	from 
	(
				 select a.definition,a.pre,stuff(a.pre,LEN(a.pre)-4,0,' return 0
			end try
					 BEGIN CATCH
		 IF @@TRANCOUNT > 0
		 BEGIN
			SET @ErrorCode = ERROR_NUMBER()
			SET @ErrorMessage = N''错误代码:''+cast(@ErrorCode as varchar(20))+N'',错误消息:'' + ERROR_MESSAGE()+ N'',来源:''+ERROR_PROCEDURE()+ N'',行号:'' + cast(ERROR_LINE() as varchar(5))  +N'',输入参数:''---+ @InputParams
			;
			print ERROR_MESSAGE()
			if(@ErrorCode < 50000 or @ErrorCode >= 60000)
				begin
					RAISERROR @ErrorCode @ErrorMessage
				end 
			--ROLLBACK TRAN;
			RETURN -1  -- Failure ???
		 END 
	 END CATCH

			') targetscript,a.parametername
			 from 
			 (
						select definition,
					stuff(definition,CHARINDEX('begin',definition,1)+6,0,' 
					BEGIN TRY ') pre,isnull(d.parametername,'as ') parametername
					--CHARINDEX('end',definition,1),
					from sys.all_sql_modules b left join
					 ( select c.object_id,(select name from sys.all_parameters where object_id=c.object_id and parameter_id=c.parid) parametername
					   from 
					   (
						   select object_id,max(parameter_id) parid from sys.all_parameters 
						   where object_id>0 
						   group by object_id
						)c  
						where  c.parid<>0 
					 ) d
					  on	b.object_id=d.object_id
					where b.object_id>0
			) a
	) m
	end