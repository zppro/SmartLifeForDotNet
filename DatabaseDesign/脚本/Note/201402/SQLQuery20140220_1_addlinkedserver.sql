
------------------检查函数的定义的文本长度
select e.name,e.lengths,f.lengths
from (
select a.name,LEN(b.definition) lengths
 from sys.all_objects a,sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) e,
 (
select a.name,LEN(b.definition) lengths
 from [remote1].[Smartlife-120399].sys.all_objects a,[remote1].[Smartlife-120399].sys.all_sql_modules b 
where a.type in ('AF','TF','FN') and a.object_id>0 and a.object_id=b.object_id) f
where  e.name=f.name and e.lengths<>f.lengths
;

---------------------------------------------------
Exec sp_droplinkedsrvlogin 'remote1','sa'       --删除映射（录与链接服务器上远程登录之间的映射）
Exec sp_dropserver 'remote1'                         --删除远程服务器链接

exec sp_addlinkedserver  @server='remote1' ,@srvproduct='',
 @provider='SQLOLEDB',
      @datasrc="115.236.175.110,10017"   --要访问的服务器


exec sp_addlinkedsrvlogin @rmtsrvname='remote1'
,@useself=false
,@locallogin='sa'
,@rmtuser='sa'
,@rmtpassword='888'

select top 10 * from [remote1].[Smartlife-120399].dbo.oca_oldmanbaseinfo order by ID desc;
