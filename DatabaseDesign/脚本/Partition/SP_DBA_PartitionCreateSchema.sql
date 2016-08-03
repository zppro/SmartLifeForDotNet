/***********************************************************************/
/*   SP_DBA_PartitionCreateSchema             */
/*  创建分区模式 */
/***********************************************************************/
create procedure SP_DBA_PartitionCreateSchema
@databasename varchar(100)
as
begin

CREATE PARTITION SCHEME OldManBaseInfoPS1
    AS PARTITION OldManBaseInfoPF1
    TO (test1fg,
test2fg,
test3fg,
test4fg,
test5fg,
test6fg,
test7fg,
test8fg,
test9fg,
test10fg,
test11fg,
test12fg,
test13fg,
test14fg,
test15fg,
test16fg,
test17fg,
test18fg,
test19fg,
test20fg,
test21fg) ;


CREATE PARTITION SCHEME OldManFamilyInfoPS1
    AS PARTITION OldManFamilyInfoPF1
    TO (test1fg,
test2fg,
test3fg,
test4fg,
test5fg,
test6fg,
test7fg,
test8fg,
test9fg,
test10fg,
test11fg,
test12fg,
test13fg,
test14fg,
test15fg,
test16fg,
test17fg,
test18fg,
test19fg,
test20fg,
test21fg,
test22fg,
test23fg,
test24fg,
test25fg,
test26fg,
test27fg,
test28fg,
test29fg,
test30fg,
test31fg) ;
end
