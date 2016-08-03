select * from dbo.Cfg_ETL_ConnectManager_V

insert into Cfg_ETL_ConnectManager (Name,Description,ConnectString,Abbr)values('impdest','导入目标库连接器','-U sa -P 1,leblue@2013 -S 115.236.175.109,10016','BCP');

update Cfg_ETL_ConnectManager set ConnectString='-U sa -P 1,leblue@2013 -S 115.236.175.109,10016 -d WeiXin-12 ' where ID=3

select @@DATEFIRST 
select @@DBTS;
select @@SPID;

create table Cfg_ETL_Variable
(
EvaluateAsExpression bit default(1) not null,----1 为TRUE，值由表达式确定，手工赋值无效。
Description         varchar(max) null,
Expression          varchar(100) null,
Name               nvarchar(100) null,
Namespace           varchar(50) null,
RaiseChangedEvent  bit default(1) not null,---1 为TRUE ，每次变量更新，都会触发事件。
Scope               varchar(100),
Value              varchar(100),
ValueType           varchar(10)
)


create table Cfg_ETL_ColumnNullRatio
(
TableName   varchar(100) not null,
ColumnName  varchar(100) not null,
Value      float not null
);

create table Cfg_ETL_ColumnStatistics
(

)

create table Cfg_ETL_ColumnValueDistribution
(

)

create table Cfg_ETL_ColumnLengthDistribution
(

)

create table Cfg_ETL_ColumnMode
(

)

create table Cfg_ETL_SelectingKey
(

)

create table Cfg_ETL_FunctionDepenceRation
(

)


insert into Cfg_ETL_Variable (Description,Expression,Name,Namespace,Scope,Value,ValueType)values('取距今多少天的数据，天数','1','DiffDaysToToday','user','package','16','int');

insert into Cfg_ETL_Variable (Description,Expression,Name,Namespace,Scope,Value,ValueType)values('该值表示每周的第一天','1','DATEFIRST','system','package','7','int');

insert into Cfg_ETL_Variable (Description,Expression,Name,Namespace,Scope,Value,ValueType)values('返回当前数据库，当前timestamp的类型值','1','DBTS','system','package','2000','int');

insert into Cfg_ETL_Variable (Description,Expression,Name,Namespace,Scope,Value,ValueType)values('取距今多少天的数据，天数','1','SPID','user','package','16','int');

insert into Cfg_ETL_Variable (Description,Expression,Name,Namespace,Scope,Value,ValueType)values('取距今多少天的数据，天数','1','DiffDaysToToday','user','package','16','int');
select value,* from Cfg_ETL_Variable where Name='DiffDaysToToday';

select * from remote_dbo.[smartlife-120301].dbo.oca_oldmanbaseinfo
where DATEDIFF(day,checkintime,getdate())='6';

update Cfg_ETL_Variable set Value=10 where Name='DiffDaysToToday';

exec SP_ETL_ExpData @tablename='[Smartlife-1203].dbo.Bas_ResidentBaseInfo'
exec SP_ETL_ExpData @tablename='[smartlife-120301].dbo.oca_oldmanbaseinfo'
select top 10 * from remote_dbo.[smartlife-1203].dbo.Bas_ResidentBaseInfo order by CheckInTime desc

drop table oca_oldmanbaseinfostep1 ;
select * into oca_oldmanbaseinfostep1 
		from remote_dbo.[smartlife-120301].dbo.oca_oldmanbaseinfo
		where 1=0


消息 117，级别 15，状态 1，第 3 行
对象 名称 'remote_dbo.smartlife-120301.dbo.dbo.oca_oldmanbaseinfo' 包含的前缀超出了最大限值。最多只能有 3 个。

select * from oca_oldmanbaseinfostep1


select COUNT(idno),SUBSTRING(name,1,1) from Oca_OldMan
group by SUBSTRING(name,1,1)
having COUNT(idno)>1
order by COUNT(idno) desc


select @@options
select @@FETCH_STATUS;
select @@CURSOR_ROWS;
select @@PROCID;
select @@ERROR;
select @@IDENTITY;
select @@ROWCOUNT;
select @@TRANCOUNT;
select @@NESTLEVEL;