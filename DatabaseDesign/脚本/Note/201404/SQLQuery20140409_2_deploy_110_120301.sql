select COUNT(*) from Oca_OldManLocateInfo
where convert(varchar(10),CheckInTime,120)<CONVERT(varchar(10),dateadd(day,-39,getdate()),120) ;

select * into Oca_OldManLocateInfoOld0409 from Oca_OldManLocateInfo;
delete from Oca_OldManLocateInfo
where convert(varchar(10),CheckInTime,120)<CONVERT(varchar(10),dateadd(day,-39,getdate()),120) ;


select OldManId,LocateTime from Oca_OldManLocateInfo group by OldManId,LocateTime
order by LocateTime

select * from Oca_OldManBaseInfo;

update Oca_OldManLocateInfo
set Loca

select top 30 * from Oca_OldManLocateInfo order by CheckInTime desc;
select * 