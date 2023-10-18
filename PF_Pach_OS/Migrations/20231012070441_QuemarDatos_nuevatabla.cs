using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PF_Pach_OS.Migrations
{
    public partial class QuemarDatos_nuevatabla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SaboresSeleccionados",
                columns: table => new
                {
                    IdSaborSeleccionado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    IdDetalleVenta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaboresSeleccionados", x => x.IdSaborSeleccionado);
                    table.ForeignKey(
                        name: "FK_SaboresSeleccionados_detalleVentas",
                        column: x => x.IdDetalleVenta,
                        principalTable: "detalleVentas",
                        principalColumn: "id_detalleVenta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaboresSeleccionados_productos",
                        column: x => x.IdProducto,
                        principalTable: "productos",
                        principalColumn: "id_producto");
                });

            migrationBuilder.AddColumn<string>(
                "NumeroFactura", 
                table: "compras", 
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Estado",
                table: "insumos",
                type: "tinyint",
                nullable: true);

            

            migrationBuilder.InsertData(
            table: "categorias",
            columns: new[] { "nom_categoria" },
            values: new object[] { "Pizza" });

            migrationBuilder.InsertData(
                table: "categorias",
                columns: new[] { "nom_categoria" },
                values: new object[] { "Gratinados" });

            migrationBuilder.InsertData(
                table: "categorias",
                columns: new[] { "nom_categoria" },
                values: new object[] { "Pastas" });

            migrationBuilder.InsertData(
                table: "categorias",
                columns: new[] { "nom_categoria" },
                values: new object[] { "Bebidas" });

            migrationBuilder.InsertData(
                table: "categorias",
                columns: new[] { "nom_categoria" },
                values: new object[] { "Bar" });

            migrationBuilder.InsertData(
            table: "tamanos",
            columns: new[] { "nombre_tamano", "tamano", "maximo_sabores" },
            values: new object[] { "Pizza(Personal)", 15, 1 });

            migrationBuilder.InsertData(
                table: "tamanos",
                columns: new[] { "nombre_tamano", "tamano","maximo_sabores" },
                values: new object[] { "Pizza(Pequeña)", 30, 2 });

            migrationBuilder.InsertData(
                table: "tamanos",
                columns: new[] { "nombre_tamano", "tamano","maximo_sabores" },
                values: new object[] { "Pizza(Grande)", 40, 2 });

            migrationBuilder.InsertData(
                table: "tamanos",
                columns: new[] { "nombre_tamano", "tamano","maximo_sabores" },
                values: new object[] { "Pizza(Familiar)", 45, 3 });

            migrationBuilder.InsertData(
                table: "productos",
                columns: new[] { "nom_producto", "precio_venta", "estado", "id_tamano", "id_categoria" },
                values: new object[] { "Pizza(Personal)", 8000, 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "productos",
                columns: new[] { "nom_producto", "precio_venta", "estado", "id_tamano", "id_categoria" },
                values: new object[] { "Pizza(Pequeña)", 27000, 1, 2, 1 });

            migrationBuilder.InsertData(
               table: "productos",
               columns: new[] { "nom_producto", "precio_venta", "estado", "id_tamano", "id_categoria" },
               values: new object[] { "Pizza(Grande)", 38000, 1, 3, 1 });

            migrationBuilder.InsertData(
               table: "productos",
               columns: new[] { "nom_producto", "precio_venta", "estado", "id_tamano", "id_categoria" },
               values: new object[] { "Pizza(Familiar)", 50000, 1, 4, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaboresSeleccionados");
        }
    }
}
