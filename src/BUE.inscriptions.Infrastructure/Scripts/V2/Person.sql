/*
 Navicat Premium Data Transfer

 Source Server         : DEV2022
 Source Server Type    : SQL Server
 Source Server Version : 16001115
 Source Host           : localhost\DEV2022:1433
 Source Catalog        : BUE_PBASE
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 16001115
 File Encoding         : 65001

 Date: 31/05/2024 12:41:32
*/


-- ----------------------------
-- Table structure for Person
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND type IN ('U'))
	DROP TABLE [dbo].[Person]
GO

CREATE TABLE [dbo].[Person] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [CompanyId] int  NOT NULL,
  [UserId] int  NOT NULL,
  [FirstName] varchar(180) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [MiddleName] varchar(180) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [LastName] varchar(120) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [SecondLastName] varchar(120) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IdentificationTypeId] int  NULL,
  [Identification] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Nomination] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [BirthDate] date  NULL,
  [BloodTypeId] int  NULL,
  [GenderId] int  NULL,
  [BirthCountryId] int  NULL,
  [BirthCity] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IsActive] bit  NULL,
  [NationalityId] int  NULL,
  [SecondaryNationalityId] int  NULL,
  [MaritalStatus] int  NULL,
  [Image] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [DocumentIdentification] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [PostalId] varchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address] varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [MainStreet] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [SecondaryStreet] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Sector] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Email] varchar(220) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [HomePhone] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [CellPhone] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [CellPhonePrefix] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [PhonePrefix] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ProfessionalSituation] int  NULL,
  [Position] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [OfficePhone] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [WorkAddress] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [WorkPlace] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [EmployerName] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [WorkCountryId] int  NULL,
  [WorkCityId] int  NULL,
  [WorkPhone] varchar(80) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [WorkCategory] int  NULL,
  [WorkPhonePrefix] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [SystemAccess] bit  NULL,
  [ShareContacts] bit  NULL,
  [AdditionalInformation] varchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ExternalId] int  NULL,
  [CreatedAt] datetime  NULL,
  [UpdatedAt] datetime  NULL,
  [DeletedAt] datetime  NULL
)
GO

ALTER TABLE [dbo].[Person] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Auto increment value for Person
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Person]', RESEED, 2)
GO


-- ----------------------------
-- Uniques structure for table Person
-- ----------------------------
ALTER TABLE [dbo].[Person] ADD CONSTRAINT [UQ__Person__FAB3ECC22947054A] UNIQUE NONCLUSTERED ([CompanyId] ASC, [Identification] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Person
-- ----------------------------
ALTER TABLE [dbo].[Person] ADD CONSTRAINT [PK__Person__3214EC0770546094] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table Person
-- ----------------------------
ALTER TABLE [dbo].[Person] ADD CONSTRAINT [FK_Person_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Person] ADD CONSTRAINT [FK_Person_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Usuarios] ([Codigo]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

