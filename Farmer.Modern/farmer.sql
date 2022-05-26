IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220430173433_Init', N'6.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220502173235_UpdateAplicationUser', N'6.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] ADD [Address] nvarchar(max) NULL;
GO

ALTER TABLE [AspNetUsers] ADD [Discriminator] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [AspNetUsers] ADD [LastName] nvarchar(max) NULL;
GO

ALTER TABLE [AspNetUsers] ADD [Name] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220503061559_Users', N'6.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Garden] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Code] nvarchar(max) NULL,
    [Size] int NOT NULL,
    [Cultivation] nvarchar(max) NULL,
    [Gps] nvarchar(max) NULL,
    [CreatorUserId] bigint NOT NULL,
    [CreationDateTime] datetime2 NOT NULL,
    [Description] nvarchar(max) NULL,
    [IsActive] bit NULL,
    CONSTRAINT [PK_Garden] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220511190638_Garden', N'6.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Category] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220512182052_Category', N'6.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Product] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220512183813_Product', N'6.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [WaterMotor] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_WaterMotor] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220512190727_water', N'6.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Experiment] (
    [Id] bigint NOT NULL IDENTITY,
    [GardenId] bigint NULL,
    [WaterMotorId] bigint NULL,
    [Result] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Experiment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Experiment_Garden_GardenId] FOREIGN KEY ([GardenId]) REFERENCES [Garden] ([Id]),
    CONSTRAINT [FK_Experiment_WaterMotor_WaterMotorId] FOREIGN KEY ([WaterMotorId]) REFERENCES [WaterMotor] ([Id])
);
GO

CREATE INDEX [IX_Experiment_GardenId] ON [Experiment] ([GardenId]);
GO

CREATE INDEX [IX_Experiment_WaterMotorId] ON [Experiment] ([WaterMotorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220513063703_Experiment', N'6.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Experiment] DROP CONSTRAINT [FK_Experiment_Garden_GardenId];
GO

ALTER TABLE [Experiment] DROP CONSTRAINT [FK_Experiment_WaterMotor_WaterMotorId];
GO

DROP INDEX [IX_Experiment_WaterMotorId] ON [Experiment];
DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Experiment]') AND [c].[name] = N'WaterMotorId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Experiment] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Experiment] ALTER COLUMN [WaterMotorId] bigint NOT NULL;
ALTER TABLE [Experiment] ADD DEFAULT CAST(0 AS bigint) FOR [WaterMotorId];
CREATE INDEX [IX_Experiment_WaterMotorId] ON [Experiment] ([WaterMotorId]);
GO

DROP INDEX [IX_Experiment_GardenId] ON [Experiment];
DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Experiment]') AND [c].[name] = N'GardenId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Experiment] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Experiment] ALTER COLUMN [GardenId] bigint NOT NULL;
ALTER TABLE [Experiment] ADD DEFAULT CAST(0 AS bigint) FOR [GardenId];
CREATE INDEX [IX_Experiment_GardenId] ON [Experiment] ([GardenId]);
GO

CREATE TABLE [Harvest] (
    [Id] bigint NOT NULL IDENTITY,
    [ProductId] bigint NULL,
    [GardenId] bigint NULL,
    [Size] int NOT NULL,
    [HarvestDate] datetime2 NOT NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Harvest] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Harvest_Garden_GardenId] FOREIGN KEY ([GardenId]) REFERENCES [Garden] ([Id]),
    CONSTRAINT [FK_Harvest_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id])
);
GO

CREATE INDEX [IX_Harvest_GardenId] ON [Harvest] ([GardenId]);
GO

CREATE INDEX [IX_Harvest_ProductId] ON [Harvest] ([ProductId]);
GO

ALTER TABLE [Experiment] ADD CONSTRAINT [FK_Experiment_Garden_GardenId] FOREIGN KEY ([GardenId]) REFERENCES [Garden] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Experiment] ADD CONSTRAINT [FK_Experiment_WaterMotor_WaterMotorId] FOREIGN KEY ([WaterMotorId]) REFERENCES [WaterMotor] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220513130901_Harvest', N'6.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Harvest] DROP CONSTRAINT [FK_Harvest_Garden_GardenId];
GO

ALTER TABLE [Harvest] DROP CONSTRAINT [FK_Harvest_Product_ProductId];
GO

DROP INDEX [IX_Harvest_ProductId] ON [Harvest];
DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Harvest]') AND [c].[name] = N'ProductId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Harvest] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Harvest] ALTER COLUMN [ProductId] bigint NOT NULL;
ALTER TABLE [Harvest] ADD DEFAULT CAST(0 AS bigint) FOR [ProductId];
CREATE INDEX [IX_Harvest_ProductId] ON [Harvest] ([ProductId]);
GO

DROP INDEX [IX_Harvest_GardenId] ON [Harvest];
DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Harvest]') AND [c].[name] = N'GardenId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Harvest] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Harvest] ALTER COLUMN [GardenId] bigint NOT NULL;
ALTER TABLE [Harvest] ADD DEFAULT CAST(0 AS bigint) FOR [GardenId];
CREATE INDEX [IX_Harvest_GardenId] ON [Harvest] ([GardenId]);
GO

CREATE TABLE [Work] (
    [Id] bigint NOT NULL IDENTITY,
    [GardenId] bigint NOT NULL,
    [ProductId] bigint NOT NULL,
    [CategoryId] bigint NOT NULL,
    [WaterMotorId] bigint NULL,
    [ActionDatetime] datetime2 NOT NULL,
    [Size] int NULL,
    [Type] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreatorUserId] uniqueidentifier NULL,
    [Agent] uniqueidentifier NULL,
    [EndActionDateTime] datetime2 NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Work] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Work_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Work_Garden_GardenId] FOREIGN KEY ([GardenId]) REFERENCES [Garden] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Work_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Work_WaterMotor_WaterMotorId] FOREIGN KEY ([WaterMotorId]) REFERENCES [WaterMotor] ([Id])
);
GO

CREATE INDEX [IX_Work_CategoryId] ON [Work] ([CategoryId]);
GO

CREATE INDEX [IX_Work_GardenId] ON [Work] ([GardenId]);
GO

CREATE INDEX [IX_Work_ProductId] ON [Work] ([ProductId]);
GO

CREATE INDEX [IX_Work_WaterMotorId] ON [Work] ([WaterMotorId]);
GO

ALTER TABLE [Harvest] ADD CONSTRAINT [FK_Harvest_Garden_GardenId] FOREIGN KEY ([GardenId]) REFERENCES [Garden] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Harvest] ADD CONSTRAINT [FK_Harvest_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220513155918_work', N'6.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Work].[Agent]', N'AgentId', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220517192021_UpdateAgentId', N'6.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Work] ADD [CreationDatetime] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220521171014_AddCreationDatetime', N'6.0.4');
GO

COMMIT;
GO

