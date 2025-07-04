USE [master]
GO
/****** Object:  Database [OnlinePaymentsAppDb]    Script Date: 6/9/2025 5:05:59 PM ******/
CREATE DATABASE [OnlinePaymentsAppDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OnlinePaymentsAppDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\OnlinePaymentsAppDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OnlinePaymentsAppDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\OnlinePaymentsAppDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlinePaymentsAppDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET RECOVERY FULL 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET  MULTI_USER 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'OnlinePaymentsAppDb', N'ON'
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [OnlinePaymentsAppDb]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 6/9/2025 5:06:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[AccountNumber] [char](22) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 6/9/2025 5:06:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[FromAccountId] [int] NOT NULL,
	[ToAccountNumber] [char](22) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Reason] [nvarchar](32) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedByUserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccounts]    Script Date: 6/9/2025 5:06:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccounts](
	[UserId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/9/2025 5:06:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [varchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([AccountId], [AccountNumber], [Balance]) VALUES (1, N'BG11AAAA11111111111111', CAST(1140.00 AS Decimal(18, 2)))
INSERT [dbo].[Accounts] ([AccountId], [AccountNumber], [Balance]) VALUES (2, N'BG22BBBB22222222222222', CAST(430.00 AS Decimal(18, 2)))
INSERT [dbo].[Accounts] ([AccountId], [AccountNumber], [Balance]) VALUES (3, N'BG33CCCC33333333333333', CAST(780.55 AS Decimal(18, 2)))
INSERT [dbo].[Accounts] ([AccountId], [AccountNumber], [Balance]) VALUES (4, N'BG44DDDD44444444444444', CAST(3000.00 AS Decimal(18, 2)))
INSERT [dbo].[Accounts] ([AccountId], [AccountNumber], [Balance]) VALUES (5, N'BG55EEEE55555555555555', CAST(25.00 AS Decimal(18, 2)))
INSERT [dbo].[Accounts] ([AccountId], [AccountNumber], [Balance]) VALUES (6, N'BG66FFFF66666666666666', CAST(980.40 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (1, 2, N'BG33CCCC33333333333333', CAST(50.00 AS Decimal(18, 2)), N'Тест', N'ОБРАБОТЕН', CAST(N'2025-06-09T14:05:20.877' AS DateTime), 2)
INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (2, 2, N'BG33CCCC33333333333333', CAST(20.00 AS Decimal(18, 2)), N'Втори тест', N'ОБРАБОТЕН', CAST(N'2025-06-09T14:06:30.227' AS DateTime), 2)
INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (3, 2, N'BG33CCCC33333333333333', CAST(490.00 AS Decimal(18, 2)), N'Трети тест', N'ОТКАЗАН', CAST(N'2025-06-09T14:09:40.893' AS DateTime), 2)
INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (4, 2, N'BG33CCCC33333333333333', CAST(10.00 AS Decimal(18, 2)), N'Четвърти тест', N'ОБРАБОТЕН', CAST(N'2025-06-09T14:28:58.287' AS DateTime), 2)
INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (5, 2, N'BG33CCCC33333333333333', CAST(5.00 AS Decimal(18, 2)), N'Пети тест', N'ОБРАБОТЕН', CAST(N'2025-06-09T14:32:39.043' AS DateTime), 2)
INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (6, 2, N'BG33CCCC33333333333333', CAST(5.00 AS Decimal(18, 2)), N'Шести тест', N'ОБРАБОТЕН', CAST(N'2025-06-09T15:09:36.590' AS DateTime), 2)
INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (7, 3, N'BG22BBBB22222222222222', CAST(30.00 AS Decimal(18, 2)), N'Седми тест', N'ОБРАБОТЕН', CAST(N'2025-06-09T15:14:44.737' AS DateTime), 2)
INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (8, 3, N'BG22BBBB22222222222222', CAST(20.00 AS Decimal(18, 2)), N'Осми тест', N'ОТКАЗАН', CAST(N'2025-06-09T15:15:20.337' AS DateTime), 2)
INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (9, 3, N'BG22BBBB22222222222222', CAST(50.00 AS Decimal(18, 2)), N'Девети тест', N'ИЗЧАКВА', CAST(N'2025-06-09T15:15:32.847' AS DateTime), 2)
INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (10, 3, N'BG22BBBB22222222222222', CAST(5.00 AS Decimal(18, 2)), N'Десети тест', N'ИЗЧАКВА', CAST(N'2025-06-09T15:15:46.610' AS DateTime), 2)
INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (11, 3, N'?????                 ', CAST(10.00 AS Decimal(18, 2)), N'Тест за невалидни стойности', N'ОТКАЗАН', CAST(N'2025-06-09T15:50:41.370' AS DateTime), 2)
INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (12, 1, N'BG33CCCC33333333333333', CAST(50.00 AS Decimal(18, 2)), N'Тест', N'ОБРАБОТЕН', CAST(N'2025-06-09T16:35:36.813' AS DateTime), 1)
INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (13, 1, N'BG33CCCC33333333333333', CAST(10.00 AS Decimal(18, 2)), N'Втори тест', N'ОБРАБОТЕН', CAST(N'2025-06-09T16:36:45.230' AS DateTime), 1)
INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (14, 1, N'BG33CCCC33333333333333', CAST(10.00 AS Decimal(18, 2)), N'Трети тест', N'ИЗЧАКВА', CAST(N'2025-06-09T16:41:06.140' AS DateTime), 1)
INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (15, 1, N'BG33CCCC33333333333333', CAST(100.00 AS Decimal(18, 2)), N'Четвърти тест', N'ИЗЧАКВА', CAST(N'2025-06-09T16:41:18.017' AS DateTime), 1)
INSERT [dbo].[Payments] ([PaymentId], [FromAccountId], [ToAccountNumber], [Amount], [Reason], [Status], [CreatedAt], [CreatedByUserId]) VALUES (16, 1, N'BG33CCCC33333333333333', CAST(20.00 AS Decimal(18, 2)), N'Пети тест', N'ИЗЧАКВА', CAST(N'2025-06-09T16:41:28.823' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Payments] OFF
GO
INSERT [dbo].[UserAccounts] ([UserId], [AccountId]) VALUES (1, 1)
INSERT [dbo].[UserAccounts] ([UserId], [AccountId]) VALUES (1, 2)
INSERT [dbo].[UserAccounts] ([UserId], [AccountId]) VALUES (2, 2)
INSERT [dbo].[UserAccounts] ([UserId], [AccountId]) VALUES (2, 3)
INSERT [dbo].[UserAccounts] ([UserId], [AccountId]) VALUES (3, 3)
INSERT [dbo].[UserAccounts] ([UserId], [AccountId]) VALUES (4, 4)
INSERT [dbo].[UserAccounts] ([UserId], [AccountId]) VALUES (4, 5)
INSERT [dbo].[UserAccounts] ([UserId], [AccountId]) VALUES (5, 6)
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Name], [Username], [Password]) VALUES (1, N'Иван Петров', N'ivan.petrov', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Users] ([UserId], [Name], [Username], [Password]) VALUES (2, N'Мария Георгиева', N'maria.georgieva', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Users] ([UserId], [Name], [Username], [Password]) VALUES (3, N'Георги Иванов', N'georgi.ivanov', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Users] ([UserId], [Name], [Username], [Password]) VALUES (4, N'Николай Димитров', N'niko.dimitrov', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Users] ([UserId], [Name], [Username], [Password]) VALUES (5, N'Елена Стоянова', N'elena.stoyanova', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Users] ([UserId], [Name], [Username], [Password]) VALUES (6, N'Михаил Динков', N'mihail.dinkov', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Users] ([UserId], [Name], [Username], [Password]) VALUES (7, N'Анелия Пейчева', N'anelia.peycheva', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Users] ([UserId], [Name], [Username], [Password]) VALUES (8, N'Йоана Йорданова', N'yoana.yordanova', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Accounts__BE2ACD6F6D75A362]    Script Date: 6/9/2025 5:06:00 PM ******/
ALTER TABLE [dbo].[Accounts] ADD UNIQUE NONCLUSTERED 
(
	[AccountNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__536C85E40B519FCD]    Script Date: 6/9/2025 5:06:00 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD FOREIGN KEY([CreatedByUserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD FOREIGN KEY([FromAccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD CHECK  (([Balance]>=(0)))
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD CHECK  (([Amount]>(0)))
GO
USE [master]
GO
ALTER DATABASE [OnlinePaymentsAppDb] SET  READ_WRITE 
GO
