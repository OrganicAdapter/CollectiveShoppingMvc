
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server Compact Edition
-- --------------------------------------------------
-- Date Created: 08/23/2014 14:47:15
-- Generated from EDMX file: d:\SoftIT\CollectiveShoppingMvc\CollectiveShoppingMvc\Models\ShoppingModel.edmx
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    ALTER TABLE [ProductSet] DROP CONSTRAINT [FK_ShopProduct];
GO
    ALTER TABLE [CategorySet] DROP CONSTRAINT [FK_ShopCategory];
GO
    ALTER TABLE [ProductSet] DROP CONSTRAINT [FK_ProductCategory];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- NOTE: if the table does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    DROP TABLE [ShopSet];
GO
    DROP TABLE [ProductSet];
GO
    DROP TABLE [CategorySet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ShopSet'
CREATE TABLE [ShopSet] (
    [ShopId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(4000)  NOT NULL,
    [Culture] nvarchar(4000)  NOT NULL,
    [IsEnabled] bit  NOT NULL
);
GO

-- Creating table 'ProductSet'
CREATE TABLE [ProductSet] (
    [ProductId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(4000)  NOT NULL,
    [Unit] nvarchar(4000)  NOT NULL,
    [Date] nvarchar(4000)  NOT NULL,
    [Price] float  NOT NULL,
    [UnitQuantity] float  NOT NULL,
    [IsEnabled] bit  NOT NULL,
    [ShopId] int  NOT NULL,
    [CategoryId] int  NOT NULL
);
GO

-- Creating table 'CategorySet'
CREATE TABLE [CategorySet] (
    [CategoryId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(4000)  NOT NULL,
    [IsEnabled] bit  NOT NULL,
    [ShopId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ShopId] in table 'ShopSet'
ALTER TABLE [ShopSet]
ADD CONSTRAINT [PK_ShopSet]
    PRIMARY KEY ([ShopId] );
GO

-- Creating primary key on [ProductId] in table 'ProductSet'
ALTER TABLE [ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY ([ProductId] );
GO

-- Creating primary key on [CategoryId] in table 'CategorySet'
ALTER TABLE [CategorySet]
ADD CONSTRAINT [PK_CategorySet]
    PRIMARY KEY ([CategoryId] );
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ShopId] in table 'ProductSet'
ALTER TABLE [ProductSet]
ADD CONSTRAINT [FK_ShopProduct]
    FOREIGN KEY ([ShopId])
    REFERENCES [ShopSet]
        ([ShopId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShopProduct'
CREATE INDEX [IX_FK_ShopProduct]
ON [ProductSet]
    ([ShopId]);
GO

-- Creating foreign key on [ShopId] in table 'CategorySet'
ALTER TABLE [CategorySet]
ADD CONSTRAINT [FK_ShopCategory]
    FOREIGN KEY ([ShopId])
    REFERENCES [ShopSet]
        ([ShopId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShopCategory'
CREATE INDEX [IX_FK_ShopCategory]
ON [CategorySet]
    ([ShopId]);
GO

-- Creating foreign key on [CategoryId] in table 'ProductSet'
ALTER TABLE [ProductSet]
ADD CONSTRAINT [FK_ProductCategory]
    FOREIGN KEY ([CategoryId])
    REFERENCES [CategorySet]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCategory'
CREATE INDEX [IX_FK_ProductCategory]
ON [ProductSet]
    ([CategoryId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------