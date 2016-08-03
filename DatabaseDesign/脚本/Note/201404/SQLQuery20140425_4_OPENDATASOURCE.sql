SELECT * FROM OPENDATASOURCE('Microsoft.Jet.OLEDB.4.0',
'Data Source=e:\江干区汇总表3.xls;Extended Properties=EXCEL 5.0')...[Sheet1$] ;

SELECT * FROM OPENDATASOURCE('Microsoft.Jet.OLEDB.4.0',
'Data Source=e:\江干区汇总表3.xls;Extended Properties="EXCEL 8.0;HDR=NO"')...[Sheet1$] ;

Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\江干区汇总表xls.xls;Extended Properties="EXCEL 8.0;HDR=NO";

SELECT *
FROM OPENDATASOURCE('SQLNCLI',
    'Data Source=Wangwei;Integrated Security=SSPI')
    .[Leblue-Etl].dbo.cfg_etl_translaterule
    


--------------------------============================================    
exec    sp_configure 'show advanced options', 1
RECONFIGURE
exec sp_configure 'Ad Hoc Distributed Queries', 1
RECONFIGURE
GO

SELECT a.*
FROM OPENROWSET('SQLNCLI', 'Server=Wangwei;Trusted_Connection=yes;',
     'SELECT *
      FROM [Leblue-Etl].dbo.cfg_etl_translaterule') AS a;
GO

SELECT *
FROM OPENDATASOURCE('SQLNCLI',
    'Server=115.236.175.110,10017;Trusted_Connection=yes').[Smartlife-120300].dbo.Oca_OldManBaseInfo;
    .[Leblue-Etl].dbo.cfg_etl_translaterule

exec SP_ETL_DataStreamTask;

select * from Bas_ResidentBaseInfostep1;
select * from cfg_etl_translaterule

update cfg_etl_translaterule set newvalue='substring(x,7,8)',type=6
where oldvalue='IDNo'

set mapExpression=replace(newvalue,'x','b.'+oldvalue)

exec sp_etl_setmapexpression