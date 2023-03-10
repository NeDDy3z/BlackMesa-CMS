USE [master]
GO
/****** Object:  Database [messenger]    Script Date: 05/03/2023 20:55:19 ******/
CREATE DATABASE [messenger]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'messenger', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\messenger.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'messenger_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\messenger_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [messenger] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [messenger].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [messenger] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [messenger] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [messenger] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [messenger] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [messenger] SET ARITHABORT OFF 
GO
ALTER DATABASE [messenger] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [messenger] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [messenger] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [messenger] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [messenger] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [messenger] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [messenger] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [messenger] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [messenger] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [messenger] SET  DISABLE_BROKER 
GO
ALTER DATABASE [messenger] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [messenger] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [messenger] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [messenger] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [messenger] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [messenger] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [messenger] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [messenger] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [messenger] SET  MULTI_USER 
GO
ALTER DATABASE [messenger] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [messenger] SET DB_CHAINING OFF 
GO
ALTER DATABASE [messenger] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [messenger] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [messenger] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [messenger] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [messenger] SET QUERY_STORE = ON
GO
ALTER DATABASE [messenger] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [messenger]
GO
/****** Object:  Table [dbo].[link]    Script Date: 05/03/2023 20:55:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[link](
	[id_li] [int] IDENTITY(1,1) NOT NULL,
	[recipient] [int] NOT NULL,
	[sender] [int] NOT NULL,
	[msg] [int] NOT NULL,
	[rec_del] [int] NOT NULL,
	[sen_del] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_li] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[msg]    Script Date: 05/03/2023 20:55:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[msg](
	[id_ms] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](54) NOT NULL,
	[contents] [nvarchar](4000) NULL,
	[timesent] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_ms] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usr]    Script Date: 05/03/2023 20:55:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usr](
	[id_us] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[pass] [nvarchar](4000) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_us] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[link] ON 

INSERT [dbo].[link] ([id_li], [recipient], [sender], [msg], [rec_del], [sen_del]) VALUES (10, 19, 17, 12, 1, 1)
INSERT [dbo].[link] ([id_li], [recipient], [sender], [msg], [rec_del], [sen_del]) VALUES (11, 19, 17, 13, 0, 1)
INSERT [dbo].[link] ([id_li], [recipient], [sender], [msg], [rec_del], [sen_del]) VALUES (12, 17, 19, 14, 0, 0)
SET IDENTITY_INSERT [dbo].[link] OFF
GO
SET IDENTITY_INSERT [dbo].[msg] ON 

INSERT [dbo].[msg] ([id_ms], [title], [contents], [timesent]) VALUES (12, N'Test', N'Testik', CAST(N'2023-03-05T17:02:12.000' AS DateTime))
INSERT [dbo].[msg] ([id_ms], [title], [contents], [timesent]) VALUES (13, N'Test2', N'ansdjadnfjksd dsvfds', CAST(N'2023-03-05T17:16:56.000' AS DateTime))
INSERT [dbo].[msg] ([id_ms], [title], [contents], [timesent]) VALUES (14, N'Test3', N'asdkjndkfjn ', CAST(N'2023-03-05T17:23:33.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[msg] OFF
GO
SET IDENTITY_INSERT [dbo].[usr] ON 

INSERT [dbo].[usr] ([id_us], [username], [pass]) VALUES (17, N'Karel', N'ATEdZpBMF17nwKBvmnE5XA==')
INSERT [dbo].[usr] ([id_us], [username], [pass]) VALUES (18, N'GordonFreemanHalflife', N'Jy09ZSIu2Aw1GTUQ+6hu/A==')
INSERT [dbo].[usr] ([id_us], [username], [pass]) VALUES (19, N'Chell', N'PNyhuZyWrwjWS5QNv9w11p27kbPKIJ45')
INSERT [dbo].[usr] ([id_us], [username], [pass]) VALUES (20, N'Glados?', N'zQehHyXqHWNPJnKJKaJGO2RxeLl6tAK/')
INSERT [dbo].[usr] ([id_us], [username], [pass]) VALUES (21, N'Alyx Vance', N'T0wmsLoudGWpfIQNhtKpShIFJKM9bDc/9XeD5LkRf3vxmvZjGftovnWC8gzbGB/3Zb0ihvfIPVQWbsTbd2Q56/knjzmY+E9FO2Er31bCQUIysaoZNwrM4drOlbQUPl5G')
SET IDENTITY_INSERT [dbo].[usr] OFF
GO
ALTER TABLE [dbo].[link]  WITH CHECK ADD FOREIGN KEY([msg])
REFERENCES [dbo].[msg] ([id_ms])
GO
ALTER TABLE [dbo].[link]  WITH CHECK ADD FOREIGN KEY([recipient])
REFERENCES [dbo].[usr] ([id_us])
GO
ALTER TABLE [dbo].[link]  WITH CHECK ADD FOREIGN KEY([sender])
REFERENCES [dbo].[usr] ([id_us])
GO
USE [master]
GO
ALTER DATABASE [messenger] SET  READ_WRITE 
GO
