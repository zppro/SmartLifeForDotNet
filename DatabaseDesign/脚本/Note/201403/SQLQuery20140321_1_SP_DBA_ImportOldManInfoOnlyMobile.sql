
/***************************************************************************/
/*  SP_DBA_CDC_AppUse */
/*  设置数据库及数据表的变化数据捕捉的特性 为启用，默认状态为禁用          */
/*  @tableName 需要设*/
/***************************************************************************/
Alter procedure SP_DBA_CDC_AppUse
@tableName varchar(255)
as
begin 
	declare @is_enable bit

	select @is_enable=is_cdc_enabled
	from sys.databases
	where name=db_name();

	if(@is_enable=0)
    begin
          EXEC sp_changedbowner 'sa';
          EXEC sys.sp_cdc_enable_db;
    end

	EXECUTE sys.sp_cdc_enable_table
    @source_schema = N'dbo' 
  , @source_name = @tableName
  , @role_name = N'CDC_Admin'--可以自动创建
  , @capture_instance=Default,
    @supports_net_changes = 0
 end; 
 
 
  select * from cdc.dbo_Oca_OldManBaseInfo_CT;
  select * from Oca_CallService;
  update Oca_OldManBaseInfo
  set OperatedOn=GETDATE()
  where ID=26231
  select * from Oca_OldManBaseInfo;
 exec  dbo.SP_DBA_CDC_AppUse 'Oca_CallService'
 
 sys.sp_cdc_cleanup_change_table 'dbo_Oca_CallService'
 
 exec sys.sp_cdc_disable_table  @source_schema = N'dbo' 
  , @source_name = 'Oca_CallService' 
  , @capture_instance='dbo_Oca_CallService'
  select * from cdc.change_tables
  
  	EXECUTE sys.sp_cdc_enable_table
    @source_schema = N'dbo' 
  , @source_name = 'Oca_CallService'
  , @role_name = N'CDC_Admin'--可以自动创建
  --, @capture_instance=Default
  ,@supports_net_changes = 0
  
  select * from pub_area;
  
  update pub_area
  set AreaName='蚕桑社区2'
  where ID=86  
  update pub_area
  set AreaName='蚕桑社区'
  where ID=86
  
  select * from Oca_OldManBaseInfo
  
  update Oca_OldManBaseInfo
  set OperatedOn=GETDATE()
  where ID=6537
  
  select * from cdc.dbo_Oca_OldManBaseInfo_CT;
  select * from dbo.sheetSource;
  
  
 go 
  CREATE    PROCEDURE [dbo].[SP_Cfg_GetObjectPrimaryKey]
@source_dbName nvarchar(100)
 AS
 BEGIN
      SET NOCOUNT ON
      DECLARE  @sql_str nvarchar(4000)  ,@databasename nvarchar(100)
       select @databasename=''''+replace(REPLACE(@source_dbName,'[',''),']','')+''''

        IF (EXISTS (select * from Cfg_ObjectPrimaryKey ))
             BEGIN
               delete from Cfg_ObjectPrimaryKey;        
             END
        IF (NOT EXISTS (select * from Cfg_ObjectPrimaryKey))
             BEGIN
           set  @sql_str='
                 insert into Cfg_ObjectPrimaryKey (databasename,username,objectname,columnname,type)
                        select '+@databasename+'  as  databasename,(select name from sys.schemas where SCHEMA_ID= m.schema_id) userName,
                           isnull((select name from '+@source_dbName+'.sys.tables where object_id=m.parent_object_id ),
                          (select name from '+@source_dbName+'.sys.table_types where parent_object_id=m.parent_object_id)
                           ) objectname,
                           dbo.joinStr(ac.name) columnname
                           , isnull((select object_id%10 from '+@source_dbName+'.sys.tables where object_id=m.parent_object_id ),-1) type
                        from    '+@source_dbName+'.sys.key_constraints m ,
                                '+@source_dbName+'.sys.index_columns ic,
                                '+@source_dbName+'.sys.all_columns ac
                        where   m.type=''PK'' 
                                and m.parent_object_id=ic.object_id 
                                and m.parent_object_id=ac.object_id 
                                and ic.index_column_id=ac.column_id 
                                group by m.schema_id,m.parent_object_id;'
                   exec sp_executesql @sql_str
             END
 END
 
 select * from Dey_Script;
 
 
 select COUNT(*) ,callno
 from Oca_OldManConfigInfo
 where CallNo is not  null
 group by callno
 having COUNT(*)>1;
 
 select f2
  from sheet51$ 
 where f2 not in
 (
			select callno
			from remote_dbo.[Smartlife-120303].dbo.Oca_OldManConfigInfo
			where CallNo is not  null  
 )
 
 select callno,OldManId,(select OldManName from dbo.Oca_OldManBaseInfo where OldManId=a.OldManId) name
 ,(select idno from dbo.Oca_OldManBaseInfo where OldManId=a.OldManId) id
			from dbo.Oca_OldManConfigInfo a			
			where   a.callno in (select b.f2 
			from  Sheet51$ b)
			
			
select * from Oca_OldManBaseInfo;

select f1 from sheet49$;

--------------------------------------
/****************************************************************************/
/**  SP_DBA_ImportOldManInfoOnlyMobile                                     **/
/*   加老人信息，只有手机的情况下。                                         */
/****************************************************************************/
create procedure SP_DBA_ImportOldManInfoOnlyMobile
as 
begin
insert into Oca_OldManBaseInfo ( OldManId, CheckInTime,Status,DataSource, OldManName,
 Gender, IDNo, HealthInsuranceFlag, SocialInsuranceFlag,
 LivingState,AreaId,AreaId2,Mobile,Remark)
 
select NEWID() OldManId,getdate() CheckInTime,1 Status,'00003' DataSource,'老人('+f2+')' OldManName,
'M' Gender,'330102193001011234' IDNo,0 HealthInsuranceFlag,0 SocialInsuranceFlag,
'00001' LivingState,'01191' AreaId,'AAA01191-0001-0000-0000-000000000000' AreaId2,f2 Mobile,'未知老人' Remark
 from Temp_Mobile
 where f2 not in
 (
			select callno
			from dbo.Oca_OldManConfigInfo
			where CallNo is not  null  
 ) and f2 not IN (select mobile from Oca_OldManBaseInfo where Mobile is not null);
 ------------------------------------------
 ----select * into Temp_Mobile from sheet51$;
 
 insert into   Oca_OldManConfigInfo  (OldManId,CheckInTime,DataSource,CallNo,LocateFlag,GovTurnkeyFlag)
         select oldmanid,GETDATE() checkintime,'00003' DataSource,mobile callno,0 locateflag,0 govturnkeyflag
         from   Oca_OldManBaseInfo 
         where 
         Remark='未知老人'  and mobile not in (select callno from Oca_OldManConfigInfo where CallNo is not null );
 end        
         -----------------------------------------------------
 
remote_dbo.[Smartlife-120303].

select   dbo.joinStr(name)
from    sys.all_columns 
where  object_id   in 
            (   select  object_id 
                from    sys.tables 
               where  name='Oca_OldManBaseInfo'
             )
