USE [SmartLife-120300]
GO

/****** Object:  User [SmartCare120300]    Script Date: 04/04/2014 09:00:33 ******/
GO

CREATE USER [SmartCare120300] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/***************************************/
/*  Func_Tol_Pass   */
/* 功能是根据钥匙对源串加密生成目标串 */
/***************************************/
alter  function Func_Tol_Pass
(@name varchar(50),@source varchar(100))
returns  varchar(500)
as 

begin
declare @str varchar(50),@i int ,@total int,@returnStr varchar(500)
set @i=0
set @total=0
set @returnStr=''

		while @i<LEN(@name)
		begin
		  set @i=@i+1
		  set @total=@total+ascii(substring(@name,@i,1))
		end
		
		set @i=0
		while @i<LEN(@source)
		begin
		    set @i=@i+1
		    select @str=cast(ascii(substring(@source,@i,1))^@total as varchar(3))
		    set @returnStr=@returnStr+@str+','
		end
		
    return  @returnStr---@total --@str
end
go

select cast('a' as int)^cast('b' as int)
select reverse('cab')
select ('a'),UNICODE('a')

select  cast(ASCII('1')^309 as varchar(3))
select QUOTENAME('a,b,a',',')

select * from cfg_bridge;

update cfg_bridge set username='dbo' ,pass=dbo.Func_Tol_Pass('dbo','leblue@2014')
where isSourceDb is not null; 

select  char(345^309)

alter table cfg_bridge add UserName varchar(50) null;
alter table cfg_bridge add Pass varchar(500) null;
dbo   leblue@2014

select dbo.Func_Tol_Pass('dbo','leblue@2014')

/***************************************/
/*  Func_Tol_Pass   */
/* 功能是根据钥匙对源串解密生成目标串 */
/***************************************/
alter  function Func_Tol_PassUn
(@name varchar(50),@source varchar(100))
returns  varchar(500)
as 

begin
declare @str varchar(50),@i int ,@total int,@returnStr varchar(500),@strSource varchar(100)
set @i=0
set @total=0
set @returnStr=''

		while @i<LEN(@name)
		begin
		  set @i=@i+1
		  set @total=@total+ascii(substring(@name,@i,1))
		end
		
		set @i=0
		set @strSource=@source
		while 0<LEN(@strSource)
		begin
		    set @i=@i+1    
		    select @str=char(cast(substring(@strSource,1,CHARINDEX(',',@strSource,1)-1) as int)^@total)
		    set @strSource=Stuff(@strSource,1,CHARINDEX(',',@strSource,1),'')
		    set @returnStr=@returnStr+@str
		end
		
    return  @returnStr---@total --@str
end
go

declare @str varchar(100)
set @str='345,336,343,345,320,336,373,263,261,260,257,'
select SUBSTRING(@str,1,CHARINDEX(',',@str,1)-1)
select Stuff(@str,1,CHARINDEX(',',@str,1),'')

select dbo.Func_Tol_Pass('dbo','leblue@2014')
select dbo.Func_Tol_PassUn('dbo','345,336,343,345,320,336,373,263,261,260,257,')