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

 Date: 31/05/2024 12:39:34
*/


-- ----------------------------
-- Table structure for StudentRelatives
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[StudentRelatives]') AND type IN ('U'))
	DROP TABLE [dbo].[StudentRelatives]
GO

CREATE TABLE [dbo].[StudentRelatives] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [StudentId] int  NOT NULL,
  [PersonId] int  NOT NULL,
  [RelationTypeId] int  NOT NULL,
  [IsPrincipalRepresentative] bit  NOT NULL,
  [IsEconomicRepresentative] bit  NOT NULL,
  [IsLegalRepresentative] bit  NOT NULL,
  [IsResponsibleRepresentative] bit  NOT NULL,
  [LivesWithStudent] bit  NOT NULL
)
GO

ALTER TABLE [dbo].[StudentRelatives] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Auto increment value for StudentRelatives
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[StudentRelatives]', RESEED, 1007)
GO


-- ----------------------------
-- Uniques structure for table StudentRelatives
-- ----------------------------
ALTER TABLE [dbo].[StudentRelatives] ADD CONSTRAINT [UQ__StudentR__B270412D26F3B63C] UNIQUE NONCLUSTERED ([StudentId] ASC, [PersonId] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table StudentRelatives
-- ----------------------------
ALTER TABLE [dbo].[StudentRelatives] ADD CONSTRAINT [PK__StudentR__3214EC27FCB8E268] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table StudentRelatives
-- ----------------------------
ALTER TABLE [dbo].[StudentRelatives] ADD CONSTRAINT [FK__StudentRe__Stude__725BF7F6] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[StudentRelatives] ADD CONSTRAINT [FK__StudentRe__Perso__73501C2F] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

