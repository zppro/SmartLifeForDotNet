/***********************************************************************/
/*   SP_DBA_PartitionCreateFunction             */
/*  创建分区函数 */
/***********************************************************************/
create procedure SP_DBA_PartitionCreateFunction
@databasename varchar(100)
as
begin
     CREATE PARTITION FUNCTION   OldManBaseInfoPF1 (char(5))
    AS RANGE LEFT FOR VALUES ('01189',
'01190',
'01191',
'01192',
'01193',
'01194',
'01195',
'01196',
'01197',
'01198',
'01199',
'01200') ;

CREATE PARTITION FUNCTION   OldManFamilyInfoPF1 (int)
    AS RANGE LEFT FOR VALUES (0,
1,
2,
3,
4,
5,
6,
7,
8,
9,
10,
11,
12,
13,
14,
15,
16,
17,
18,
19,
20,
21,
22,
23,
24,
25,
26,
27,
28,
29) ;


end
