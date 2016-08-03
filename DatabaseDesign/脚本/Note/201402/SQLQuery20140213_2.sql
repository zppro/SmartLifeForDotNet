SELECT name
                FROM master.dbo.sysdatabases
                WHERE (name = N'smartlife-120301')
                
                delete from dbo.t_deploy_object;
                delete from dbo.t_deploy_script;
                select * from dbo.t_deploy_object;
                select * from dbo.t_deploy_function;
                select * from dbo.t_deploy_script  where type='P';
                select * from t_deploy_error_log;
                select * from t_deploy_execution_log;
                select * from t_deploy_status_log;
                select * from t_deploy_bridge;
                
                select * from t_deploy_orderformat;
                
                select distinct version_id ,version_id_int from v_deploy_version_for_function  order by version_id_int
                
                alter table t_deploy_script alter column update_script nvarchar(4000) not null;
                alter table t_deploy_object alter column table_name nvarchar(60) not null;
                alter table t_deploy_bridge add  database_name varchar(20)  null;
                
                alter table t_deploy_orderformat add order_time datetime null;
                alter table t_deploy_orderformat add owner_persion nvarchar(20) null;
                alter table t_deploy_orderformat add response_persion nvarchar(20) null;
                alter table t_deploy_orderformat add do_persion nvarchar(20) null;
                alter table t_deploy_orderformat add start_time datetime null;
                alter table t_deploy_orderformat add finish_time datetime null;
                
                
                alter table t_deploy_orderformat add level int null;
                alter table t_deploy_orderformat add name nvarchar(30) null;
                alter table t_deploy_orderformat add content nvarchar(100) null;
                
                insert into t_deploy_orderformat (function_id,level,name,content)
                values (140201,1,'更改字典11013下的字典项目','11013下的itemid[01%]打头的数据修改为3项：家政清洁/水电家电维修/医疗保健服务');
                
                insert into t_deploy_orderformat (function_id,level,name,content)
                values (140201,1,'数据库备份计划','杭州服务器110和109的两个服务器上的业务数据库的备份计划');
               
                
                
                insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,smartlife-120300);
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120301');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120302');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120303');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120304');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120305');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120306');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120307');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120308');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120309');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120310');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120311');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120312');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120313');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120314');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120315');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120316');
insert into t_deploy_bridge (city,node_ip,date_time,version_id,node_id,database_name) values ('杭州西湖区','115.236.175.110',Getdate(),1,1,'smartlife-120317');
             
                create table [dbo].Sys_TreeItem (TreeId char(5)  not null  ,ItemId char(5)  not null  ,Id int  not null ,Status tinyint  not null ,ItemCode varchar (100)  null ,ItemName nvarchar(100)  null ,GenerateMode char(5)  not null ,GenerateContent nvarchar(0)  not null ,PrefixId varchar (30)  null ,Description nvarchar(400)  null ,OrderNo int  not null ,CONSTRAINT  PK_Sys_TreeItem  PRIMARY KEY CLUSTERED  (ItemId, TreeId));
                create table [dbo].Sys_TreeItem (TreeId char(5)  not null  ,ItemId char(5)  not null  ,Id int  not null ,Status tinyint  not null ,ItemCode varchar (100)  null ,ItemName nvarchar (1maxmax)  null ,GenerateMode char(5)  not null ,GenerateContent nvarchar (max)  not null ,PrefixId varchar (30)  null ,Description nvarchar (4maxmax)  null ,OrderNo int  not null ,CONSTRAINT  PK_Sys_TreeItem  PRIMARY KEY CLUSTERED  (ItemId, TreeId));
                create table [dbo].Sys_TreeItem (TreeId char(5)  not null  ,ItemId char(5)  not null  ,Id int  not null ,Status tinyint  not null ,ItemCode varchar(100)  null ,ItemName nvarchar(100)  null ,GenerateMode char(5)  not null ,GenerateContent nvarchar(max)  not null ,PrefixId varchar(30)  null ,Description nvarchar(400)  null ,OrderNo int  not null ,CONSTRAINT  PK_Sys_TreeItem  PRIMARY KEY CLUSTERED  (ItemId, TreeId));
                create table [dbo].Sys_Tree (TreeId char(5)  not null  ,Id int  not null ,Status tinyint  not null ,CodeClass char(5)  null ,TreeType char(5)  not null ,LoadMode char(5)  not null ,TreeCode varchar (30)  not null ,TreeName nvarchar(100)  not null ,TreeParamFormat nvarchar(500)  null ,RootNodeGenerateMode tinyint  not null ,RootNodeContent varchar (1000)  null ,RootNodeFormat nvarchar(30)  null ,RootNodeIconCls nvarchar(30)  null ,Description nvarchar(400)  null ,CONSTRAINT  PK_Sys_Tree  PRIMARY KEY CLUSTERED  (TreeId));
                create table [dbo].Sys_Server (ServerId char(5)  not null  ,Id int  not null ,Status tinyint  not null ,CheckInTime datetime  not null ,ServerCode varchar (100)  not null ,ServerName nvarchar(100)  not null ,IpAddress varchar (100)  not null ,CONSTRAINT  PK_Sys_Server  PRIMARY KEY CLUSTERED  (ServerId));
                create table [dbo].Sys_Platform (PlatformId char(3)  not null  ,Id int  not null ,Status tinyint  not null ,PlatformCode varchar (100)  not null ,PlatformName nvarchar(100)  not null ,AliasName nvarchar(100)  null ,Picture varchar (200)  null ,OrderNo int  not null ,Description nvarchar(400)  null ,CONSTRAINT  PK_Sys_Platform  PRIMARY KEY CLUSTERED  (PlatformId));
                create table [dbo].Sys_Server (ServerId char(5)  not null  ,Id int  not null ,Status tinyint  not null ,CheckInTime datetime  not null ,ServerCode varchar (100)  not null ,ServerName nvarchar(100)  not null ,IpAddress varchar (100)  not null ,CONSTRAINT  PK_Sys_Server  PRIMARY KEY CLUSTERED  (ServerId));
        select         len('create table [dbo].Sys_Parameter (ParameterId varchar (100)  not null  ,Id int  not null ,Status tinyint  not null ,CodeClass char(5)  null ,ParameterName nvarchar(100)  not null ,Description nvarchar(400)  null ,ParameterValue varchar (200)  null ,CONSTRA')
        INT  PK_Sys_Parameter  PRIMARY KEY CLUSTERED  (ParameterId));
        
      select b.definition ,len(b.definition) from   sys.all_sql_modules b where len(b.definition)>7999
      
      select replace(left(cast(CHARACTER_OCTET_LENGTH/2 as nvarchar(32)),1),'0','max')+SUBSTRING(cast(CHARACTER_OCTET_LENGTH/2 as nvarchar(32)),2,LEN(cast(CHARACTER_OCTET_LENGTH/2 as nvarchar(32))))
       from INFORMATION_SCHEMA.COLUMNS where table_name = 'Sys_TreeItem' and DATA_TYPE='nvarchar'
      
  insert into dbo.t_deploy_object (database_name,user_name,table_name,object_type,change_type,version_id,function_id)
select DB_NAME() as database_name,user_name() as user_name,
SUBSTRING(recover_script,CHARINDEX('PROCEDURE',recover_script)+10,len(recover_script)-CHARINDEX('PROCEDURE',recover_script)-9) as table_name,
'Procedure' object_type,
'create' change_type,
--cast(id as nvarchar(32))+'_'+cast(version_id as nvarchar(32)) 
version_id,
 '3' as function_id from dbo.t_deploy_script where TYPE='P';
 
 select LEN(' FUNC_Tol_GetDictionaryItemIdByItemNameWithParentId')
 
 drop PROCEDURE sp_upgraddiagrams