USE [master]
GO
/****** Object:  Database [GemoWiz]    Script Date: 31-05-2020 11:15:36 ******/
CREATE DATABASE [GemoWiz]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GemoWiz', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\GemoWiz.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GemoWiz_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\GemoWiz_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [GemoWiz] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GemoWiz].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GemoWiz] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GemoWiz] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GemoWiz] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GemoWiz] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GemoWiz] SET ARITHABORT OFF 
GO
ALTER DATABASE [GemoWiz] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GemoWiz] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GemoWiz] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GemoWiz] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GemoWiz] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GemoWiz] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GemoWiz] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GemoWiz] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GemoWiz] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GemoWiz] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GemoWiz] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GemoWiz] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GemoWiz] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GemoWiz] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GemoWiz] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GemoWiz] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GemoWiz] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GemoWiz] SET RECOVERY FULL 
GO
ALTER DATABASE [GemoWiz] SET  MULTI_USER 
GO
ALTER DATABASE [GemoWiz] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GemoWiz] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GemoWiz] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GemoWiz] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [GemoWiz] SET DELAYED_DURABILITY = DISABLED 
GO
USE [GemoWiz]
GO
/****** Object:  Table [dbo].[Clan]    Script Date: 31-05-2020 11:15:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClanName] [varchar](100) NULL,
	[ClanDescription] [varchar](500) NULL,
	[RegistrationDate] [datetime] NULL,
	[UserId] [int] NULL,
	[ContactPerson] [int] NULL,
	[LogoPath] [varchar](max) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Clan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ContactPerson]    Script Date: 31-05-2020 11:15:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContactPerson](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CPName] [varchar](100) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[NIC] [varchar](10) NULL,
 CONSTRAINT [PK_ContactPerson] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Match]    Script Date: 31-05-2020 11:15:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Match](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MatchDescription] [varchar](max) NULL,
	[MatchDate] [datetime] NULL,
	[TournamentId] [int] NOT NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Match] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MatchScore]    Script Date: 31-05-2020 11:15:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MatchScore](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MatchId] [int] NULL,
	[TeamId] [int] NULL,
	[Score] [decimal](18, 0) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_MatchScore] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Player]    Script Date: 31-05-2020 11:15:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Player](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlayerName] [varchar](100) NULL,
	[NIC] [varchar](10) NULL,
	[TeamId] [int] NULL,
	[UserName] [varchar](100) NULL,
	[PlayerTag] [varchar](100) NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Team]    Script Date: 31-05-2020 11:15:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Team](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeamName] [varchar](100) NULL,
	[TeamDescription] [varchar](max) NULL,
	[RegistrationDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[Score] [decimal](18, 0) NULL,
	[Rank] [int] NULL,
	[ClanId] [int] NULL,
	[UserId] [int] NULL,
	[ContactPerson] [int] NULL,
	[LogoPath] [varchar](max) NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TeamTournamentGroup]    Script Date: 31-05-2020 11:15:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeamTournamentGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeamId] [int] NULL,
	[GroupId] [int] NULL,
 CONSTRAINT [PK_TeamTournamentGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tournament]    Script Date: 31-05-2020 11:15:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tournament](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TournamentName] [varchar](100) NULL,
	[TournamentDescription] [varchar](max) NULL,
	[SceduledDate] [datetime] NULL,
	[ContactPerson] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[IsActive] [bit] NULL,
	[EntryFee] [int] NULL,
 CONSTRAINT [PK_Tournament] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TournamentDraw]    Script Date: 31-05-2020 11:15:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TournamentDraw](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DrawDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[TournamentId] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_TournamentDraw] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TournamentGroup]    Script Date: 31-05-2020 11:15:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TournamentGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[TournamentId] [int] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TournamentTeam]    Script Date: 31-05-2020 11:15:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TournamentTeam](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TournamentId] [int] NULL,
	[TeamId] [int] NULL,
	[EnrollmentDate] [datetime] NULL,
	[PaymentType] [int] NULL,
	[IsPaymentMade] [bit] NULL,
	[PaymentProof] [varchar](max) NULL,
 CONSTRAINT [PK_TournamentTeam] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 31-05-2020 11:15:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](100) NULL,
	[Username] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[IsActive] [bit] NULL,
	[RegisteredDate] [datetime] NULL,
	[LastLoginDate] [datetime] NULL,
	[RoleId] [int] NULL,
	[IsLocked] [bit] NULL,
	[Token] [varchar](500) NULL,
	[IsVerified] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 31-05-2020 11:15:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[UserRole] [varchar](50) NULL,
	[Description] [varchar](500) NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[ContactPerson] ON 

GO
INSERT [dbo].[ContactPerson] ([Id], [CPName], [PhoneNumber], [Email], [NIC]) VALUES (1, N'Richie', N'0713068142', N'abhithabhasuru@gmail.com', N'112200000V')
GO
INSERT [dbo].[ContactPerson] ([Id], [CPName], [PhoneNumber], [Email], [NIC]) VALUES (2, N'Richie', N'0713068142', N'abhithabhasuru@gmail.com', N'112200000V')
GO
INSERT [dbo].[ContactPerson] ([Id], [CPName], [PhoneNumber], [Email], [NIC]) VALUES (3, N'Richie', N'0713068142', N'abhithabhasuru@gmail.com', N'112200000V')
GO
SET IDENTITY_INSERT [dbo].[ContactPerson] OFF
GO
SET IDENTITY_INSERT [dbo].[Player] ON 

GO
INSERT [dbo].[Player] ([Id], [PlayerName], [NIC], [TeamId], [UserName], [PlayerTag]) VALUES (1, N'Player 1', N'220011224c', NULL, NULL, NULL)
GO
INSERT [dbo].[Player] ([Id], [PlayerName], [NIC], [TeamId], [UserName], [PlayerTag]) VALUES (2, N'asad', N'as1212', NULL, NULL, NULL)
GO
INSERT [dbo].[Player] ([Id], [PlayerName], [NIC], [TeamId], [UserName], [PlayerTag]) VALUES (3, N'Test', N'112212', 2, NULL, NULL)
GO
INSERT [dbo].[Player] ([Id], [PlayerName], [NIC], [TeamId], [UserName], [PlayerTag]) VALUES (4, N'Player 2', N'2121545000', 2, NULL, NULL)
GO
INSERT [dbo].[Player] ([Id], [PlayerName], [NIC], [TeamId], [UserName], [PlayerTag]) VALUES (15, N'AAA', N'123', 1, N'Test', N'Main')
GO
INSERT [dbo].[Player] ([Id], [PlayerName], [NIC], [TeamId], [UserName], [PlayerTag]) VALUES (17, N'sdfsdf', N'rweer', 1, N'dsf', N'Main')
GO
INSERT [dbo].[Player] ([Id], [PlayerName], [NIC], [TeamId], [UserName], [PlayerTag]) VALUES (19, N'sdfsd', N'3243', 1, N'sdfsd', N'Main')
GO
INSERT [dbo].[Player] ([Id], [PlayerName], [NIC], [TeamId], [UserName], [PlayerTag]) VALUES (21, N'dddd', N'AAA', 1, N'AAAA', N'Reserve')
GO
INSERT [dbo].[Player] ([Id], [PlayerName], [NIC], [TeamId], [UserName], [PlayerTag]) VALUES (22, N'Test', N'sdfsd', 1, N'Test123', N'Reserve')
GO
SET IDENTITY_INSERT [dbo].[Player] OFF
GO
SET IDENTITY_INSERT [dbo].[Team] ON 

GO
INSERT [dbo].[Team] ([Id], [TeamName], [TeamDescription], [RegistrationDate], [IsActive], [Score], [Rank], [ClanId], [UserId], [ContactPerson], [LogoPath]) VALUES (1, N'Team Test', N'Team Test', CAST(N'2020-04-11 06:31:34.697' AS DateTime), 1, NULL, NULL, NULL, 3, 1, N'77cf6f2b-786b-4584-b0fb-def3642a7693.jpg')
GO
INSERT [dbo].[Team] ([Id], [TeamName], [TeamDescription], [RegistrationDate], [IsActive], [Score], [Rank], [ClanId], [UserId], [ContactPerson], [LogoPath]) VALUES (2, N'Team Test2', N'Team Test2 Description', CAST(N'2020-04-11 08:00:28.253' AS DateTime), 1, NULL, NULL, NULL, 4, 2, N'84728fe4-fcce-4786-ac40-7aa4c9ddfcf1.jpg')
GO
INSERT [dbo].[Team] ([Id], [TeamName], [TeamDescription], [RegistrationDate], [IsActive], [Score], [Rank], [ClanId], [UserId], [ContactPerson], [LogoPath]) VALUES (3, N'Team Test3', N'Team Test3', CAST(N'2020-04-11 08:05:31.037' AS DateTime), 1, NULL, NULL, NULL, 5, 3, N'c091a059-a8bf-4a0e-9476-c38318bebd7a.jpg')
GO
SET IDENTITY_INSERT [dbo].[Team] OFF
GO
SET IDENTITY_INSERT [dbo].[Tournament] ON 

GO
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (2, N'Test', N'saddasd', CAST(N'2020-04-30 00:00:00.000' AS DateTime), N'asa123', CAST(N'2020-04-11 00:00:00.000' AS DateTime), 1, 1, 120)
GO
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (3, N'Test23', N'sadasdsad', CAST(N'2020-04-25 00:00:00.000' AS DateTime), N'asasd', CAST(N'2020-04-11 00:00:00.000' AS DateTime), 1, 1, 120)
GO
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (4, N'Test333', N'adsdasd', CAST(N'2020-04-22 00:00:00.000' AS DateTime), N'213', CAST(N'2020-04-11 00:00:00.000' AS DateTime), 1, 1, 1203)
GO
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (5, N'TT', N'adsdasdadsd asdas aas dsa as', CAST(N'2020-04-14 00:00:00.000' AS DateTime), N'asa123', CAST(N'2020-04-11 00:00:00.000' AS DateTime), 1, 1, 1211)
GO
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (6, N'Test Again', N'asdsa sadas asd asda', CAST(N'2020-04-30 00:00:00.000' AS DateTime), N'asa123', CAST(N'2020-04-11 00:00:00.000' AS DateTime), 1, 1, 1500)
GO
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (7, N'Test', N'sda asd sa asdad', CAST(N'2020-04-30 00:00:00.000' AS DateTime), N'1214132', CAST(N'2020-04-11 00:00:00.000' AS DateTime), 1, 1, 500)
GO
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (8, N'ads', N'asd sad  assadasd', CAST(N'2020-04-30 00:00:00.000' AS DateTime), N'asd', CAST(N'2020-04-11 00:00:00.000' AS DateTime), 1, 1, 2000)
GO
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (9, N'asddsa', N'sdadasdasd', CAST(N'2020-05-01 00:00:00.000' AS DateTime), N'12334324342', CAST(N'2020-04-11 00:00:00.000' AS DateTime), 1, 1, 2000)
GO
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (10, NULL, NULL, NULL, NULL, CAST(N'2020-05-24 14:02:30.513' AS DateTime), 2, 1, NULL)
GO
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (11, N'Test Tournament', N'Testing', NULL, N'9999999999', CAST(N'2020-05-26 08:29:54.373' AS DateTime), 2, 1, 120)
GO
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (12, N'Test by Vidit', N'Testing', NULL, N'9916735094', CAST(N'2020-05-26 16:52:00.343' AS DateTime), 2, 1, 100)
GO
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (13, N'TTTT', N'Test', NULL, N'9999999999', CAST(N'2020-05-26 16:59:28.507' AS DateTime), 2, 1, 150)
GO
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (14, N'Test Thaveesha', N'Testing', NULL, N'9916350945', CAST(N'2020-05-27 06:50:33.073' AS DateTime), 2, 1, 100)
GO
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (15, N'Testing', N'Tested by Vidit', CAST(N'2020-12-06 00:00:00.000' AS DateTime), N'9999999999', CAST(N'2020-05-30 15:06:47.523' AS DateTime), 2, 1, 100)
GO
SET IDENTITY_INSERT [dbo].[Tournament] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([Id], [Email], [Username], [Password], [IsActive], [RegisteredDate], [LastLoginDate], [RoleId], [IsLocked], [Token], [IsVerified]) VALUES (2, N'abhithabhasuru1@gmail.com', N'admin', N'8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918', 1, CAST(N'2020-04-11 00:00:00.000' AS DateTime), CAST(N'2020-05-30 15:06:06.437' AS DateTime), 1, 0, NULL, 1)
GO
INSERT [dbo].[User] ([Id], [Email], [Username], [Password], [IsActive], [RegisteredDate], [LastLoginDate], [RoleId], [IsLocked], [Token], [IsVerified]) VALUES (3, N'abhithabhasuru1@gmail.com', N'team', N'9D9F168C34214AE75B9EEFFE6DA64334BF13E1C6A9DBD5054F9EDB4D24B99B7B', 1, CAST(N'2020-04-11 06:29:41.627' AS DateTime), CAST(N'2020-05-30 15:07:19.640' AS DateTime), 2, 0, N'58610be1-3204-4830-9255-646c21abc33d', 1)
GO
INSERT [dbo].[User] ([Id], [Email], [Username], [Password], [IsActive], [RegisteredDate], [LastLoginDate], [RoleId], [IsLocked], [Token], [IsVerified]) VALUES (4, N'abhithabhasuru1', N'team2', N'9D9F168C34214AE75B9EEFFE6DA64334BF13E1C6A9DBD5054F9EDB4D24B99B7B', 1, CAST(N'2020-04-11 08:00:22.043' AS DateTime), NULL, 2, 0, N'5116c17e-2b8a-49d0-8f12-e6d458a17e05', 1)
GO
INSERT [dbo].[User] ([Id], [Email], [Username], [Password], [IsActive], [RegisteredDate], [LastLoginDate], [RoleId], [IsLocked], [Token], [IsVerified]) VALUES (5, N'abhithabhasuru@gmail.com', N'team3', N'9D9F168C34214AE75B9EEFFE6DA64334BF13E1C6A9DBD5054F9EDB4D24B99B7B', 1, CAST(N'2020-04-11 08:05:30.143' AS DateTime), CAST(N'2020-04-11 09:05:03.040' AS DateTime), 2, 0, N'eda167b5-d6f7-4850-adec-66052a345a3c', 1)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 

GO
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserRole], [Description]) VALUES (1, 1, N'Admin', N'Application Administrator')
GO
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserRole], [Description]) VALUES (2, 2, N'Team', N'Leader of the Team')
GO
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
/****** Object:  Index [IX_UserRole_RoleId]    Script Date: 31-05-2020 11:15:36 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserRole_RoleId] ON [dbo].[UserRole]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Clan]  WITH CHECK ADD  CONSTRAINT [FK_Clan_ContactPerson] FOREIGN KEY([ContactPerson])
REFERENCES [dbo].[ContactPerson] ([Id])
GO
ALTER TABLE [dbo].[Clan] CHECK CONSTRAINT [FK_Clan_ContactPerson]
GO
ALTER TABLE [dbo].[Clan]  WITH CHECK ADD  CONSTRAINT [FK_Clan_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Clan] CHECK CONSTRAINT [FK_Clan_User]
GO
ALTER TABLE [dbo].[Match]  WITH CHECK ADD  CONSTRAINT [FK_Match_Tournament] FOREIGN KEY([TournamentId])
REFERENCES [dbo].[Tournament] ([Id])
GO
ALTER TABLE [dbo].[Match] CHECK CONSTRAINT [FK_Match_Tournament]
GO
ALTER TABLE [dbo].[MatchScore]  WITH CHECK ADD  CONSTRAINT [FK_MatchScore_Match] FOREIGN KEY([MatchId])
REFERENCES [dbo].[Match] ([Id])
GO
ALTER TABLE [dbo].[MatchScore] CHECK CONSTRAINT [FK_MatchScore_Match]
GO
ALTER TABLE [dbo].[MatchScore]  WITH CHECK ADD  CONSTRAINT [FK_MatchScore_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([Id])
GO
ALTER TABLE [dbo].[MatchScore] CHECK CONSTRAINT [FK_MatchScore_Team]
GO
ALTER TABLE [dbo].[MatchScore]  WITH CHECK ADD  CONSTRAINT [FK_MatchScore_User] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[MatchScore] CHECK CONSTRAINT [FK_MatchScore_User]
GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([Id])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK_Table_1_Team]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_Clan] FOREIGN KEY([ClanId])
REFERENCES [dbo].[Clan] ([Id])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_Clan]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_ContactPerson] FOREIGN KEY([ContactPerson])
REFERENCES [dbo].[ContactPerson] ([Id])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_ContactPerson]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_User]
GO
ALTER TABLE [dbo].[TeamTournamentGroup]  WITH CHECK ADD  CONSTRAINT [FK_TeamTournamentGroup_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([Id])
GO
ALTER TABLE [dbo].[TeamTournamentGroup] CHECK CONSTRAINT [FK_TeamTournamentGroup_Team]
GO
ALTER TABLE [dbo].[TeamTournamentGroup]  WITH CHECK ADD  CONSTRAINT [FK_TeamTournamentGroup_TournamentGroup] FOREIGN KEY([GroupId])
REFERENCES [dbo].[TournamentGroup] ([Id])
GO
ALTER TABLE [dbo].[TeamTournamentGroup] CHECK CONSTRAINT [FK_TeamTournamentGroup_TournamentGroup]
GO
ALTER TABLE [dbo].[TournamentDraw]  WITH CHECK ADD  CONSTRAINT [FK_TournamentDraw_Tournament] FOREIGN KEY([TournamentId])
REFERENCES [dbo].[Tournament] ([Id])
GO
ALTER TABLE [dbo].[TournamentDraw] CHECK CONSTRAINT [FK_TournamentDraw_Tournament]
GO
ALTER TABLE [dbo].[TournamentDraw]  WITH CHECK ADD  CONSTRAINT [FK_TournamentDraw_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[TournamentDraw] CHECK CONSTRAINT [FK_TournamentDraw_User]
GO
ALTER TABLE [dbo].[TournamentGroup]  WITH CHECK ADD  CONSTRAINT [FK_TournamentGroup_Tournament] FOREIGN KEY([TournamentId])
REFERENCES [dbo].[Tournament] ([Id])
GO
ALTER TABLE [dbo].[TournamentGroup] CHECK CONSTRAINT [FK_TournamentGroup_Tournament]
GO
ALTER TABLE [dbo].[TournamentGroup]  WITH CHECK ADD  CONSTRAINT [FK_TournamentGroup_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[TournamentGroup] CHECK CONSTRAINT [FK_TournamentGroup_User]
GO
ALTER TABLE [dbo].[TournamentTeam]  WITH CHECK ADD  CONSTRAINT [FK_TournamentTeam_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([Id])
GO
ALTER TABLE [dbo].[TournamentTeam] CHECK CONSTRAINT [FK_TournamentTeam_Team]
GO
ALTER TABLE [dbo].[TournamentTeam]  WITH CHECK ADD  CONSTRAINT [FK_TournamentTeam_Tournament] FOREIGN KEY([TournamentId])
REFERENCES [dbo].[Tournament] ([Id])
GO
ALTER TABLE [dbo].[TournamentTeam] CHECK CONSTRAINT [FK_TournamentTeam_Tournament]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[UserRole] ([RoleId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserRole]
GO
USE [master]
GO
ALTER DATABASE [GemoWiz] SET  READ_WRITE 
GO
