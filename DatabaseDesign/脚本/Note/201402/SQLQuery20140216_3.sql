USE [SmartLife-120301]
GO
/****** Object:  UserDefinedFunction [dbo].[FUNC_Tol_GetLngLat]    Script Date: 02/16/2014 18:19:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER Function [dbo].[FUNC_Tol_GetLngLat]
(
@id int
)
RETURNS 
@LngLat TABLE (
		locatelongitudeS VARCHAR(50) ,
		locatelatitudeS VARCHAR(50)
		)
AS
  BEGIN
declare @firstlongitude nvarchar(30)
declare @firstlatitude  nvarchar(30)
declare @secondlongitude  nvarchar(30)
declare @secondlatitude   nvarchar(30)
declare @thirdlongitude  nvarchar(30)
declare @distance12    float
declare @distance13    float
declare @thirdlatitude   nvarchar(30)


(select top 1 @firstlongitude=LocateLongitudeS,@firstlatitude=LocateLatitudeS from dbo.Oca_OldManLocateInfo  where 
 checkintime in(select top 30 checkintime from dbo.Oca_OldManLocateInfo 
 order by checkintime desc) and OldManId =(select OldManId from dbo.Oca_OldManBaseInfo where ID=@id))
-- select @firstlatitude
 
 (select top 1 @secondlongitude=LocateLongitudeS,@secondlatitude=LocateLatitudeS from dbo.Oca_OldManLocateInfo  where 
 checkintime in(select top 60 checkintime from dbo.Oca_OldManLocateInfo 
 order by checkintime desc) and OldManId =(select OldManId from dbo.Oca_OldManBaseInfo where ID=@id))
 order by checkintime asc
-- select @secondlongitude
 
 select @thirdlongitude=cast(a.aa as varchar(32))+cast(a.aa*1000000000-CAST(a.aa*1000 as bigint)*1000000 as varchar(32)) 
from (select round((select isnull(@firstlongitude,120.18246899999997))+(cast(right(SYSDATETIME(),7)%373 as float)/373-0.25*(@id-@id/4*4))*0.0001,9) aa)a
 
 select @thirdlatitude=cast(b.bb as varchar(32))+cast(b.bb*1000000000-CAST(b.bb*1000 as bigint)*1000000 as varchar(32)) 
from (select round((select isnull(@firstlatitude,30.234391))+(cast(right(SYSDATETIME(),7)%373 as float)/373-0.25*(@id-@id/4*4))*0.0001,9)  bb )b
-- select @thirdlatitude
 
 select @distance12=[dbo].FUNC_Tol_GetDistanceFromPoints(@firstlongitude,@firstlatitude,@secondlongitude,@secondlatitude)
 select @distance13=[dbo].FUNC_Tol_GetDistanceFromPoints(@thirdlongitude,@thirdlatitude,@secondlongitude,@secondlatitude)
 /**
-- select @distance12 d1,@distance13 d2
while (@distance12>@distance13)
   begin
 select @thirdlongitude=cast(a.aa as varchar(32))+cast(a.aa*1000000000-CAST(a.aa*1000 as bigint)*1000000 as varchar(32)) 
from (select round((select isnull(@firstlongitude,120.18246899999997))+(cast(right(SYSDATETIME(),7)%373 as float)/373-0.25*(@id-@id/4*4))*0.0001,9) aa)a
 
 select @thirdlatitude=cast(b.bb as varchar(32))+cast(b.bb*1000000000-CAST(b.bb*1000 as bigint)*1000000 as varchar(32)) 
from (select round((select isnull(@firstlatitude,30.234391))+(cast(right(SYSDATETIME(),7)%373 as float)/373-0.25*(@id-@id/4*4))*0.0001,9)  bb )b
-- select @thirdlatitude

 select @distance13=[dbo].FUNC_Tol_GetDistanceFromPoints(@thirdlongitude,@thirdlatitude,@secondlongitude,@secondlatitude)
   end
 *****/  
 select top 1 @thirdlongitude=a.LongitudeS,@thirdlatitude=a.LatitudeS
 from (
 select  ROW_NUMBER() OVER(ORDER BY id aSC) AS id,LongitudeS,LatitudeS from dbo.oca_oldmanbaseinfo where LongitudeS is not null) a
 where a.id=@id%15 ;
 
 DECLARE @RESULT  nvarchar(50)
set @RESULT=@thirdlongitude+','+@thirdlatitude
INSERT @LngLat (locatelongitudeS, locatelatitudeS) VALUES(@thirdlongitude, @thirdlatitude)
RETURN 
    END  