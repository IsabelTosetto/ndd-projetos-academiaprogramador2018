﻿CREATE DATABASE DBBibliotecaRosangela;

USE DBBibliotecaRosangela;

CREATE TABLE [dbo].[TBBook]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] VARCHAR(50) NOT NULL,
	[Theme] VARCHAR(50) NOT NULL,
	[Author] VARCHAR(50) NOT NULL,
	[Volume] INT NOT NULL,
	[PublicationDate] DATE NOT NULL,
	[Disponibility] BIT NOT NULL
)

CREATE TABLE [dbo].[TBLoan]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ClientName] VARCHAR(50) NOT NULL,
	[BookId] INT NOT NULL,
	[ReturnDate] DATE NOT NULL,
	CONSTRAINT [FK_TBLoan_TBBook] FOREIGN KEY ([BookId]) REFERENCES [dbo].[TBBook] ([Id]) ON DELETE CASCADE
)