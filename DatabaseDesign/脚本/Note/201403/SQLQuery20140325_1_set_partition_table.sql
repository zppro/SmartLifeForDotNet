------------查询目录视图，了解系统内已有的文件组，文件，数据空间。
select *
from sys.filegroups 
select *
from sys.database_files;
select * 
from sys.data_spaces;

--------加文件组
alter database smartlife add filegroup test1fg;

declare @i int,@str nvarchar(4000)
set @i=0
set @str=''
while (select @i)<20
begin
set @i=@i+1
print @i
set @str='alter database smartlife add filegroup test'+cast(@i as nvarchar(10))+'fg;'
exec sp_executesql @str
end

-----------------为文件组加上文件
ALTER DATABASE smartlife 
ADD FILE 
(
    NAME = Test1dat2,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\100\t1dat2.mdf',
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
) TO FILEGROUP test1fg
;

declare @i int,@str nvarchar(4000)
set @i=0
set @str=''
while (select @i)<20
begin
set @i=@i+1
print @i
set @str='ALTER DATABASE smartlife 
ADD FILE 
(
    NAME = Test1dat2,
    FILENAME =''C:\Program Files\Microsoft SQL Server\100\t'+cast(@i as nvarchar(10))+'dat2.mdf'',
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
) TO FILEGROUP test'+cast(@i as nvarchar(10))+'fg
;'
exec sp_executesql @str
end

/*------------
创建已分区表
以下示例创建一个分区函数，将表或索引分为四个分区。然后，此示例创建用于指定保存四个分区
的文件组的分区架构。最后，此示例创建使用此分区架构的表。此示例假定数据库中已经存在文件组。

 复制代码 */
CREATE PARTITION FUNCTION myRangePF1 (int)
    AS RANGE LEFT FOR VALUES (1, 100, 1000) ;
GO

CREATE PARTITION SCHEME myRangePS1
    AS PARTITION myRangePF1
    TO (test1fg, test2fg, test3fg, test4fg) ;
GO

CREATE TABLE PartitionTable (col1 int, col2 char(10))
    ON myRangePS1 (col1) ;


CREATE TABLE [dbo].[zping.com](
 
    [id] [varchar](32) NOT NULL,
 
    [sid] int,
 
    [hashid] AS (checksum([id])) PERSISTED
 
)
 
ON [zping.com.Ps] ([hashid])



消息 1908，级别 16，状态 1，第 3 行
列 'Id' 是索引 'PK_Oca_OldManBaseInfoPartition' 的分区依据列。唯一索引的分区依据列必须是索引键的子集。
消息 1750，级别 16，状态 0，第 3 行
无法创建约束。请参阅前面的错误消息。


消息 7726，级别 16，状态 1，第 1 行
分区依据列 'OldManId' 的数据类型 uniqueidentifier 与分区函数 'myRangePF1' 的参数数据类型 int 不同。
消息 1750，级别 16，状态 0，第 1 行
无法创建约束。请参阅前面的错误消息。

为聚集索引 'PK_Oca_OldManBaseInfoPartition' 指定的 文件组'PRIMARY' 用于表 'dbo.Oca_OldManBaseInfoPartition'，
虽然为它指定了 分区方案 'myRangePS1'。

-------------------------------------------------------------------------------------------------------------------------

如果经常执行的查询涉及两个或多个已分区表之间的同等联接，则这些查询的分区依据列应该与联接表所基于的列相同。
另外，应当已配置了这些表或它们的索引。这意味着它们要么使用同一命名分区函数，要么使用在以下方面基本相同的不同函数：
?
具有相同数量的用于进行分区的参数，对应的参数具有相同的数据类型。

?
定义了相同数量的分区。

?
定义了相同的分区边界值。


这样一来，SQL Server 查询优化器就可以更快地处理联接，因为分区可以自行联接。如果查询联接的是联接字段未配置
或未分区的两个表，则分区的出现实际上会降低而不是提高查询处理速度。


不仅仅是数字或者日期类型，即使是字符串类型的列，也可以按照字母顺序进行分区。而以下类型的列不可用于分区：
text、ntext、image、xml、timestamp、varchar(max)、nvarchar(max)、varbinary(max)、别名、hierarchyid、
空间索引或 CLR 用户定义的数据类型。此外，如果使用计算列作为分区列，则必须将该列设为持久化列（Persisit）。

在列表下方，提供了两个选项：
1.分配到可用分区表： 
这要求在同一数据库下有另一张已分好区的表，同时该表的分区列和当前选中的列的类型完全一致。 
这样的好处是当两张表在查询中有关联时，并且其关联列就是分区列时，使用同样的分区策略会更有效率。 
2.将非唯一索引和唯一索引的存储空间调整为与索引分区列一致： 
这样会将表中的所有索引也一同分区，实现“对齐”。这是一个重要而麻烦的选项，具体需求请参阅MSDN（已分区索引的特殊指导原则）。 
这样的好处是表和索引的分区一致，一方面查询时利用索引更为高效，而且在下文提到的移入移出分区也会更为高效。 

注意：这里建议使用聚集索引列作为分区列。一方面索引结构本身就应与查询相关，那么分区列与索引一致会保证查询的最大效率；
另一方面，保证索引对齐而且是聚集索引对齐是保证分区的移入移出操作顺畅的前提，否则可能会出现无法移入移出的情况，而
分区的移入移出又是管理大数据的重要策略——滑动窗口（SlideWindow）策略的基础操作。另外，如果要进行索引对齐，需要
所有索引和表的压缩模式一致。


通过拆分或合并边界值更改分区函数。通过执行 ALTER PARTITION FUNCTION，可以将使用分区函数的任何表或索引的某个分区拆分为两个分区，也可以将两个分区合并为一个分区。

注意： 
多个表或索引可以使用同一分区函数。ALTER PARTITION FUNCTION 在单个事务中影响所有这些表或索引。 

ALTER PARTITION FUNCTION 在单个原子操作中对使用该函数的任何表和索引进行重新分区。但该操作在脱机状态下进行，并且根据重新分区的范围，可能会消耗大量资源。

ALTER PARTITION FUNCTION 只能用于将一个分区拆分为两个分区，或将两个分区合并为一个分区。若要更改其他情况下对表进行分区方法（例如，将 10 个分区合并为 5 个分区），可以尝试使用以下任何选项。根据系统配置，这些选项可能在资源消耗方面有所不同：

使用所需的分区函数创建一个新的已分区表，然后使用 INSERT INTO...SELECT FROM 语句将旧表中的数据插入新表。


为堆创建分区聚集索引。

注意： 
删除已分区的聚集索引将产生分区堆。
 


通过将 Transact-SQL CREATE INDEX 语句与 DROP EXISTING = ON 子句一起使用来删除并重新生成现有的已分区索引。


执行一系列 ALTER PARTITION FUNCTION 语句。


ALTER PARITITION FUNCTION 所影响的全部文件组都必须处于联机状态。

如果使用分区函数的任何表中存在已禁用的聚集索引，ALTER PARTITION FUNCTION 都将失败。

SQL Server 2008 不对修改分区函数提供复制支持。必须在订阅数据库中手动应用对发布数据库中的分区函数的更改。


select *
from sys.filegroups 
select *,
reverse(substring(reverse(physical_name),charindex('\',reverse(physical_name),0),LEN(physical_name)))
from sys.database_files where data_space_id=1;
select * 
from sys.data_spaces ;

select 
D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\

select @filePath=
reverse(substring(reverse(physical_name),charindex('\',reverse(physical_name),0),LEN(physical_name)))
from sys.database_files where data_space_id=1;

select * 
from sys.partitions
where object_id in(select object_id from sys.tables where name='Bas_ResidentBaseInfo')

select * 
from  sys.partition_range_values

ALTER PARTITION FUNCTION OldManBaseInfoPF1 ()
SPLIT RANGE ('01201');
ALTER PARTITION FUNCTION OldManBaseInfoPF1 ()
MERGE RANGE ('01200');

ALTER PARTITION FUNCTION OldManFamilyInfoPF1 ()
SPLIT RANGE (30);

ALTER PARTITION SCHEME OldManFamilyInfoPS1
NEXT USED test31fg;