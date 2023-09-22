using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PF_Pach_OS.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    id_categoria = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom_categoria = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__categori__CD54BC5AC013A8AC", x => x.id_categoria);
                });

            migrationBuilder.CreateTable(
                name: "empleados",
                columns: table => new
                {
                    id_empleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo_documento = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    num_documento = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    nombre = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    apellido = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    contrasena = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    celular = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    correo = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    estado = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__empleado__88B5139483CC8B74", x => x.id_empleado);
                });

            migrationBuilder.CreateTable(
                name: "insumos",
                columns: table => new
                {
                    id_insumo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom_insumo = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    cant_insumo = table.Column<int>(type: "int", nullable: true),
                    medida = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__insumos__D4F202B1297E3169", x => x.id_insumo);
                });

            migrationBuilder.CreateTable(
                name: "proveedores",
                columns: table => new
                {
                    id_proveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nit = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    nom_local = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    direccion = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    telefono = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    correo = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__proveedo__8D3DFE281562B3BA", x => x.id_proveedor);
                });

            migrationBuilder.CreateTable(
                name: "tamanos",
                columns: table => new
                {
                    id_tamano = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_tamano = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    tamano = table.Column<byte>(type: "tinyint", nullable: true),
                    maximo_sabores = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tamanos__073FB91C101651A2", x => x.id_tamano);
                });

            migrationBuilder.CreateTable(
                name: "ventas",
                columns: table => new
                {
                    id_venta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha_venta = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    total_venta = table.Column<int>(type: "int", nullable: true),
                    tipo_pago = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    pago = table.Column<int>(type: "int", nullable: true),
                    pago_domicilio = table.Column<int>(type: "int", nullable: true),
                    estado = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    id_empleado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ventas__459533BF1E99A8D0", x => x.id_venta);
                    table.ForeignKey(
                        name: "FK__ventas__id_emple__02084FDA",
                        column: x => x.id_empleado,
                        principalTable: "empleados",
                        principalColumn: "id_empleado");
                });

            migrationBuilder.CreateTable(
                name: "compras",
                columns: table => new
                {
                    id_compra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha_compra = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    total = table.Column<int>(type: "int", nullable: true),
                    id_empleado = table.Column<int>(type: "int", nullable: true),
                    id_proveedor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__compras__C4BAA6043CD63BFB", x => x.id_compra);
                    table.ForeignKey(
                        name: "FK__compras__id_prov__6D0D32F4",
                        column: x => x.id_proveedor,
                        principalTable: "proveedores",
                        principalColumn: "id_proveedor");
                });

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    id_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom_producto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    precio_venta = table.Column<int>(type: "int", nullable: true),
                    estado = table.Column<byte>(type: "tinyint", nullable: true),
                    id_tamano = table.Column<byte>(type: "tinyint", nullable: true),
                    id_categoria = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__producto__FF341C0DF0AEECF5", x => x.id_producto);
                    table.ForeignKey(
                        name: "FK__productos__id_ca__7A672E12",
                        column: x => x.id_categoria,
                        principalTable: "categorias",
                        principalColumn: "id_categoria");
                    table.ForeignKey(
                        name: "FK__productos__id_ta__797309D9",
                        column: x => x.id_tamano,
                        principalTable: "tamanos",
                        principalColumn: "id_tamano");
                });

            migrationBuilder.CreateTable(
                name: "detalles_Compras",
                columns: table => new
                {
                    id_detalles_compra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    precio_insumo = table.Column<int>(type: "int", nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: true),
                    medida = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    id_compra = table.Column<int>(type: "int", nullable: true),
                    id_insumo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__detalles__905DB0ED54001F5C", x => x.id_detalles_compra);
                    table.ForeignKey(
                        name: "FK__detalles___id_co__72C60C4A",
                        column: x => x.id_compra,
                        principalTable: "compras",
                        principalColumn: "id_compra");
                    table.ForeignKey(
                        name: "FK__detalles___id_in__71D1E811",
                        column: x => x.id_insumo,
                        principalTable: "insumos",
                        principalColumn: "id_insumo");
                });

            migrationBuilder.CreateTable(
                name: "detalleVentas",
                columns: table => new
                {
                    id_detalleVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cant_vendida = table.Column<int>(type: "int", nullable: true),
                    precio = table.Column<int>(type: "int", nullable: true),
                    id_venta = table.Column<int>(type: "int", nullable: true),
                    id_producto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__detalleV__3C2E445C48FCB97E", x => x.id_detalleVenta);
                    table.ForeignKey(
                        name: "FK__detalleVe__id_pr__04E4BC85",
                        column: x => x.id_producto,
                        principalTable: "productos",
                        principalColumn: "id_producto");
                    table.ForeignKey(
                        name: "FK__detalleVe__id_ve__05D8E0BE",
                        column: x => x.id_venta,
                        principalTable: "ventas",
                        principalColumn: "id_venta");
                });

            migrationBuilder.CreateTable(
                name: "recetas",
                columns: table => new
                {
                    id_receta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cant_insumo = table.Column<int>(type: "int", nullable: true),
                    id_producto = table.Column<int>(type: "int", nullable: true),
                    id_insumo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__recetas__11DB53AB771D52E7", x => x.id_receta);
                    table.ForeignKey(
                        name: "FK__recetas__id_insu__7E37BEF6",
                        column: x => x.id_insumo,
                        principalTable: "insumos",
                        principalColumn: "id_insumo");
                    table.ForeignKey(
                        name: "FK__recetas__id_prod__7D439ABD",
                        column: x => x.id_producto,
                        principalTable: "productos",
                        principalColumn: "id_producto");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_compras_id_proveedor",
                table: "compras",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_detalles_Compras_id_compra",
                table: "detalles_Compras",
                column: "id_compra");

            migrationBuilder.CreateIndex(
                name: "IX_detalles_Compras_id_insumo",
                table: "detalles_Compras",
                column: "id_insumo");

            migrationBuilder.CreateIndex(
                name: "IX_detalleVentas_id_producto",
                table: "detalleVentas",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_detalleVentas_id_venta",
                table: "detalleVentas",
                column: "id_venta");

            migrationBuilder.CreateIndex(
                name: "IX_productos_id_categoria",
                table: "productos",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_productos_id_tamano",
                table: "productos",
                column: "id_tamano");

            migrationBuilder.CreateIndex(
                name: "IX_recetas_id_insumo",
                table: "recetas",
                column: "id_insumo");

            migrationBuilder.CreateIndex(
                name: "IX_recetas_id_producto",
                table: "recetas",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_ventas_id_empleado",
                table: "ventas",
                column: "id_empleado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "detalles_Compras");

            migrationBuilder.DropTable(
                name: "detalleVentas");

            migrationBuilder.DropTable(
                name: "recetas");

            migrationBuilder.DropTable(
                name: "compras");

            migrationBuilder.DropTable(
                name: "ventas");

            migrationBuilder.DropTable(
                name: "insumos");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "proveedores");

            migrationBuilder.DropTable(
                name: "empleados");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "tamanos");
        }
    }
}
