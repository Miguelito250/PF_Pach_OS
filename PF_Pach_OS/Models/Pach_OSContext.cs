using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PF_Pach_OS.Models
{
    public partial class Pach_OSContext : IdentityDbContext
    {
        public Pach_OSContext()
        {
        }

        public Pach_OSContext(DbContextOptions<Pach_OSContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<DetalleVenta> DetalleVentas { get; set; } = null!;
        public virtual DbSet<DetallesCompra> DetallesCompras { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Insumo> Insumos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Proveedore> Proveedores { get; set; } = null!;
        public virtual DbSet<Receta> Recetas { get; set; } = null!;
        public virtual DbSet<Tamano> Tamanos { get; set; } = null!;
        public virtual DbSet<Venta> Ventas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=Pach_OS;integrated security=True; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Name")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("NormalizedName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasDatabaseName("RoleNameIndex")
                    .HasFilter("[NormalizedName] IS NOT NULL");

                b.ToTable("AspNetRoles", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<string>("ClaimType")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ClaimValue")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("RoleId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("AspNetRoleClaims", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<int>("AccessFailedCount")
                    .HasColumnType("int");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Email")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<bool>("EmailConfirmed")
                    .HasColumnType("bit");

                b.Property<bool>("LockoutEnabled")
                    .HasColumnType("bit");

                b.Property<DateTimeOffset?>("LockoutEnd")
                    .HasColumnType("datetimeoffset");

                b.Property<string>("NormalizedEmail")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("NormalizedUserName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("PasswordHash")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("PhoneNumber")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("PhoneNumberConfirmed")
                    .HasColumnType("bit");

                b.Property<string>("SecurityStamp")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("TwoFactorEnabled")
                    .HasColumnType("bit");

                b.Property<string>("UserName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasDatabaseName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasDatabaseName("UserNameIndex")
                    .HasFilter("[NormalizedUserName] IS NOT NULL");

                b.ToTable("AspNetUsers", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<string>("ClaimType")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ClaimValue")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserClaims", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.Property<string>("LoginProvider")
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("ProviderKey")
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("ProviderDisplayName")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserLogins", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("RoleId")
                    .HasColumnType("nvarchar(450)");

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.ToTable("AspNetUserRoles", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("LoginProvider")
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("Name")
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("Value")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens", (string)null);
            });
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK_roles");

                entity.ToTable("Rol");

                entity.Property(e => e.NomRol)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombre_Rol");
            });
            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasKey(e => e.IdPermiso)
                    .HasName("PK_permisos");

                entity.ToTable("Rol");

                entity.Property(e => e.NomPermiso)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombre_Premiso");
            });
            modelBuilder.Entity<Permiso_Rol>(entity =>
            {
               

                entity.ToTable("roles_permisos");

                entity.Property(e => e.IdRol).HasColumnName("FK_roles_permisos_roles_id_rol");
                entity.Property(e => e.Id_Permiso).HasColumnName("FK_roles_permisos_permisos_id_permiso");
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
                    .HasConstraintName("FK__detalleVe__id_ve__5070F446")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DetallesCompra>(entity =>
            {
                entity.HasKey(e => e.IdDetallesCompra)
                    .HasName("PK__detalles__905DB0ED175A41C9");

                entity.ToTable("detalles_Compras");

                entity.Property(e => e.IdDetallesCompra).HasColumnName("id_detalles_compra");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdCompra).HasColumnName("id_compra");

                entity.Property(e => e.IdInsumo).HasColumnName("id_insumo");

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

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__producto__FF341C0DAD2F40EB");

                entity.ToTable("productos");

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
