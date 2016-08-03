select 2014-substring(idno,7,4),birthday from dbo.Oca_OldManBaseInfo;
select '0571-'+right(longitudes,8) from dbo.Oca_OldManBaseInfo where id<18 Address is null;
select * from dbo.Oca_OldManLocateInfo;
select * from dbo.Oca_CallService;

select  * from dbo.vSystem_Info_Columns where table_name='oca_callservice';
update dbo.Oca_OldManBaseInfo set Birthday=substring(idno,7,8) where Birthday is null;
update dbo.Oca_OldManBaseInfo set Mobile='15'+right(latitudes,9) where Mobile is null and ID<18;
update dbo.Oca_OldManBaseInfo set Tel='0571-'+right(longitudes,8) where Tel is null and ID<18;

update dbo.oca_oldmanbaseinfo set Address='望江家园西园20幢301',LongitudeS='120.1950352771',LatitudeS='30.2382705789' where id%16=1;
update dbo.oca_oldmanbaseinfo set Address='望江府3幢1102',LongitudeS='120.1974939487',LatitudeS='30.2337696514' where id%16=2;
update dbo.oca_oldmanbaseinfo set Address='林风花园A区6幢503',LongitudeS='120.1965222771',LatitudeS='30.2334315789' where id%16=3;
update dbo.oca_oldmanbaseinfo set Address='兴隆西村15幢102',LongitudeS='120.1941794083',LatitudeS='30.2462822276' where id%16=4;
update dbo.oca_oldmanbaseinfo set Address='兴隆西村13幢301',LongitudeS='120.1941794083',LatitudeS='30.2462822276' where id%16=5;
update dbo.oca_oldmanbaseinfo set Address='霞晖北村12幢505',LongitudeS='120.1895683511',LatitudeS='30.2436933494' where id%16=6;
update dbo.oca_oldmanbaseinfo set Address='霞晖北村3幢502',LongitudeS='120.1890319093',LatitudeS='30.2434894295' where id%16=7;
update dbo.oca_oldmanbaseinfo set Address='霞晖南村9幢301',LongitudeS='120.1879590257',LatitudeS='30.2422102859' where id%16=8;
update dbo.oca_oldmanbaseinfo set Address='望江家园东园37幢1502',LongitudeS='120.1975392771',LatitudeS='30.2375945789' where id%16=9;
update dbo.oca_oldmanbaseinfo set Address='海潮新村7幢401',LongitudeS='120.184523331',LatitudeS='30.2389835195' where id%16=10;
update dbo.oca_oldmanbaseinfo set Address='婺江路2弄4单元102',LongitudeS='120.2031495347',LatitudeS='30.2378401763' where id%16=11;
update dbo.oca_oldmanbaseinfo set Address='兴隆东村5幢201',LongitudeS='120.1953294083',LatitudeS='30.2452932276' where id%16=12;
update dbo.oca_oldmanbaseinfo set Address='兴隆路1弄5幢302',LongitudeS='120.1957840083',LatitudeS='30.2448586276' where id%16=13;
update dbo.oca_oldmanbaseinfo set Address='始板桥花园7幢403',LongitudeS='120.1883238061',LatitudeS='30.2447870944' where id%16=14;
update dbo.oca_oldmanbaseinfo set Address='陆家河头小区7幢301',LongitudeS='120.1840984758',LatitudeS='30.2422901642' where id%16=15;
update dbo.oca_oldmanbaseinfo set Address='陆家河头小区7幢301',LongitudeS='120.1840984758',LatitudeS='30.2422901642' where id%16=0;

