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

 Date: 31/05/2024 10:38:47
*/


-- ----------------------------
-- Table structure for Inscriptions
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Inscriptions]') AND type IN ('U'))
	DROP TABLE [dbo].[Inscriptions]
GO

CREATE TABLE [dbo].[Inscriptions] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [CompanyId] int  NOT NULL,
  [StudentId] int  NOT NULL,
  [UserId] int  NULL,
  [AcademicYearId] int  NULL,
  [PhotographyStatus] int  NULL,
  [RequestStatus] int  NULL,
  [IsActive] bit  NULL,
  [FileUrl] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Comment1] varchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Comment2] varchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Comment3] varchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [StudentRepresentativeInfo] varchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IntegrationInformation] varchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [AdditionalInformation] varchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Notes] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [SecondRepresentativeReason] int  NULL,
  [SecondRepresentativeRegistration] bit  NULL,
  [SingServiceId] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [SingServiceUrl] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [SingServiceStatus] bit  NULL,
  [SingTransportId] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [SingTransportUrl] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [SingTransportStatus] bit  NULL,
  [BankSingServiceId] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [BankSingServiceUrl] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [BankSingServiceStatus] bit  NULL,
  [PreviousStatus] int  NULL,
  [EnrollmentProcess] varchar(800) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [SecondRepresentativeJustificationUrl] varchar(300) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [CompleteReportUrl] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [PreviousGradeCourseId] int  NULL,
  [LevelId] int  NULL,
  [GradeCourseId] int  NULL,
  [ParallelId] int  NULL,
  [SpecializationId] int  NULL,
  [IsRepeater] bit  NULL,
  [EnrollmentProcessStatus] bit  NULL,
  [CompleteDocumentation] bit  NULL,
  [EnrollmentDate] datetime  NULL,
  [Withdrawn] bit  NULL,
  [WithdrawalDate] date  NULL,
  [WithdrawalDestination] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [InternationalSection] bit  NULL,
  [EnrollmentType] int  NULL,
  [AcceptConditions] bit  NULL,
  [AuthorizeImageUsage] bit  NULL,
  [AuthorizeImagePublication] bit  NULL,
  [AuthorizeImageSharing] bit  NULL,
  [AuthorizeImageRetention] bit  NULL,
  [PreviousEstablishmentCountryId] int  NULL,
  [PreviousEstablishmentCityId] int  NULL,
  [IsScholarshipRecipient] bit  NULL,
  [ScholarshipOrganism] int  NULL,
  [UseTransport] bit  NULL,
  [AcceptTransportConditions] bit  NULL,
  [AuthorizeDeparture] bit  NULL,
  [EmergencyReference] int  NULL,
  [PedagogicalTrip] bit  NULL,
  [InstitutionalRepresentation] bit  NULL,
  [AcceptTerms] bit  NULL,
  [AcceptUseData] bit  NULL,
  [InstitutionQuestion] bit  NULL,
  [WithdrawalQuestion] bit  NULL,
  [NotAuthorizedLeaveAlone] bit  NULL,
  [AuthorizedFreeHoursDeparture] bit  NULL,
  [ExternalId] int  NULL,
  [CreatedAt] datetime  NULL,
  [UpdatedAt] datetime  NULL,
  [DeletedAt] datetime  NULL
)
GO

ALTER TABLE [dbo].[Inscriptions] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Auto increment value for Inscriptions
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Inscriptions]', RESEED, 1003)
GO


-- ----------------------------
-- Uniques structure for table Inscriptions
-- ----------------------------
ALTER TABLE [dbo].[Inscriptions] ADD CONSTRAINT [UQ__Inscript__AE7E026E507D3B60] UNIQUE NONCLUSTERED ([CompanyId] ASC, [StudentId] ASC, [AcademicYearId] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Inscriptions
-- ----------------------------
ALTER TABLE [dbo].[Inscriptions] ADD CONSTRAINT [PK__Inscript__3214EC07C086D04E] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

