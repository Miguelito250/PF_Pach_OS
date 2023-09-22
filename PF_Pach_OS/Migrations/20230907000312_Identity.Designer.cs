﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PF_Pach_OS.Models;

#nullable disable

namespace PF_Pach_OS.Migrations
{
    [DbContext(typeof(Pach_OSContext))]
    [Migration("20230907000312_Identity")]
    partial class Identity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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

            modelBuilder.Entity("PF_Pach_OS.Models.Categoria", b =>
                {
                    b.Property<byte>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("id_categoria");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("IdCategoria"), 1L, 1);

                    b.Property<string>("NomCategoria")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("nom_categoria");

                    b.HasKey("IdCategoria")
                        .HasName("PK__categori__CD54BC5AC013A8AC");

                    b.ToTable("categorias", (string)null);
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Compra", b =>
                {
                    b.Property<int>("IdCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_compra");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCompra"), 1L, 1);

                    b.Property<DateTime?>("FechaCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("fecha_compra")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("IdEmpleado")
                        .HasColumnType("int")
                        .HasColumnName("id_empleado");

                    b.Property<int?>("IdProveedor")
                        .HasColumnType("int")
                        .HasColumnName("id_proveedor");

                    b.Property<int?>("Total")
                        .HasColumnType("int")
                        .HasColumnName("total");

                    b.HasKey("IdCompra")
                        .HasName("PK__compras__C4BAA6043CD63BFB");

                    b.HasIndex("IdProveedor");

                    b.ToTable("compras", (string)null);
                });

            modelBuilder.Entity("PF_Pach_OS.Models.DetallesCompra", b =>
                {
                    b.Property<int>("IdDetallesCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_detalles_compra");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetallesCompra"), 1L, 1);

                    b.Property<int?>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.Property<int?>("IdCompra")
                        .HasColumnType("int")
                        .HasColumnName("id_compra");

                    b.Property<int?>("IdInsumo")
                        .HasColumnType("int")
                        .HasColumnName("id_insumo");

                    b.Property<string>("Medida")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("medida");

                    b.Property<int?>("PrecioInsumo")
                        .HasColumnType("int")
                        .HasColumnName("precio_insumo");

                    b.HasKey("IdDetallesCompra")
                        .HasName("PK__detalles__905DB0ED54001F5C");

                    b.HasIndex("IdCompra");

                    b.HasIndex("IdInsumo");

                    b.ToTable("detalles_Compras", (string)null);
                });

            modelBuilder.Entity("PF_Pach_OS.Models.DetalleVenta", b =>
                {
                    b.Property<int>("IdDetalleVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_detalleVenta");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetalleVenta"), 1L, 1);

                    b.Property<int?>("CantVendida")
                        .HasColumnType("int")
                        .HasColumnName("cant_vendida");

                    b.Property<int?>("IdProducto")
                        .HasColumnType("int")
                        .HasColumnName("id_producto");

                    b.Property<int?>("IdVenta")
                        .HasColumnType("int")
                        .HasColumnName("id_venta");

                    b.Property<int?>("Precio")
                        .HasColumnType("int")
                        .HasColumnName("precio");

                    b.HasKey("IdDetalleVenta")
                        .HasName("PK__detalleV__3C2E445C48FCB97E");

                    b.HasIndex("IdProducto");

                    b.HasIndex("IdVenta");

                    b.ToTable("detalleVentas", (string)null);
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Empleado", b =>
                {
                    b.Property<int>("IdEmpleado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_empleado");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEmpleado"), 1L, 1);

                    b.Property<string>("Apellido")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("apellido");

                    b.Property<string>("Celular")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("celular");

                    b.Property<string>("Contrasena")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("contrasena");

                    b.Property<string>("Correo")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("correo");

                    b.Property<string>("Estado")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("estado");

                    b.Property<string>("Nombre")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("nombre");

                    b.Property<string>("NumDocumento")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("num_documento");

                    b.Property<string>("TipoDocumento")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("tipo_documento");

                    b.HasKey("IdEmpleado")
                        .HasName("PK__empleado__88B5139483CC8B74");

                    b.ToTable("empleados", (string)null);
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Insumo", b =>
                {
                    b.Property<int>("IdInsumo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_insumo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdInsumo"), 1L, 1);

                    b.Property<int?>("CantInsumo")
                        .HasColumnType("int")
                        .HasColumnName("cant_insumo");

                    b.Property<string>("Medida")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("medida");

                    b.Property<string>("NomInsumo")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("nom_insumo");

                    b.HasKey("IdInsumo")
                        .HasName("PK__insumos__D4F202B1297E3169");

                    b.ToTable("insumos", (string)null);
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Producto", b =>
                {
                    b.Property<int>("IdProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_producto");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProducto"), 1L, 1);

                    b.Property<byte?>("Estado")
                        .HasColumnType("tinyint")
                        .HasColumnName("estado");

                    b.Property<byte?>("IdCategoria")
                        .HasColumnType("tinyint")
                        .HasColumnName("id_categoria");

                    b.Property<byte?>("IdTamano")
                        .HasColumnType("tinyint")
                        .HasColumnName("id_tamano");

                    b.Property<string>("NomProducto")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("nom_producto");

                    b.Property<int?>("PrecioVenta")
                        .HasColumnType("int")
                        .HasColumnName("precio_venta");

                    b.HasKey("IdProducto")
                        .HasName("PK__producto__FF341C0DF0AEECF5");

                    b.HasIndex("IdCategoria");

                    b.HasIndex("IdTamano");

                    b.ToTable("productos", (string)null);
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Proveedore", b =>
                {
                    b.Property<int>("IdProveedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_proveedor");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProveedor"), 1L, 1);

                    b.Property<string>("Correo")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("correo");

                    b.Property<string>("Direccion")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("direccion");

                    b.Property<int?>("Estado")
                        .HasColumnType("int");

                    b.Property<string>("Nit")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("nit");

                    b.Property<string>("NomLocal")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("nom_local");

                    b.Property<string>("Telefono")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("telefono");

                    b.HasKey("IdProveedor")
                        .HasName("PK__proveedo__8D3DFE281562B3BA");

                    b.ToTable("proveedores", (string)null);
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Receta", b =>
                {
                    b.Property<int>("IdReceta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_receta");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdReceta"), 1L, 1);

                    b.Property<int?>("CantInsumo")
                        .HasColumnType("int")
                        .HasColumnName("cant_insumo");

                    b.Property<int?>("IdInsumo")
                        .HasColumnType("int")
                        .HasColumnName("id_insumo");

                    b.Property<int?>("IdProducto")
                        .HasColumnType("int")
                        .HasColumnName("id_producto");

                    b.HasKey("IdReceta")
                        .HasName("PK__recetas__11DB53AB771D52E7");

                    b.HasIndex("IdInsumo");

                    b.HasIndex("IdProducto");

                    b.ToTable("recetas", (string)null);
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Tamano", b =>
                {
                    b.Property<byte>("IdTamano")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("id_tamano");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("IdTamano"), 1L, 1);

                    b.Property<byte?>("MaximoSabores")
                        .HasColumnType("tinyint")
                        .HasColumnName("maximo_sabores");

                    b.Property<string>("NombreTamano")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("nombre_tamano");

                    b.Property<byte?>("Tamano1")
                        .HasColumnType("tinyint")
                        .HasColumnName("tamano");

                    b.HasKey("IdTamano")
                        .HasName("PK__tamanos__073FB91C101651A2");

                    b.ToTable("tamanos", (string)null);
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Venta", b =>
                {
                    b.Property<int>("IdVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_venta");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdVenta"), 1L, 1);

                    b.Property<string>("Estado")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("estado");

                    b.Property<DateTime?>("FechaVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("fecha_venta")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("IdEmpleado")
                        .HasColumnType("int")
                        .HasColumnName("id_empleado");

                    b.Property<int?>("Pago")
                        .HasColumnType("int")
                        .HasColumnName("pago");

                    b.Property<int?>("PagoDomicilio")
                        .HasColumnType("int")
                        .HasColumnName("pago_domicilio");

                    b.Property<string>("TipoPago")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("tipo_pago");

                    b.Property<int?>("TotalVenta")
                        .HasColumnType("int")
                        .HasColumnName("total_venta");

                    b.HasKey("IdVenta")
                        .HasName("PK__ventas__459533BF1E99A8D0");

                    b.HasIndex("IdEmpleado");

                    b.ToTable("ventas", (string)null);
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Compra", b =>
                {
                    b.HasOne("PF_Pach_OS.Models.Proveedore", "IdProveedorNavigation")
                        .WithMany("Compras")
                        .HasForeignKey("IdProveedor")
                        .HasConstraintName("FK__compras__id_prov__6D0D32F4");

                    b.Navigation("IdProveedorNavigation");
                });

            modelBuilder.Entity("PF_Pach_OS.Models.DetallesCompra", b =>
                {
                    b.HasOne("PF_Pach_OS.Models.Compra", "IdCompraNavigation")
                        .WithMany("DetallesCompras")
                        .HasForeignKey("IdCompra")
                        .HasConstraintName("FK__detalles___id_co__72C60C4A");

                    b.HasOne("PF_Pach_OS.Models.Insumo", "IdInsumoNavigation")
                        .WithMany("DetallesCompras")
                        .HasForeignKey("IdInsumo")
                        .HasConstraintName("FK__detalles___id_in__71D1E811");

                    b.Navigation("IdCompraNavigation");

                    b.Navigation("IdInsumoNavigation");
                });

            modelBuilder.Entity("PF_Pach_OS.Models.DetalleVenta", b =>
                {
                    b.HasOne("PF_Pach_OS.Models.Producto", "IdProductoNavigation")
                        .WithMany("DetalleVenta")
                        .HasForeignKey("IdProducto")
                        .HasConstraintName("FK__detalleVe__id_pr__04E4BC85");

                    b.HasOne("PF_Pach_OS.Models.Venta", "IdVentaNavigation")
                        .WithMany("DetalleVenta")
                        .HasForeignKey("IdVenta")
                        .HasConstraintName("FK__detalleVe__id_ve__05D8E0BE");

                    b.Navigation("IdProductoNavigation");

                    b.Navigation("IdVentaNavigation");
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Producto", b =>
                {
                    b.HasOne("PF_Pach_OS.Models.Categoria", "IdCategoriaNavigation")
                        .WithMany("Productos")
                        .HasForeignKey("IdCategoria")
                        .HasConstraintName("FK__productos__id_ca__7A672E12");

                    b.HasOne("PF_Pach_OS.Models.Tamano", "IdTamanoNavigation")
                        .WithMany("Productos")
                        .HasForeignKey("IdTamano")
                        .HasConstraintName("FK__productos__id_ta__797309D9");

                    b.Navigation("IdCategoriaNavigation");

                    b.Navigation("IdTamanoNavigation");
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Receta", b =>
                {
                    b.HasOne("PF_Pach_OS.Models.Insumo", "IdInsumoNavigation")
                        .WithMany("Receta")
                        .HasForeignKey("IdInsumo")
                        .HasConstraintName("FK__recetas__id_insu__7E37BEF6");

                    b.HasOne("PF_Pach_OS.Models.Producto", "IdProductoNavigation")
                        .WithMany("Receta")
                        .HasForeignKey("IdProducto")
                        .HasConstraintName("FK__recetas__id_prod__7D439ABD");

                    b.Navigation("IdInsumoNavigation");

                    b.Navigation("IdProductoNavigation");
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Venta", b =>
                {
                    b.HasOne("PF_Pach_OS.Models.Empleado", "IdEmpleadoNavigation")
                        .WithMany("Venta")
                        .HasForeignKey("IdEmpleado")
                        .HasConstraintName("FK__ventas__id_emple__02084FDA");

                    b.Navigation("IdEmpleadoNavigation");
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Categoria", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Compra", b =>
                {
                    b.Navigation("DetallesCompras");
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Empleado", b =>
                {
                    b.Navigation("Venta");
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Insumo", b =>
                {
                    b.Navigation("DetallesCompras");

                    b.Navigation("Receta");
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Producto", b =>
                {
                    b.Navigation("DetalleVenta");

                    b.Navigation("Receta");
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Proveedore", b =>
                {
                    b.Navigation("Compras");
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Tamano", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("PF_Pach_OS.Models.Venta", b =>
                {
                    b.Navigation("DetalleVenta");
                });
#pragma warning restore 612, 618
        }
    }
}
