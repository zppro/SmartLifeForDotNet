-----------------
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
 order by checkintime desc) and OldManId =(select OldManId from dbo.Oca_OldManBaseInfo where ID=40))
-- select @firstlatitude
 
 (select top 1 @secondlongitude=LocateLongitudeS,@secondlatitude=LocateLatitudeS from dbo.Oca_OldManLocateInfo  where 
 checkintime in(select top 60 checkintime from dbo.Oca_OldManLocateInfo 
 order by checkintime desc) and OldManId =(select OldManId from dbo.Oca_OldManBaseInfo where ID=40))
 order by checkintime asc
-- select @secondlongitude
 
 select @thirdlongitude=cast(a.aa as varchar(32))+cast(a.aa*1000000000-CAST(a.aa*1000 as bigint)*1000000 as varchar(32)) 
from (select round((select isnull(@firstlongitude,120.18246899999997))+(RAND(right(SYSDATETIME(),7))-0.25*(40-40/4*4))*0.0002,9) aa)a
 
 select @thirdlatitude=cast(b.bb as varchar(32))+cast(b.bb*1000000000-CAST(b.bb*1000 as bigint)*1000000 as varchar(32)) 
from (select round((select isnull(@firstlatitude,30.234391))+(RAND(right(SYSDATETIME(),7))-0.25*(40-40/4*4))*0.0002,9)  bb )b
-- select @thirdlatitude
 
 select @distance12=[dbo].FUNC_Tol_GetDistanceFromPoints(@firstlongitude,@firstlatitude,@secondlongitude,@secondlatitude)
 select @distance13=[dbo].FUNC_Tol_GetDistanceFromPoints(@thirdlongitude,@thirdlatitude,@secondlongitude,@secondlatitude)
 select @distance12 d1,@distance13 d2
 -----------------------------------
 
 select RAND(right(SYSDATETIME(),7)),cast(right(SYSDATETIME(),7)%373 as float)/373
 
 select a.locatelongitudes from (select * from dbo.FUNC_Tol_GetLngLat(40))  a where a.id=40
 select top 30 * from dbo.Oca_OldManLocateInfo order by CheckInTime desc;
 
 insert into dbo.Oca_OldManLocateInfo  (oldmanid,homelongitudeS,homelatitudes,locatetime,locatelongitudeS,locatelatitudes)
select b.oldmanid ,120.18246899999997 homelongitudeS,30.234391 homelatitudes,getdate() locatetime,
a.*
from dbo.oca_oldmanbaseinfo b,(select * from dbo.FUNC_Tol_GetLngLat(40))  a  where b.ID=40 
 
 (select isnull((select LongitudeS from dbo.Oca_OldManBaseInfo where ID=40),120.18246899999997))
 
 (select isnull((select LatitudeS from dbo.Oca_OldManBaseInfo where ID=20),30.234391))
 
 select COUNT(*) from dbo.Oca_OldManLocateInfo
 delete  from dbo.Oca_OldManLocateInfo where 
 cast(DATENAME(HH,checkintime) as int)= 23 and cast(DATENAME(day,checkintime) as int)= 10
 
 select * from sys.spatial_reference_systems;
 
 DECLARE @Location GEOGRAPHY 
SET @Location = geography::STGeomFromText 
('LINESTRING(47.653 -89.358, 48.1 -89.320, 49.0 -88.28)', 4326) 

SELECT @Location
 
 DECLARE @Point GEOMETRY 
SET @Point = geometry::STGeomFromText('POINT (190 0)',0) 
SELECT @Point 

------------=======================
USE [SmartLife-120301]
GO
/****** Object:  UserDefinedFunction [dbo].[FUNC_Tol_GetWB]    Script Date: 02/12/2014 15:33:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create Function [dbo].[FUNC_Tol_GetDistanceFromPoints]
(
@firstlongitude nvarchar(30),
@firstlatitude  nvarchar(30),
@secondlongitude  nvarchar(30),
@secondlatitude   nvarchar(30)
)
RETURNS float
AS 
    BEGIN
    DECLARE @RESULT  float
set @RESULT=(select sqrt(power((cast(@secondlongitude as float)-cast(@firstlongitude as float))*111.111,2)+power((cast(@secondlatitude as float)-cast(@firstlatitude as float))*111.111,2))*1000)
   RETURN @RESULT
    END
    
-------=========================
drop Function [dbo].[FUNC_Tol_GetLngLat]
USE [SmartLife-120301]
GO
/****** Object:  UserDefinedFunction [dbo].[FUNC_Tol_GetWB]    Script Date: 02/12/2014 15:33:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter Function [dbo].[FUNC_Tol_GetLngLat]
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
from (select round((select isnull(@firstlongitude,120.18246899999997))+(cast(right(SYSDATETIME(),7)%373 as float)/373-0.25*(@id-@id/4*4))*0.0002,9) aa)a
 
 select @thirdlatitude=cast(b.bb as varchar(32))+cast(b.bb*1000000000-CAST(b.bb*1000 as bigint)*1000000 as varchar(32)) 
from (select round((select isnull(@firstlatitude,30.234391))+(cast(right(SYSDATETIME(),7)%373 as float)/373-0.25*(@id-@id/4*4))*0.0002,9)  bb )b
-- select @thirdlatitude
 
 select @distance12=[dbo].FUNC_Tol_GetDistanceFromPoints(@firstlongitude,@firstlatitude,@secondlongitude,@secondlatitude)
 select @distance13=[dbo].FUNC_Tol_GetDistanceFromPoints(@thirdlongitude,@thirdlatitude,@secondlongitude,@secondlatitude)
-- select @distance12 d1,@distance13 d2
while (@distance12>@distance13)
   begin
 select @thirdlongitude=cast(a.aa as varchar(32))+cast(a.aa*1000000000-CAST(a.aa*1000 as bigint)*1000000 as varchar(32)) 
from (select round((select isnull(@firstlongitude,120.18246899999997))+(cast(right(SYSDATETIME(),7)%373 as float)/373-0.25*(@id-@id/4*4))*0.0002,9) aa)a
 
 select @thirdlatitude=cast(b.bb as varchar(32))+cast(b.bb*1000000000-CAST(b.bb*1000 as bigint)*1000000 as varchar(32)) 
from (select round((select isnull(@firstlatitude,30.234391))+(cast(right(SYSDATETIME(),7)%373 as float)/373-0.25*(@id-@id/4*4))*0.0002,9)  bb )b
-- select @thirdlatitude

 select @distance13=[dbo].FUNC_Tol_GetDistanceFromPoints(@thirdlongitude,@thirdlatitude,@secondlongitude,@secondlatitude)
   end

 DECLARE @RESULT  nvarchar(50)
set @RESULT=@thirdlongitude+','+@thirdlatitude
INSERT @LngLat (locatelongitudeS, locatelatitudeS) VALUES(@thirdlongitude, @thirdlatitude)
RETURN 
    END    