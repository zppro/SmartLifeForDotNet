EXEC sys.sp_addsrvrolemember @loginame = N'dbo', @rolename = N'setupadmin'
GO

exec sp_addlinkedsrvlogin @rmtsrvname='remote_dbo_64'
,@useself=false
,@locallogin='sa'
,@rmtuser='sa'
,@rmtpassword='123'
GRANT EXECUTE ON SYS.XP_PROP_OLEDB_PROVIDER TO dbo; 

select OBJECT_ID(N'FUNC_Tol_JSONEscaped', 'FN')
select a.object_id from sys.all_objects a where a.type in ('AF','TF','FN') and a.name='FUNC_rTol_JSONEscaped'
select * from remote_dbo_64.[smartlife-120300].dbo.oca_oldmanbaseinfo;
----------------------------=========================================
alter  PROCEDURE [dbo].[p_dba_mobile_number_query]
@city_id  char(4)='1203',
@type     char(1)=0   /*参数 type: 为一时，显示正常的手机注册，为0或者是其它值时，显示异常的手机号。默认值为0.*/
 AS
 BEGIN
SET NOCOUNT ON
 

DECLARE  @database_name nvarchar(50),@database nvarchar(50),@select_str nvarchar(4000),@from_str nvarchar(4000),@where_str varchar(40),@column_str nvarchar(4000),@checksum_str nvarchar(4000),@where_condition nvarchar(4000)

 
PRINT '-------- make script of the query mobile number  --------'
set @select_str='select a.CallNo'
set @from_str=' from (select Caller CallNo from [IPCC-'+@city_id+'].dbo.IPCC_caller where Caller is not null and Caller <>'''') a '
set @where_str='smartlife-'+@city_id+'%'
set @checksum_str=''
set @column_str=''
set @where_condition='where x.checkresult=1'
DECLARE database_name_cursor CURSOR FOR 
select name database_name from sys.databases where name like @where_str  and name <>'smartlife-'+@city_id order by name;
 
OPEN database_name_cursor
 
FETCH NEXT FROM database_name_cursor 
INTO  @database_name
 
WHILE @@FETCH_STATUS = 0
 BEGIN
set @database=replace(@database_name,'-','_')  
set @checksum_str=@checksum_str+'(case isnull('+@database+'.CallNo,''9'') when ''9''  then 0 else 1 end)+'
set @column_str =@column_str+', case isnull('+@database+'.CallNo,''9'') when ''9''  then 0 else 1 end '+@database+''
set @from_str=@from_str+'left join ['+@database_name+'].dbo.Oca_OldManConfigInfo '+@database+'  on a.CallNo='+@database+'.CallNo '
 
       
     FETCH NEXT FROM database_name_cursor 
    INTO  @database_name
 END 
 if (@type='1')
 begin
set @where_condition='where x.checkresult<>1'
 end

set @select_str ='select x.*  from ('+@select_str+','+substring(@checksum_str,1,LEN(@checksum_str)-1)+'  checkresult '+@column_str+@from_str
+') x where x.checkresult='+@type
--select @select_str
exec sp_executesql @select_str
CLOSE database_name_cursor
 DEALLOCATE database_name_cursor
 END
 
 exec p_dba_mobile_number_query @city_id='1203',@type='3'
 
 select x.*  from (select a.CallNo,(case isnull(SmartLife_120300.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120301.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120302.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120303.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120304.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120305.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120306.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120307.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120311.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120396.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120397.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120398.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120399.CallNo,'9') when '9'  then 0 else 1 end)  checkresult , case isnull(SmartLife_120300.CallNo,'9') when '9'  then 0 else 1 end SmartLife_120300, case isnull(smartlife_120301.CallNo,'9') when '9'  then 0 else 1 end smartlife_120301, case isnull(smartlife_120302.CallNo,'9') when '9'  then 0 else 1 end smartlife_120302, case isnull(smartlife_120303.CallNo,'9') when '9'  then 0 else 1 end smartlife_120303, case isnull(smartlife_120304.CallNo,'9') when '9'  then 0 else 1 end smartlife_120304, case isnull(smartlife_120305.CallNo,'9') when '9'  then 0 else 1 end smartlife_120305, case isnull(smartlife_120306.CallNo,'9') when '9'  then 0 else 1 end smartlife_120306, case isnull(smartlife_120307.CallNo,'9') when '9'  then 0 else 1 end smartlife_120307, case isnull(smartlife_120311.CallNo,'9') when '9'  then 0 else 1 end smartlife_120311, case isnull(smartlife_120396.CallNo,'9') when '9'  then 0 else 1 end smartlife_120396, case isnull(smartlife_120397.CallNo,'9') when '9'  then 0 else 1 end smartlife_120397, case isnull(smartlife_120398.CallNo,'9') when '9'  then 0 else 1 end smartlife_120398, case isnull(smartlife_120399.CallNo,'9') when '9'  then 0 else 1 end smartlife_120399 from (select Caller CallNo from [IPCC-1203].dbo.IPCC_caller where Caller is not null and Caller <>'') a left join [SmartLife-120300].dbo.Oca_OldManConfigInfo SmartLife_120300  on a.CallNo=SmartLife_120300.CallNo left join [smartlife-120301].dbo.Oca_OldManConfigInfo smartlife_120301  on a.CallNo=smartlife_120301.CallNo left join [smartlife-120302].dbo.Oca_OldManConfigInfo smartlife_120302  on a.CallNo=smartlife_120302.CallNo left join [smartlife-120303].dbo.Oca_OldManConfigInfo smartlife_120303  on a.CallNo=smartlife_120303.CallNo left join [smartlife-120304].dbo.Oca_OldManConfigInfo smartlife_120304  on a.CallNo=smartlife_120304.CallNo left join [smartlife-120305].dbo.Oca_OldManConfigInfo smartlife_120305  on a.CallNo=smartlife_120305.CallNo left join [smartlife-120306].dbo.Oca_OldManConfigInfo smartlife_120306  on a.CallNo=smartlife_120306.CallNo left join [smartlife-120307].dbo.Oca_OldManConfigInfo smartlife_120307  on a.CallNo=smartlife_120307.CallNo left join [smartlife-120311].dbo.Oca_OldManConfigInfo smartlife_120311  on a.CallNo=smartlife_120311.CallNo left join [smartlife-120396].dbo.Oca_OldManConfigInfo smartlife_120396  on a.CallNo=smartlife_120396.CallNo left join [smartlife-120397].dbo.Oca_OldManConfigInfo smartlife_120397  on a.CallNo=smartlife_120397.CallNo left join [smartlife-120398].dbo.Oca_OldManConfigInfo smartlife_120398  on a.CallNo=smartlife_120398.CallNo left join [smartlife-120399].dbo.Oca_OldManConfigInfo smartlife_120399  on a.CallNo=smartlife_120399.CallNo ) x where x.checkresult=1
 
 select x.*  from (select a.CallNo,(case isnull(SmartLife_120300.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120301.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120302.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120303.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120304.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120305.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120306.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120307.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120311.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120396.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120397.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120398.CallNo,'9') when '9'  then 0 else 1 end)+(case isnull(smartlife_120399.CallNo,'9') when '9'  then 0 else 1 end)  checkresult , case isnull(SmartLife_120300.CallNo,'9') when '9'  then 0 else 1 end SmartLife_120300, case isnull(smartlife_120301.CallNo,'9') when '9'  then 0 else 1 end smartlife_120301, case isnull(smartlife_120302.CallNo,'9') when '9'  then 0 else 1 end smartlife_120302, case isnull(smartlife_120303.CallNo,'9') when '9'  then 0 else 1 end smartlife_120303, case isnull(smartlife_120304.CallNo,'9') when '9'  then 0 else 1 end smartlife_120304, case isnull(smartlife_120305.CallNo,'9') when '9'  then 0 else 1 end smartlife_120305, case isnull(smartlife_120306.CallNo,'9') when '9'  then 0 else 1 end smartlife_120306, case isnull(smartlife_120307.CallNo,'9') when '9'  then 0 else 1 end smartlife_120307, case isnull(smartlife_120311.CallNo,'9') when '9'  then 0 else 1 end smartlife_120311, case isnull(smartlife_120396.CallNo,'9') when '9'  then 0 else 1 end smartlife_120396, case isnull(smartlife_120397.CallNo,'9') when '9'  then 0 else 1 end smartlife_120397, case isnull(smartlife_120398.CallNo,'9') when '9'  then 0 else 1 end smartlife_120398, case isnull(smartlife_120399.CallNo,'9') when '9'  then 0 else 1 end smartlife_120399 from (select Caller CallNo from [IPCC-1203].dbo.IPCC_caller where Caller is not null and Caller <>'') a left join [SmartLife-120300].dbo.Oca_OldManConfigInfo SmartLife_120300  on a.CallNo=SmartLife_120300.CallNo left join [smartlife-120301].dbo.Oca_OldManConfigInfo smartlife_120301  on a.CallNo=smartlife_120301.CallNo left join [smartlife-120302].dbo.Oca_OldManConfigInfo smartlife_120302  on a.CallNo=smartlife_120302.CallNo left join [smartlife-120303].dbo.Oca_OldManConfigInfo smartlife_120303  on a.CallNo=smartlife_120303.CallNo left join [smartlife-120304].dbo.Oca_OldManConfigInfo smartlife_120304  on a.CallNo=smartlife_120304.CallNo left join [smartlife-120305].dbo.Oca_OldManConfigInfo smartlife_120305  on a.CallNo=smartlife_120305.CallNo left join [smartlife-120306].dbo.Oca_OldManConfigInfo smartlife_120306  on a.CallNo=smartlife_120306.CallNo left join [smartlife-120307].dbo.Oca_OldManConfigInfo smartlife_120307  on a.CallNo=smartlife_120307.CallNo left join [smartlife-120311].dbo.Oca_OldManConfigInfo smartlife_120311  on a.CallNo=smartlife_120311.CallNo left join [smartlife-120396].dbo.Oca_OldManConfigInfo smartlife_120396  on a.CallNo=smartlife_120396.CallNo left join [smartlife-120397].dbo.Oca_OldManConfigInfo smartlife_120397  on a.CallNo=smartlife_120397.CallNo left join [smartlife-120398].dbo.Oca_OldManConfigInfo smartlife_120398  on a.CallNo=smartlife_120398.CallNo left join [smartlife-120399].dbo.Oca_OldManConfigInfo smartlife_120399  on a.CallNo=smartlife_120399.CallNo ) x where x.checkresult<>1
 