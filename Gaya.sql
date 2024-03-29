﻿USE [master]
GO
/****** Object:  Database [Gaya]    Script Date: 2/26/2023 2:24:37 AM ******/
CREATE DATABASE [Gaya]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Gaya', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Gaya.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Gaya_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Gaya_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Gaya] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Gaya].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Gaya] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Gaya] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Gaya] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Gaya] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Gaya] SET ARITHABORT OFF 
GO
ALTER DATABASE [Gaya] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Gaya] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Gaya] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Gaya] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Gaya] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Gaya] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Gaya] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Gaya] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Gaya] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Gaya] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Gaya] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Gaya] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Gaya] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Gaya] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Gaya] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Gaya] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Gaya] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Gaya] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Gaya] SET  MULTI_USER 
GO
ALTER DATABASE [Gaya] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Gaya] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Gaya] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Gaya] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Gaya] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Gaya] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Gaya] SET QUERY_STORE = OFF
GO
USE [Gaya]
GO
/****** Object:  Table [dbo].[History]    Script Date: 2/26/2023 2:24:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Operation] [nvarchar](32) NULL,
	[Input1] [float] NULL,
	[Input2] [float] NULL,
	[Input3] [float] NULL,
	[Query] [nvarchar](256) NULL,
	[Result] [float] NULL,
	[Timestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_History] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operation]    Script Date: 2/26/2023 2:24:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](16) NULL,
 CONSTRAINT [PK_Operation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[History] ADD  DEFAULT (getdate()) FOR [Timestamp]
GO
/****** Object:  StoredProcedure [dbo].[GetHistoryByOperation]    Script Date: 2/26/2023 2:24:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[GetHistoryByOperation] 
	@depth int,
	@operation varchar(16)

AS
BEGIN
	
	select	TOP(@depth) *
	FROM	[dbo].[History] 
	where	Operation = @operation
			--Query like '%' + @operation + '%'
	order by Timestamp desc

END

GO
/****** Object:  StoredProcedure [dbo].[GetStatisticsByOperation]    Script Date: 2/26/2023 2:24:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[GetStatisticsByOperation] 
	@operation varchar(16)

AS
BEGIN
	select	COUNT(1) as op_count,
			MIN(Result) as op_min,
			MAX(Result) as op_max,
			AVG(Result) as op_avg
	from	[dbo].[History]
	where	Timestamp >= DATEADD(mm, DATEDIFF(m,0,GETDATE()),0) and		
			--Query like '%' + @operation + '%'
			Operation = @operation
END

GO
/****** Object:  StoredProcedure [dbo].[WriteHistory]    Script Date: 2/26/2023 2:24:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE     PROCEDURE [dbo].[WriteHistory] 
	@operation nvarchar(256),
	@input1 float,
	@input2 float,
	@result float

AS
BEGIN
	
	INSERT INTO dbo.History (
            Operation,
	    Input1,
	    Input2,
            Result                 
          ) 
     VALUES 
          ( 
            @operation,
	    @input1,
	    @input2,
            @result                 
          ) 
	
	select MAX(Id)
	FROM [dbo].[History] 

END

GO
USE [master]
GO
ALTER DATABASE [Gaya] SET  READ_WRITE 
GO
