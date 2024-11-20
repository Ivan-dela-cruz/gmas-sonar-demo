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

 Date: 31/05/2024 10:39:51
*/


-- ----------------------------
-- Table structure for Student
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Student]') AND type IN ('U'))
	DROP TABLE [dbo].[Student]
GO

CREATE TABLE [dbo].[Student] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [CompanyId] int  NOT NULL,
  [FirstName] varchar(180) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [MiddleName] varchar(180) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [LastName] varchar(120) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [SecondLastName] varchar(120) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IdentificationTypeId] int  NULL,
  [BirthCountryId] int  NULL,
  [NationalityId] int  NULL,
  [SecondaryNationalityId] int  NULL,
  [GenderId] int  NULL,
  [BloodTypeId] int  NULL,
  [Identification] char(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [BirthDate] date  NULL,
  [Address] varchar(4000) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [PreviousSchoolName] varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [BirthCity] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IsActive] bit  NULL,
  [Image] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [DocumentIdentification] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Sector] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Telephone] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Email] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [EmergencyContactPrefix] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [EmergencyContact] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ExternalId] int  NULL,
  [CreatedAt] datetime  NULL,
  [UpdatedAt] datetime  NULL,
  [DeletedAt] datetime  NULL
)
GO

ALTER TABLE [dbo].[Student] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Auto increment value for Student
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Student]', RESEED, 24)
GO


-- ----------------------------
-- Uniques structure for table Student
-- ----------------------------
ALTER TABLE [dbo].[Student] ADD CONSTRAINT [UQ__Student__FAB3ECC204FCC630] UNIQUE NONCLUSTERED ([CompanyId] ASC, [Identification] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Student
-- ----------------------------
ALTER TABLE [dbo].[Student] ADD CONSTRAINT [PK__Student__3214EC07CEBBF04C] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table Student
-- ----------------------------
ALTER TABLE [dbo].[Student] ADD CONSTRAINT [FK_Student_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

