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
                   id_permiso = table.Column<int>(nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   nombre_Premiso = table.Column<string>(maxLength: 30, nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_permisos", x => x.id_permiso);
               });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id_rol = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_Rol = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id_rol);
                });

            migrationBuilder.CreateTable(
                name: "roles_permisos",
                columns: table => new
                {
                    id_permiso = table.Column<int>(nullable: true),
                    id_rol = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_roles_permisos_permisos_id_permiso",
                        column: x => x.id_permiso,
                        principalTable: "permisos",
                        principalColumn: "id_permiso",
                        onDelete: ReferentialAction.Restrict);

                    table.ForeignKey(
                        name: "FK_roles_permisos_roles_id_rol",
                        column: x => x.id_rol,
                        principalTable: "roles",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.AddColumn<String>(
                name: "id_Rol",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_Rol_User",
                table: "AspNetUsers",
                column: "id_Rol",
                principalTable: "roles",
                principalColumn: "id_rol",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roles_permisos");

            migrationBuilder.DropTable(
                name: "permisos");

            migrationBuilder.DropTable(
                name: "roles");
            migrationBuilder.DropColumn(
                name: "id_Rol",
                table: "AspNetUsers");
            migrationBuilder.DropForeignKey(
                name: "FK_Rol_User",
                table: "AspNetUsers");
        }
    }
}
