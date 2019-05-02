
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/27/2018 15:33:29
-- Generated from EDMX file: C:\Users\Administrator\EFMVCApp\Model\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [School];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Students_ToClassTab]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_Students_ToClassTab];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ClassTab]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClassTab];
GO
IF OBJECT_ID(N'[dbo].[Students]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Students];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ClassTab'
CREATE TABLE [dbo].[ClassTab] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ClassName] nvarchar(50)  NULL
);
GO

-- Creating table 'Students'
CREATE TABLE [dbo].[Students] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StudentName] nvarchar(50)  NULL,
    [Age] int  NULL,
    [Sex] nvarchar(1)  NULL,
    [Chinese] decimal(18,2)  NULL,
    [Math] decimal(18,2)  NULL,
    [English] decimal(18,2)  NULL,
    [PictruePath] varchar(100)  NULL,
    [ClassId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LoginName] varchar(50)  NULL,
    [UserName] nvarchar(50)  NULL,
    [PassWord] varchar(100)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ClassTab'
ALTER TABLE [dbo].[ClassTab]
ADD CONSTRAINT [PK_ClassTab]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [PK_Students]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ClassId] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [FK_Students_ToClassTab]
    FOREIGN KEY ([ClassId])
    REFERENCES [dbo].[ClassTab]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Students_ToClassTab'
CREATE INDEX [IX_FK_Students_ToClassTab]
ON [dbo].[Students]
    ([ClassId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------