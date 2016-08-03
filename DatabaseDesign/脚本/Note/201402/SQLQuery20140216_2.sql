select * from t_deploy_bridge where node_ip='115.236.175.110';
update t_deploy_bridge set database_name='smartlife-120311' where id=29
update t_deploy_bridge set database_name='smartlife-120396' where id=30
update t_deploy_bridge set database_name='smartlife-120397' where id=31
update t_deploy_bridge set database_name='smartlife-120398' where id=32
update t_deploy_bridge set node_ip='115.236.175.109' where id>32
update t_deploy_bridge set node_ip='115.236.175.110' where id=32
update t_deploy_bridge set node_id=id%20 where  node_ip='115.236.175.110';
update t_deploy_bridge set city='杭州西湖区机器一' where  node_ip='115.236.175.110';
update t_deploy_bridge set node_ip='192.168.1.6',city='杭州西湖区' where  id=11 
node_ip='115.236.175.110';


update t_deploy_bridge set city='杭州西湖区机器一' ,  node_ip='192.168.1.109' ,node_id=id%32 ,database_name='smartlife-12030'+cast(id%32 as varchar(1)) where id>32;
update t_deploy_bridge set city='杭州西湖区机器二' where  node_ip='192.168.1.109';
select cast(node_id as nvarchar(32))+':'+cast(node_ip as nvarchar(32))+':'+database_name+':' from dbo.t_deploy_bridge
 where city='杭州西湖区机器一' and version_id=1;

