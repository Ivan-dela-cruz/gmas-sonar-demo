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

 Date: 29/05/2024 11:51:27
*/


-- ----------------------------
-- Table structure for OrganizationElection
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[OrganizationElection]') AND type IN ('U'))
	DROP TABLE [dbo].[OrganizationElection]
GO

CREATE TABLE [dbo].[OrganizationElection] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [election_id] int  NOT NULL,
  [organization_id] int  NOT NULL,
  [is_representative] bit  NULL,
  [status] bit  NULL,
  [CreatedAt] datetime  NULL,
  [UpdatedAt] datetime  NULL,
  [DeletedAt] datetime  NULL
)
GO

ALTER TABLE [dbo].[OrganizationElection] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Auto increment value for OrganizationElection
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[OrganizationElection]', RESEED, 20)
GO


-- ----------------------------
-- Primary Key structure for table OrganizationElection
-- ----------------------------
ALTER TABLE [dbo].[OrganizationElection] ADD CONSTRAINT [PK__Organiza__3213E83F65CBCF38] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO
