CREATE DATABASE DBDonaLaura;

USE DBDonaLaura;

CREATE TABLE [dbo].[TBProduct]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(40) NOT NULL,
	[SalePrice] NUMERIC(7,2) NOT NULL,
	[CostPrice] NUMERIC(7,2) NOT NULL,
	[Disponibility] BIT NOT NULL,
	[FabricationDate] DATE NOT NULL,
	[ExpirationDate] DATE NOT NULL
)

CREATE TABLE [dbo].[TBSale]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ProductId] INT NOT NULL,
	[ClientName] VARCHAR(40) NOT NULL,
	[Quantity] INT NOT NULL,
	[Lucre] NUMERIC(7,2) NOT NULL,
	CONSTRAINT [FK_TBSale_TBProduct] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[TBProduct] ([Id])
)