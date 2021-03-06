/****** Object:  Table [dbo].[Clan]    Script Date: 4/12/2020 10:47:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[ContactPerson]    Script Date: 4/12/2020 10:47:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[Match]    Script Date: 4/12/2020 10:47:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[MatchScore]    Script Date: 4/12/2020 10:47:26 AM ******/
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
/****** Object:  Table [dbo].[Player]    Script Date: 4/12/2020 10:47:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlayerName] [varchar](100) NULL,
	[NIC] [varchar](10) NULL,
	[TeamId] [int] NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team]    Script Date: 4/12/2020 10:47:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[TeamTournamentGroup]    Script Date: 4/12/2020 10:47:26 AM ******/
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
/****** Object:  Table [dbo].[Tournament]    Script Date: 4/12/2020 10:47:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[TournamentDraw]    Script Date: 4/12/2020 10:47:26 AM ******/
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
/****** Object:  Table [dbo].[TournamentGroup]    Script Date: 4/12/2020 10:47:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[TournamentTeam]    Script Date: 4/12/2020 10:47:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[User]    Script Date: 4/12/2020 10:47:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[UserRole]    Script Date: 4/12/2020 10:47:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
SET IDENTITY_INSERT [dbo].[ContactPerson] ON 

INSERT [dbo].[ContactPerson] ([Id], [CPName], [PhoneNumber], [Email], [NIC]) VALUES (1, N'Richie', N'0713068142', N'abhithabhasuru@gmail.com', N'112200000V')
INSERT [dbo].[ContactPerson] ([Id], [CPName], [PhoneNumber], [Email], [NIC]) VALUES (2, N'Richie', N'0713068142', N'abhithabhasuru@gmail.com', N'112200000V')
INSERT [dbo].[ContactPerson] ([Id], [CPName], [PhoneNumber], [Email], [NIC]) VALUES (3, N'Richie', N'0713068142', N'abhithabhasuru@gmail.com', N'112200000V')
SET IDENTITY_INSERT [dbo].[ContactPerson] OFF
SET IDENTITY_INSERT [dbo].[Player] ON 

INSERT [dbo].[Player] ([Id], [PlayerName], [NIC], [TeamId]) VALUES (1, N'Player 1', N'220011224c', NULL)
INSERT [dbo].[Player] ([Id], [PlayerName], [NIC], [TeamId]) VALUES (2, N'asad', N'as1212', NULL)
INSERT [dbo].[Player] ([Id], [PlayerName], [NIC], [TeamId]) VALUES (3, N'Test', N'112212', 2)
INSERT [dbo].[Player] ([Id], [PlayerName], [NIC], [TeamId]) VALUES (4, N'Player 2', N'2121545000', 2)
SET IDENTITY_INSERT [dbo].[Player] OFF
SET IDENTITY_INSERT [dbo].[Team] ON 

INSERT [dbo].[Team] ([Id], [TeamName], [TeamDescription], [RegistrationDate], [IsActive], [Score], [Rank], [ClanId], [UserId], [ContactPerson], [LogoPath]) VALUES (1, N'Team Test', N'Team Test', CAST(N'2020-04-11T06:31:34.697' AS DateTime), 1, NULL, NULL, NULL, 3, 1, N'77cf6f2b-786b-4584-b0fb-def3642a7693.jpg')
INSERT [dbo].[Team] ([Id], [TeamName], [TeamDescription], [RegistrationDate], [IsActive], [Score], [Rank], [ClanId], [UserId], [ContactPerson], [LogoPath]) VALUES (2, N'Team Test2', N'Team Test2 Description', CAST(N'2020-04-11T08:00:28.253' AS DateTime), 1, NULL, NULL, NULL, 4, 2, N'84728fe4-fcce-4786-ac40-7aa4c9ddfcf1.jpg')
INSERT [dbo].[Team] ([Id], [TeamName], [TeamDescription], [RegistrationDate], [IsActive], [Score], [Rank], [ClanId], [UserId], [ContactPerson], [LogoPath]) VALUES (3, N'Team Test3', N'Team Test3', CAST(N'2020-04-11T08:05:31.037' AS DateTime), 1, NULL, NULL, NULL, 5, 3, N'c091a059-a8bf-4a0e-9476-c38318bebd7a.jpg')
SET IDENTITY_INSERT [dbo].[Team] OFF
SET IDENTITY_INSERT [dbo].[Tournament] ON 

INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (2, N'Test', N'saddasd', CAST(N'2020-04-30T00:00:00.000' AS DateTime), N'asa123', CAST(N'2020-04-11T00:00:00.000' AS DateTime), 1, 1, 120)
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (3, N'Test23', N'sadasdsad', CAST(N'2020-04-25T00:00:00.000' AS DateTime), N'asasd', CAST(N'2020-04-11T00:00:00.000' AS DateTime), 1, 1, 120)
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (4, N'Test333', N'adsdasd', CAST(N'2020-04-22T00:00:00.000' AS DateTime), N'213', CAST(N'2020-04-11T00:00:00.000' AS DateTime), 1, 1, 1203)
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (5, N'TT', N'adsdasdadsd asdas aas dsa as', CAST(N'2020-04-14T00:00:00.000' AS DateTime), N'asa123', CAST(N'2020-04-11T00:00:00.000' AS DateTime), 1, 1, 1211)
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (6, N'Test Again', N'asdsa sadas asd asda', CAST(N'2020-04-30T00:00:00.000' AS DateTime), N'asa123', CAST(N'2020-04-11T00:00:00.000' AS DateTime), 1, 1, 1500)
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (7, N'Test', N'sda asd sa asdad', CAST(N'2020-04-30T00:00:00.000' AS DateTime), N'1214132', CAST(N'2020-04-11T00:00:00.000' AS DateTime), 1, 1, 500)
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (8, N'ads', N'asd sad  assadasd', CAST(N'2020-04-30T00:00:00.000' AS DateTime), N'asd', CAST(N'2020-04-11T00:00:00.000' AS DateTime), 1, 1, 2000)
INSERT [dbo].[Tournament] ([Id], [TournamentName], [TournamentDescription], [SceduledDate], [ContactPerson], [CreatedDate], [CreatedBy], [IsActive], [EntryFee]) VALUES (9, N'asddsa', N'sdadasdasd', CAST(N'2020-05-01T00:00:00.000' AS DateTime), N'12334324342', CAST(N'2020-04-11T00:00:00.000' AS DateTime), 1, 1, 2000)
SET IDENTITY_INSERT [dbo].[Tournament] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Email], [Username], [Password], [IsActive], [RegisteredDate], [LastLoginDate], [RoleId], [IsLocked], [Token], [IsVerified]) VALUES (2, N'abhithabhasuru1@gmail.com', N'admin', N'240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9', 1, CAST(N'2020-04-11T00:00:00.000' AS DateTime), CAST(N'2020-04-11T15:00:24.073' AS DateTime), 1, 0, NULL, 1)
INSERT [dbo].[User] ([Id], [Email], [Username], [Password], [IsActive], [RegisteredDate], [LastLoginDate], [RoleId], [IsLocked], [Token], [IsVerified]) VALUES (3, N'abhithabhasuru1@gmail.com', N'team', N'127C116A9F4E2228D43F82BDB8534F237DDB91797A385D4A27CF941C5FB49D01', 1, CAST(N'2020-04-11T06:29:41.627' AS DateTime), NULL, 2, 0, N'58610be1-3204-4830-9255-646c21abc33d', 0)
INSERT [dbo].[User] ([Id], [Email], [Username], [Password], [IsActive], [RegisteredDate], [LastLoginDate], [RoleId], [IsLocked], [Token], [IsVerified]) VALUES (4, N'abhithabhasuru1', N'team2', N'127C116A9F4E2228D43F82BDB8534F237DDB91797A385D4A27CF941C5FB49D01', 0, CAST(N'2020-04-11T08:00:22.043' AS DateTime), NULL, 2, 0, N'5116c17e-2b8a-49d0-8f12-e6d458a17e05', 0)
INSERT [dbo].[User] ([Id], [Email], [Username], [Password], [IsActive], [RegisteredDate], [LastLoginDate], [RoleId], [IsLocked], [Token], [IsVerified]) VALUES (5, N'abhithabhasuru@gmail.com', N'team3', N'127C116A9F4E2228D43F82BDB8534F237DDB91797A385D4A27CF941C5FB49D01', 1, CAST(N'2020-04-11T08:05:30.143' AS DateTime), CAST(N'2020-04-11T09:05:03.040' AS DateTime), 2, 0, N'eda167b5-d6f7-4850-adec-66052a345a3c', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[UserRole] ON 

INSERT [dbo].[UserRole] ([Id], [RoleId], [UserRole], [Description]) VALUES (1, 1, N'Admin', N'Application Administrator')
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserRole], [Description]) VALUES (2, 2, N'Team', N'Leader of the Team')
SET IDENTITY_INSERT [dbo].[UserRole] OFF
/****** Object:  Index [IX_UserRole_RoleId]    Script Date: 4/12/2020 10:47:26 AM ******/
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
