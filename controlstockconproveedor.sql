USE [ControlStock]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 04/29/2018 12:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Producto](
	[Cod_Producto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Modelo] [varchar](50) NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK__Producto__8D1DFB7A15502E78] PRIMARY KEY CLUSTERED 
(
	[Cod_Producto] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 04/29/2018 12:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pedido](
	[id_pedido] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
	[fecha] [datetime] NOT NULL,
	[proveedor] [varchar](50) NULL,
 CONSTRAINT [PK__Pedido__6FF01489117F9D94] PRIMARY KEY CLUSTERED 
(
	[id_pedido] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Marca]    Script Date: 04/29/2018 12:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Marca](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Color]    Script Date: 04/29/2018 12:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Color](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[color] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 04/29/2018 12:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Proveedor](
	[id] [int] NOT NULL,
	[nombre] [varchar](50) NULL,
	[cuit] [varchar](50) NULL,
	[cbu] [varchar](50) NULL,
	[direccion] [varchar](150) NULL,
	[telefono] [varchar](50) NULL,
	[correo] [varchar](50) NULL,
	[pagina] [varchar](100) NULL,
 CONSTRAINT [PK_Proveedor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 04/29/2018 12:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 04/29/2018 12:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](50) NULL,
	[Contrasenia] [varchar](50) NULL,
	[Tipo] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProXm]    Script Date: 04/29/2018 12:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
 CONSTRAINT [PK__ProXm__3213E83F2C3393D0] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProXm] UNIQUE NONCLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProdXVenta]    Script Date: 04/29/2018 12:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProdXProveedor]    Script Date: 04/29/2018 12:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProdXProveedor](
	[id] [int] NOT NULL,
	[id_producto] [int] NULL,
	[id_proveedor] [int] NULL,
 CONSTRAINT [PK_ProdXProveedor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProdXPedido]    Script Date: 04/29/2018 12:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_ProXm_baja]    Script Date: 04/29/2018 12:24:29 ******/
ALTER TABLE [dbo].[ProXm] ADD  CONSTRAINT [DF_ProXm_baja]  DEFAULT ((0)) FOR [baja]
GO
/****** Object:  Default [DF_Venta_diferencia_cambio]    Script Date: 04/29/2018 12:24:29 ******/
ALTER TABLE [dbo].[Venta] ADD  CONSTRAINT [DF_Venta_diferencia_cambio]  DEFAULT ((0)) FOR [diferencia_cambio]
GO
/****** Object:  ForeignKey [FK_ProdXPedido_ToTable]    Script Date: 04/29/2018 12:24:29 ******/
ALTER TABLE [dbo].[ProdXPedido]  WITH CHECK ADD  CONSTRAINT [FK_ProdXPedido_ToTable] FOREIGN KEY([id_producto])
REFERENCES [dbo].[ProXm] ([codigo])
GO
ALTER TABLE [dbo].[ProdXPedido] CHECK CONSTRAINT [FK_ProdXPedido_ToTable]
GO
/****** Object:  ForeignKey [FK_ProdXPedido_ToTable_1]    Script Date: 04/29/2018 12:24:29 ******/
ALTER TABLE [dbo].[ProdXPedido]  WITH CHECK ADD  CONSTRAINT [FK_ProdXPedido_ToTable_1] FOREIGN KEY([id_pedido])
REFERENCES [dbo].[Pedido] ([id_pedido])
GO
ALTER TABLE [dbo].[ProdXPedido] CHECK CONSTRAINT [FK_ProdXPedido_ToTable_1]
GO
/****** Object:  ForeignKey [FK_ProdXProveedor_Proveedor]    Script Date: 04/29/2018 12:24:29 ******/
ALTER TABLE [dbo].[ProdXProveedor]  WITH CHECK ADD  CONSTRAINT [FK_ProdXProveedor_Proveedor] FOREIGN KEY([id_proveedor])
REFERENCES [dbo].[Proveedor] ([id])
GO
ALTER TABLE [dbo].[ProdXProveedor] CHECK CONSTRAINT [FK_ProdXProveedor_Proveedor]
GO
/****** Object:  ForeignKey [FK_ProdXProveedor_ProXm]    Script Date: 04/29/2018 12:24:29 ******/
ALTER TABLE [dbo].[ProdXProveedor]  WITH CHECK ADD  CONSTRAINT [FK_ProdXProveedor_ProXm] FOREIGN KEY([id_producto])
REFERENCES [dbo].[ProXm] ([id])
GO
ALTER TABLE [dbo].[ProdXProveedor] CHECK CONSTRAINT [FK_ProdXProveedor_ProXm]
GO
/****** Object:  ForeignKey [FK_ProdXVenta_ToTable]    Script Date: 04/29/2018 12:24:29 ******/
ALTER TABLE [dbo].[ProdXVenta]  WITH CHECK ADD  CONSTRAINT [FK_ProdXVenta_ToTable] FOREIGN KEY([id_producto])
REFERENCES [dbo].[ProXm] ([codigo])
GO
ALTER TABLE [dbo].[ProdXVenta] CHECK CONSTRAINT [FK_ProdXVenta_ToTable]
GO
/****** Object:  ForeignKey [FK_ProdXVenta_ToTable_1]    Script Date: 04/29/2018 12:24:29 ******/
ALTER TABLE [dbo].[ProdXVenta]  WITH CHECK ADD  CONSTRAINT [FK_ProdXVenta_ToTable_1] FOREIGN KEY([id_venta])
REFERENCES [dbo].[Venta] ([id])
GO
ALTER TABLE [dbo].[ProdXVenta] CHECK CONSTRAINT [FK_ProdXVenta_ToTable_1]
GO
/****** Object:  ForeignKey [FK_ProXm_ToTable]    Script Date: 04/29/2018 12:24:29 ******/
ALTER TABLE [dbo].[ProXm]  WITH CHECK ADD  CONSTRAINT [FK_ProXm_ToTable] FOREIGN KEY([id_color])
REFERENCES [dbo].[Color] ([id])
GO
ALTER TABLE [dbo].[ProXm] CHECK CONSTRAINT [FK_ProXm_ToTable]
GO
/****** Object:  ForeignKey [FK_Table_ToTable1]    Script Date: 04/29/2018 12:24:29 ******/
ALTER TABLE [dbo].[ProXm]  WITH CHECK ADD  CONSTRAINT [FK_Table_ToTable1] FOREIGN KEY([id_marca])
REFERENCES [dbo].[Marca] ([id])
GO
ALTER TABLE [dbo].[ProXm] CHECK CONSTRAINT [FK_Table_ToTable1]
GO
/****** Object:  ForeignKey [FK_Table_ToTable2]    Script Date: 04/29/2018 12:24:29 ******/
ALTER TABLE [dbo].[ProXm]  WITH CHECK ADD  CONSTRAINT [FK_Table_ToTable2] FOREIGN KEY([id_producto])
REFERENCES [dbo].[Producto] ([Cod_Producto])
GO
ALTER TABLE [dbo].[ProXm] CHECK CONSTRAINT [FK_Table_ToTable2]
GO
