USE [SmartLife-120300]
GO
/****** Object:  StoredProcedure [dbo].[SP_Oca_RefreshOldmanLocate]    Script Date: 01/18/2014 17:09:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   PROCEDURE [dbo].[SP_Oca_RefreshOldmanLocate]
 AS
 BEGIN
SET NOCOUNT ON
 
DECLARE  @id nvarchar(50)
 
DECLARE id_cursor CURSOR FOR 
select ID from dbo.Oca_OldManBaseInfo order by id;
 
OPEN id_cursor
 
FETCH NEXT FROM id_cursor 
INTO  @id
 
WHILE @@FETCH_STATUS = 0
 BEGIN
    
insert into dbo.Oca_OldManLocateInfo  (oldmanid,homelongitudeS,homelatitudes,locatetime,locatelongitudeS,locatelatitudes)
select oldmanid ,120.18246899999997 homelongitudeS,30.234391 homelatitudes,getdate() locatetime,round(120.18246899999997+RAND()*0.002,8) locatelongitudeS,round(30.234391+RAND()*0.002,8) locatelatitudes 
from dbo.oca_oldmanbaseinfo where ID=@id;
    
     FETCH NEXT FROM id_cursor 
    INTO  @id
 END 
CLOSE id_cursor
 DEALLOCATE id_cursor
 END