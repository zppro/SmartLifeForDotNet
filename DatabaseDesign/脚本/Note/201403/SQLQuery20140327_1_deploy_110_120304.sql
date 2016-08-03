select LocateFlag,OldManId 
from dbo.Oca_OldManConfigInfo
where oldmanid in(select oldmanid 
from Oca_OldManBaseInfo
where AreaId3 =
(select areaid from Pub_Area where AreaName like 'ÃÔ‘∞%'));

select LocateFlag,OldManId 
from dbo.Oca_OldManConfigInfo;

select oldmanid from dbo.Oca_OldManLocateInfo;

insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ();



insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ('84106946-EC8A-4CDC-B95E-00359BB3B21C',30.3559614827,120.1958090697);
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ('260BE78B-5816-4D4B-828A-05B204108FA0',30.3559921514,120.1954429482);
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ('E2C651A0-F2F6-4545-A4EB-06C3F076A64B',30.3561883152,120.1954074089);
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ('81E55797-2EB9-46B7-B273-0E02A30CA24E',30.3562461805,120.1957225685);
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ('198841C7-BFA0-4C6E-930C-116702187117',30.3564110966,120.1954724525);
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ('E21A76A2-6202-494A-8CC2-1358DDB9D03F',30.3564585461,120.1957949881);
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ('776187B8-F777-497A-B245-195D8067B25E',30.3565829562,120.1953276132);
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ('DF39CBBD-202D-421D-BEEE-1C1D5F3D98D3',30.3568074726,120.1955428605);
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ('91C42436-9058-49E7-A019-1FA2708A3F0C',30.3567351414,120.195844609);
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ('D004C2D5-8503-4F7F-95C1-27FAC2714101',30.3566356134,120.1959425096);
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ('E9F8A720-F104-463A-B8DF-2878147B44F1',30.3576233658,120.1956025396);
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ('957CFAD3-B353-452B-994F-31626011E792',30.3577969592,120.1949105297);
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ('4A487981-6667-4A38-A636-36309AFC9385',30.3582598734,120.1942399774);
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ('9F45AD37-FC05-4F31-BF24-370264FFD300',30.3583779162,120.1950097714);
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes) values ('93EFD684-F755-4A2A-9F7C-3B8BEFDEC89C',30.3580885954,120.1955703531);


insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes,locatetime) values ('84106946-EC8A-4CDC-B95E-00359BB3B21C',30.3559614827,120.1958090697,getdate());
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes,locatetime) values ('260BE78B-5816-4D4B-828A-05B204108FA0',30.3559921514,120.1954429482,getdate());
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes,locatetime) values ('E2C651A0-F2F6-4545-A4EB-06C3F076A64B',30.3561883152,120.1954074089,getdate());
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes,locatetime) values ('81E55797-2EB9-46B7-B273-0E02A30CA24E',30.3562461805,120.1957225685,getdate());
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes,locatetime) values ('198841C7-BFA0-4C6E-930C-116702187117',30.3564110966,120.1954724525,getdate());
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes,locatetime) values ('E21A76A2-6202-494A-8CC2-1358DDB9D03F',30.3564585461,120.1957949881,getdate());
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes,locatetime) values ('776187B8-F777-497A-B245-195D8067B25E',30.3565829562,120.1953276132,getdate());
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes,locatetime) values ('DF39CBBD-202D-421D-BEEE-1C1D5F3D98D3',30.3568074726,120.1955428605,getdate());
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes,locatetime) values ('91C42436-9058-49E7-A019-1FA2708A3F0C',30.3567351414,120.195844609,getdate());
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes,locatetime) values ('D004C2D5-8503-4F7F-95C1-27FAC2714101',30.3566356134,120.1959425096,getdate());
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes,locatetime) values ('E9F8A720-F104-463A-B8DF-2878147B44F1',30.3576233658,120.1956025396,getdate());
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes,locatetime) values ('957CFAD3-B353-452B-994F-31626011E792',30.3577969592,120.1949105297,getdate());
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes,locatetime) values ('4A487981-6667-4A38-A636-36309AFC9385',30.3582598734,120.1942399774,getdate());
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes,locatetime) values ('9F45AD37-FC05-4F31-BF24-370264FFD300',30.3583779162,120.1950097714,getdate());
insert into dbo.Oca_OldManLocateInfo(oldmanid,locatelatitudes,locatelongitudes,locatetime) values ('93EFD684-F755-4A2A-9F7C-3B8BEFDEC89C',30.3580885954,120.1955703531,getdate());

update dbo.Oca_OldManConfigInfo set LocateFlag=1
where OldManId in (select oldmanid from dbo.Oca_OldManLocateInfo)
