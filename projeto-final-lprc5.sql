USE [master]
GO
/****** Object:  Database [faculdade]    Script Date: 06/06/2023 09:38:26 ******/
CREATE DATABASE [faculdade]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'faculdade', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\faculdade.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'faculdade_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\faculdade_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [faculdade] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [faculdade].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [faculdade] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [faculdade] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [faculdade] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [faculdade] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [faculdade] SET ARITHABORT OFF 
GO
ALTER DATABASE [faculdade] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [faculdade] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [faculdade] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [faculdade] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [faculdade] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [faculdade] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [faculdade] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [faculdade] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [faculdade] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [faculdade] SET  DISABLE_BROKER 
GO
ALTER DATABASE [faculdade] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [faculdade] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [faculdade] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [faculdade] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [faculdade] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [faculdade] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [faculdade] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [faculdade] SET RECOVERY FULL 
GO
ALTER DATABASE [faculdade] SET  MULTI_USER 
GO
ALTER DATABASE [faculdade] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [faculdade] SET DB_CHAINING OFF 
GO
ALTER DATABASE [faculdade] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [faculdade] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [faculdade] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [faculdade] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'faculdade', N'ON'
GO
ALTER DATABASE [faculdade] SET QUERY_STORE = ON
GO
ALTER DATABASE [faculdade] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [faculdade]
GO
/****** Object:  Table [dbo].[aluno]    Script Date: 06/06/2023 09:38:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aluno](
	[matricula] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NULL,
	[data_nasc] [date] NULL,
	[data_ing] [date] NULL,
	[curso] [int] NULL,
	[cidade] [varchar](50) NULL,
	[estado] [char](2) NULL,
	[email] [varchar](50) NULL,
	[endereco] [varchar](50) NULL,
	[data_inc] [datetime] NULL,
	[data_alt] [datetime] NULL,
	[cep] [char](8) NULL,
 CONSTRAINT [PK_aluno] PRIMARY KEY CLUSTERED 
(
	[matricula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aluno_turma]    Script Date: 06/06/2023 09:38:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aluno_turma](
	[id_aluno] [int] NOT NULL,
	[id_turma] [int] NOT NULL,
 CONSTRAINT [PK_aluno_turma] PRIMARY KEY CLUSTERED 
(
	[id_aluno] ASC,
	[id_turma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[curso]    Script Date: 06/06/2023 09:38:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[curso](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NULL,
	[duracao_sem] [int] NULL,
	[tipo] [varchar](50) NULL,
	[data_inc] [datetime] NULL,
	[data_alt] [datetime] NULL,
 CONSTRAINT [PK_curso] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[departamento]    Script Date: 06/06/2023 09:38:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[departamento](
	[nome] [varchar](50) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[prof_responsavel] [int] NULL,
	[localizacao] [varchar](50) NULL,
	[data_inc] [datetime] NULL,
	[data_alt] [datetime] NULL,
 CONSTRAINT [PK_departamento] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[disciplina]    Script Date: 06/06/2023 09:38:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[disciplina](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NULL,
	[semestre] [int] NULL,
	[carga_horario] [float] NULL,
	[data_inc] [datetime] NULL,
	[data_alt] [datetime] NULL,
 CONSTRAINT [PK_disciplina] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[professor]    Script Date: 06/06/2023 09:38:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[professor](
	[registro] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NULL,
	[endereco] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[id_departamento] [int] NULL,
	[estado] [char](2) NULL,
	[cidade] [varchar](50) NULL,
	[data_inc] [datetime] NULL,
	[data_alt] [datetime] NULL,
	[cep] [char](8) NULL,
 CONSTRAINT [PK_professor] PRIMARY KEY CLUSTERED 
(
	[registro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[turma]    Script Date: 06/06/2023 09:38:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[turma](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[curso] [int] NULL,
	[professor] [int] NULL,
	[disciplina] [int] NULL,
	[data_inc] [datetime] NULL,
	[data_alt] [datetime] NULL,
 CONSTRAINT [PK_turma] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[aluno_turma]  WITH CHECK ADD  CONSTRAINT [FK_aluno_turma] FOREIGN KEY([id_aluno])
REFERENCES [dbo].[aluno] ([matricula])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[aluno_turma] CHECK CONSTRAINT [FK_aluno_turma]
GO
ALTER TABLE [dbo].[aluno_turma]  WITH CHECK ADD  CONSTRAINT [FK_turma_aluno_turma] FOREIGN KEY([id_turma])
REFERENCES [dbo].[turma] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[aluno_turma] CHECK CONSTRAINT [FK_turma_aluno_turma]
GO
ALTER TABLE [dbo].[departamento]  WITH CHECK ADD  CONSTRAINT [FK_departamento_professor] FOREIGN KEY([prof_responsavel])
REFERENCES [dbo].[professor] ([registro])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[departamento] CHECK CONSTRAINT [FK_departamento_professor]
GO
ALTER TABLE [dbo].[turma]  WITH CHECK ADD  CONSTRAINT [FK_turma_curso] FOREIGN KEY([curso])
REFERENCES [dbo].[curso] ([id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[turma] CHECK CONSTRAINT [FK_turma_curso]
GO
ALTER TABLE [dbo].[turma]  WITH CHECK ADD  CONSTRAINT [FK_turma_disciplina] FOREIGN KEY([disciplina])
REFERENCES [dbo].[disciplina] ([id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[turma] CHECK CONSTRAINT [FK_turma_disciplina]
GO
ALTER TABLE [dbo].[turma]  WITH CHECK ADD  CONSTRAINT [FK_turma_professor] FOREIGN KEY([professor])
REFERENCES [dbo].[professor] ([registro])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[turma] CHECK CONSTRAINT [FK_turma_professor]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'turma referenciando professor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'turma', @level2type=N'CONSTRAINT',@level2name=N'FK_turma_professor'
GO
USE [master]
GO
ALTER DATABASE [faculdade] SET  READ_WRITE 
GO
