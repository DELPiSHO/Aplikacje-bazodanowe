USE [master]
GO
/****** Object:  Database [project_game]    Script Date: 6/7/2018 11:30:13 AM ******/
CREATE DATABASE [project_game] ON  PRIMARY 
( NAME = N'project_game', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.DELPISHO\MSSQL\DATA\project_game.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'project_game_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.DELPISHO\MSSQL\DATA\project_game_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [project_game] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [project_game].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [project_game] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [project_game] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [project_game] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [project_game] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [project_game] SET ARITHABORT OFF 
GO
ALTER DATABASE [project_game] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [project_game] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [project_game] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [project_game] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [project_game] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [project_game] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [project_game] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [project_game] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [project_game] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [project_game] SET  DISABLE_BROKER 
GO
ALTER DATABASE [project_game] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [project_game] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [project_game] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [project_game] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [project_game] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [project_game] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [project_game] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [project_game] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [project_game] SET  MULTI_USER 
GO
ALTER DATABASE [project_game] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [project_game] SET DB_CHAINING OFF 
GO
USE [project_game]
GO
/****** Object:  Table [dbo].[adres]    Script Date: 6/7/2018 11:30:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[adres](
	[id_adres] [int] NOT NULL,
	[panstwo] [varchar](50) NOT NULL,
	[miasto] [varchar](50) NOT NULL,
	[ulica] [varchar](50) NOT NULL,
	[nr_domu] [varchar](50) NOT NULL,
	[nr_pokoju] [int] NOT NULL,
 CONSTRAINT [PK_adres] PRIMARY KEY CLUSTERED 
(
	[id_adres] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[gra]    Script Date: 6/7/2018 11:30:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[gra](
	[id_gra] [int] NOT NULL,
	[nazwa] [varchar](50) NOT NULL,
	[ograniczenie_wiekowe] [int] NOT NULL,
	[gatunek] [varchar](50) NOT NULL,
	[platforma] [varchar](50) NOT NULL,
	[cena] [float] NOT NULL,
	[release_year] [int] NOT NULL,
 CONSTRAINT [PK_gra] PRIMARY KEY CLUSTERED 
(
	[id_gra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[klient]    Script Date: 6/7/2018 11:30:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[klient](
	[id_klient] [int] NOT NULL,
	[imie] [varchar](50) NOT NULL,
	[nazwisko] [varchar](50) NOT NULL,
	[pesel] [char](11) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[id_adres] [int] NOT NULL,
 CONSTRAINT [PK_klient] PRIMARY KEY CLUSTERED 
(
	[id_klient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pracownik]    Script Date: 6/7/2018 11:30:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pracownik](
	[id_pracownik] [int] NOT NULL,
	[imie] [varchar](50) NOT NULL,
	[nazwisko] [varchar](50) NOT NULL,
	[pesel] [char](11) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[id_adres] [int] NOT NULL,
 CONSTRAINT [PK_pracownik] PRIMARY KEY CLUSTERED 
(
	[id_pracownik] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[wypozyczenie]    Script Date: 6/7/2018 11:30:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wypozyczenie](
	[id_wypozyczenie] [int] NOT NULL,
	[data_wypozyczenia] [date] NOT NULL,
	[id_gra] [int] NOT NULL,
	[id_pracownik] [int] NOT NULL,
	[id_klient] [int] NOT NULL,
 CONSTRAINT [PK_wypozyczenie] PRIMARY KEY CLUSTERED 
(
	[id_wypozyczenie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[klient]  WITH CHECK ADD  CONSTRAINT [FK_klient_adres] FOREIGN KEY([id_adres])
REFERENCES [dbo].[adres] ([id_adres])
GO
ALTER TABLE [dbo].[klient] CHECK CONSTRAINT [FK_klient_adres]
GO
ALTER TABLE [dbo].[pracownik]  WITH CHECK ADD  CONSTRAINT [FK_pracownik_adres] FOREIGN KEY([id_adres])
REFERENCES [dbo].[adres] ([id_adres])
GO
ALTER TABLE [dbo].[pracownik] CHECK CONSTRAINT [FK_pracownik_adres]
GO
ALTER TABLE [dbo].[wypozyczenie]  WITH CHECK ADD  CONSTRAINT [FK_wypozyczenie_gra] FOREIGN KEY([id_gra])
REFERENCES [dbo].[gra] ([id_gra])
GO
ALTER TABLE [dbo].[wypozyczenie] CHECK CONSTRAINT [FK_wypozyczenie_gra]
GO
ALTER TABLE [dbo].[wypozyczenie]  WITH CHECK ADD  CONSTRAINT [FK_wypozyczenie_klient] FOREIGN KEY([id_klient])
REFERENCES [dbo].[klient] ([id_klient])
GO
ALTER TABLE [dbo].[wypozyczenie] CHECK CONSTRAINT [FK_wypozyczenie_klient]
GO
ALTER TABLE [dbo].[wypozyczenie]  WITH CHECK ADD  CONSTRAINT [FK_wypozyczenie_pracownik] FOREIGN KEY([id_pracownik])
REFERENCES [dbo].[pracownik] ([id_pracownik])
GO
ALTER TABLE [dbo].[wypozyczenie] CHECK CONSTRAINT [FK_wypozyczenie_pracownik]
GO
USE [master]
GO
ALTER DATABASE [project_game] SET  READ_WRITE 
GO
