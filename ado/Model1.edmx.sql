
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/28/2016 09:32:58
-- Generated from EDMX file: C:\Users\uri\Documents\Visual Studio 2015\Projects\example\ado\Model1.edmx
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

IF OBJECT_ID(N'[dbo].[FK_Calibration_Batches]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Calibrations] DROP CONSTRAINT [FK_Calibration_Batches];
GO
IF OBJECT_ID(N'[dbo].[FK_CalibrationDatas_Calibrations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CalibrationDatas] DROP CONSTRAINT [FK_CalibrationDatas_Calibrations];
GO
IF OBJECT_ID(N'[dbo].[FK_ElectricalTest_To_Batch]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ElectricalTests] DROP CONSTRAINT [FK_ElectricalTest_To_Batch];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Batches]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Batches];
GO
IF OBJECT_ID(N'[dbo].[CalibrationDatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CalibrationDatas];
GO
IF OBJECT_ID(N'[dbo].[Calibrations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Calibrations];
GO
IF OBJECT_ID(N'[dbo].[ElectricalTests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ElectricalTests];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Batches'
CREATE TABLE [dbo].[Batches] (
    [Id] int  NOT NULL,
    [BatchNumber] nchar(15)  NULL,
    [Date] datetime  NULL
);
GO

-- Creating table 'Calibrations'
CREATE TABLE [dbo].[Calibrations] (
    [SerialNo] nchar(20)  NOT NULL,
    [MAC] nchar(12)  NULL,
    [Date] datetime  NULL,
    [BatchId] int  NULL
);
GO

-- Creating table 'ElectricalTests'
CREATE TABLE [dbo].[ElectricalTests] (
    [SerialNo] nchar(20)  NOT NULL,
    [MAC] nchar(12)  NOT NULL,
    [Result] bit  NOT NULL,
    [SwVersion] nchar(20)  NULL,
    [HwVersion] nchar(20)  NULL,
    [BatchId] int  NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [IdleCurrent] int  NULL,
    [ConnectedCurrent] int  NULL,
    [PS1_TargetOutput] int  NULL,
    [PS1_Output] int  NULL,
    [PS2_TargetOutput] int  NULL,
    [PS2_Output] int  NULL,
    [PS_Left_A2D_Idle] int  NULL,
    [PS_Left_A2D_UnderPressure] int  NULL,
    [PS_Right_A2D_Idle] int  NULL,
    [PS_Right_A2D_UnderPressure] int  NULL,
    [Current_6V] int  NULL,
    [Current_24V] int  NULL,
    [Output_1_6V] int  NULL,
    [Output_2_6V] int  NULL,
    [Output_1_24V] nchar(10)  NULL,
    [Output_2_24V] nchar(10)  NULL,
    [JigTemp] decimal(1,0)  NULL,
    [DpTemp] decimal(1,0)  NULL
);
GO

-- Creating table 'CalibrationDatas'
CREATE TABLE [dbo].[CalibrationDatas] (
    [SerialNo] nchar(20)  NOT NULL,
    [SetPoint] float  NOT NULL,
    [Pressure] float  NOT NULL,
    [RightA2D] int  NULL,
    [LeftA2D] int  NULL,
    [Temp] float  NULL,
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Batches'
ALTER TABLE [dbo].[Batches]
ADD CONSTRAINT [PK_Batches]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [SerialNo] in table 'Calibrations'
ALTER TABLE [dbo].[Calibrations]
ADD CONSTRAINT [PK_Calibrations]
    PRIMARY KEY CLUSTERED ([SerialNo] ASC);
GO

-- Creating primary key on [SerialNo] in table 'ElectricalTests'
ALTER TABLE [dbo].[ElectricalTests]
ADD CONSTRAINT [PK_ElectricalTests]
    PRIMARY KEY CLUSTERED ([SerialNo] ASC);
GO

-- Creating primary key on [Id] in table 'CalibrationDatas'
ALTER TABLE [dbo].[CalibrationDatas]
ADD CONSTRAINT [PK_CalibrationDatas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BatchId] in table 'Calibrations'
ALTER TABLE [dbo].[Calibrations]
ADD CONSTRAINT [FK_Calibration_Batches]
    FOREIGN KEY ([BatchId])
    REFERENCES [dbo].[Batches]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Calibration_Batches'
CREATE INDEX [IX_FK_Calibration_Batches]
ON [dbo].[Calibrations]
    ([BatchId]);
GO

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

-- Creating foreign key on [SerialNo] in table 'CalibrationDatas'
ALTER TABLE [dbo].[CalibrationDatas]
ADD CONSTRAINT [FK_CalibrationDatas_Calibrations]
    FOREIGN KEY ([SerialNo])
    REFERENCES [dbo].[Calibrations]
        ([SerialNo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CalibrationDatas_Calibrations'
CREATE INDEX [IX_FK_CalibrationDatas_Calibrations]
ON [dbo].[CalibrationDatas]
    ([SerialNo]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------