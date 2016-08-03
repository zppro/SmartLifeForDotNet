USE [smartlife-120303]
GO

/****** Object:  PartitionFunction [myRangePF1]    Script Date: 04/04/2014 15:22:11 ******/
CREATE PARTITION FUNCTION [myRangePF1](int) AS RANGE LEFT FOR VALUES (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20)
GO


delete  from  pub_area
insert into pub_area (AreaId,Status,CheckInTime,AreaCode,AreaName,Description,ParentId,Levels,EndFlag,OrderNo,InputCode1,InputCode2)
select AreaId,Status,CheckInTime,AreaCode,AreaName,Description,ParentId,Levels,EndFlag,OrderNo,InputCode1,InputCode2
 from remote_dbo.[smartlife-120303].dbo.pub_area;

delete  from  pub_area
insert into pub_area (id,AreaId,Status,CheckInTime,AreaCode,AreaName,Description,ParentId,Levels,EndFlag,OrderNo,InputCode1,InputCode2)
select id,AreaId,Status,CheckInTime,AreaCode,AreaName,Description,ParentId,Levels,EndFlag,OrderNo,InputCode1,InputCode2
 from remote_dbo.[smartlife-120304].dbo.pub_area;

select   dbo.joinStr(name)
from    sys.all_columns 
where  object_id   in 
            (   select  object_id 
                from    sys.tables 
               where  name='pub_area'
             )
             
             
             exec SP_Tol_UspOutputData @tablename='pub_area'
             
             select * from dey_script;
             
     CREATE PARTITION FUNCTION [OldManBaseInfoPF1](char(5)) AS 
     RANGE LEFT FOR VALUES (N'01189', N'01190', N'01191', N'01192', N'01193',
      N'01194', N'01195', N'01196', N'01197', N'01198', N'01199', N'01200')
GO

CREATE PARTITION SCHEME [OldManBaseInfoPS1] AS PARTITION [OldManBaseInfoPF1] TO 
([test1fg], [test2fg], [test3fg], [test4fg], [test5fg], [test6fg], [test7fg], 
[test8fg], [test9fg], [test10fg], [test11fg], [test12fg], [test13fg])
GO
