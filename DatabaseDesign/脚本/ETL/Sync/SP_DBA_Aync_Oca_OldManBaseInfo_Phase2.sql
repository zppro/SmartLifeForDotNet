/*******************************************************************************************/
/* SP_DBA_Aync_Oca_OldManBaseInfo                                                          */
/* 把1203库的居民信息 同步到业务库的老人基本信息表和评估表                                 */
/* */
/*******************************************************************************************/
create procedure [dbo].[SP_DBA_Aync_Oca_OldManBaseInfo_Phase2]

as
begin
declare @type varchar(10),@str nvarchar(max),@ColumnName  varchar(max)='Status,ResidentName,PostCode,Tel,Mobile,Remark,ResidentialAddress,Gender',@whereclause varchar(max),@refreshtime datetime
declare @count int,@i int,@item nvarchar(max),@srccol nvarchar(max),@desccol nvarchar(max) ,@updateStr nvarchar(max),@updateStr2 nvarchar(max)
  set @i=0
  set  @updateStr=''
    select @count=COUNT(*) from [smartlife-120300].dbo.FUNC_Tol_String2Table(@ColumnName,',')
    select @refreshtime=MAX(refreshtime) from Cfg_ExecuteLog;
    

	
	--------------插入老人基本信息数据  
	exec SP_DBA_Aync_Oca_OldManBaseInfoInsert @refreshtime
	
	------------- 插入评估数据 
	exec SP_DBA_Aync_Eva_EvaluatedBaseInfoInsert @refreshtime
		
	  while @i<@count
			  begin
					  set @i=@i+1
					  set @item=( select item from [smartlife-120300].dbo.FUNC_Tol_String2Table(@ColumnName,',') where Pos=@i);
					  set @srccol=@item
											
						--set @updateStr=@updateStr+isnull(@desccol+',','') 
			 end
			  set @desccol=( select  dbo.joinStr(b.name+'=b.'+a.name) from (
						select name,column_id,object_id from [SmartLife-1203].sys.all_columns 
						where object_id in(select object_id from [SmartLife-1203].sys.tables where name='Bas_ResidentBaseInfo')
						--and name in(@srccol) 
						) a,
						(select name,column_id,object_id from [SmartLife-120300].sys.all_columns
						 where object_id in(select object_id from [SmartLife-120300].sys.tables where name='Oca_OldManBaseInfo')) b
						 ,[SmartLife-1203].dbo.Cfg_lineage c
						 where a.column_id=c.columnid and b.column_id=c.lineageid and a.name <>'Id'  and a.object_id=c.SrcObjectId
						 and b.object_id=c.DescObjectId  and c.phase=2
						 )
        -- set @updateStr2=SUBSTRING(@updateStr,1,len(@updateStr)-1)
         set @whereclause=' where  OperatedOn > '''+convert(varchar(20),isnull(@refreshtime,GETDATE()),120)+''''
		 set @str='	update [SmartLife-120300].dbo.Oca_OldManBaseInfo set  '+@desccol+'
			from [SmartLife-120300].dbo.Oca_OldManBaseInfo a,(select *  from [SmartLife-1203].dbo.Bas_ResidentBaseInfo
			'+@whereclause+' and AreaId=''99999''  and (status=1 or status=0)  ) b
			where a.IDNo=b.idno and status=1 ;'
		  print '--'+@str
 			exec sp_executesql @str;
 	
 			set @str='	update [SmartLife-120301].dbo.Oca_OldManBaseInfo set  '+@desccol+'
			from [SmartLife-120301].dbo.Oca_OldManBaseInfo a,(select *  from [SmartLife-1203].dbo.Bas_ResidentBaseInfo
			'+@whereclause+' and AreaId=''01189'' and (status=1 or status=0)  ) b
			where a.IDNo=b.idno and status=1 ;'
		    print '--'+@str
 			exec sp_executesql @str;
 	
 	 set @desccol=( select  dbo.joinStr(b.name+'=b.'+a.name) from (
						select name,column_id,object_id from [SmartLife-1203].sys.all_columns 
						where object_id in(select object_id from [SmartLife-1203].sys.tables where name='Bas_ResidentBaseInfo')
						--and name in(@srccol) 
						) a,
						(select name,column_id,object_id from [SmartLife-120300].sys.all_columns
						 where object_id in(select object_id from [SmartLife-120300].sys.tables where name='Eva_EvaluatedBaseInfo')) b
						 ,[SmartLife-1203].dbo.Cfg_lineage c
						 where a.column_id=c.columnid and b.column_id=c.lineageid and a.name <>'Id'  and a.object_id=c.SrcObjectId
						 and b.object_id=c.DescObjectId  and c.phase=2
						 )
		set @str='	update [SmartLife-120300].dbo.Eva_EvaluatedBaseInfo set  '+@desccol+'
		from [SmartLife-120300].dbo.Eva_EvaluatedBaseInfo a,(select *  from [SmartLife-1203].dbo.Bas_ResidentBaseInfo
		'+@whereclause+' and AreaId=''99999'' and (status=1 or status=0) ) b
		where a.IDNo=b.idno and status=1 ;'
		print '--'+@str
 		exec sp_executesql @str;				 
 	 
         ------------老人们批量地迁移居住区域的数据库操作    
 	  exec SP_DBA_Aync_Oca_OldMenTransfer 
 	  
    insert into Cfg_ExecuteLog(Mark) values(1);
end