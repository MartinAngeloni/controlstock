USE [master]
GO
/****** Object:  Database [ControlStock]    Script Date: 19/5/2018 9:46:28 a. m. ******/
CREATE DATABASE [ControlStock]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ControlStock', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\ControlStock.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ControlStock_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\ControlStock_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ControlStock] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ControlStock].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ControlStock] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ControlStock] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ControlStock] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ControlStock] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ControlStock] SET ARITHABORT OFF 
GO
ALTER DATABASE [ControlStock] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ControlStock] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ControlStock] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ControlStock] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ControlStock] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ControlStock] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ControlStock] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ControlStock] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ControlStock] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ControlStock] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ControlStock] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ControlStock] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ControlStock] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ControlStock] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ControlStock] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ControlStock] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ControlStock] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ControlStock] SET RECOVERY FULL 
GO
ALTER DATABASE [ControlStock] SET  MULTI_USER 
GO
ALTER DATABASE [ControlStock] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ControlStock] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ControlStock] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ControlStock] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ControlStock] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ControlStock', N'ON'
GO
ALTER DATABASE [ControlStock] SET QUERY_STORE = OFF
GO
USE [ControlStock]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [ControlStock]
GO
/****** Object:  User [ferpc]    Script Date: 19/5/2018 9:46:28 a. m. ******/
CREATE USER [ferpc] FOR LOGIN [ferpc] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [fer]    Script Date: 19/5/2018 9:46:28 a. m. ******/
CREATE USER [fer] FOR LOGIN [fer] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [electrored]    Script Date: 19/5/2018 9:46:28 a. m. ******/
CREATE USER [electrored] FOR LOGIN [electrored] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [ferpc]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [ferpc]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [ferpc]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [ferpc]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [ferpc]
GO
ALTER ROLE [db_datareader] ADD MEMBER [ferpc]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [ferpc]
GO
ALTER ROLE [db_owner] ADD MEMBER [fer]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [fer]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [fer]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [fer]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [fer]
GO
ALTER ROLE [db_datareader] ADD MEMBER [fer]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [fer]
GO
ALTER ROLE [db_owner] ADD MEMBER [electrored]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [electrored]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [electrored]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [electrored]
GO
ALTER ROLE [db_datareader] ADD MEMBER [electrored]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [electrored]
GO
/****** Object:  Table [dbo].[Color]    Script Date: 19/5/2018 9:46:28 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Color](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[color] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Marca]    Script Date: 19/5/2018 9:46:28 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marca](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 19/5/2018 9:46:28 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[id_pedido] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
	[fecha] [datetime] NOT NULL,
	[proveedor] [varchar](50) NULL,
 CONSTRAINT [PK__Pedido__6FF01489117F9D94] PRIMARY KEY CLUSTERED 
(
	[id_pedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 19/5/2018 9:46:28 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[Cod_Producto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Modelo] [varchar](50) NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK__Producto__8D1DFB7A15502E78] PRIMARY KEY CLUSTERED 
(
	[Cod_Producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProdXPedido]    Script Date: 19/5/2018 9:46:29 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProdXPedido](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_producto] [varchar](50) NOT NULL,
	[id_pedido] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio] [float] NULL,
 CONSTRAINT [PK__ProdXPed__3213E83F20C1E124] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProdXProveedor]    Script Date: 19/5/2018 9:46:29 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProdXProveedor](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_producto] [int] NULL,
	[id_proveedor] [int] NULL,
 CONSTRAINT [PK_ProdXProveedor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProdXVenta]    Script Date: 19/5/2018 9:46:29 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProdXVenta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_producto] [varchar](50) NOT NULL,
	[id_venta] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio] [float] NULL,
 CONSTRAINT [PK__ProdXVen__3213E83F267ABA7A] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 19/5/2018 9:46:29 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedor](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[cuit] [varchar](50) NOT NULL,
	[cbu] [varchar](50) NOT NULL,
	[direccion] [varchar](150) NULL,
	[telefono] [varchar](50) NULL,
	[correo] [varchar](50) NULL,
	[pagina] [varchar](50) NULL,
 CONSTRAINT [PK_Proveedor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProXm]    Script Date: 19/5/2018 9:46:29 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProXm](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_marca] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
	[precio] [float] NULL,
	[id_color] [int] NOT NULL,
	[stock] [int] NOT NULL,
	[codigo] [varchar](50) NOT NULL,
	[alerta_stock] [int] NULL,
	[baja] [bit] NOT NULL,
	[precio_lista] [int] NOT NULL,
	[precio_contado] [int] NOT NULL,
	[precio_re_venta] [int] NOT NULL,
 CONSTRAINT [PK__ProXm__3213E83F2C3393D0] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProXm] UNIQUE NONCLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 19/5/2018 9:46:29 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](50) NULL,
	[Contrasenia] [varchar](50) NULL,
	[Tipo] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 19/5/2018 9:46:29 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [datetime] NOT NULL,
	[descripcion] [varchar](50) NULL,
	[total] [float] NULL,
	[diferencia_cambio] [float] NULL,
 CONSTRAINT [PK__Venta__3213E83F1CF15040] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProXm] ADD  CONSTRAINT [DF_ProXm_baja]  DEFAULT ((0)) FOR [baja]
GO
ALTER TABLE [dbo].[ProXm] ADD  CONSTRAINT [DF_ProXm_precio_lista]  DEFAULT ((0)) FOR [precio_lista]
GO
ALTER TABLE [dbo].[ProXm] ADD  CONSTRAINT [DF_ProXm_precio_contado]  DEFAULT ((0)) FOR [precio_contado]
GO
ALTER TABLE [dbo].[ProXm] ADD  CONSTRAINT [DF_ProXm_precio_re_venta]  DEFAULT ((0)) FOR [precio_re_venta]
GO
ALTER TABLE [dbo].[Venta] ADD  CONSTRAINT [DF_Venta_diferencia_cambio]  DEFAULT ((0)) FOR [diferencia_cambio]
GO
ALTER TABLE [dbo].[ProdXPedido]  WITH CHECK ADD  CONSTRAINT [FK_ProdXPedido_ToTable] FOREIGN KEY([id_producto])
REFERENCES [dbo].[ProXm] ([codigo])
GO
ALTER TABLE [dbo].[ProdXPedido] CHECK CONSTRAINT [FK_ProdXPedido_ToTable]
GO
ALTER TABLE [dbo].[ProdXPedido]  WITH CHECK ADD  CONSTRAINT [FK_ProdXPedido_ToTable_1] FOREIGN KEY([id_pedido])
REFERENCES [dbo].[Pedido] ([id_pedido])
GO
ALTER TABLE [dbo].[ProdXPedido] CHECK CONSTRAINT [FK_ProdXPedido_ToTable_1]
GO
ALTER TABLE [dbo].[ProdXProveedor]  WITH CHECK ADD  CONSTRAINT [FK_ProdXProveedor_Proveedor] FOREIGN KEY([id_proveedor])
REFERENCES [dbo].[Proveedor] ([id])
GO
ALTER TABLE [dbo].[ProdXProveedor] CHECK CONSTRAINT [FK_ProdXProveedor_Proveedor]
GO
ALTER TABLE [dbo].[ProdXProveedor]  WITH CHECK ADD  CONSTRAINT [FK_ProdXProveedor_ProXm] FOREIGN KEY([id_producto])
REFERENCES [dbo].[ProXm] ([id])
GO
ALTER TABLE [dbo].[ProdXProveedor] CHECK CONSTRAINT [FK_ProdXProveedor_ProXm]
GO
ALTER TABLE [dbo].[ProdXVenta]  WITH CHECK ADD  CONSTRAINT [FK_ProdXVenta_ToTable] FOREIGN KEY([id_producto])
REFERENCES [dbo].[ProXm] ([codigo])
GO
ALTER TABLE [dbo].[ProdXVenta] CHECK CONSTRAINT [FK_ProdXVenta_ToTable]
GO
ALTER TABLE [dbo].[ProdXVenta]  WITH CHECK ADD  CONSTRAINT [FK_ProdXVenta_ToTable_1] FOREIGN KEY([id_venta])
REFERENCES [dbo].[Venta] ([id])
GO
ALTER TABLE [dbo].[ProdXVenta] CHECK CONSTRAINT [FK_ProdXVenta_ToTable_1]
GO
ALTER TABLE [dbo].[ProXm]  WITH CHECK ADD  CONSTRAINT [FK_ProXm_ToTable] FOREIGN KEY([id_color])
REFERENCES [dbo].[Color] ([id])
GO
ALTER TABLE [dbo].[ProXm] CHECK CONSTRAINT [FK_ProXm_ToTable]
GO
ALTER TABLE [dbo].[ProXm]  WITH CHECK ADD  CONSTRAINT [FK_Table_ToTable1] FOREIGN KEY([id_marca])
REFERENCES [dbo].[Marca] ([id])
GO
ALTER TABLE [dbo].[ProXm] CHECK CONSTRAINT [FK_Table_ToTable1]
GO
ALTER TABLE [dbo].[ProXm]  WITH CHECK ADD  CONSTRAINT [FK_Table_ToTable2] FOREIGN KEY([id_producto])
REFERENCES [dbo].[Producto] ([Cod_Producto])
GO
ALTER TABLE [dbo].[ProXm] CHECK CONSTRAINT [FK_Table_ToTable2]
GO
USE [master]
GO
ALTER DATABASE [ControlStock] SET  READ_WRITE 
GO
