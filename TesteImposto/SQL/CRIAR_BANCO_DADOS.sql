USE [Teste]
GO

/****** Object:  Table [dbo].[NotaFiscal]    Script Date: 24/07/2015 11:58:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.NotaFiscal') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[NotaFiscal](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[NumeroNotaFiscal] [int] NULL,
		[Serie] [int] NULL,
		[NomeCliente] [varchar](50) NULL,
		[EstadoDestino] [varchar](50) NULL,
		[EstadoOrigem] [varchar](50) NULL,
	 CONSTRAINT [PK_NotaFiscal] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.NotaFiscalItem') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[NotaFiscalItem](
		[Id] [int]  IDENTITY(1,1) NOT NULL,
		[IdNotaFiscal] [int] NULL,
		[Cfop] [varchar](5) NULL,
		[TipoIcms] [varchar](20) NULL,
		[BaseIcms] [decimal](18, 5) NULL,
		[AliquotaIcms] [decimal](18, 5) NULL,
		[ValorIcms] [decimal](18, 5) NULL,
		[NomeProduto] [varchar](50) NULL,
		[CodigoProduto] [varchar](20) NULL
	 CONSTRAINT [PK_NotaFiscalItem] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[NotaFiscalItem]  WITH CHECK ADD  CONSTRAINT [FK_NotaFiscal] FOREIGN KEY([Id])
	REFERENCES [dbo].[NotaFiscalItem] ([Id])

	ALTER TABLE [dbo].[NotaFiscalItem] CHECK CONSTRAINT [FK_NotaFiscal]

	SET ANSI_PADDING OFF
END
GO

--Colunas adicionais de NotaFiscalItem
IF NOT EXISTS(SELECT 1 FROM syscolumns WHERE id = OBJECT_ID(N'dbo.NotaFiscalItem') AND name = 'BaseIpi')
BEGIN
	ALTER TABLE dbo.NotaFiscalItem 
		ADD BaseIpi [decimal](18, 5) NULL
END
GO
IF NOT EXISTS(SELECT 1 FROM syscolumns WHERE id = OBJECT_ID(N'dbo.NotaFiscalItem') AND name = 'AliquotaIpi')
BEGIN
	ALTER TABLE dbo.NotaFiscalItem 
		ADD AliquotaIpi [decimal](18, 5) NULL
END
GO
IF NOT EXISTS(SELECT 1 FROM syscolumns WHERE id = OBJECT_ID(N'dbo.NotaFiscalItem') AND name = 'ValorIpi')
BEGIN
	ALTER TABLE dbo.NotaFiscalItem 
		ADD ValorIpi [decimal](18, 5) NULL
END
GO
IF NOT EXISTS(SELECT 1 FROM syscolumns WHERE id = OBJECT_ID(N'dbo.NotaFiscalItem') AND name = 'TaxaDesconto')
BEGIN
	ALTER TABLE dbo.NotaFiscalItem 
		ADD TaxaDesconto [decimal](18, 5) NULL
END
GO