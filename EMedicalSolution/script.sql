USE [master]
GO
/****** Object:  Database [emedical321_PatientMgmt]    Script Date: 10/13/2017 11:02:13 PM ******/
CREATE DATABASE [emedical321_PatientMgmt]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'emedical321_PatientMgmt', FILENAME = N'E:\MSSQL.MSSQLSERVER\DATA\emedical321_PatientMgmt.mdf' , SIZE = 5440KB , MAXSIZE = 204800KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'emedical321_PatientMgmt_log', FILENAME = N'D:\MSSQL.MSSQLSERVER\DATA\emedical321_PatientMgmt_log.ldf' , SIZE = 1792KB , MAXSIZE = 102400KB , FILEGROWTH = 1024KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [emedical321_PatientMgmt].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [emedical321_PatientMgmt] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET ARITHABORT OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET  ENABLE_BROKER 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET  MULTI_USER 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [emedical321_PatientMgmt] SET DB_CHAINING OFF 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [emedical321_PatientMgmt] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [emedical321_PatientMgmt]
GO
/****** Object:  User [emedical321]    Script Date: 10/13/2017 11:02:15 PM ******/
CREATE USER [emedical321] FOR LOGIN [emedical321] WITH DEFAULT_SCHEMA=[emedical321]
GO
/****** Object:  DatabaseRole [gd_execprocs]    Script Date: 10/13/2017 11:02:16 PM ******/
CREATE ROLE [gd_execprocs]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [emedical321]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [emedical321]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [emedical321]
GO
ALTER ROLE [db_datareader] ADD MEMBER [emedical321]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [emedical321]
GO
/****** Object:  Schema [emedical321]    Script Date: 10/13/2017 11:02:16 PM ******/
CREATE SCHEMA [emedical321]
GO
/****** Object:  Table [dbo].[Diseases]    Script Date: 10/13/2017 11:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Diseases](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[DiseaseTypeID] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Diseases] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DiseaseTypes]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DiseaseTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_DiseaseTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InsuranceCardPictures]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InsuranceCardPictures](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[FilePath] [varchar](150) NOT NULL,
	[HistoryID] [int] NOT NULL,
	[PatientID] [int] NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_InsuranceCardPictures] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InsuranceTypes]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InsuranceTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](20) NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_InsuranceTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IntakeFormHead]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IntakeFormHead](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HistoryID] [int] NOT NULL,
	[HaveSupportDevice] [varchar](10) NOT NULL,
	[isPregnant] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_IntakeFormHead] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InterferingConditions]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InterferingConditions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_InterferingConditions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MedicalNecessities]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MedicalNecessities](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ICD10Code] [varchar](20) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_MedicalNecessities] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Offices]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Offices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](20) NOT NULL,
	[Description] [varchar](50) NULL,
	[Location] [varchar](100) NOT NULL,
	[Tel] [varchar](15) NULL,
	[Email] [varchar](50) NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Offices] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PatientDiseases]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientDiseases](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HistoryID] [int] NOT NULL,
	[IntakeFormID] [int] NOT NULL,
	[DiseaseID] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_PatientDiseases] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PatientHistory]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PatientHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NULL,
	[InsuranceTypeID] [int] NULL,
	[MedicalNecessitiesID] [int] NULL,
	[SecondaryNecessities] [varchar](100) NULL,
	[OfficeID] [int] NULL,
	[StatusID] [int] NULL,
	[PhysicianRemarks] [varchar](150) NULL,
	[isApprovedByPhysician] [bit] NULL,
	[PhysicianID] [int] NULL,
	[PhysicianApprovedDate] [datetime] NULL,
	[SpecialistRemarks] [varchar](500) NULL,
	[isApprovedBySpecialist] [bit] NULL,
	[SpecialistID] [int] NULL,
	[SpecialistApprovedDate] [datetime] NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_PatientHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PatientInterferingCondition]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientInterferingCondition](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HistoryID] [int] NOT NULL,
	[IntakeFormID] [int] NOT NULL,
	[PatientConditionID] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_PatientInterferingCondition] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PatientNecessities]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientNecessities](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NecessityID] [int] NOT NULL,
	[HistoryID] [int] NOT NULL,
	[PatientID] [int] NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_PatientNecessities] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PatientProcedures]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PatientProcedures](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HistoryID] [int] NOT NULL,
	[ProcedureTypeID] [int] NOT NULL,
	[FilePath] [varchar](150) NULL,
	[FileDate] [datetime] NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_PatientProcedures] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PatientReports]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PatientReports](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[FilePath] [varchar](150) NOT NULL,
	[HistoryID] [int] NOT NULL,
	[PatientID] [int] NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_PatientReports] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Patients](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](20) NOT NULL,
	[LastName] [varchar](20) NOT NULL,
	[DOB] [date] NOT NULL,
	[Gender] [char](6) NOT NULL,
	[IDNumber] [varchar](20) NULL,
	[Tel] [varchar](15) NULL,
	[Email] [varchar](50) NULL,
	[OfficeID] [int] NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PatientSymtoms]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientSymtoms](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HistoryID] [int] NOT NULL,
	[IntakeFormID] [int] NOT NULL,
	[SymptomID] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_PatientSymtoms] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProcedureTypes]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProcedureTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ProcedureTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Staff](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](20) NOT NULL,
	[LastName] [varchar](20) NOT NULL,
	[DOB] [date] NOT NULL,
	[Gender] [char](6) NOT NULL,
	[IDNumber] [varchar](20) NULL,
	[Tel] [varchar](15) NULL,
	[Email] [varchar](50) NULL,
	[SignatureFilePath] [varchar](200) NULL,
	[SignaturePassword] [nvarchar](20) NULL,
	[OfficeID] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Statuses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Symptoms]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Symptoms](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Symptoms] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/13/2017 11:02:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](20) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[RoleID] [int] NOT NULL,
	[StaffID] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Diseases] ON 

INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (4, N'Diabetes', 3, CAST(0x0000A733015BEC66 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (5, N'Atherosclerosis', 3, CAST(0x0000A733015BF748 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (6, N'Syncope & Collapse', 3, CAST(0x0000A733015BFA8A AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (7, N'Syncope & Collapse', 3, CAST(0x0000A733015BFBBB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (8, N'Orthostatic Hypotension', 3, CAST(0x0000A733015BFD04 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (9, N'Tachycardia', 3, CAST(0x0000A733015BFDDC AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (10, N'Small Vessel Disease', 3, CAST(0x0000A733015BFF85 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (11, N'Physical Injury', 3, CAST(0x0000A733015C00A4 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (12, N'Cancer', 3, CAST(0x0000A733015C0257 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (13, N'Diabetes Angiopathy', 3, CAST(0x0000A733015C05C8 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (14, N'Raynaud’s Disease ', 1, CAST(0x0000A733015C0710 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (15, N'Guillain-Barrè Syndrome', 1, CAST(0x0000A733015C08B2 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (16, N'Lupus', 1, CAST(0x0000A733015C09EF AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (17, N'Rheumatoid Arthritis', 1, CAST(0x0000A733015CB98E AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (18, N'Shingles', 2, CAST(0x0000A733015CC73D AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (19, N'HIV / AIDS', 2, CAST(0x0000A733015CD095 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (20, N'Epstein-Barr Virus', 2, CAST(0x0000A733015CD99E AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (21, N'Lyme Disease', 2, CAST(0x0000A733015CE1E8 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (22, N'Hepatitis', 2, CAST(0x0000A733015CEB6A AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (23, N'R.S.D.', 2, CAST(0x0000A733015CF309 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (24, N'Kidney Disease', 2, CAST(0x0000A733015CFACA AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (25, N'Thyroid Disease', 2, CAST(0x0000A733015D026C AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (26, N'Alcohol/Drug Abuse', 2, CAST(0x0000A733015D0A1C AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (27, N'Amyloidosis', 2, CAST(0x0000A733015D120C AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (29, N'Sleep Apnea', 2, CAST(0x0000A733015D439D AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (30, N'Parkinson’s Disease', 2, CAST(0x0000A733015D4CDC AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (31, N'Multiple Sclerosis', 2, CAST(0x0000A733015D5874 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Diseases] ([ID], [Title], [DiseaseTypeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (32, N'Dementia', 2, CAST(0x0000A733015D62C4 AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Diseases] OFF
SET IDENTITY_INSERT [dbo].[DiseaseTypes] ON 

INSERT [dbo].[DiseaseTypes] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1, N'Autoimmune Diseases', CAST(0x0000A733015B8F13 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[DiseaseTypes] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (2, N'Infectious Diseases', CAST(0x0000A733015B9BE5 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[DiseaseTypes] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (3, N'Other', CAST(0x0000A733015BADF5 AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[DiseaseTypes] OFF
SET IDENTITY_INSERT [dbo].[InsuranceCardPictures] ON 

INSERT [dbo].[InsuranceCardPictures] ([ID], [Title], [FilePath], [HistoryID], [PatientID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (0, N'aetna', N'G:\PleskVhosts\e-medicalsolution.com\httpdocs\Files\Cards\Card_1.png', 1, 2, CAST(0x0000A77100BFCC34 AS DateTime), 2, NULL, NULL)
INSERT [dbo].[InsuranceCardPictures] ([ID], [Title], [FilePath], [HistoryID], [PatientID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1, N'test', N'G:\PleskVhosts\e-medicalsolution.com\httpdocs\Files\Cards\Card_1.png', 2, 3, CAST(0x0000A7F3009226AB AS DateTime), 2, NULL, NULL)
INSERT [dbo].[InsuranceCardPictures] ([ID], [Title], [FilePath], [HistoryID], [PatientID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (2, N'Testing', N'G:\PleskVhosts\e-medicalsolution.com\httpdocs\Files\Cards\Card_1.png', 3, 2, CAST(0x0000A80A007549D5 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[InsuranceCardPictures] ([ID], [Title], [FilePath], [HistoryID], [PatientID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (3, N'Testing', N'G:\PleskVhosts\e-medicalsolution.com\httpdocs\Files\Cards\Card_1.png', 4, 2, CAST(0x0000A80A007584D5 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[InsuranceCardPictures] ([ID], [Title], [FilePath], [HistoryID], [PatientID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (4, N'Testing', N'G:\PleskVhosts\e-medicalsolution.com\httpdocs\Files\Cards\Card_1.png', 5, 2, CAST(0x0000A80A007599F2 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[InsuranceCardPictures] ([ID], [Title], [FilePath], [HistoryID], [PatientID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (5, N'Testing', N'G:\PleskVhosts\e-medicalsolution.com\httpdocs\Files\Cards\Card_1.png', 6, 2, CAST(0x0000A80A0075C3AC AS DateTime), 1, NULL, NULL)
INSERT [dbo].[InsuranceCardPictures] ([ID], [Title], [FilePath], [HistoryID], [PatientID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (6, N'Testing', N'G:\PleskVhosts\e-medicalsolution.com\httpdocs\Files\Cards\Card_1.png', 7, 2, CAST(0x0000A80A00760B95 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[InsuranceCardPictures] ([ID], [Title], [FilePath], [HistoryID], [PatientID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (7, N'Testing', N'G:\PleskVhosts\e-medicalsolution.com\httpdocs\Files\Cards\Card_1.png', 8, 2, CAST(0x0000A80A007651E9 AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[InsuranceCardPictures] OFF
SET IDENTITY_INSERT [dbo].[InsuranceTypes] ON 

INSERT [dbo].[InsuranceTypes] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1, N'Primary', CAST(0x0000A733015DADEA AS DateTime), 1, NULL, NULL)
INSERT [dbo].[InsuranceTypes] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (2, N'Secondary', CAST(0x0000A733015DB4F0 AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[InsuranceTypes] OFF
SET IDENTITY_INSERT [dbo].[IntakeFormHead] ON 

INSERT [dbo].[IntakeFormHead] ([ID], [HistoryID], [HaveSupportDevice], [isPregnant], [Date], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (7, 1, N'Yes', 0, CAST(0xCC3C0B00 AS Date), CAST(0x0000A77100CA6E78 AS DateTime), 3, NULL, NULL)
SET IDENTITY_INSERT [dbo].[IntakeFormHead] OFF
SET IDENTITY_INSERT [dbo].[InterferingConditions] ON 

INSERT [dbo].[InterferingConditions] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1, N'Sleep', CAST(0x0000A7600147D520 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[InterferingConditions] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (2, N'Work', CAST(0x0000A7330163C37A AS DateTime), 1, NULL, NULL)
INSERT [dbo].[InterferingConditions] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (3, N'Walking', CAST(0x0000A7330163C380 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[InterferingConditions] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (4, N'Standing', CAST(0x0000A7330163C380 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[InterferingConditions] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (6, N'Daily Activities', CAST(0x0000A7330163C38C AS DateTime), 1, NULL, NULL)
INSERT [dbo].[InterferingConditions] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (7, N'Driving', CAST(0x0000A7330163C38C AS DateTime), 1, NULL, NULL)
INSERT [dbo].[InterferingConditions] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (8, N'Family Time', CAST(0x0000A7330163C38C AS DateTime), 1, NULL, NULL)
INSERT [dbo].[InterferingConditions] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (9, N'Recreational Activities/Exercising', CAST(0x0000A7330163EB57 AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[InterferingConditions] OFF
SET IDENTITY_INSERT [dbo].[MedicalNecessities] ON 

INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1, N'E10.41', N'Type 1 diabetes mellitus with diabetic mononeurpathy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (2, N'E10.42', N'Type 1 diabetes mellitus with diabetic polyneuropathy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (4, N'E10.44', N'Type 1 diabetes mellitus with diabetic amyotrophy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (5, N'E10.49', N'Type 1 diabetes mellitus with other diabtic neurological complication', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (6, N'E10.610', N'Type 1 diabetes mellitus with diabetic neuropathic arthropathy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (7, N'E11.41', N'Type 2 diabetes mellitus with diabetic mononeuropathy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (8, N'E11.42', N'Type 2 diabetes mellitus with diabetic polyneuropathy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (9, N'E11.43', N'Type 2 diabetes mellitus with diabetic autonomic (poly) neuropathy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (10, N'E11.44', N'Type 2 mellitus with diabetic amyotrophy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (11, N'E11.49', N'Type 2 diabetes mellitus with other diabetic neurological complication', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (12, N'E11.610', N'Type 2 diabetes mellitus with diabetic neuropathic arthropathy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (13, N'E13.41', N'Other specified diabetes mellitus with diabetic mononeuropathy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (14, N'E13.42', N'Other specified diabetes mellitus with diabetic polyneuropathy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (15, N'E13.43', N'Other specified diabetes mellitus with diabetic autonomic (poly)neuropathy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (16, N'E13.44', N'Other specified diabetes mellitus with diabetic amyotrophy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (17, N'E13.49', N'Other specified diabetes mellitus with other diabetic neurological complication', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (18, N'E13.610', N'Other specified diabetes mellitus with diabetic neuropathic arthropathy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (19, N'E85.0', N'Non-neuropathic heredofamilial amyloidosis', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (20, N'E85.1', N'Neuropathy heredofamilial amyloidosis', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (21, N'E85.3', N'Secondary systemic amyloidosis', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (22, N'E85.4', N'Organ-limited amyloidosis', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (23, N'E85.8', N'Other amyloidosis', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (24, N'G60.3', N'Idiopathic progressive neuropathy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (25, N'G60.8', N'Other hereditary and idiopathic neuropathies', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (26, N'G90.09', N'Other idiopathic peripheral autonomic neuropathy', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (27, N'G90.3', N'Multi-system degeneration of the autonomic nervous system', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (28, N'G90.50', N'Complex regional pain syndrome I, unspecified', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (29, N'G90.511', N'Complex regional pain syndrome I of right upper limb ', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (30, N'G90.512', N'Complex regional pain syndrome I of left upper limb', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (31, N'G90.513', N'Complex regional pain syndrome I of upper limb, bilateral', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (32, N'G90.521', N'Complex regonal pain syndrome I of right lower limb', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (33, N'G90.522', N'Complex regional pain syndrome I of left lower limb', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (34, N'G90.523', N'Complex regional pain syndrome I of lower limb, bilateral', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (35, N'G90.59', N'Complex regional pain syndrome I of other specified site', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (36, N'I95.1', N'Orthostatic Hypotension', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (37, N'R00.0', N'Tachycardia, unspecified', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (38, N'R55', N'Syncope and Collapse', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (39, N'R61', N'Generalized hyperhidrosis', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (40, N'G23.0', N'Hallervorden-Spatz disease', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (41, N'G23.1', N'Progressive supreanuclear ophthalmoplegia (Steele-Richardson-Olszewski)', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (42, N'G23.2', N'Striatonigral degeneration', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[MedicalNecessities] ([ID], [ICD10Code], [Description], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (43, N'G23.8', N'Other specified degenerative diseases of basal ganglia', CAST(0x0000A733015F45AB AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[MedicalNecessities] OFF
SET IDENTITY_INSERT [dbo].[Offices] ON 

INSERT [dbo].[Offices] ([ID], [Title], [Description], [Location], [Tel], [Email], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1, N'Head Office', N'Abc, xyz', N'xyz', N'+134098439', N'ho@gmail.com', CAST(0x0000A7540130FEB1 AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Offices] OFF
SET IDENTITY_INSERT [dbo].[PatientDiseases] ON 

INSERT [dbo].[PatientDiseases] ([ID], [HistoryID], [IntakeFormID], [DiseaseID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (0, 1, 7, 4, CAST(0x0000A77100CA6E86 AS DateTime), 3, NULL, NULL)
SET IDENTITY_INSERT [dbo].[PatientDiseases] OFF
SET IDENTITY_INSERT [dbo].[PatientHistory] ON 

INSERT [dbo].[PatientHistory] ([ID], [PatientID], [InsuranceTypeID], [MedicalNecessitiesID], [SecondaryNecessities], [OfficeID], [StatusID], [PhysicianRemarks], [isApprovedByPhysician], [PhysicianID], [PhysicianApprovedDate], [SpecialistRemarks], [isApprovedBySpecialist], [SpecialistID], [SpecialistApprovedDate], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1, 2, 1, 1, N'E.875', NULL, 5, N' see neuro', 1, 3, NULL, NULL, NULL, NULL, CAST(0x0000A77100CC467C AS DateTime), CAST(0x0000A77100BFCC34 AS DateTime), 2, NULL, NULL)
INSERT [dbo].[PatientHistory] ([ID], [PatientID], [InsuranceTypeID], [MedicalNecessitiesID], [SecondaryNecessities], [OfficeID], [StatusID], [PhysicianRemarks], [isApprovedByPhysician], [PhysicianID], [PhysicianApprovedDate], [SpecialistRemarks], [isApprovedBySpecialist], [SpecialistID], [SpecialistApprovedDate], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (2, 3, 1, 1, N'', NULL, 6, N' Signature', 1, 3, NULL, N' go', 1, 4, CAST(0x0000A7F300C44FB5 AS DateTime), CAST(0x0000A7F3009226AB AS DateTime), 2, NULL, NULL)
INSERT [dbo].[PatientHistory] ([ID], [PatientID], [InsuranceTypeID], [MedicalNecessitiesID], [SecondaryNecessities], [OfficeID], [StatusID], [PhysicianRemarks], [isApprovedByPhysician], [PhysicianID], [PhysicianApprovedDate], [SpecialistRemarks], [isApprovedBySpecialist], [SpecialistID], [SpecialistApprovedDate], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (3, 2, 1, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A80A007549D5 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PatientHistory] ([ID], [PatientID], [InsuranceTypeID], [MedicalNecessitiesID], [SecondaryNecessities], [OfficeID], [StatusID], [PhysicianRemarks], [isApprovedByPhysician], [PhysicianID], [PhysicianApprovedDate], [SpecialistRemarks], [isApprovedBySpecialist], [SpecialistID], [SpecialistApprovedDate], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (4, 2, 1, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A80A007584D5 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PatientHistory] ([ID], [PatientID], [InsuranceTypeID], [MedicalNecessitiesID], [SecondaryNecessities], [OfficeID], [StatusID], [PhysicianRemarks], [isApprovedByPhysician], [PhysicianID], [PhysicianApprovedDate], [SpecialistRemarks], [isApprovedBySpecialist], [SpecialistID], [SpecialistApprovedDate], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (5, 2, 1, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A80A007599F2 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PatientHistory] ([ID], [PatientID], [InsuranceTypeID], [MedicalNecessitiesID], [SecondaryNecessities], [OfficeID], [StatusID], [PhysicianRemarks], [isApprovedByPhysician], [PhysicianID], [PhysicianApprovedDate], [SpecialistRemarks], [isApprovedBySpecialist], [SpecialistID], [SpecialistApprovedDate], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (6, 2, 1, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A80A0075C3AC AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PatientHistory] ([ID], [PatientID], [InsuranceTypeID], [MedicalNecessitiesID], [SecondaryNecessities], [OfficeID], [StatusID], [PhysicianRemarks], [isApprovedByPhysician], [PhysicianID], [PhysicianApprovedDate], [SpecialistRemarks], [isApprovedBySpecialist], [SpecialistID], [SpecialistApprovedDate], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (7, 2, 1, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A80A00760B95 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[PatientHistory] ([ID], [PatientID], [InsuranceTypeID], [MedicalNecessitiesID], [SecondaryNecessities], [OfficeID], [StatusID], [PhysicianRemarks], [isApprovedByPhysician], [PhysicianID], [PhysicianApprovedDate], [SpecialistRemarks], [isApprovedBySpecialist], [SpecialistID], [SpecialistApprovedDate], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (8, 2, 1, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A80A007651E9 AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[PatientHistory] OFF
SET IDENTITY_INSERT [dbo].[PatientInterferingCondition] ON 

INSERT [dbo].[PatientInterferingCondition] ([ID], [HistoryID], [IntakeFormID], [PatientConditionID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (0, 1, 7, 1, CAST(0x0000A77100CA6E8B AS DateTime), 3, NULL, NULL)
SET IDENTITY_INSERT [dbo].[PatientInterferingCondition] OFF
SET IDENTITY_INSERT [dbo].[PatientNecessities] ON 

INSERT [dbo].[PatientNecessities] ([ID], [NecessityID], [HistoryID], [PatientID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (8, 1, 1, NULL, CAST(0x0000A77100CC2273 AS DateTime), 3, NULL, NULL)
INSERT [dbo].[PatientNecessities] ([ID], [NecessityID], [HistoryID], [PatientID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (9, 2, 1, NULL, CAST(0x0000A77100CC2277 AS DateTime), 3, NULL, NULL)
INSERT [dbo].[PatientNecessities] ([ID], [NecessityID], [HistoryID], [PatientID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (10, 10, 1, NULL, CAST(0x0000A77100CC2277 AS DateTime), 3, NULL, NULL)
INSERT [dbo].[PatientNecessities] ([ID], [NecessityID], [HistoryID], [PatientID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1002, 1, 2, NULL, CAST(0x0000A7F30092ED86 AS DateTime), 2, NULL, NULL)
INSERT [dbo].[PatientNecessities] ([ID], [NecessityID], [HistoryID], [PatientID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1003, 5, 2, NULL, CAST(0x0000A7F30092ED90 AS DateTime), 2, NULL, NULL)
SET IDENTITY_INSERT [dbo].[PatientNecessities] OFF
SET IDENTITY_INSERT [dbo].[PatientProcedures] ON 

INSERT [dbo].[PatientProcedures] ([ID], [HistoryID], [ProcedureTypeID], [FilePath], [FileDate], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (6, 1, 1, NULL, NULL, CAST(0x0000A77100CBF393 AS DateTime), 3, NULL, NULL)
INSERT [dbo].[PatientProcedures] ([ID], [HistoryID], [ProcedureTypeID], [FilePath], [FileDate], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (7, 1, 2, NULL, NULL, CAST(0x0000A77100CBF393 AS DateTime), 3, NULL, NULL)
INSERT [dbo].[PatientProcedures] ([ID], [HistoryID], [ProcedureTypeID], [FilePath], [FileDate], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1006, 2, 3, NULL, NULL, CAST(0x0000A7F300CF11DF AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[PatientProcedures] OFF
SET IDENTITY_INSERT [dbo].[PatientReports] ON 

INSERT [dbo].[PatientReports] ([ID], [Title], [FilePath], [HistoryID], [PatientID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (0, N'', N'G:\PleskVhosts\e-medicalsolution.com\httpdocs\Files\Reports\Report_1.pdf', 2, NULL, CAST(0x0000A7F30092D8BE AS DateTime), 2, NULL, NULL)
INSERT [dbo].[PatientReports] ([ID], [Title], [FilePath], [HistoryID], [PatientID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1, N'', N'G:\PleskVhosts\e-medicalsolution.com\httpdocs\Files\Reports\Report_0.pdf', 2, NULL, CAST(0x0000A7F300CF5FD5 AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[PatientReports] OFF
SET IDENTITY_INSERT [dbo].[Patients] ON 

INSERT [dbo].[Patients] ([ID], [FirstName], [LastName], [DOB], [Gender], [IDNumber], [Tel], [Email], [OfficeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (2, N'Dylan', N'Morvan', CAST(0xDB0A0B00 AS Date), N'Male  ', N'1', N'1', N'Boxmacfl@gmail.com', 1, CAST(0x0000A7700101FB1C AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Patients] ([ID], [FirstName], [LastName], [DOB], [Gender], [IDNumber], [Tel], [Email], [OfficeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (3, N'olga', N'vasquez', CAST(0x7B110B00 AS Date), N'Female', N'1545', N'3051111111', N'dylanmorvan@yahoo.com', 1, CAST(0x0000A7F30091AE5E AS DateTime), 2, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Patients] OFF
SET IDENTITY_INSERT [dbo].[PatientSymtoms] ON 

INSERT [dbo].[PatientSymtoms] ([ID], [HistoryID], [IntakeFormID], [SymptomID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (0, 1, 7, 1, CAST(0x0000A77100CA6E7D AS DateTime), 3, NULL, NULL)
SET IDENTITY_INSERT [dbo].[PatientSymtoms] OFF
SET IDENTITY_INSERT [dbo].[ProcedureTypes] ON 

INSERT [dbo].[ProcedureTypes] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1, N'Autonomic Nervous System (ANS) Test', CAST(0x0000A760017223C7 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ProcedureTypes] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (2, N'Vascular Study / Ankle Brachial Index (ABI) Test', CAST(0x0000A733016060AD AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ProcedureTypes] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (3, N'Home Sleep Study (HST) Test', CAST(0x0000A7330160696E AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ProcedureTypes] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (4, N'Allergy Test', CAST(0x0000A733016071CC AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ProcedureTypes] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (5, N'CPAP', CAST(0x0000A7F300912712 AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ProcedureTypes] OFF
SET IDENTITY_INSERT [dbo].[Staff] ON 

INSERT [dbo].[Staff] ([ID], [FirstName], [LastName], [DOB], [Gender], [IDNumber], [Tel], [Email], [SignatureFilePath], [SignaturePassword], [OfficeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1, N'doc', N'doc', CAST(0xA4040B00 AS Date), N'Male  ', N'321-4654-66', N'+132164564', N'boxmacfl@gmail.com', N'E:\Clients project\EMedicalSolution\EMedicalSolution\EMedicalSolution\Files\Signatures\Signature_1.jpg', N'doc', 1, CAST(0x0000A73A015F5348 AS DateTime), 1, CAST(0x0000A760017D9423 AS DateTime), 1)
INSERT [dbo].[Staff] ([ID], [FirstName], [LastName], [DOB], [Gender], [IDNumber], [Tel], [Email], [SignatureFilePath], [SignaturePassword], [OfficeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (2, N'test1', N'test', CAST(0x37030B00 AS Date), N'Male  ', N'111', N'3059227654', N'DYLANMORVAN@YAHOO.COM', NULL, NULL, 1, CAST(0x0000A77100BCEA60 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Staff] ([ID], [FirstName], [LastName], [DOB], [Gender], [IDNumber], [Tel], [Email], [SignatureFilePath], [SignaturePassword], [OfficeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (3, N'Doc', N'Doc', CAST(0xA4040B00 AS Date), N'Male  ', N'222', N'3212706513', N'boxmacfl@gmail.com', NULL, NULL, 1, CAST(0x0000A77100C3561B AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Staff] ([ID], [FirstName], [LastName], [DOB], [Gender], [IDNumber], [Tel], [Email], [SignatureFilePath], [SignaturePassword], [OfficeID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (4, N'specialist', N'specialist', CAST(0x37030B00 AS Date), N'Male  ', N'001', N'3059227654', N'DYLANMORVAN@YAHOO.COM', NULL, NULL, 1, CAST(0x0000A7F300C3EA24 AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Staff] OFF
SET IDENTITY_INSERT [dbo].[Statuses] ON 

INSERT [dbo].[Statuses] ([ID], [Title]) VALUES (1, N'Pending')
INSERT [dbo].[Statuses] ([ID], [Title]) VALUES (2, N'Completed')
INSERT [dbo].[Statuses] ([ID], [Title]) VALUES (3, N'Physician approval pending')
INSERT [dbo].[Statuses] ([ID], [Title]) VALUES (4, N'Specialist approval pending')
INSERT [dbo].[Statuses] ([ID], [Title]) VALUES (5, N'Approved by Physician')
INSERT [dbo].[Statuses] ([ID], [Title]) VALUES (6, N'Approved by Specialist')
SET IDENTITY_INSERT [dbo].[Statuses] OFF
SET IDENTITY_INSERT [dbo].[Symptoms] ON 

INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1, N'Aching Pain', CAST(0x0000A76001464C17 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (2, N'Stabbing Pain', CAST(0x0000A7330162902D AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (3, N'Sharp Pain', CAST(0x0000A7330162902D AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (4, N'Throbbing Pain', CAST(0x0000A73301629033 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (5, N'Difficulty Breathing', CAST(0x0000A73301629033 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (6, N'Excessive Sweating', CAST(0x0000A73301629033 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (7, N'Erectile Dysfunction', CAST(0x0000A73301629033 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (8, N'Ulcers / Colitis / Diarrhea', CAST(0x0000A73301629033 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (9, N'Leg Numbness/Weakness', CAST(0x0000A73301629033 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (10, N'Burning', CAST(0x0000A73301629033 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (11, N'Numbness', CAST(0x0000A754012BB736 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (12, N'Tingling', CAST(0x0000A73301629034 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (13, N'Dead Feeling', CAST(0x0000A73301629034 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (14, N'Irregular Heart Beat', CAST(0x0000A73301629034 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (15, N'Dizziness', CAST(0x0000A73301629034 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (16, N'Loss of Vision', CAST(0x0000A73301629034 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (17, N'Paralysis', CAST(0x0000A73301629034 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (18, N'Weak Pulse in Legs', CAST(0x0000A73301629036 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (19, N'Swelling', CAST(0x0000A73301629036 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (20, N'Tiredness', CAST(0x0000A73301629036 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (21, N'Cramping', CAST(0x0000A73301629036 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (22, N'Heavy Feeling', CAST(0x0000A73301629036 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (23, N'Epilepsy', CAST(0x0000A73301629036 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (24, N'Fainting', CAST(0x0000A73301629037 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (25, N'Seizures', CAST(0x0000A73301629037 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (26, N'Claudication', CAST(0x0000A73301629037 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (27, N'Leg Pain While  at Rest', CAST(0x0000A73301629038 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (28, N'Hot Sensation', CAST(0x0000A73301629038 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (29, N'Electric Shocks', CAST(0x0000A73301629038 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (30, N'Pins and Needles Pain', CAST(0x0000A73301629038 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (31, N'Cold Hands / Feet', CAST(0x0000A73301629038 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (32, N'Frequent Headaches', CAST(0x0000A73301629038 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (33, N'Non-healing Wounds', CAST(0x0000A73301629039 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (34, N'Bladder Urgency', CAST(0x0000A73301629039 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (35, N'Sore Feet', CAST(0x0000A73301629039 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (36, N'Family History', CAST(0x0000A73301629039 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (38, N'ffg', CAST(0x0000A77000D90335 AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Symptoms] ([ID], [Title], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (39, N'bfff', CAST(0x0000A77000D9179B AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Symptoms] OFF
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([ID], [Title]) VALUES (1, N'Admin')
INSERT [dbo].[UserRoles] ([ID], [Title]) VALUES (2, N'Physician')
INSERT [dbo].[UserRoles] ([ID], [Title]) VALUES (3, N'Specialist')
INSERT [dbo].[UserRoles] ([ID], [Title]) VALUES (4, N'Technician')
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [Username], [Password], [RoleID], [StaffID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1, N'admin', N'admin', 1, 1, CAST(0x0000A73A01624028 AS DateTime), 1, CAST(0x0000A76A01226882 AS DateTime), 1)
INSERT [dbo].[Users] ([ID], [Username], [Password], [RoleID], [StaffID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (2, N'test', N'test', 4, 2, CAST(0x0000A77100BD3B1C AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Users] ([ID], [Username], [Password], [RoleID], [StaffID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (3, N'doc', N'doc', 2, 3, CAST(0x0000A77100C377FB AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Users] ([ID], [Username], [Password], [RoleID], [StaffID], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (4, N'specialist', N'specialist', 3, 4, CAST(0x0000A7F300C41438 AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Diseases] ADD  CONSTRAINT [DF__Diseases__Create__1920BF5C]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[DiseaseTypes] ADD  CONSTRAINT [DF__DiseaseTy__Creat__164452B1]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[InsuranceCardPictures] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[InsuranceTypes] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[IntakeFormHead] ADD  CONSTRAINT [DF__IntakeFor__isPre__3A81B327]  DEFAULT ((0)) FOR [isPregnant]
GO
ALTER TABLE [dbo].[IntakeFormHead] ADD  CONSTRAINT [DF__IntakeFor__Creat__3B75D760]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[InterferingConditions] ADD  CONSTRAINT [DF__Interferi__Creat__403A8C7D]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[MedicalNecessities] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[Offices] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[PatientDiseases] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[PatientHistory] ADD  CONSTRAINT [DF_PatientHistory_StatusID]  DEFAULT ((1)) FOR [StatusID]
GO
ALTER TABLE [dbo].[PatientHistory] ADD  CONSTRAINT [DF__PatientHi__isApp__21B6055D]  DEFAULT ((0)) FOR [isApprovedByPhysician]
GO
ALTER TABLE [dbo].[PatientHistory] ADD  CONSTRAINT [DF_PatientHistory_isApprovedByPhysician1]  DEFAULT ((0)) FOR [isApprovedBySpecialist]
GO
ALTER TABLE [dbo].[PatientHistory] ADD  CONSTRAINT [DF__PatientHi__Creat__22AA2996]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[PatientInterferingCondition] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[PatientNecessities] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[PatientProcedures] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[PatientReports] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[Patients] ADD  CONSTRAINT [DF_Patients_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[PatientSymtoms] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[ProcedureTypes] ADD  CONSTRAINT [DF__Procedure__Creat__1367E606]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[Symptoms] ADD  CONSTRAINT [DF__Symptoms__Create__1BFD2C07]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__Created__0DAF0CB0]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[Diseases]  WITH CHECK ADD  CONSTRAINT [FK_Diseases_DiseaseTypes] FOREIGN KEY([DiseaseTypeID])
REFERENCES [dbo].[DiseaseTypes] ([ID])
GO
ALTER TABLE [dbo].[Diseases] CHECK CONSTRAINT [FK_Diseases_DiseaseTypes]
GO
ALTER TABLE [dbo].[InsuranceCardPictures]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceCardPictures_PatientHistory] FOREIGN KEY([HistoryID])
REFERENCES [dbo].[PatientHistory] ([ID])
GO
ALTER TABLE [dbo].[InsuranceCardPictures] CHECK CONSTRAINT [FK_InsuranceCardPictures_PatientHistory]
GO
ALTER TABLE [dbo].[InsuranceCardPictures]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceCardPictures_Patients] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patients] ([ID])
GO
ALTER TABLE [dbo].[InsuranceCardPictures] CHECK CONSTRAINT [FK_InsuranceCardPictures_Patients]
GO
ALTER TABLE [dbo].[PatientDiseases]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientDiseases_Diseases] FOREIGN KEY([DiseaseID])
REFERENCES [dbo].[Diseases] ([ID])
GO
ALTER TABLE [dbo].[PatientDiseases] CHECK CONSTRAINT [FK_PatientDiseases_Diseases]
GO
ALTER TABLE [dbo].[PatientDiseases]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientDiseases_IntakeFormHead] FOREIGN KEY([IntakeFormID])
REFERENCES [dbo].[IntakeFormHead] ([ID])
GO
ALTER TABLE [dbo].[PatientDiseases] CHECK CONSTRAINT [FK_PatientDiseases_IntakeFormHead]
GO
ALTER TABLE [dbo].[PatientDiseases]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientDiseases_PatientHistory] FOREIGN KEY([HistoryID])
REFERENCES [dbo].[PatientHistory] ([ID])
GO
ALTER TABLE [dbo].[PatientDiseases] CHECK CONSTRAINT [FK_PatientDiseases_PatientHistory]
GO
ALTER TABLE [dbo].[PatientHistory]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientHistory_InsuranceTypes] FOREIGN KEY([InsuranceTypeID])
REFERENCES [dbo].[InsuranceTypes] ([ID])
GO
ALTER TABLE [dbo].[PatientHistory] CHECK CONSTRAINT [FK_PatientHistory_InsuranceTypes]
GO
ALTER TABLE [dbo].[PatientHistory]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientHistory_MedicalNecessities] FOREIGN KEY([MedicalNecessitiesID])
REFERENCES [dbo].[MedicalNecessities] ([ID])
GO
ALTER TABLE [dbo].[PatientHistory] CHECK CONSTRAINT [FK_PatientHistory_MedicalNecessities]
GO
ALTER TABLE [dbo].[PatientHistory]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientHistory_Offices] FOREIGN KEY([OfficeID])
REFERENCES [dbo].[Offices] ([ID])
GO
ALTER TABLE [dbo].[PatientHistory] CHECK CONSTRAINT [FK_PatientHistory_Offices]
GO
ALTER TABLE [dbo].[PatientHistory]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientHistory_Patients] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patients] ([ID])
GO
ALTER TABLE [dbo].[PatientHistory] CHECK CONSTRAINT [FK_PatientHistory_Patients]
GO
ALTER TABLE [dbo].[PatientHistory]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientHistory_Statuses] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[PatientHistory] CHECK CONSTRAINT [FK_PatientHistory_Statuses]
GO
ALTER TABLE [dbo].[PatientInterferingCondition]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientInterferingCondition_IntakeFormHead] FOREIGN KEY([IntakeFormID])
REFERENCES [dbo].[IntakeFormHead] ([ID])
GO
ALTER TABLE [dbo].[PatientInterferingCondition] CHECK CONSTRAINT [FK_PatientInterferingCondition_IntakeFormHead]
GO
ALTER TABLE [dbo].[PatientInterferingCondition]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientInterferingCondition_InterferingConditions] FOREIGN KEY([PatientConditionID])
REFERENCES [dbo].[InterferingConditions] ([ID])
GO
ALTER TABLE [dbo].[PatientInterferingCondition] CHECK CONSTRAINT [FK_PatientInterferingCondition_InterferingConditions]
GO
ALTER TABLE [dbo].[PatientInterferingCondition]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientInterferingCondition_PatientHistory] FOREIGN KEY([HistoryID])
REFERENCES [dbo].[PatientHistory] ([ID])
GO
ALTER TABLE [dbo].[PatientInterferingCondition] CHECK CONSTRAINT [FK_PatientInterferingCondition_PatientHistory]
GO
ALTER TABLE [dbo].[PatientNecessities]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientNecessities_PatientHistory] FOREIGN KEY([HistoryID])
REFERENCES [dbo].[PatientHistory] ([ID])
GO
ALTER TABLE [dbo].[PatientNecessities] CHECK CONSTRAINT [FK_PatientNecessities_PatientHistory]
GO
ALTER TABLE [dbo].[PatientNecessities]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientNecessities_Patients] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patients] ([ID])
GO
ALTER TABLE [dbo].[PatientNecessities] CHECK CONSTRAINT [FK_PatientNecessities_Patients]
GO
ALTER TABLE [dbo].[PatientProcedures]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientProcedures_PatientHistory] FOREIGN KEY([HistoryID])
REFERENCES [dbo].[PatientHistory] ([ID])
GO
ALTER TABLE [dbo].[PatientProcedures] CHECK CONSTRAINT [FK_PatientProcedures_PatientHistory]
GO
ALTER TABLE [dbo].[PatientProcedures]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientProcedures_ProcedureTypes] FOREIGN KEY([ProcedureTypeID])
REFERENCES [dbo].[ProcedureTypes] ([ID])
GO
ALTER TABLE [dbo].[PatientProcedures] CHECK CONSTRAINT [FK_PatientProcedures_ProcedureTypes]
GO
ALTER TABLE [dbo].[PatientReports]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientReports_PatientHistory] FOREIGN KEY([HistoryID])
REFERENCES [dbo].[PatientHistory] ([ID])
GO
ALTER TABLE [dbo].[PatientReports] CHECK CONSTRAINT [FK_PatientReports_PatientHistory]
GO
ALTER TABLE [dbo].[PatientReports]  WITH NOCHECK ADD  CONSTRAINT [FK_PatientReports_Patients] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patients] ([ID])
GO
ALTER TABLE [dbo].[PatientReports] CHECK CONSTRAINT [FK_PatientReports_Patients]
GO
ALTER TABLE [dbo].[PatientSymtoms]  WITH CHECK ADD  CONSTRAINT [FK_PatientSymtoms_IntakeFormHead] FOREIGN KEY([IntakeFormID])
REFERENCES [dbo].[IntakeFormHead] ([ID])
GO
ALTER TABLE [dbo].[PatientSymtoms] CHECK CONSTRAINT [FK_PatientSymtoms_IntakeFormHead]
GO
ALTER TABLE [dbo].[PatientSymtoms]  WITH CHECK ADD  CONSTRAINT [FK_PatientSymtoms_PatientHistory] FOREIGN KEY([HistoryID])
REFERENCES [dbo].[PatientHistory] ([ID])
GO
ALTER TABLE [dbo].[PatientSymtoms] CHECK CONSTRAINT [FK_PatientSymtoms_PatientHistory]
GO
ALTER TABLE [dbo].[PatientSymtoms]  WITH CHECK ADD  CONSTRAINT [FK_PatientSymtoms_Symptoms] FOREIGN KEY([SymptomID])
REFERENCES [dbo].[Symptoms] ([ID])
GO
ALTER TABLE [dbo].[PatientSymtoms] CHECK CONSTRAINT [FK_PatientSymtoms_Symptoms]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Offices] FOREIGN KEY([OfficeID])
REFERENCES [dbo].[Offices] ([ID])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Offices]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Staff] FOREIGN KEY([StaffID])
REFERENCES [dbo].[Staff] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Staff]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Users] FOREIGN KEY([RoleID])
REFERENCES [dbo].[UserRoles] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Users]
GO
USE [master]
GO
ALTER DATABASE [emedical321_PatientMgmt] SET  READ_WRITE 
GO
