select * from sys.dm_exec_background_job_queue

select * from sys.dm_exec_background_job_queue_stats
select * from Pub_userMerge

CREATE TABLE [dbo].[Pub_userMerge](
	[UserId] [uniqueidentifier] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[CheckInTime] [datetime] NOT NULL,
	[OperatedBy] [uniqueidentifier] NULL,
	[OperatedOn] [datetime] NULL,
	[UserCode] [varchar](30) NOT NULL,
	[UserName] [nvarchar](30) NOT NULL,
	[UserType] [char](5) NOT NULL,
	[Gender] [char](1) NOT NULL,
	[PasswordHash] [char](32) NULL,
	[SystemFlag] [tinyint] NOT NULL,
	[StopFlag] [tinyint] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[Area1] [char](5) NULL,
	[Area2] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Pub_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


select   dbo.joinStr(name)
from    sys.all_columns 
where  object_id   in 
            (   select  object_id 
                from    sys.tables 
               where  name='Pub_userMerge'
             )
             
             
             insert into Pub_userMerge(UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2)
select UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2
 from [Smartlife-120300].dbo.Pub_User 
 where UserId not in (select UserId from Pub_userMerge);
 
              insert into Pub_userMerge(UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2)
select UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2
 from [Smartlife-120301].dbo.Pub_User 
 where UserId not in (select UserId from Pub_userMerge);
 
 
              insert into Pub_userMerge(UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2)
select UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2
 from [Smartlife-120302].dbo.Pub_User 
 where UserId not in (select UserId from Pub_userMerge);
 
 
              insert into Pub_userMerge(UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2)
select UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2
 from [Smartlife-120303].dbo.Pub_User 
 where UserId not in (select UserId from Pub_userMerge);
 
 
              insert into Pub_userMerge(UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2)
select UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2
 from [Smartlife-120304].dbo.Pub_User 
 where UserId not in (select UserId from Pub_userMerge);
 
              insert into Pub_userMerge(UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2)
select UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2
 from [Smartlife-120305].dbo.Pub_User 
 where UserId not in (select UserId from Pub_userMerge);
 
              insert into Pub_userMerge(UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2)
select UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2
 from [Smartlife-120306].dbo.Pub_User 
 where UserId not in (select UserId from Pub_userMerge);
 
              insert into Pub_userMerge(UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2)
select UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2
 from [Smartlife-120307].dbo.Pub_User 
 where UserId not in (select UserId from Pub_userMerge);
 
              insert into Pub_userMerge(UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2)
select UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2
 from [Smartlife-120311].dbo.Pub_User 
 where UserId not in (select UserId from Pub_userMerge);
 
              insert into Pub_userMerge(UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2)
select UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2
 from [Smartlife-120395].dbo.Pub_User 
 where UserId not in (select UserId from Pub_userMerge);
 
 
              insert into Pub_userMerge(UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2)
select UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2
 from [Smartlife-120396].dbo.Pub_User 
 where UserId not in (select UserId from Pub_userMerge);
 
              insert into Pub_userMerge(UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2)
select UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2
 from [Smartlife-120397].dbo.Pub_User 
 where UserId not in (select UserId from Pub_userMerge);

     insert into Pub_userMerge(UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2)
select UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2
 from [Smartlife-120398].dbo.Pub_User 
 where UserId not in (select UserId from Pub_userMerge);
 
      insert into Pub_userMerge(UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2)
select UserId,Status,CheckInTime,OperatedBy,OperatedOn,UserCode,UserName,UserType,Gender,PasswordHash,SystemFlag,StopFlag,CreatedBy,CreatedOn,Area1,Area2
 from [Smartlife-120399].dbo.Pub_User 
 where UserId not in (select UserId from Pub_userMerge);