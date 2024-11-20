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

 Date: 29/05/2024 11:49:57
*/


-- ----------------------------
-- Table structure for Organizations
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Organizations]') AND type IN ('U'))
	DROP TABLE [dbo].[Organizations]
GO

CREATE TABLE [dbo].[Organizations] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [name] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [image] varchar(300) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [banner_image] varchar(300) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [acronym] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [slogan] varchar(600) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [content] varchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [proposal] varchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [additional_files] varchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [status] bit  NULL,
  [CreatedAt] datetime  NULL,
  [UpdatedAt] datetime  NULL,
  [DeletedAt] datetime  NULL
)
GO

ALTER TABLE [dbo].[Organizations] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Auto increment value for Organizations
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Organizations]', RESEED, 4)
GO


-- ----------------------------
-- Primary Key structure for table Organizations
-- ----------------------------
ALTER TABLE [dbo].[Organizations] ADD CONSTRAINT [PK__Organiza__3213E83FFB138A36] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

