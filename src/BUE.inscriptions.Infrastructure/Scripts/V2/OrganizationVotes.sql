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

 Date: 29/05/2024 11:50:04
*/


-- ----------------------------
-- Table structure for OrganizationVotes
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[OrganizationVotes]') AND type IN ('U'))
	DROP TABLE [dbo].[OrganizationVotes]
GO

CREATE TABLE [dbo].[OrganizationVotes] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [organization_election_id] int  NULL,
  [election_id] int  NOT NULL,
  [vote_type] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [vote_date] datetime  NULL,
  [user_id] int  NOT NULL,
  [CreatedAt] datetime  NULL,
  [DeletedAt] datetime  NULL,
  [UpdatedAt] datetime  NULL
)
GO

ALTER TABLE [dbo].[OrganizationVotes] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Auto increment value for OrganizationVotes
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[OrganizationVotes]', RESEED, 439)
GO


-- ----------------------------
-- Primary Key structure for table OrganizationVotes
-- ----------------------------
ALTER TABLE [dbo].[OrganizationVotes] ADD CONSTRAINT [PK__Organiza__3213E83F87C88AF0] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

