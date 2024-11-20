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

 Date: 29/05/2024 11:51:40
*/


-- ----------------------------
-- Table structure for Positions
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Positions]') AND type IN ('U'))
	DROP TABLE [dbo].[Positions]
GO

CREATE TABLE [dbo].[Positions] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [name] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [description] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [image] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [abbreviation] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [DeletedAt] datetime  NULL,
  [CreatedAt] datetime  NULL,
  [UpdatedAt] datetime  NULL,
  [status] bit  NULL
)
GO

ALTER TABLE [dbo].[Positions] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Auto increment value for Positions
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Positions]', RESEED, 5)
GO


-- ----------------------------
-- Primary Key structure for table Positions
-- ----------------------------
ALTER TABLE [dbo].[Positions] ADD CONSTRAINT [PK__position__3213E83F6FD27CE2] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

