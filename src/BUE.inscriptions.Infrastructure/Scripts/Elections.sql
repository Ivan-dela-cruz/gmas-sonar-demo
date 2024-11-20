/*
 Navicat Premium Data Transfer
 
 Source Server         : DEV2022
 Source Server Type    : SQL Server
 Source Server Version : 16001105
 Source Host           : localhost\DEV2022:1433
 Source Catalog        : BUE_WMCR
 Source Schema         : dbo
 
 Target Server Type    : SQL Server
 Target Server Version : 16001105
 File Encoding         : 65001
 
 Date: 13/12/2023 17:19:59
 */
-- ----------------------------
-- Table structure for AcademicYears
-- ----------------------------
IF EXISTS (
    SELECT
        *
    FROM
        sys.all_objects
    WHERE
        object_id = OBJECT_ID(N'[dbo].[AcademicYears]')
        AND type IN ('U')
) DROP TABLE [dbo].[AcademicYears]
GO
    CREATE TABLE [dbo].[AcademicYears] (
        [id] int IDENTITY(1, 1) NOT NULL,
        [name] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
        [folio_number] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
        [status] bit NOT NULL,
        [is_current] bit NULL,
        [start_date] datetime NULL,
        [end_date] datetime NULL,
        [bueId] int NULL,
        [idExternal] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [CreatedAt] datetime NULL,
        [UpdatedAt] datetime NULL,
        [DeletedAt] datetime NULL
    )
GO
ALTER TABLE
    [dbo].[AcademicYears]
SET
    (LOCK_ESCALATION = TABLE)
GO
    -- ----------------------------
    -- Records of AcademicYears
    -- ----------------------------
SET
    IDENTITY_INSERT [dbo].[AcademicYears] ON
GO
INSERT INTO
    [dbo].[AcademicYears] (
        [id],
        [name],
        [folio_number],
        [status],
        [is_current],
        [start_date],
        [end_date],
        [bueId],
        [idExternal],
        [CreatedAt],
        [UpdatedAt],
        [DeletedAt]
    )
VALUES
    (
        N'2',
        N'2023-2024',
        N'2023-204',
        N'1',
        N'1',
        NULL,
        NULL,
        NULL,
        NULL,
        N'2023-12-13 17:13:15.000',
        NULL,
        NULL
    )
GO
SET
    IDENTITY_INSERT [dbo].[AcademicYears] OFF
GO
    -- ----------------------------
    -- Table structure for Candidates
    -- ----------------------------
    IF EXISTS (
        SELECT
            *
        FROM
            sys.all_objects
        WHERE
            object_id = OBJECT_ID(N'[dbo].[Candidates]')
            AND type IN ('U')
    ) DROP TABLE [dbo].[Candidates]
GO
    CREATE TABLE [dbo].[Candidates] (
        [id] int IDENTITY(1, 1) NOT NULL,
        [academic_years_id] int NOT NULL,
        [election_id] int NOT NULL,
        [organization_id] int NOT NULL,
        [position_id] int NOT NULL,
        [first_name] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [second_name] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [lastname] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [second_lastname] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [entry_date] date NULL,
        [exit_date] date NULL,
        [identification] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [email] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [phone1] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [phone2] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [address] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [status] tinyint DEFAULT '1' NOT NULL,
        [abbreviation] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [deleted_at] datetime NULL,
        [created_at] datetime NULL,
        [updated_at] datetime NULL
    )
GO
ALTER TABLE
    [dbo].[Candidates]
SET
    (LOCK_ESCALATION = TABLE)
GO
    -- ----------------------------
    -- Records of Candidates
    -- ----------------------------
SET
    IDENTITY_INSERT [dbo].[Candidates] ON
GO
SET
    IDENTITY_INSERT [dbo].[Candidates] OFF
GO
    -- ----------------------------
    -- Table structure for Elections
    -- ----------------------------
    IF EXISTS (
        SELECT
            *
        FROM
            sys.all_objects
        WHERE
            object_id = OBJECT_ID(N'[dbo].[Elections]')
            AND type IN ('U')
    ) DROP TABLE [dbo].[Elections]
GO
    CREATE TABLE [dbo].[Elections] (
        [id] int IDENTITY(1, 1) NOT NULL,
        [academic_years_id] int NOT NULL,
        [title] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [description] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [date_election] date NULL,
        [start_datetime] datetime NULL,
        [end_datetime] datetime NULL,
        [cover_image] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [thumbnail_image] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [election_type] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [academic_period] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [year] int NULL,
        [results] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [status] tinyint DEFAULT '1' NOT NULL,
        [status_election] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'Abierto' NOT NULL,
        [deleted_at] datetime NULL,
        [created_at] datetime NULL,
        [updated_at] datetime NULL
    )
GO
ALTER TABLE
    [dbo].[Elections]
SET
    (LOCK_ESCALATION = TABLE)
GO
    -- ----------------------------
    -- Records of Elections
    -- ----------------------------
SET
    IDENTITY_INSERT [dbo].[Elections] ON
GO
SET
    IDENTITY_INSERT [dbo].[Elections] OFF
GO
    -- ----------------------------
    -- Table structure for Organizations
    -- ----------------------------
    IF EXISTS (
        SELECT
            *
        FROM
            sys.all_objects
        WHERE
            object_id = OBJECT_ID(N'[dbo].[Organizations]')
            AND type IN ('U')
    ) DROP TABLE [dbo].[Organizations]
GO
    CREATE TABLE [dbo].[Organizations] (
        [id] int IDENTITY(1, 1) NOT NULL,
        [academic_years_id] int NOT NULL,
        [election_id] int NOT NULL,
        [name] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [image] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [list] int NULL,
        [acronym] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [representative] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [members] int NULL,
        [status] tinyint DEFAULT '1' NOT NULL,
        [deleted_at] datetime NULL,
        [created_at] datetime NULL,
        [updated_at] datetime NULL
    )
GO
ALTER TABLE
    [dbo].[Organizations]
SET
    (LOCK_ESCALATION = TABLE)
GO
    -- ----------------------------
    -- Records of Organizations
    -- ----------------------------
SET
    IDENTITY_INSERT [dbo].[Organizations] ON
GO
SET
    IDENTITY_INSERT [dbo].[Organizations] OFF
GO
    -- ----------------------------
    -- Table structure for Positions
    -- ----------------------------
    IF EXISTS (
        SELECT
            *
        FROM
            sys.all_objects
        WHERE
            object_id = OBJECT_ID(N'[dbo].[Positions]')
            AND type IN ('U')
    ) DROP TABLE [dbo].[Positions]
GO
    CREATE TABLE [dbo].[Positions] (
        [id] int IDENTITY(1, 1) NOT NULL,
        [election_id] int NOT NULL,
        [academic_years_id] int NOT NULL,
        [name] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [description] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [image] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [status] tinyint DEFAULT '1' NOT NULL,
        [abbreviation] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [deleted_at] datetime NULL,
        [created_at] datetime NULL,
        [updated_at] datetime NULL
    )
GO
ALTER TABLE
    [dbo].[Positions]
SET
    (LOCK_ESCALATION = TABLE)
GO
    -- ----------------------------
    -- Records of Positions
    -- ----------------------------
SET
    IDENTITY_INSERT [dbo].[Positions] ON
GO
SET
    IDENTITY_INSERT [dbo].[Positions] OFF
GO
    -- ----------------------------
    -- Auto increment value for AcademicYears
    -- ----------------------------
    DBCC CHECKIDENT ('[dbo].[AcademicYears]', RESEED, 2)
GO
    -- ----------------------------
    -- Primary Key structure for table AcademicYears
    -- ----------------------------
ALTER TABLE
    [dbo].[AcademicYears]
ADD
    CONSTRAINT [PK__academic__3213E83F5BCF2D98] PRIMARY KEY CLUSTERED ([id]) WITH (
        PAD_INDEX = OFF,
        STATISTICS_NORECOMPUTE = OFF,
        IGNORE_DUP_KEY = OFF,
        ALLOW_ROW_LOCKS = ON,
        ALLOW_PAGE_LOCKS = ON
    ) ON [PRIMARY]
GO
    -- ----------------------------
    -- Auto increment value for Candidates
    -- ----------------------------
    DBCC CHECKIDENT ('[dbo].[Candidates]', RESEED, 1)
GO
    -- ----------------------------
    -- Primary Key structure for table Candidates
    -- ----------------------------
ALTER TABLE
    [dbo].[Candidates]
ADD
    CONSTRAINT [PK__candidat__3213E83F0F4E362E] PRIMARY KEY CLUSTERED ([id]) WITH (
        PAD_INDEX = OFF,
        STATISTICS_NORECOMPUTE = OFF,
        IGNORE_DUP_KEY = OFF,
        ALLOW_ROW_LOCKS = ON,
        ALLOW_PAGE_LOCKS = ON
    ) ON [PRIMARY]
GO
    -- ----------------------------
    -- Auto increment value for Elections
    -- ----------------------------
    DBCC CHECKIDENT ('[dbo].[Elections]', RESEED, 1)
GO
    -- ----------------------------
    -- Checks structure for table Elections
    -- ----------------------------
ALTER TABLE
    [dbo].[Elections]
ADD
    CONSTRAINT [CK__elections__statu__4BAC3F29] CHECK (
        [status_election] = N'Cancelado'
        OR [status_election] = N'Finalizado'
        OR [status_election] = N'En Proceso'
        OR [status_election] = N'Abierto'
    )
GO
    -- ----------------------------
    -- Primary Key structure for table Elections
    -- ----------------------------
ALTER TABLE
    [dbo].[Elections]
ADD
    CONSTRAINT [PK__election__3213E83F6E2288A3] PRIMARY KEY CLUSTERED ([id]) WITH (
        PAD_INDEX = OFF,
        STATISTICS_NORECOMPUTE = OFF,
        IGNORE_DUP_KEY = OFF,
        ALLOW_ROW_LOCKS = ON,
        ALLOW_PAGE_LOCKS = ON
    ) ON [PRIMARY]
GO
    -- ----------------------------
    -- Auto increment value for Organizations
    -- ----------------------------
    DBCC CHECKIDENT ('[dbo].[Organizations]', RESEED, 1)
GO
    -- ----------------------------
    -- Primary Key structure for table Organizations
    -- ----------------------------
ALTER TABLE
    [dbo].[Organizations]
ADD
    CONSTRAINT [PK__organiza__3213E83F79D7B100] PRIMARY KEY CLUSTERED ([id]) WITH (
        PAD_INDEX = OFF,
        STATISTICS_NORECOMPUTE = OFF,
        IGNORE_DUP_KEY = OFF,
        ALLOW_ROW_LOCKS = ON,
        ALLOW_PAGE_LOCKS = ON
    ) ON [PRIMARY]
GO
    -- ----------------------------
    -- Auto increment value for Positions
    -- ----------------------------
    DBCC CHECKIDENT ('[dbo].[Positions]', RESEED, 1)
GO
    -- ----------------------------
    -- Primary Key structure for table Positions
    -- ----------------------------
ALTER TABLE
    [dbo].[Positions]
ADD
    CONSTRAINT [PK__position__3213E83F6FD27CE2] PRIMARY KEY CLUSTERED ([id]) WITH (
        PAD_INDEX = OFF,
        STATISTICS_NORECOMPUTE = OFF,
        IGNORE_DUP_KEY = OFF,
        ALLOW_ROW_LOCKS = ON,
        ALLOW_PAGE_LOCKS = ON
    ) ON [PRIMARY]
GO
    -- ----------------------------
    -- Foreign Keys structure for table Candidates
    -- ----------------------------
ALTER TABLE
    [dbo].[Candidates]
ADD
    CONSTRAINT [candidates_election_id_foreign] FOREIGN KEY ([election_id]) REFERENCES [dbo].[Elections] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE
    [dbo].[Candidates]
ADD
    CONSTRAINT [candidates_organization_id_foreign] FOREIGN KEY ([organization_id]) REFERENCES [dbo].[Organizations] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE
    [dbo].[Candidates]
ADD
    CONSTRAINT [candidates_position_id_foreign] FOREIGN KEY ([position_id]) REFERENCES [dbo].[Positions] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE
    [dbo].[Candidates]
ADD
    CONSTRAINT [candidates_academic_years_id_foreign] FOREIGN KEY ([academic_years_id]) REFERENCES [dbo].[AcademicYears] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
    -- ----------------------------
    -- Foreign Keys structure for table Elections
    -- ----------------------------
ALTER TABLE
    [dbo].[Elections]
ADD
    CONSTRAINT [elections_academic_years_id_foreign] FOREIGN KEY ([academic_years_id]) REFERENCES [dbo].[AcademicYears] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
    -- ----------------------------
    -- Foreign Keys structure for table Organizations
    -- ----------------------------
ALTER TABLE
    [dbo].[Organizations]
ADD
    CONSTRAINT [organizations_election_id_foreign] FOREIGN KEY ([election_id]) REFERENCES [dbo].[Elections] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE
    [dbo].[Organizations]
ADD
    CONSTRAINT [organizations_academic_years_id_foreign] FOREIGN KEY ([academic_years_id]) REFERENCES [dbo].[AcademicYears] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
    -- ----------------------------
    -- Foreign Keys structure for table Positions
    -- ----------------------------
ALTER TABLE
    [dbo].[Positions]
ADD
    CONSTRAINT [positions_election_id_foreign] FOREIGN KEY ([election_id]) REFERENCES [dbo].[Elections] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE
    [dbo].[Positions]
ADD
    CONSTRAINT [positions_academic_years_id_foreign] FOREIGN KEY ([academic_years_id]) REFERENCES [dbo].[AcademicYears] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO



CREATE TABLE [dbo].[CandidateElection](
	[id] [int] IDENTITY(1,1) primary key NOT NULL,
	[academic_years_id] [int] NOT NULL,
	[election_id] [int] NOT NULL,
	[organization_id] [int] NULL,
	[position_id] [int] NOT NULL,
	[candidate_id] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
	[status] [bit] NULL,
)