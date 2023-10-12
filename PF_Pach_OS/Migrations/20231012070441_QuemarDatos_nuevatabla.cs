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
                        principalColumn: "id_detalleVenta");
                    table.ForeignKey(
                        name: "FK_SaboresSeleccionados_productos",
                        column: x => x.IdProducto,
                        principalTable: "productos",
                        principalColumn: "id_producto");
                });


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
        columns: new[] { "nombre_tamano" },
        values: new object[] { "Pizza(Personal)" });

            migrationBuilder.InsertData(
                table: "tamanos",
                columns: new[] { "nombre_tamano" },
                values: new object[] { "Pizza(Pequeña)" });

            migrationBuilder.InsertData(
                table: "tamanos",
                columns: new[] { "nombre_tamano" },
                values: new object[] { "Pizza(Grande)" });

            migrationBuilder.InsertData(
                table: "tamanos",
                columns: new[] { "nombre_tamano" },
                values: new object[] { "Pizza(Familiar)" });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaboresSeleccionados");
        }
    }
}
