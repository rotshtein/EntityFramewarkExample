
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/24/2016 12:12:36
-- Generated from EDMX file: c:\users\uri\documents\visual studio 2015\Projects\ado\ado\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RIT_QA];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ElectricalTest_To_Batch]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ElectricalTest] DROP CONSTRAINT [FK_ElectricalTest_To_Batch];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Batch]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Batch];
GO
IF OBJECT_ID(N'[dbo].[ElectricalTest]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ElectricalTest];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ElectricalTests'
CREATE TABLE [dbo].[ElectricalTests] (
    [SerialNo] nchar(10)  NOT NULL,
    [MAC] nchar(12)  NOT NULL,
    [Result] bit  NOT NULL,
    [BatchId] int  NULL
);
GO

-- Creating table 'Batches'
CREATE TABLE [dbo].[Batches] (
    [Id] int  NOT NULL,
    [BatchNumber] nchar(15)  NULL,
    [Date] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [SerialNo] in table 'ElectricalTests'
ALTER TABLE [dbo].[ElectricalTests]
ADD CONSTRAINT [PK_ElectricalTests]
    PRIMARY KEY CLUSTERED ([SerialNo] ASC);
GO

-- Creating primary key on [Id] in table 'Batches'
ALTER TABLE [dbo].[Batches]
ADD CONSTRAINT [PK_Batches]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BatchId] in table 'ElectricalTests'
ALTER TABLE [dbo].[ElectricalTests]
ADD CONSTRAINT [FK_ElectricalTest_To_Batch]
    FOREIGN KEY ([BatchId])
    REFERENCES [dbo].[Batches]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ElectricalTest_To_Batch'
CREATE INDEX [IX_FK_ElectricalTest_To_Batch]
ON [dbo].[ElectricalTests]
    ([BatchId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------