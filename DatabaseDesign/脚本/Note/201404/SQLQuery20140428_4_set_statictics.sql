set statistics time on 
set statistics profile on 
select * from dbo.Bas_ResidentBaseInfoStep2;
set statistics time off 
set statistics profile off

SELECT * FROM sys.configurations
ORDER BY name ;


SELECT count(*)AS cached_pages_count
    ,CASE database_id 
        WHEN 32767 THEN 'ResourceDb' 
        ELSE db_name(database_id) 
        END AS Database_name
FROM sys.dm_os_buffer_descriptors
GROUP BY db_name(database_id) ,database_id
ORDER BY cached_pages_count DESC;


SELECT count(*)AS cached_pages_count 
    ,name ,index_id 
FROM sys.dm_os_buffer_descriptors AS bd 
    INNER JOIN 
    (
        SELECT object_name(object_id) AS name 
            ,index_id ,allocation_unit_id
        FROM sys.allocation_units AS au
            INNER JOIN sys.partitions AS p 
                ON au.container_id = p.hobt_id 
                    AND (au.type = 1 OR au.type = 3)
        UNION ALL
        SELECT object_name(object_id) AS name   
            ,index_id, allocation_unit_id
        FROM sys.allocation_units AS au
            INNER JOIN sys.partitions AS p 
                ON au.container_id = p.partition_id 
                    AND au.type = 2
    ) AS obj 
        ON bd.allocation_unit_id = obj.allocation_unit_id
WHERE database_id = db_id()
GROUP BY name, index_id 
ORDER BY cached_pages_count DESC;


select * from sys.dm_os_loaded_modules ;

exec sp_help_jobstep @job_name='backup_database'

select * from sys.syscomments;
select * from sys.indexes;

exec sp_update_job @job_name='backup_database',@category_name='我要自定义'
exec sp_help_category


exec sp_update_jobschedule @job_name='setworkorder',@name='setworkorder',@freq_subday_type='2',@freq_subday_interval='3600'
