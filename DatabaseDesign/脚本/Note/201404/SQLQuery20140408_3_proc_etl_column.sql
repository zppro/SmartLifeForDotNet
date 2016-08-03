/************************************************************/
/* sp_etl_column */
/* 更新表列 */
/************************************************************/
alter procedure sp_etl_column
@sourcetabname varchar(100),
@desttabname   varchar(100),
@sourcecolname varchar(100),
@destcolname   varchar(100),
@sourcejoincolname varchar(100),
@destjoincolname   varchar(100)
as 
begin
declare @sqlStr nvarchar(4000),@expression varchar(1000)
set @expression=dbo.func_etl_GetMapExpression(@sourcetabname,@sourcecolname)
set @sqlStr='update '+@desttabname+'
set '+@destcolname+'=(case b.'+@sourcecolname+' when ''男'' then ''M'' when ''女'' then ''F'' else  ''M'' end)
from '+@desttabname+' a,'+@sourcetabname+' b
where a.'+@destjoincolname+'=b.'+@sourcejoincolname+';'

print '--'+@sqlStr
--exec sp_executesql @sqlStr

end

-------------------------------------------------------=====================
exec sp_etl_column @sourcetabname='dbo.Sheet65$',@desttabname='dbo.temp_excelsource',@sourcecolname='F12',
@destcolname='tel',@sourcejoincolname='F4',@destjoincolname='idno'


create table Cfg_ETL_TranslateRule
(
sourcetablename  varchar(100) null,
sourcecolumnname varchar(100) null,
desttablename    varchar(100) null,
destcolumnname   varchar(100) null,
oldvalue         varchar(100) null,
newvalue         varchar(100) null,
mapExpression    varchar(100) null, 
type             int  null,
typedest         varchar(100) null,
brokerTable      varchar(100) null,
brokerColumn     varchar(100) null,
brokerTargetColumn varchar(100) null
);


select *,' when '''+oldvalue+ ''' then '''+newvalue+''' ' from  Cfg_ETL_TranslateRule where sourcetablename='temp_excelsource' and type=3
update Cfg_ETL_TranslateRule set mapExpression=' when '''+oldvalue+ ''' then '''+newvalue+''' '
 where sourcetablename='temp_excelsource' and type=3;
 
select *,' (select '+brokerTargetColumn+ ' from  '+brokerTable+' where  '+brokerColumn+'='+sourcecolumnname+')' from  Cfg_ETL_TranslateRule 
where sourcetablename='temp_excelsource' and type=4
update Cfg_ETL_TranslateRule set mapExpression=' (select '+brokerTargetColumn+ ' from  '+brokerTable+' where  '+brokerColumn+'='+sourcecolumnname+')'
where sourcetablename='temp_excelsource' and type=4

update Cfg_ETL_TranslateRule set mapExpression='cast(cast( '+sourcecolumnname+ ' as  '+oldvalue+') as  '+newvalue+')'
where sourcetablename='temp_excelsource' and type=2

update Cfg_ETL_TranslateRule set type=3
update Cfg_ETL_TranslateRule set type=4 where brokerColumn is not null;
update Cfg_ETL_TranslateRule set oldvalue='bigint',newvalue='varchar(20)' where sourcecolumnname='mobile';







/********************************************************/
/* func_etl_GetMapExpression */
/* 获取映射的表达式 */
/********************************************************/
alter  function func_etl_GetMapExpression
(@tablename varchar(100),@columnname varchar(100))
returns varchar(1000)
as 
begin
declare @str varchar(100)
select top 1 @str=mapExpression from Cfg_ETL_TranslateRule
where sourcecolumnname='gender' and sourcetablename='temp_excelsource' and desttablename='oca_oldmanbaseinfo'
return @str
end

/********************************************************/
/* sp_etl_setmapexpression */
/* 设置映射的表达式 */
/********************************************************/
alter procedure sp_etl_setmapexpression
as 
begin

update Cfg_ETL_TranslateRule set mapExpression=' when '''+oldvalue+ ''' then '''+newvalue+''' '
 where sourcetablename='temp_excelsource' and type=3;

update Cfg_ETL_TranslateRule set mapExpression=' (select '+brokerTargetColumn+ ' from  '+brokerTable+' where  '+brokerColumn+'='+sourcecolumnname+')'
where sourcetablename='temp_excelsource' and type=4

update Cfg_ETL_TranslateRule set mapExpression='cast(cast( '+sourcecolumnname+ ' as  '+oldvalue+') as  '+newvalue+')'
where sourcetablename='temp_excelsource' and type=2

end 

exec sp_etl_setmapexpression