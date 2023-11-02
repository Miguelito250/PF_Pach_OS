using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PF_Pach_OS.Migrations
{
    public partial class Roles_permisos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permisos",
                columns: table => new
                {
                    Id_permiso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom_permiso = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__permisos__A5D405E8B6F5126A", x => x.Id_permiso);
                });
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id_rol = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom_rol = table.Column<string>(maxLength: 30, nullable: true),
                    estado = table.Column<byte>( nullable: true),



                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__roles__2D95A8946F1A4A66", x => x.Id_rol);
                });
            migrationBuilder.CreateTable(
                name: "Rol_permisos",
                columns: table => new
                {
                    Id_rol_permisos = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_permiso = table.Column<int>(nullable: false),
                    Id_rol = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol_permisos", x => x.Id_rol_permisos);
                    table.ForeignKey(
                        name: "FK__Rol_permi__Id_pe__40F9A68C",
                        column: x => x.Id_permiso,
                        principalTable: "permisos",
                        principalColumn: "Id_permiso",
                        onDelete: ReferentialAction.Restrict);


                    table.ForeignKey(
                        name: "FK__Rol_permi__Id_ro__40058253",
                        column: x => x.Id_rol,
                        principalTable: "roles",
                        principalColumn: "Id_rol",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.AddColumn<int>(
                name: "Id_Rol",
                table: "AspNetUsers",
                nullable: false
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Id_Rol",
                table: "AspNetUsers",
                column: "Id_Rol",
                principalTable: "roles",
                principalColumn: "Id_rol",
                onDelete: ReferentialAction.Restrict
            );
            migrationBuilder.InsertData(
                table: "permisos",
                columns: new[] { "nom_permiso" },
                values: new object[] { "Informes" });

            migrationBuilder.InsertData(
                table: "permisos",
                columns: new[] { "nom_permiso" },
                values: new object[] { "Ventas" });

            migrationBuilder.InsertData(
                table: "permisos",
                columns: new[] { "nom_permiso" },
                values: new object[] { "Productos" });

            migrationBuilder.InsertData(
                table: "permisos",
                columns: new[] { "nom_permiso" },
                values: new object[] { "Insumos" });

            migrationBuilder.InsertData(
                table: "permisos",
                columns: new[] { "nom_permiso" },
                values: new object[] { "Compras" });

            migrationBuilder.InsertData(
                table: "permisos",
                columns: new[] { "nom_permiso" },
                values: new object[] { "Proveedores" });

            migrationBuilder.InsertData(
                table: "permisos",
                columns: new[] { "nom_permiso" },
                values: new object[] { "Configuraciones" });

            migrationBuilder.InsertData(
                table: "permisos",
                columns: new[] { "nom_permiso" },
                values: new object[] { "Usuarios" });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "nom_rol"},
                values: new object[] { "Administrador" });

            migrationBuilder.InsertData(
                table: "Rol_permisos",
                columns: new[] { "Id_rol", "Id_permiso" },
                values: new object[] { 1, 1 });
            migrationBuilder.InsertData(
                table: "Rol_permisos",
                columns: new[] { "Id_rol", "Id_permiso" },
                values: new object[] { 1, 2 });
            migrationBuilder.InsertData(
                table: "Rol_permisos",
                columns: new[] { "Id_rol", "Id_permiso" },
                values: new object[] { 1, 3 });
            migrationBuilder.InsertData(
                table: "Rol_permisos",
                columns: new[] { "Id_rol", "Id_permiso" },
                values: new object[] { 1, 4 });
            migrationBuilder.InsertData(
                table: "Rol_permisos",
                columns: new[] { "Id_rol", "Id_permiso" },
                values: new object[] { 1, 5 });
            migrationBuilder.InsertData(
                table: "Rol_permisos",
                columns: new[] { "Id_rol", "Id_permiso" },
                values: new object[] { 1, 6 });
            migrationBuilder.InsertData(
                table: "Rol_permisos",
                columns: new[] { "Id_rol", "Id_permiso" },
                values: new object[] { 1, 7 });
            migrationBuilder.InsertData(
                table: "Rol_permisos",
                columns: new[] { "Id_rol", "Id_permiso" },
                values: new object[] { 1, 8 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rol_permisos");

            migrationBuilder.DropTable(
                name: "permisos");

            migrationBuilder.DropTable(
                name: "roles");
        }

    }
}

