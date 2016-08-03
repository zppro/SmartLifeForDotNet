
select * from sys.all_parameters where object_id>0 


select definition,
stuff(definition,CHARINDEX('begin',definition,1)+6,0,' BEGIN TRY '),
CHARINDEX('end',definition,1),
stuff(definition,LEN(definition)-4,0,' return 0
end try')
 from sys.all_sql_modules where object_id>0
 
 select a.definition,a.pre,stuff(a.pre,LEN(a.pre)-4,0,' return 0
end try
	 BEGIN CATCH
		DECLARE @strMsg AS varchar(2000)
		SET @strMsg= ERROR_MESSAGE()
		RAISERROR 20000 @strMsg
		return -1
	 END CATCH

') targetscript
 from 
 (
		 select definition,
		stuff(definition,CHARINDEX('begin',definition,1)+6,0,' 
		BEGIN TRY ') pre
		--CHARINDEX('end',definition,1),
		from sys.all_sql_modules where object_id>0
) a
go
create procedure [dbo].[sp_etl_column_oca_oldmanbaseinfo_mobile]    as begin      BEGIN TRY     update oca_oldmanbaseinfo 

   set mobile=cast(cast( mobile as  bigint) as  varchar(20))    from oca_oldmanbaseinfo a,temp_excelsource b    where a.idno=b.f4;    return 0  end try    BEGIN CATCH    DECLARE @strMsg AS varchar(2000)    SET @strMsg= ERROR_MESSAGE()    RAISERROR 20000 @strMsg    return -1    END CATCH      end
   
   
    select definition,
		stuff(definition,CHARINDEX('begin',definition,1)+6,0,' 
		BEGIN TRY ') pre,isnull(d.parametername,'as') parametername
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
   
   select c.object_id,(select name from sys.all_parameters where object_id=c.object_id and parameter_id=c.parid)
   from 
   (
	   select object_id,max(parameter_id) parid from sys.all_parameters 
	   where object_id>0 
	   group by object_id
	)c  
	where  c.parid<>0;
	
	go
	
	create procedure [dbo].[sp_etl_column_oca_oldma@ErrorCode int output,  @ErrorMessage as nvarchar(500) outputnbaseinfo_address]   
	 as begin         BEGIN TRY     update oca_oldmanbaseinfo    set address=b.address    from oca_oldmanbaseinfo a
	 ,temp_excelsource b    where a.idno=b.f4;    return 0     end try       BEGIN CATCH       DECLARE @strMsg AS varchar(2000) 
	       SET @strMsg= ERROR_MESSAGE()       RAISERROR 20000 @strMsg       return -1       END CATCH         end