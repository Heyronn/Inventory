USE [master]
GO
/****** Object:  Database [Nissan]    Script Date: 02/03/2024 09:05:20 p. m. ******/
CREATE DATABASE [Nissan]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Nissan', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Nissan.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Nissan_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Nissan_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Nissan] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Nissan].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Nissan] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Nissan] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Nissan] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Nissan] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Nissan] SET ARITHABORT OFF 
GO
ALTER DATABASE [Nissan] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Nissan] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Nissan] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Nissan] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Nissan] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Nissan] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Nissan] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Nissan] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Nissan] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Nissan] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Nissan] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Nissan] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Nissan] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Nissan] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Nissan] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Nissan] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Nissan] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Nissan] SET RECOVERY FULL 
GO
ALTER DATABASE [Nissan] SET  MULTI_USER 
GO
ALTER DATABASE [Nissan] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Nissan] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Nissan] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Nissan] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Nissan] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Nissan] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Nissan', N'ON'
GO
ALTER DATABASE [Nissan] SET QUERY_STORE = ON
GO
ALTER DATABASE [Nissan] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Nissan]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 02/03/2024 09:05:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Category_id] [int] IDENTITY(1,1) NOT NULL,
	[Category_name] [varchar](100) NOT NULL,
	[Category_description] [varchar](300) NOT NULL,
	[Function_id] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[Category_identifier] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeviceType]    Script Date: 02/03/2024 09:05:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceType](
	[D_type_id] [int] IDENTITY(1,1) NOT NULL,
	[D_type_description] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Device2] PRIMARY KEY CLUSTERED 
(
	[D_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Function]    Script Date: 02/03/2024 09:05:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Function](
	[Function_id] [int] IDENTITY(1,1) NOT NULL,
	[Function_description] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Function] PRIMARY KEY CLUSTERED 
(
	[Function_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 02/03/2024 09:05:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[Inventory_id] [int] IDENTITY(1,1) NOT NULL,
	[D_type_id] [int] NOT NULL,
	[SerialNo] [varchar](100) NOT NULL,
	[PurchaseDate] [date] NOT NULL,
	[Location_id] [int] NOT NULL,
	[Version_id] [int] NOT NULL,
	[Model_id] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Inventory2] PRIMARY KEY CLUSTERED 
(
	[Inventory_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 02/03/2024 09:05:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[Location_id] [int] IDENTITY(1,1) NOT NULL,
	[Location_description] [varchar](50) NOT NULL,
	[Plant_id] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Location_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 02/03/2024 09:05:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Log_id] [int] IDENTITY(1,1) NOT NULL,
	[LogType_id] [int] NOT NULL,
	[User_id] [int] NOT NULL,
	[DateLog] [datetime] NOT NULL,
	[Message] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogType]    Script Date: 02/03/2024 09:05:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogType](
	[LogType_id] [int] IDENTITY(1,1) NOT NULL,
	[LogTypeDesc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_LogType] PRIMARY KEY CLUSTERED 
(
	[LogType_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Model]    Script Date: 02/03/2024 09:05:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Model](
	[Model_id] [int] IDENTITY(1,1) NOT NULL,
	[Model_description] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Model] PRIMARY KEY CLUSTERED 
(
	[Model_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plant]    Script Date: 02/03/2024 09:05:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plant](
	[Plant_id] [int] IDENTITY(1,1) NOT NULL,
	[Plant_description] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Plant] PRIMARY KEY CLUSTERED 
(
	[Plant_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Security]    Script Date: 02/03/2024 09:05:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Security](
	[Security_id] [int] IDENTITY(1,1) NOT NULL,
	[SubCategory_id] [int] NOT NULL,
	[Plant_id] [int] NOT NULL,
	[Result] [bit] NOT NULL,
	[Comment] [varchar](100) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Security] PRIMARY KEY CLUSTERED 
(
	[Security_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 02/03/2024 09:05:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCategory](
	[SubCategory_id] [int] IDENTITY(1,1) NOT NULL,
	[SubCategory_description] [varchar](150) NOT NULL,
	[Active] [bit] NOT NULL,
	[Category_id] [int] NOT NULL,
 CONSTRAINT [PK_SubCategory] PRIMARY KEY CLUSTERED 
(
	[SubCategory_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 02/03/2024 09:05:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[User_id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[FullName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Plant_id] [int] NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Privilege] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[User_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Version]    Script Date: 02/03/2024 09:05:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Version](
	[Version_id] [int] IDENTITY(1,1) NOT NULL,
	[Version_description] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[EndOfSupport] [date] NULL,
 CONSTRAINT [PK_Version] PRIMARY KEY CLUSTERED 
(
	[Version_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VersionDT]    Script Date: 02/03/2024 09:05:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VersionDT](
	[idVersionDT] [int] IDENTITY(1,1) NOT NULL,
	[Version_id] [int] NOT NULL,
	[D_type_id] [int] NOT NULL,
 CONSTRAINT [PK_VersionDT] PRIMARY KEY CLUSTERED 
(
	[idVersionDT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Function] FOREIGN KEY([Function_id])
REFERENCES [dbo].[Function] ([Function_id])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_Function]
GO
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_DeviceType] FOREIGN KEY([D_type_id])
REFERENCES [dbo].[DeviceType] ([D_type_id])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_DeviceType]
GO
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_Location] FOREIGN KEY([Location_id])
REFERENCES [dbo].[Location] ([Location_id])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_Location]
GO
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_Model] FOREIGN KEY([Model_id])
REFERENCES [dbo].[Model] ([Model_id])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_Model]
GO
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_Version] FOREIGN KEY([Version_id])
REFERENCES [dbo].[Version] ([Version_id])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_Version]
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Plant] FOREIGN KEY([Plant_id])
REFERENCES [dbo].[Plant] ([Plant_id])
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Plant]
GO
ALTER TABLE [dbo].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Log_LogType] FOREIGN KEY([LogType_id])
REFERENCES [dbo].[LogType] ([LogType_id])
GO
ALTER TABLE [dbo].[Log] CHECK CONSTRAINT [FK_Log_LogType]
GO
ALTER TABLE [dbo].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Log_Users] FOREIGN KEY([User_id])
REFERENCES [dbo].[Users] ([User_id])
GO
ALTER TABLE [dbo].[Log] CHECK CONSTRAINT [FK_Log_Users]
GO
ALTER TABLE [dbo].[Security]  WITH CHECK ADD  CONSTRAINT [FK_Security_Plant] FOREIGN KEY([Plant_id])
REFERENCES [dbo].[Plant] ([Plant_id])
GO
ALTER TABLE [dbo].[Security] CHECK CONSTRAINT [FK_Security_Plant]
GO
ALTER TABLE [dbo].[Security]  WITH CHECK ADD  CONSTRAINT [FK_Security_SubCategory] FOREIGN KEY([SubCategory_id])
REFERENCES [dbo].[SubCategory] ([SubCategory_id])
GO
ALTER TABLE [dbo].[Security] CHECK CONSTRAINT [FK_Security_SubCategory]
GO
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_Categories] FOREIGN KEY([Category_id])
REFERENCES [dbo].[Categories] ([Category_id])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_Categories]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Plant] FOREIGN KEY([Plant_id])
REFERENCES [dbo].[Plant] ([Plant_id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Plant]
GO
ALTER TABLE [dbo].[VersionDT]  WITH CHECK ADD  CONSTRAINT [FK_VersionDT_DeviceType] FOREIGN KEY([D_type_id])
REFERENCES [dbo].[DeviceType] ([D_type_id])
GO
ALTER TABLE [dbo].[VersionDT] CHECK CONSTRAINT [FK_VersionDT_DeviceType]
GO
ALTER TABLE [dbo].[VersionDT]  WITH CHECK ADD  CONSTRAINT [FK_VersionDT_Version] FOREIGN KEY([Version_id])
REFERENCES [dbo].[Version] ([Version_id])
GO
ALTER TABLE [dbo].[VersionDT] CHECK CONSTRAINT [FK_VersionDT_Version]
GO
USE [master]
GO
ALTER DATABASE [Nissan] SET  READ_WRITE 
GO
