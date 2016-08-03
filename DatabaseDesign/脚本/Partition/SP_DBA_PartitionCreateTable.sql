/***********************************************************************/
/*   SP_DBA_PartitionCreateTable            */
/*  创建分区表 */
/***********************************************************************/
create procedure SP_DBA_PartitionCreateTable
@databasename varchar(100)
as
begin

	IF (NOT OBJECT_ID(N'dbo.Bas_ResidentBaseInfo', 'U') IS NULL)
	BEGIN
		   DROP TABLE dbo.Bas_ResidentBaseInfo
	END

	IF (OBJECT_ID(N'dbo.Bas_ResidentBaseInfo', 'U') IS NULL)
	BEGIN
		   PRINT ''
		   PRINT 'Creating table Bas_ResidentBaseInfo ...'
		   
			 CREATE TABLE [dbo].[Bas_ResidentBaseInfo](
			[ResidentId] [uniqueidentifier] NOT NULL,
			[Id] [int] NOT NULL,
			[CheckInTime] [datetime] NOT NULL,
			[Status] [tinyint] NOT NULL,
			[OperatedBy] [uniqueidentifier] NULL,
			[OperatedOn] [datetime] NULL,
			[DataSource] [char](5) NULL,
			[ResidentName] [nvarchar](20) NOT NULL,
			[ResidentStatus] [tinyint] NOT NULL,
			[ResidentBizId] [char](5) NULL,
			[IDNo] [char](18) NOT NULL,
			[Gender] [char](1) NOT NULL,
			[NativePlace] [char](5) NULL,
			[HouseholdRegister] [char](5) NULL,
			[EducationLevel] [char](5) NULL,
			[Marriage] [char](5) NULL,
			[LivingStatus] [char](5) NULL,
			[HousingStatus] [char](5) NULL,
			[IncomeStatus] [char](5) NULL,
			[PlaceOfHouseholdRegister] [nvarchar](200) NULL,
			[ResidentialAddress] [nvarchar](200) NULL,
			[AreaId] [char](5) NOT NULL,
			[AreaId2] [varchar](40) NOT NULL,
			[AreaId3] [varchar](40) NULL,
			[PostCode] [char](6) NULL,
			[Tel] [varchar](30) NULL,
			[Mobile] [varchar](20) NULL,
			[Remark] [nvarchar](400) NULL,
			[InputCode1] [varchar](30) NULL,
			[InputCode2] [varchar](30) NULL
		) on OldManBaseInfoPS1 (AreaId);
	end

	IF (NOT OBJECT_ID(N'dbo.Bas_ResidentFamilyInfo', 'U') IS NULL)
	BEGIN
	        DROP TABLE dbo.Bas_ResidentFamilyInfo
	END

	IF (OBJECT_ID(N'dbo.Bas_ResidentFamilyInfo', 'U') IS NULL)
	BEGIN
		   PRINT ''
		   PRINT 'Creating table Bas_ResidentFamilyInfo ...'
			CREATE TABLE [dbo].[Bas_ResidentFamilyInfo](
				[Id] [int] NOT NULL,
				[CheckInTime] [datetime] NOT NULL,
				[Status] [tinyint] NOT NULL,
				[OperatedBy] [uniqueidentifier] NULL,
				[OperatedOn] [datetime] NULL,
				[DataSource] [char](5) NULL,
				[FamilyId] [int] NOT NULL,
				[PartitionId]  AS ([FamilyId]%(30)) PERSISTED,
				[ResidentIdOfA] [uniqueidentifier] NOT NULL,
				[ResidentIdOfB] [uniqueidentifier] NOT NULL,
				[AToB] [char](5) NOT NULL,
				[BToA] [char](5) NOT NULL
			) on  OldManFamilyInfoPS1(PartitionId);
	end 
end