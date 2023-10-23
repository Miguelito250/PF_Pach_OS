using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PF_Pach_OS.Models
{
    public partial class Pach_OSContext : DbContext
    {
        public Pach_OSContext()
        {
        }

        public Pach_OSContext(DbContextOptions<Pach_OSContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<DetalleVenta> DetalleVentas { get; set; } = null!;
        public virtual DbSet<DetallesCompra> DetallesCompras { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Insumo> Insumos { get; set; } = null!;
        public virtual DbSet<Permiso> Permisos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Proveedore> Proveedores { get; set; } = null!;
        public virtual DbSet<SaborSeleccionado> SaboresSeleccionados { get; set; } = null!;
        public virtual DbSet<Receta> Recetas { get; set; } = null!;
        public virtual DbSet<RolPermiso> RolPermisos { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Tamano> Tamanos { get; set; } = null!;
        public virtual DbSet<Venta> Ventas { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=Pach_OS;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Discriminator).HasDefaultValueSql("(N'')");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.IdRol).HasColumnName("Id_rol");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_rol_usuario");
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__categori__CD54BC5AB649B0E9");

                entity.ToTable("categorias");

                entity.Property(e => e.IdCategoria)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_categoria");

                entity.Property(e => e.NomCategoria)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nom_categoria");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra)
                    .HasName("PK__compras__C4BAA604A7DD9276");

                entity.ToTable("compras");

                entity.HasIndex(e => e.IdProveedor, "IX_compras_id_proveedor");

                entity.Property(e => e.IdCompra).HasColumnName("id_compra");

                entity.Property(e => e.FechaCompra)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_compra")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK__compras__id_prov__4CA06362");
            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta)
                    .HasName("PK__detalleV__3C2E445CAC618977");

                entity.ToTable("detalleVentas");

                entity.HasIndex(e => e.IdProducto, "IX_detalleVentas_id_producto");

                entity.HasIndex(e => e.IdVenta, "IX_detalleVentas_id_venta");

                entity.Property(e => e.IdDetalleVenta).HasColumnName("id_detalleVenta");

                entity.Property(e => e.CantVendida).HasColumnName("cant_vendida");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.IdVenta).HasColumnName("id_venta");

                entity.Property(e => e.Precio).HasColumnName("precio");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__detalleVe__id_pr__4F7CD00D");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__detalleVe__id_ve__5070F446");
            });

            modelBuilder.Entity<DetallesCompra>(entity =>
            {
                entity.HasKey(e => e.IdDetallesCompra)
                    .HasName("PK__detalles__905DB0ED175A41C9");

                entity.ToTable("detalles_Compras");

                entity.HasIndex(e => e.IdCompra, "IX_detalles_Compras_id_compra");

                entity.HasIndex(e => e.IdInsumo, "IX_detalles_Compras_id_insumo");

                entity.Property(e => e.IdDetallesCompra).HasColumnName("id_detalles_compra");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdCompra).HasColumnName("id_compra");

                entity.Property(e => e.IdInsumo).HasColumnName("id_insumo");

                entity.Property(e => e.Medida).HasDefaultValueSql("(N'')");

                entity.Property(e => e.PrecioInsumo).HasColumnName("precio_insumo");

                entity.HasOne(d => d.IdCompraNavigation)
                    .WithMany(p => p.DetallesCompras)
                    .HasForeignKey(d => d.IdCompra)
                    .HasConstraintName("FK__detalles___id_co__4D94879B");

                entity.HasOne(d => d.IdInsumoNavigation)
                    .WithMany(p => p.DetallesCompras)
                    .HasForeignKey(d => d.IdInsumo)
                    .HasConstraintName("FK__detalles___id_in__4E88ABD4");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PK__empleado__88B513941CE0EF7C");

                entity.ToTable("empleados");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Celular)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("celular");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contrasena");

                entity.Property(e => e.Correo)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NumDocumento)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("num_documento");

                entity.Property(e => e.TipoDocumento)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tipo_documento");
            });

            modelBuilder.Entity<Insumo>(entity =>
            {
                entity.HasKey(e => e.IdInsumo)
                    .HasName("PK__insumos__D4F202B1AE226FBC");

                entity.ToTable("insumos");

                entity.Property(e => e.IdInsumo).HasColumnName("id_insumo");

                entity.Property(e => e.CantInsumo).HasColumnName("cant_insumo");

                entity.Property(e => e.Medida)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("medida");

                entity.Property(e => e.NomInsumo)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nom_insumo");
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasKey(e => e.IdPermiso)
                    .HasName("PK__permisos__A5D405E8B6F5126A");

                entity.ToTable("permisos");

                entity.Property(e => e.IdPermiso).HasColumnName("Id_permiso");

                entity.Property(e => e.NomPermiso)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nom_permiso");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__producto__FF341C0DAD2F40EB");

                entity.ToTable("productos");

                entity.HasIndex(e => e.IdCategoria, "IX_productos_id_categoria");

                entity.HasIndex(e => e.IdTamano, "IX_productos_id_tamano");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");

                entity.Property(e => e.IdTamano).HasColumnName("id_tamano");

                entity.Property(e => e.NomProducto)
                    .HasMaxLength(255)
                    .HasColumnName("nom_producto");

                entity.Property(e => e.PrecioVenta).HasColumnName("precio_venta");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__productos__id_ca__5165187F");

                entity.HasOne(d => d.IdTamanoNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdTamano)
                    .HasConstraintName("FK__productos__id_ta__52593CB8");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("PK__proveedo__8D3DFE288859DED1");

                entity.ToTable("proveedores");

                entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

                entity.Property(e => e.Nit)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nit");

                entity.Property(e => e.NomLocal)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nom_local");
            });

            modelBuilder.Entity<Receta>(entity =>
            {
                entity.HasKey(e => e.IdReceta)
                    .HasName("PK__recetas__11DB53ABAE38FAD1");

                entity.ToTable("recetas");

                entity.HasIndex(e => e.IdInsumo, "IX_recetas_id_insumo");

                entity.HasIndex(e => e.IdProducto, "IX_recetas_id_producto");

                entity.Property(e => e.IdReceta).HasColumnName("id_receta");

                entity.Property(e => e.CantInsumo).HasColumnName("cant_insumo");

                entity.Property(e => e.IdInsumo).HasColumnName("id_insumo");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.HasOne(d => d.IdInsumoNavigation)
                    .WithMany(p => p.Receta)
                    .HasForeignKey(d => d.IdInsumo)
                    .HasConstraintName("FK__recetas__id_insu__534D60F1");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Receta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__recetas__id_prod__5441852A");
            });


            modelBuilder.Entity<RolPermiso>(entity =>
            {
                entity.HasKey(e => e.IdRolPermisos)
                    .HasName("PK__Rol_perm__C48C7EB25F860379");

                entity.ToTable("Rol_permisos");

                entity.Property(e => e.IdRolPermisos).HasColumnName("Id_rol_permisos");

                entity.Property(e => e.IdPermiso).HasColumnName("Id_permiso");

                entity.Property(e => e.IdRol).HasColumnName("Id_rol");

                entity.HasOne(d => d.IdPermisoNavigation)
                    .WithMany(p => p.RolPermisos)
                    .HasForeignKey(d => d.IdPermiso)
                    .HasConstraintName("FK__Rol_permi__Id_pe__40F9A68C");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolPermisos)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__Rol_permi__Id_ro__40058253");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__roles__2D95A8946F1A4A66");

                entity.ToTable("roles");

                entity.Property(e => e.IdRol).HasColumnName("Id_rol");

                entity.Property(e => e.NomRol)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nom_rol");
            });

            modelBuilder.Entity<SaborSeleccionado>(entity =>
            {
                entity.HasKey(e => e.IdSaborSeleccionado);

                entity.HasOne(d => d.IdDetalleVentaNavigation)
                    .WithMany(p => p.SaboresSeleccionados)
                    .HasForeignKey(d => d.IdDetalleVenta)
                    .HasConstraintName("FK_SaboresSeleccionados_detalleVentas")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.SaboresSeleccionados)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_SaboresSeleccionados_productos"); 
            });



            modelBuilder.Entity<Tamano>(entity =>
            {
                entity.HasKey(e => e.IdTamano)
                    .HasName("PK__tamanos__073FB91C364ED960");

                entity.ToTable("tamanos");

                entity.Property(e => e.IdTamano)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_tamano");

                entity.Property(e => e.MaximoSabores).HasColumnName("maximo_sabores");

                entity.Property(e => e.NombreTamano)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombre_tamano");

                entity.Property(e => e.Tamano1).HasColumnName("tamano");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__ventas__459533BF24E19040");

                entity.ToTable("ventas");

                entity.HasIndex(e => e.IdEmpleado, "IX_ventas_id_empleado");

                entity.Property(e => e.IdVenta).HasColumnName("id_venta");

                entity.Property(e => e.Estado)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaVenta)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_venta")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.Property(e => e.Pago).HasColumnName("pago");

                entity.Property(e => e.PagoDomicilio).HasColumnName("pago_domicilio");

                entity.Property(e => e.TipoPago)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("tipo_pago");

                entity.Property(e => e.TotalVenta).HasColumnName("total_venta");

                entity.Property(e => e.Mesa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("mesa");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK__ventas__id_emple__5535A963");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}