
select a.major_id,a.minor_id,value,
(select name from sys.tables where object_id=a.major_id) tablename,
(select name from sys.columns where object_id=a.major_id and column_id=a.minor_id) columnname
 from sys.extended_properties a where a.name='MS_Description';
 
select name from sys.tables where object_id=

select name from sys.columns where object_id= and column_id=


EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备Id' , 
@level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',
@level1name=N'Oca_FamilyCamera', @level2type=N'COLUMN',@level2name=N'DeviceId'
GO

'EXEC sys.sp_addextendedproperty @name=N'''+a.name+''', @value=N'''+a.value+''' , 
@level0type=N''SCHEMA'',@level0name=N''dbo'', @level1type=N''TABLE'',
@level1name=N'''+(select name from sys.tables where object_id=a.major_id)+
''', @level2type=N''COLUMN'',@level2name=N'''+(select name from sys.columns where object_id=a.major_id and column_id=a.minor_id) 
+'''
GO'

----------------------------=============================*****************表列的注释。
select 'EXEC sys.sp_addextendedproperty @name='''+a.name
+''', @value='''+cast(a.value as nvarchar(32))
+''' , 
@level0type=''SCHEMA'',@level0name=''dbo'', @level1type=''TABLE'',
@level1name='''+(select name from sys.tables where object_id=a.major_id)+
''', @level2type=''COLUMN'',@level2name='''+(select name from sys.columns 
where object_id=a.major_id and column_id=a.minor_id) 
+'''
GO'
 from sys.extended_properties a where a.name='MS_Description' and a.minor_id<>0;
 
 --------------------=======================================
 
 EXEC sys.sp_addextendedproperty @name='MS_Description', @value='Id' ,   @level0type='SCHEMA',@level0name='dbo', @level1type='TABLE',  @level1name='Pub_ReminderObject', @level2type='COLUMN',@level2name='Id' 
  GO
  
  ------------------------------------========================***********数据表的注释
  select 'EXEC sys.sp_addextendedproperty @name='''+a.name
+''', @value='''+cast(a.value as nvarchar(32))
+''' , 
@level0type=''SCHEMA'',@level0name=''dbo'', @level1type=''TABLE'',
@level1name='''+(select name from sys.tables where object_id=a.major_id)+
'''
GO'
 from sys.extended_properties a where a.name='MS_Description' and a.minor_id=0;
 -----------------==============================================
 
 EXEC sys.sp_addextendedproperty @name='MS_Description', @value='公共_提醒对象' ,   @level0type='SCHEMA',@level0name='dbo', @level1type='TABLE',  @level1name='Pub_ReminderObject'  
 GO