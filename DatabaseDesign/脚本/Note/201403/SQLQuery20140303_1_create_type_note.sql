select name,type_table_object_id from sys.table_types where type_table_object_id='565577053' order by name desc ;
select name from sys.table_types  order by name desc ;
select type_table_object_id from sys.table_types where name='JSONHierarchy'
select object_id from sys.objects where type='TT' order by name desc ;

565577053
TT_JSONHierarchy_21B6055D



 
 select b.name+'  '+(select a.name from sys.types  a where a.user_type_id=b.user_type_id)+
case  b.user_type_id when 167 then replace('('+CAST (b.max_length as nvarchar(32))+')','-1','max')
when 175 then '('+CAST (b.max_length as nvarchar(32))+')'
when 231 then  '('+replace(left(cast(b.max_length/2 as nvarchar(32)),1),'0','max')+SUBSTRING(cast(b.max_length/2 as nvarchar(32)),2,LEN(cast(b.max_length/2 as nvarchar(32))))+')' else '' end +' '
+case b.is_nullable when 0 then ' not null' when 1 then ' null' else '' end +' ,' as columnname ,b.column_id,b.object_id,b.default_object_id
from sys.all_columns b where object_id=565577053

select * from sys.types;


 
 select m.object_id,m.schema_id,m.parent_object_id,ac.name+',' columnname
from sys.key_constraints m ,
sys.index_columns ic,sys.all_columns ac
 where m.type='PK' and m.parent_object_id=(select type_table_object_id from sys.table_types where name='JSONHierarchy' )
and m.parent_object_id=ic.object_id and m.parent_object_id=ac.object_id and ic.index_column_id=ac.column_id

 select * from sys.index_columns where object_id=565577053
 
  select * from sys.all_columns where object_id=565577053
  
  select * from remote_dbo.[Smartlife-120300].dbo.sysobjects where xtype='U';
  select * from sys.all_objects where name ='JoinStr' type_desc= 'AGGREGATE';
  
  select * from t_deploy_script where OBJECT_NAME='JoinStr';
  select * from t_deploy_object where table_name='JSONHierarchy'
  
  create type  [dbo].JSONHierarchy as table (element_id  int  not null ,parent_ID  int  null ,Object_ID  int  null ,NAME  varchar(2000)  null ,StringValue  varchar(max)  not null ,ValueType  varchar(10)  not null ,CONSTRAINT  PK_JSONHierarchy  PRIMARY KEY CLUSTERED  (element_id));
  
  create type  [dbo].JSONHierarchy as table (element_id  int  not null ,parent_ID  int  null ,Object_ID  int  null ,NAME  varchar(2000)
  
  
    null ,StringValue  varchar(max)  not null ,ValueType  varchar(10)  not null ,  PRIMARY KEY CLUSTERED  (element_id));
    
     select m.object_id,m.schema_id,m.parent_object_id,ac.name+',' columnname
from sys.key_constraints m ,
sys.index_columns ic,sys.all_columns ac
 where m.type='PK' and m.parent_object_id=(select type_table_object_id from sys.table_types where name='JSONHierarchy' )
and m.parent_object_id=ic.object_id and m.parent_object_id=ac.object_id and ic.index_column_id=ac.column_id 


create type  [dbo].JSONHierarchy as table (element_id  int  not null ,parent_ID  int  null ,Object_ID  int  null ,NAME  varchar(2000)  null ,StringValue  varchar(max)  not null ,ValueType  varchar(10)  not null ,  PRIMARY KEY CLUSTERED  (element_id));           

declare @source_database varchar(4000)
declare @dest_database varchar(4000)
set @source_database='remote_dbo.[smartlife-120300]'
set @dest_database='[smartlife-120304]'
select e.name,e.lengths
from (
select a.name,LEN(b.definition) lengths
 from remote_dbo.[smartlife-120300].sys.all_objects a,remote_dbo.[smartlife-120300].sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id) e
where e.name not in (
select a.name
 from [smartlife-120304].sys.all_objects a,[smartlife-120304].sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id
);      


select e.name,e.lengths
from (
select a.name,LEN(b.definition) lengths
 from remote_dbo.[smartlife-120300].sys.all_objects a,remote_dbo.[smartlife-120300].sys.all_sql_modules b 
where a.type in ('V') and a.object_id>0 and a.object_id=b.object_id) e
where e.name not in (
select a.name
 from [smartlife-120304].sys.all_objects a,[smartlife-120304].sys.all_sql_modules b 
where a.type in ('V') and a.object_id>0 and a.object_id=b.object_id
);  

select e.name,e.lengths
from (
select a.name,LEN(b.definition) lengths,a.modify_date
 from remote_dbo.[smartlife-120300].sys.all_objects a,remote_dbo.[smartlife-120300].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) e
where e.name not in (
select a.name
 from [smartlife-120304].sys.all_objects a,[smartlife-120304].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id
);    

select e.name,e.lengths,f.lengths
from (
select a.name,LEN(b.definition) lengths
  from remote_dbo.[smartlife-120300].sys.all_objects a,remote_dbo.[smartlife-120300].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) e,
 (
select a.name,LEN(b.definition) lengths
 from [smartlife-120304].sys.all_objects a,[smartlife-120304].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) f
where  e.name=f.name and e.lengths<>f.lengths

select e.name,e.lengths,f.lengths
from (
select a.name,LEN(b.definition) lengths
  from remote_dbo.[smartlife-120300].sys.all_objects a,remote_dbo.[smartlife-120300].sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id) e,
 (
select a.name,LEN(b.definition) lengths
 from [smartlife-120304].sys.all_objects a,[smartlife-120304].sys.all_sql_modules b 
where a.type in ('P') and a.object_id>0 and a.object_id=b.object_id) f
where  e.name=f.name and e.lengths<>f.lengths

select name from remote_dbo.[smartlife-120300].sys.tables where name not in (select name from [smartlife-120304].sys.tables );

select * from sys.login_token where name='dbo' and type='SQL LOGIN';

select * from sys.linked_logins
select * from sys.servers where name='remote_dbo';
SELECT *
            FROM dbo.sysobjects
            WHERE (name = N'SP_IPCC_CallIn')
              AND (type = 'P')
              
             select b.definition,LEN(b.definition) from sys.all_sql_modules  b where object_id= 1693249087
             
             select substring(idno,7,4)+'-'+SUBSTRING(idno,11,2)+'-'+SUBSTRING(idno,13,2) from Oca_OldManBaseInfo
              
              select CHARINDEX('.','remotedbo.[]',0)
              select SUBSTRING('remotedbo.[]',0,10)
              
              
              
                                                                                                                                                         
  
     update Oca_OldManBaseInfo set Oca_OldManBaseInfo.Birthday=oldmaninfo.birthday 
     
     select a.OldManId,a.Birthday,oldmaninfo.OldManId,oldmaninfo.birthday
     from Oca_OldManBaseInfo a,
     (select oldmanid,substring(idno,7,4)+'-'+SUBSTRING(idno,11,2)+'-'+SUBSTRING(idno,13,2) birthday 
     from Oca_OldManBaseInfo) oldmaninfo
      where oldmaninfo.OldManId=a.OldManId