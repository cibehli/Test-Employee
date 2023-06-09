USE [master]
GO

CREATE DATABASE [Employees]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Employees', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Employees.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Employees_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Employees_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Employees].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Employees] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Employees] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Employees] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Employees] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Employees] SET ARITHABORT OFF 
GO

ALTER DATABASE [Employees] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Employees] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Employees] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Employees] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Employees] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Employees] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Employees] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Employees] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Employees] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Employees] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Employees] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Employees] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Employees] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Employees] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Employees] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Employees] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Employees] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Employees] SET RECOVERY FULL 
GO

ALTER DATABASE [Employees] SET  MULTI_USER 
GO

ALTER DATABASE [Employees] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Employees] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Employees] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Employees] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [Employees] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [Employees] SET QUERY_STORE = OFF
GO

ALTER DATABASE [Employees] SET  READ_WRITE 
GO



USE [Employees]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[RFC] [nvarchar](13) NOT NULL,
	[BornDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Id], [Name], [LastName], [RFC], [BornDate], [Status]) VALUES (1, N'pruebaqq', N'pruebarr', N'zxcv123444444', CAST(N'2023-03-17T00:00:00.000' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[Employee] OFF

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Brenda Zatarain>
-- Create date: <16/03/2023>
-- Description:	<Inserta o Actualiza registros de Empleados>
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertModEmployee]
@Id int=null, @Name nvarchar(200),	@LastName nvarchar(200),	@RFC nvarchar(20),	@BornDate datetime,	@Status int= 0
AS
BEGIN
	
	SET NOCOUNT ON;
	if exists(select name from Employee where id=@id)
	begin
		if not exists(select rfc from Employee where Id!= @Id and rfc=@RFC)
		begin
			update Employee set name=@Name, LastName=@LastName, RFC=@RFC, BornDate=@BornDate, Status=@Status where id=@Id
		select 0, 'Se actualizo el registro Correctamente' as Mensaje
	
		end
		else
		begin
		select 1, 'Ya existe un RFC capturado' as Mensaje
		
		end
	end
	else
	begin
		if not exists(select rfc from Employee where  rfc=@RFC)
		begin
			Insert into employee 
					(Name,	LastName,	RFC,	BornDate,	Status) 
			Values (@Name,	@LastName,	@RFC,	@BornDate,	@Status) 
				select 0, 'Se inserto registro Correctamente' as Mensaje

		end
		else
		begin
			select 1, 'Ya existe un RFC capturado' as Mensaje
		end
	end


	SELECT @Id, @Name,	@LastName,	@RFC,	@BornDate,	@Status
END
GO
