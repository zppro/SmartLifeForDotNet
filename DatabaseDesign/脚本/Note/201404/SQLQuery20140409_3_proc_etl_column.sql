select distinct sourcecolumnname,destcolumnname
from Cfg_ETL_TranslateRule where sourcetablename='temp_excelsource' and desttablename='oca_oldmanbaseinfo';

exec sp_etl_setmapexpression

select dbo.func_etl_GetMapExpression('temp_excelsource','gender','oca_oldmanbaseinfo');

exec sp_etl_column @sourcetabname='temp_excelsource',@desttabname='oca_oldmanbaseinfo',@sourcecolname='gender',
@destcolname='gender',@sourcejoincolname='idno',@destjoincolname='idno'

exec sp_etl_column @sourcetabname='temp_excelsource',@desttabname='oca_oldmanbaseinfo',@sourcecolname='livingstate',
@destcolname='livingstate',@sourcejoincolname='idno',@destjoincolname='idno'

exec sp_etl_column @sourcetabname='temp_excelsource',@desttabname='oca_oldmanbaseinfo',@sourcecolname='SocialInsuranceFlag',
@destcolname='SocialInsuranceFlag',@sourcejoincolname='idno',@destjoincolname='idno'

exec sp_etl_column @sourcetabname='temp_excelsource',@desttabname='oca_oldmanbaseinfo',@sourcecolname='areaid3',
@destcolname='areaid3',@sourcejoincolname='idno',@destjoincolname='idno'

exec sp_etl_column @sourcetabname='temp_excelsource',@desttabname='oca_oldmanbaseinfo',@sourcecolname='mobile',
@destcolname='mobile',@sourcejoincolname='idno',@destjoincolname='idno'

exec sp_etl_column @sourcetabname='temp_excelsource',@desttabname='oca_oldmanbaseinfo',@sourcecolname='status',
@destcolname='status',@sourcejoincolname='idno',@destjoincolname='idno'

exec sp_etl_column @sourcetabname='temp_excelsource',@desttabname='oca_oldmanbaseinfo',@sourcecolname='address',
@destcolname='address',@sourcejoincolname='idno',@destjoincolname='idno'

insert into Cfg_ETL_TranslateRule (sourcetablename,sourcecolumnname,desttablename,destcolumnname,oldvalue,newvalue,type)
values ('temp_excelsource',  'areaid'          ,'oca_oldmanbaseinfo' ,   'areaid'         , '01191','F' ,0 );


insert into Cfg_ETL_TranslateRule (sourcetablename,sourcecolumnname,desttablename,destcolumnname,oldvalue,newvalue,type)
values ('temp_excelsource',  'status'          ,'oca_oldmanbaseinfo' ,   'status'         , '1','F' ,0 );

insert into Cfg_ETL_TranslateRule (sourcetablename,sourcecolumnname,desttablename,destcolumnname,oldvalue,newvalue,type)
values ('temp_excelsource',  'checkintime'          ,'oca_oldmanbaseinfo' ,   'checkintime'         , 'getdate()','F' ,0 );

insert into Cfg_ETL_TranslateRule (sourcetablename,sourcecolumnname,desttablename,destcolumnname,oldvalue,newvalue,type)
values ('temp_excelsource',  'DataSource'          ,'oca_oldmanbaseinfo' ,   'DataSource'         , '''00003''','F' ,0 );

insert into Cfg_ETL_TranslateRule (sourcetablename,sourcecolumnname,desttablename,destcolumnname,oldvalue,newvalue,type)
values ('temp_excelsource',  'address'          ,'oca_oldmanbaseinfo' ,   'address'         , 'address','F' ,1 );

update Cfg_ETL_TranslateRule set oldvalue=0,newvalue=1,type=5
where sourcecolumnname='SocialInsuranceFlag' or sourcecolumnname='HealthInsuranceFlag'

update Cfg_ETL_TranslateRule set type=2
where sourcecolumnname='tel'

