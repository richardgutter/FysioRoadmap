
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/30/2020 10:25:58
-- Generated from EDMX file: C:\Users\Richard\Documents\projecten\FysioRoadmap\FysioRoadmap\FysioRoadmap\Models\FRMModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FysioRoadmap];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Afsprakenverzekerde]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AfspraakSet] DROP CONSTRAINT [FK_Afsprakenverzekerde];
GO
IF OBJECT_ID(N'[dbo].[FK_Verzekerdeverzekering]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VerzekerdeSet] DROP CONSTRAINT [FK_Verzekerdeverzekering];
GO
IF OBJECT_ID(N'[dbo].[FK_Verzekeringsregelverzekering]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VerzekeringsregelSet] DROP CONSTRAINT [FK_Verzekeringsregelverzekering];
GO
IF OBJECT_ID(N'[dbo].[FK_Declaratieverzekerde]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeclaratieSet] DROP CONSTRAINT [FK_Declaratieverzekerde];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[VerzekerdeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VerzekerdeSet];
GO
IF OBJECT_ID(N'[dbo].[VerzekeringSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VerzekeringSet];
GO
IF OBJECT_ID(N'[dbo].[AfspraakSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AfspraakSet];
GO
IF OBJECT_ID(N'[dbo].[VerzekeringsregelSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VerzekeringsregelSet];
GO
IF OBJECT_ID(N'[dbo].[DeclaratieSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeclaratieSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'VerzekerdeSet'
CREATE TABLE [dbo].[VerzekerdeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NaamVerzekerde] nvarchar(max)  NOT NULL,
    [Geboortedatum] datetime  NOT NULL,
    [VerzekeringId] int  NOT NULL
);
GO

-- Creating table 'VerzekeringSet'
CREATE TABLE [dbo].[VerzekeringSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Naamverzekering] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AfspraakSet'
CREATE TABLE [dbo].[AfspraakSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Datum] datetime  NOT NULL,
    [VerzekerdeId] int  NOT NULL
);
GO

-- Creating table 'VerzekeringsregelSet'
CREATE TABLE [dbo].[VerzekeringsregelSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [VerzekeringId] int  NOT NULL,
    [Minleeftijd] smallint  NOT NULL,
    [Maxleeftijd] smallint  NOT NULL,
    [BedragBehandeling] decimal(10,4)  NOT NULL,
    [Jaartotaal] decimal(10,4)  NOT NULL
);
GO

-- Creating table 'DeclaratieSet'
CREATE TABLE [dbo].[DeclaratieSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DatumBehandeling] datetime  NOT NULL,
    [DeclaratieBedrag] decimal(10,4)  NOT NULL,
    [Totaal] decimal(10,4)  NOT NULL,
    [VerzekerdeId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'VerzekerdeSet'
ALTER TABLE [dbo].[VerzekerdeSet]
ADD CONSTRAINT [PK_VerzekerdeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VerzekeringSet'
ALTER TABLE [dbo].[VerzekeringSet]
ADD CONSTRAINT [PK_VerzekeringSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AfspraakSet'
ALTER TABLE [dbo].[AfspraakSet]
ADD CONSTRAINT [PK_AfspraakSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VerzekeringsregelSet'
ALTER TABLE [dbo].[VerzekeringsregelSet]
ADD CONSTRAINT [PK_VerzekeringsregelSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DeclaratieSet'
ALTER TABLE [dbo].[DeclaratieSet]
ADD CONSTRAINT [PK_DeclaratieSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [VerzekerdeId] in table 'AfspraakSet'
ALTER TABLE [dbo].[AfspraakSet]
ADD CONSTRAINT [FK_Afsprakenverzekerde]
    FOREIGN KEY ([VerzekerdeId])
    REFERENCES [dbo].[VerzekerdeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Afsprakenverzekerde'
CREATE INDEX [IX_FK_Afsprakenverzekerde]
ON [dbo].[AfspraakSet]
    ([VerzekerdeId]);
GO

-- Creating foreign key on [VerzekeringId] in table 'VerzekerdeSet'
ALTER TABLE [dbo].[VerzekerdeSet]
ADD CONSTRAINT [FK_Verzekerdeverzekering]
    FOREIGN KEY ([VerzekeringId])
    REFERENCES [dbo].[VerzekeringSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Verzekerdeverzekering'
CREATE INDEX [IX_FK_Verzekerdeverzekering]
ON [dbo].[VerzekerdeSet]
    ([VerzekeringId]);
GO

-- Creating foreign key on [VerzekeringId] in table 'VerzekeringsregelSet'
ALTER TABLE [dbo].[VerzekeringsregelSet]
ADD CONSTRAINT [FK_Verzekeringsregelverzekering]
    FOREIGN KEY ([VerzekeringId])
    REFERENCES [dbo].[VerzekeringSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Verzekeringsregelverzekering'
CREATE INDEX [IX_FK_Verzekeringsregelverzekering]
ON [dbo].[VerzekeringsregelSet]
    ([VerzekeringId]);
GO

-- Creating foreign key on [VerzekerdeId] in table 'DeclaratieSet'
ALTER TABLE [dbo].[DeclaratieSet]
ADD CONSTRAINT [FK_Declaratieverzekerde]
    FOREIGN KEY ([VerzekerdeId])
    REFERENCES [dbo].[VerzekerdeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Declaratieverzekerde'
CREATE INDEX [IX_FK_Declaratieverzekerde]
ON [dbo].[DeclaratieSet]
    ([VerzekerdeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------