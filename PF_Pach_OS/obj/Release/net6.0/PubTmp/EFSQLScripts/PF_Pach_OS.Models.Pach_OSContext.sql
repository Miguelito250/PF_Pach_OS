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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
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
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [categorias] (
        [id_categoria] tinyint NOT NULL IDENTITY,
        [nom_categoria] varchar(30) NOT NULL,
        CONSTRAINT [PK__categori__CD54BC5AC013A8AC] PRIMARY KEY ([id_categoria])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [empleados] (
        [id_empleado] int NOT NULL IDENTITY,
        [tipo_documento] varchar(25) NULL,
        [num_documento] varchar(25) NULL,
        [nombre] varchar(30) NULL,
        [apellido] varchar(30) NULL,
        [contrasena] varchar(50) NULL,
        [celular] varchar(30) NULL,
        [correo] varchar(30) NULL,
        [estado] varchar(20) NULL,
        CONSTRAINT [PK__empleado__88B5139483CC8B74] PRIMARY KEY ([id_empleado])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [insumos] (
        [id_insumo] int NOT NULL IDENTITY,
        [nom_insumo] varchar(30) NULL,
        [cant_insumo] int NULL,
        [medida] varchar(50) NULL,
        CONSTRAINT [PK__insumos__D4F202B1297E3169] PRIMARY KEY ([id_insumo])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [proveedores] (
        [id_proveedor] int NOT NULL IDENTITY,
        [nit] varchar(15) NULL,
        [nom_local] varchar(30) NULL,
        [direccion] varchar(30) NULL,
        [telefono] varchar(30) NULL,
        [correo] varchar(30) NULL,
        [Estado] int NULL,
        CONSTRAINT [PK__proveedo__8D3DFE281562B3BA] PRIMARY KEY ([id_proveedor])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [tamanos] (
        [id_tamano] tinyint NOT NULL IDENTITY,
        [nombre_tamano] varchar(30) NULL,
        [tamano] tinyint NULL,
        [maximo_sabores] tinyint NULL,
        CONSTRAINT [PK__tamanos__073FB91C101651A2] PRIMARY KEY ([id_tamano])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [ventas] (
        [id_venta] int NOT NULL IDENTITY,
        [fecha_venta] datetime NULL DEFAULT ((getdate())),
        [total_venta] int NULL,
        [tipo_pago] varchar(40) NULL,
        [pago] int NULL,
        [pago_domicilio] int NULL,
        [estado] varchar(30) NULL,
        [id_empleado] int NULL,
        CONSTRAINT [PK__ventas__459533BF1E99A8D0] PRIMARY KEY ([id_venta]),
        CONSTRAINT [FK__ventas__id_emple__02084FDA] FOREIGN KEY ([id_empleado]) REFERENCES [empleados] ([id_empleado])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [compras] (
        [id_compra] int NOT NULL IDENTITY,
        [fecha_compra] datetime NULL DEFAULT ((getdate())),
        [total] int NULL,
        [id_empleado] int NULL,
        [id_proveedor] int NULL,
        CONSTRAINT [PK__compras__C4BAA6043CD63BFB] PRIMARY KEY ([id_compra]),
        CONSTRAINT [FK__compras__id_prov__6D0D32F4] FOREIGN KEY ([id_proveedor]) REFERENCES [proveedores] ([id_proveedor])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [productos] (
        [id_producto] int NOT NULL IDENTITY,
        [nom_producto] nvarchar(255) NULL,
        [precio_venta] int NULL,
        [estado] tinyint NULL,
        [id_tamano] tinyint NULL,
        [id_categoria] tinyint NULL,
        CONSTRAINT [PK__producto__FF341C0DF0AEECF5] PRIMARY KEY ([id_producto]),
        CONSTRAINT [FK__productos__id_ca__7A672E12] FOREIGN KEY ([id_categoria]) REFERENCES [categorias] ([id_categoria]),
        CONSTRAINT [FK__productos__id_ta__797309D9] FOREIGN KEY ([id_tamano]) REFERENCES [tamanos] ([id_tamano])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [detalles_Compras] (
        [id_detalles_compra] int NOT NULL IDENTITY,
        [precio_insumo] int NULL,
        [cantidad] int NULL,
        [medida] varchar(20) NULL,
        [id_compra] int NULL,
        [id_insumo] int NULL,
        CONSTRAINT [PK__detalles__905DB0ED54001F5C] PRIMARY KEY ([id_detalles_compra]),
        CONSTRAINT [FK__detalles___id_co__72C60C4A] FOREIGN KEY ([id_compra]) REFERENCES [compras] ([id_compra]),
        CONSTRAINT [FK__detalles___id_in__71D1E811] FOREIGN KEY ([id_insumo]) REFERENCES [insumos] ([id_insumo])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [detalleVentas] (
        [id_detalleVenta] int NOT NULL IDENTITY,
        [cant_vendida] int NULL,
        [precio] int NULL,
        [id_venta] int NULL,
        [id_producto] int NULL,
        CONSTRAINT [PK__detalleV__3C2E445C48FCB97E] PRIMARY KEY ([id_detalleVenta]),
        CONSTRAINT [FK__detalleVe__id_pr__04E4BC85] FOREIGN KEY ([id_producto]) REFERENCES [productos] ([id_producto]),
        CONSTRAINT [FK__detalleVe__id_ve__05D8E0BE] FOREIGN KEY ([id_venta]) REFERENCES [ventas] ([id_venta])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE TABLE [recetas] (
        [id_receta] int NOT NULL IDENTITY,
        [cant_insumo] int NULL,
        [id_producto] int NULL,
        [id_insumo] int NULL,
        CONSTRAINT [PK__recetas__11DB53AB771D52E7] PRIMARY KEY ([id_receta]),
        CONSTRAINT [FK__recetas__id_insu__7E37BEF6] FOREIGN KEY ([id_insumo]) REFERENCES [insumos] ([id_insumo]),
        CONSTRAINT [FK__recetas__id_prod__7D439ABD] FOREIGN KEY ([id_producto]) REFERENCES [productos] ([id_producto])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE INDEX [IX_compras_id_proveedor] ON [compras] ([id_proveedor]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE INDEX [IX_detalles_Compras_id_compra] ON [detalles_Compras] ([id_compra]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE INDEX [IX_detalles_Compras_id_insumo] ON [detalles_Compras] ([id_insumo]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE INDEX [IX_detalleVentas_id_producto] ON [detalleVentas] ([id_producto]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE INDEX [IX_detalleVentas_id_venta] ON [detalleVentas] ([id_venta]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE INDEX [IX_productos_id_categoria] ON [productos] ([id_categoria]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE INDEX [IX_productos_id_tamano] ON [productos] ([id_tamano]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE INDEX [IX_recetas_id_insumo] ON [recetas] ([id_insumo]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE INDEX [IX_recetas_id_producto] ON [recetas] ([id_producto]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    CREATE INDEX [IX_ventas_id_empleado] ON [ventas] ([id_empleado]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230907000312_Identity')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230907000312_Identity', N'6.0.23');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230912235806_PersonalizacionIdentityUsuarios')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Discriminator] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230912235806_PersonalizacionIdentityUsuarios')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [DocumentNumber] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230912235806_PersonalizacionIdentityUsuarios')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [DocumentType] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230912235806_PersonalizacionIdentityUsuarios')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [EntryDay] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230912235806_PersonalizacionIdentityUsuarios')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [FirstName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230912235806_PersonalizacionIdentityUsuarios')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [LastName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230912235806_PersonalizacionIdentityUsuarios')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [State] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230912235806_PersonalizacionIdentityUsuarios')
BEGIN
    ALTER TABLE [AspNetRoleClaims] ADD CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230912235806_PersonalizacionIdentityUsuarios')
BEGIN
    ALTER TABLE [AspNetUserClaims] ADD CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230912235806_PersonalizacionIdentityUsuarios')
BEGIN
    ALTER TABLE [AspNetUserLogins] ADD CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230912235806_PersonalizacionIdentityUsuarios')
BEGIN
    ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230912235806_PersonalizacionIdentityUsuarios')
BEGIN
    ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230912235806_PersonalizacionIdentityUsuarios')
BEGIN
    ALTER TABLE [AspNetUserTokens] ADD CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230912235806_PersonalizacionIdentityUsuarios')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230912235806_PersonalizacionIdentityUsuarios', N'6.0.23');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [AspNetUserTokens] DROP CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [compras] DROP CONSTRAINT [FK__compras__id_prov__6D0D32F4];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [detalles_Compras] DROP CONSTRAINT [FK__detalles___id_co__72C60C4A];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [detalles_Compras] DROP CONSTRAINT [FK__detalles___id_in__71D1E811];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [detalleVentas] DROP CONSTRAINT [FK__detalleVe__id_pr__04E4BC85];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [detalleVentas] DROP CONSTRAINT [FK__detalleVe__id_ve__05D8E0BE];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [productos] DROP CONSTRAINT [FK__productos__id_ca__7A672E12];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [productos] DROP CONSTRAINT [FK__productos__id_ta__797309D9];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [recetas] DROP CONSTRAINT [FK__recetas__id_insu__7E37BEF6];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [recetas] DROP CONSTRAINT [FK__recetas__id_prod__7D439ABD];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [ventas] DROP CONSTRAINT [FK__ventas__id_emple__02084FDA];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [ventas] DROP CONSTRAINT [PK__ventas__459533BF1E99A8D0];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [tamanos] DROP CONSTRAINT [PK__tamanos__073FB91C101651A2];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [recetas] DROP CONSTRAINT [PK__recetas__11DB53AB771D52E7];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [proveedores] DROP CONSTRAINT [PK__proveedo__8D3DFE281562B3BA];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [productos] DROP CONSTRAINT [PK__producto__FF341C0DF0AEECF5];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [insumos] DROP CONSTRAINT [PK__insumos__D4F202B1297E3169];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [empleados] DROP CONSTRAINT [PK__empleado__88B5139483CC8B74];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [detalleVentas] DROP CONSTRAINT [PK__detalleV__3C2E445C48FCB97E];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [detalles_Compras] DROP CONSTRAINT [PK__detalles__905DB0ED54001F5C];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [compras] DROP CONSTRAINT [PK__compras__C4BAA6043CD63BFB];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [categorias] DROP CONSTRAINT [PK__categori__CD54BC5AC013A8AC];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    EXEC sp_rename N'[proveedores].[telefono]', N'Telefono', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    EXEC sp_rename N'[proveedores].[direccion]', N'Direccion', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    EXEC sp_rename N'[proveedores].[correo]', N'Correo', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    EXEC sp_rename N'[detalles_Compras].[medida]', N'Medida', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[proveedores]') AND [c].[name] = N'Telefono');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [proveedores] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [proveedores] ALTER COLUMN [Telefono] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[proveedores]') AND [c].[name] = N'Direccion');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [proveedores] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [proveedores] ALTER COLUMN [Direccion] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[proveedores]') AND [c].[name] = N'Correo');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [proveedores] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [proveedores] ALTER COLUMN [Correo] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[empleados]') AND [c].[name] = N'estado');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [empleados] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [empleados] ALTER COLUMN [estado] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[detalles_Compras]') AND [c].[name] = N'precio_insumo');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [detalles_Compras] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [detalles_Compras] ALTER COLUMN [precio_insumo] int NOT NULL;
    ALTER TABLE [detalles_Compras] ADD DEFAULT 0 FOR [precio_insumo];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[detalles_Compras]') AND [c].[name] = N'Medida');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [detalles_Compras] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [detalles_Compras] ALTER COLUMN [Medida] nvarchar(max) NOT NULL;
    ALTER TABLE [detalles_Compras] ADD DEFAULT N'' FOR [Medida];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[detalles_Compras]') AND [c].[name] = N'cantidad');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [detalles_Compras] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [detalles_Compras] ALTER COLUMN [cantidad] int NOT NULL;
    ALTER TABLE [detalles_Compras] ADD DEFAULT 0 FOR [cantidad];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [ventas] ADD CONSTRAINT [PK__ventas__459533BF24E19040] PRIMARY KEY ([id_venta]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [tamanos] ADD CONSTRAINT [PK__tamanos__073FB91C364ED960] PRIMARY KEY ([id_tamano]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [recetas] ADD CONSTRAINT [PK__recetas__11DB53ABAE38FAD1] PRIMARY KEY ([id_receta]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [proveedores] ADD CONSTRAINT [PK__proveedo__8D3DFE288859DED1] PRIMARY KEY ([id_proveedor]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [productos] ADD CONSTRAINT [PK__producto__FF341C0DAD2F40EB] PRIMARY KEY ([id_producto]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [insumos] ADD CONSTRAINT [PK__insumos__D4F202B1AE226FBC] PRIMARY KEY ([id_insumo]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [empleados] ADD CONSTRAINT [PK__empleado__88B513941CE0EF7C] PRIMARY KEY ([id_empleado]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [detalleVentas] ADD CONSTRAINT [PK__detalleV__3C2E445CAC618977] PRIMARY KEY ([id_detalleVenta]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [detalles_Compras] ADD CONSTRAINT [PK__detalles__905DB0ED175A41C9] PRIMARY KEY ([id_detalles_compra]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [compras] ADD CONSTRAINT [PK__compras__C4BAA604A7DD9276] PRIMARY KEY ([id_compra]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [categorias] ADD CONSTRAINT [PK__categori__CD54BC5AB649B0E9] PRIMARY KEY ([id_categoria]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [compras] ADD CONSTRAINT [FK__compras__id_prov__4CA06362] FOREIGN KEY ([id_proveedor]) REFERENCES [proveedores] ([id_proveedor]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [detalles_Compras] ADD CONSTRAINT [FK__detalles___id_co__4D94879B] FOREIGN KEY ([id_compra]) REFERENCES [compras] ([id_compra]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [detalles_Compras] ADD CONSTRAINT [FK__detalles___id_in__4E88ABD4] FOREIGN KEY ([id_insumo]) REFERENCES [insumos] ([id_insumo]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [detalleVentas] ADD CONSTRAINT [FK__detalleVe__id_pr__4F7CD00D] FOREIGN KEY ([id_producto]) REFERENCES [productos] ([id_producto]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [detalleVentas] ADD CONSTRAINT [FK__detalleVe__id_ve__5070F446] FOREIGN KEY ([id_venta]) REFERENCES [ventas] ([id_venta]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [productos] ADD CONSTRAINT [FK__productos__id_ca__5165187F] FOREIGN KEY ([id_categoria]) REFERENCES [categorias] ([id_categoria]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [productos] ADD CONSTRAINT [FK__productos__id_ta__52593CB8] FOREIGN KEY ([id_tamano]) REFERENCES [tamanos] ([id_tamano]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [recetas] ADD CONSTRAINT [FK__recetas__id_insu__534D60F1] FOREIGN KEY ([id_insumo]) REFERENCES [insumos] ([id_insumo]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [recetas] ADD CONSTRAINT [FK__recetas__id_prod__5441852A] FOREIGN KEY ([id_producto]) REFERENCES [productos] ([id_producto]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    ALTER TABLE [ventas] ADD CONSTRAINT [FK__ventas__id_emple__5535A963] FOREIGN KEY ([id_empleado]) REFERENCES [empleados] ([id_empleado]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230924222345_EliminarCascada_Ventas')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230924222345_EliminarCascada_Ventas', N'6.0.23');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231010034750_Campos_Tokens_Contraseña_Correo')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [EmailConfirmationToken] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231010034750_Campos_Tokens_Contraseña_Correo')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [PasswordResetToken] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231010034750_Campos_Tokens_Contraseña_Correo')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231010034750_Campos_Tokens_Contraseña_Correo', N'6.0.23');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    CREATE TABLE [SaboresSeleccionados] (
        [IdSaborSeleccionado] int NOT NULL IDENTITY,
        [IdProducto] int NULL,
        [IdDetalleVenta] int NULL,
        CONSTRAINT [PK_SaboresSeleccionados] PRIMARY KEY ([IdSaborSeleccionado]),
        CONSTRAINT [FK_SaboresSeleccionados_detalleVentas] FOREIGN KEY ([IdDetalleVenta]) REFERENCES [detalleVentas] ([id_detalleVenta]) ON DELETE CASCADE,
        CONSTRAINT [FK_SaboresSeleccionados_productos] FOREIGN KEY ([IdProducto]) REFERENCES [productos] ([id_producto])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    ALTER TABLE [compras] ADD [NumeroFactura] nvarchar(30) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    ALTER TABLE [insumos] ADD [Estado] tinyint NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    ALTER TABLE [proveedores] ADD [TipoDocumento] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    ALTER TABLE [ventas] ADD [mesa] varchar(20) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    ALTER TABLE [detalleVentas] ADD [estado] varchar(20) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_categoria') AND [object_id] = OBJECT_ID(N'[categorias]'))
        SET IDENTITY_INSERT [categorias] ON;
    EXEC(N'INSERT INTO [categorias] ([nom_categoria])
    VALUES (''Pizza'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_categoria') AND [object_id] = OBJECT_ID(N'[categorias]'))
        SET IDENTITY_INSERT [categorias] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_categoria') AND [object_id] = OBJECT_ID(N'[categorias]'))
        SET IDENTITY_INSERT [categorias] ON;
    EXEC(N'INSERT INTO [categorias] ([nom_categoria])
    VALUES (''Gratinados'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_categoria') AND [object_id] = OBJECT_ID(N'[categorias]'))
        SET IDENTITY_INSERT [categorias] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_categoria') AND [object_id] = OBJECT_ID(N'[categorias]'))
        SET IDENTITY_INSERT [categorias] ON;
    EXEC(N'INSERT INTO [categorias] ([nom_categoria])
    VALUES (''Pastas'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_categoria') AND [object_id] = OBJECT_ID(N'[categorias]'))
        SET IDENTITY_INSERT [categorias] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_categoria') AND [object_id] = OBJECT_ID(N'[categorias]'))
        SET IDENTITY_INSERT [categorias] ON;
    EXEC(N'INSERT INTO [categorias] ([nom_categoria])
    VALUES (''Bebidas'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_categoria') AND [object_id] = OBJECT_ID(N'[categorias]'))
        SET IDENTITY_INSERT [categorias] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_categoria') AND [object_id] = OBJECT_ID(N'[categorias]'))
        SET IDENTITY_INSERT [categorias] ON;
    EXEC(N'INSERT INTO [categorias] ([nom_categoria])
    VALUES (''Bar'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_categoria') AND [object_id] = OBJECT_ID(N'[categorias]'))
        SET IDENTITY_INSERT [categorias] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nombre_tamano', N'tamano', N'maximo_sabores') AND [object_id] = OBJECT_ID(N'[tamanos]'))
        SET IDENTITY_INSERT [tamanos] ON;
    EXEC(N'INSERT INTO [tamanos] ([nombre_tamano], [tamano], [maximo_sabores])
    VALUES (''Pizza(Personal)'', CAST(15 AS tinyint), CAST(1 AS tinyint))');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nombre_tamano', N'tamano', N'maximo_sabores') AND [object_id] = OBJECT_ID(N'[tamanos]'))
        SET IDENTITY_INSERT [tamanos] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nombre_tamano', N'tamano', N'maximo_sabores') AND [object_id] = OBJECT_ID(N'[tamanos]'))
        SET IDENTITY_INSERT [tamanos] ON;
    EXEC(N'INSERT INTO [tamanos] ([nombre_tamano], [tamano], [maximo_sabores])
    VALUES (''Pizza(Pequeña)'', CAST(30 AS tinyint), CAST(2 AS tinyint))');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nombre_tamano', N'tamano', N'maximo_sabores') AND [object_id] = OBJECT_ID(N'[tamanos]'))
        SET IDENTITY_INSERT [tamanos] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nombre_tamano', N'tamano', N'maximo_sabores') AND [object_id] = OBJECT_ID(N'[tamanos]'))
        SET IDENTITY_INSERT [tamanos] ON;
    EXEC(N'INSERT INTO [tamanos] ([nombre_tamano], [tamano], [maximo_sabores])
    VALUES (''Pizza(Grande)'', CAST(40 AS tinyint), CAST(2 AS tinyint))');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nombre_tamano', N'tamano', N'maximo_sabores') AND [object_id] = OBJECT_ID(N'[tamanos]'))
        SET IDENTITY_INSERT [tamanos] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nombre_tamano', N'tamano', N'maximo_sabores') AND [object_id] = OBJECT_ID(N'[tamanos]'))
        SET IDENTITY_INSERT [tamanos] ON;
    EXEC(N'INSERT INTO [tamanos] ([nombre_tamano], [tamano], [maximo_sabores])
    VALUES (''Pizza(Familiar)'', CAST(45 AS tinyint), CAST(3 AS tinyint))');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nombre_tamano', N'tamano', N'maximo_sabores') AND [object_id] = OBJECT_ID(N'[tamanos]'))
        SET IDENTITY_INSERT [tamanos] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_producto', N'precio_venta', N'estado', N'id_tamano', N'id_categoria') AND [object_id] = OBJECT_ID(N'[productos]'))
        SET IDENTITY_INSERT [productos] ON;
    EXEC(N'INSERT INTO [productos] ([nom_producto], [precio_venta], [estado], [id_tamano], [id_categoria])
    VALUES (N''Pizza(Personal)'', 8000, CAST(1 AS tinyint), CAST(1 AS tinyint), CAST(1 AS tinyint))');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_producto', N'precio_venta', N'estado', N'id_tamano', N'id_categoria') AND [object_id] = OBJECT_ID(N'[productos]'))
        SET IDENTITY_INSERT [productos] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_producto', N'precio_venta', N'estado', N'id_tamano', N'id_categoria') AND [object_id] = OBJECT_ID(N'[productos]'))
        SET IDENTITY_INSERT [productos] ON;
    EXEC(N'INSERT INTO [productos] ([nom_producto], [precio_venta], [estado], [id_tamano], [id_categoria])
    VALUES (N''Pizza(Pequeña)'', 27000, CAST(1 AS tinyint), CAST(2 AS tinyint), CAST(1 AS tinyint))');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_producto', N'precio_venta', N'estado', N'id_tamano', N'id_categoria') AND [object_id] = OBJECT_ID(N'[productos]'))
        SET IDENTITY_INSERT [productos] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_producto', N'precio_venta', N'estado', N'id_tamano', N'id_categoria') AND [object_id] = OBJECT_ID(N'[productos]'))
        SET IDENTITY_INSERT [productos] ON;
    EXEC(N'INSERT INTO [productos] ([nom_producto], [precio_venta], [estado], [id_tamano], [id_categoria])
    VALUES (N''Pizza(Grande)'', 38000, CAST(1 AS tinyint), CAST(3 AS tinyint), CAST(1 AS tinyint))');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_producto', N'precio_venta', N'estado', N'id_tamano', N'id_categoria') AND [object_id] = OBJECT_ID(N'[productos]'))
        SET IDENTITY_INSERT [productos] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_producto', N'precio_venta', N'estado', N'id_tamano', N'id_categoria') AND [object_id] = OBJECT_ID(N'[productos]'))
        SET IDENTITY_INSERT [productos] ON;
    EXEC(N'INSERT INTO [productos] ([nom_producto], [precio_venta], [estado], [id_tamano], [id_categoria])
    VALUES (N''Pizza(Familiar)'', 50000, CAST(1 AS tinyint), CAST(4 AS tinyint), CAST(1 AS tinyint))');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'nom_producto', N'precio_venta', N'estado', N'id_tamano', N'id_categoria') AND [object_id] = OBJECT_ID(N'[productos]'))
        SET IDENTITY_INSERT [productos] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231012070441_QuemarDatos_nuevatabla')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231012070441_QuemarDatos_nuevatabla', N'6.0.23');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023194705_ValidacionesBaseDeDatos')
BEGIN
    ALTER TABLE Insumos ADD CONSTRAINT CK_Longitud CHECK (LEN(nom_insumo) >= 3 AND LEN(nom_insumo) <= 30);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023194705_ValidacionesBaseDeDatos')
BEGIN
    ALTER TABLE Insumos ALTER COLUMN Nom_insumo varchar(30) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023194705_ValidacionesBaseDeDatos')
BEGIN
    ALTER TABLE Insumos ALTER COLUMN cant_insumo int NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023194705_ValidacionesBaseDeDatos')
BEGIN
    ALTER TABLE Insumos ALTER COLUMN medida varchar(20) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023194705_ValidacionesBaseDeDatos')
BEGIN
    ALTER TABLE Insumos ALTER COLUMN Estado tinyint NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023194705_ValidacionesBaseDeDatos')
BEGIN
    ALTER TABLE detalles_Compras ADD CONSTRAINT CK_Mayor100 CHECK (precio_insumo >= 100);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023194705_ValidacionesBaseDeDatos')
BEGIN
    ALTER TABLE detalles_Compras ADD CONSTRAINT CK_Mayor0 CHECK (cantidad > 0);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023194705_ValidacionesBaseDeDatos')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231023194705_ValidacionesBaseDeDatos', N'6.0.23');
END;
GO

COMMIT;
GO

