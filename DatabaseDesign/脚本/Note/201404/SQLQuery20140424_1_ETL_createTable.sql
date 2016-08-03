declare @desttabname varchar(100),@destcolname varchar(190) ,@str nvarchar(400)
set @destcolname='CheckInTime'
set @desttabname='Bas_ResidentBaseInfoStep2'
set @str='[dbo].[sp_etl_column_'+@desttabname+N'_'+@destcolname+N']'
exec(@str)

select * from Oca_OldManBaseInfo
select a.*,b.policy_name from t_deploy_bridge a left join t_deploy_configure b
on a.id=b.id;
select * from t_deploy_configure

select a.*,b.value from t_deploy_execution_log a left join t_deploy_configure b
on a.id=b.id where a.id<100;

create table  Cfg_ETL_OutputColumn
(
  Id         int ,
  Name        nvarchar(100),
  Description nvarchar(400),
  Precision   int ,
  Scale       int,
  Length      int,
  DataType    varchar(100),
  CodePage    int,
  MappedColumnId int,
  ExternalMetadataColumnId int,
  LineageId     int
);

create table  Cfg_ETL_InputColumn
(
  Id         int ,
  Name        nvarchar(100),
  Description nvarchar(400),
  Precision   int ,
  Scale       int,
  Length      int,
  DataType    varchar(100),
  CodePage    int,
  MappedColumnId int,
  ExternalMetadataColumnId int,
  UsageType  int,
  LineageId int
);

create table Cfg_ETL_ExternalMetadataColumn
 ( 
  Id         int ,
  Name        nvarchar(100),
  Description nvarchar(400),
  Precision   int ,
  Scale       int,
  Length      int,
  DataType    varchar(100),
  CodePage    int,
  MappedColumnId int
);

create table Cfg_ETL_Package
(
  CreatorName   nvarchar(100),
  CreatorComputerName nvarchar(100),
  CreationDate    datetime,
  PackageType   int,
  EnableConfig  bit
);

create table Cfg_ETL_Component 
(
     Id    int,
     Name  nvarchar(100),
    OpenRowSets  nvarchar(100),
    SqlCommand  nvarchar(4000)
);

create table Cfg_ETL_TypeMapping
(
SourceDataType varchar(100),
DestinationDataType varchar(100),
FromToDatabaseId  int
);

create table Cfg_ETL_TypeMappingFromToDatabase
(
FromToDatabaseId int,
SourceType  varchar(400),
MinSourceVersion varchar(10),
MaxSourceVersion varchar(10),
DestinationType  varchar(400),
MinDestinationVersion varchar(10),
MaxDestinatoinVersion varchar(10)
);

create table Cfg_ETL_TypeConvert
(
SourceType  varchar(400),
DestinationType  varchar(400),
IsImplicitConversion bit
);

exec SP_ETL_DataStreamTask

select * from Cfg_ETL_TranslateRule;


USE msdb ;
GO

EXEC dbo.sp_start_job N'backup_database' ;
GO

EXEC dbo.sp_stop_job N'backup_database' ;
GO
