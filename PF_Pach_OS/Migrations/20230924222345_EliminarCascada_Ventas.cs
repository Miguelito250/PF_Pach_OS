using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PF_Pach_OS.Migrations
{
    public partial class EliminarCascada_Ventas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK__compras__id_prov__6D0D32F4",
                table: "compras");

            migrationBuilder.DropForeignKey(
                name: "FK__detalles___id_co__72C60C4A",
                table: "detalles_Compras");

            migrationBuilder.DropForeignKey(
                name: "FK__detalles___id_in__71D1E811",
                table: "detalles_Compras");

            migrationBuilder.DropForeignKey(
                name: "FK__detalleVe__id_pr__04E4BC85",
                table: "detalleVentas");

            migrationBuilder.DropForeignKey(
                name: "FK__detalleVe__id_ve__05D8E0BE",
                table: "detalleVentas");

            migrationBuilder.DropForeignKey(
                name: "FK__productos__id_ca__7A672E12",
                table: "productos");

            migrationBuilder.DropForeignKey(
                name: "FK__productos__id_ta__797309D9",
                table: "productos");

            migrationBuilder.DropForeignKey(
                name: "FK__recetas__id_insu__7E37BEF6",
                table: "recetas");

            migrationBuilder.DropForeignKey(
                name: "FK__recetas__id_prod__7D439ABD",
                table: "recetas");

            migrationBuilder.DropPrimaryKey(
                name: "PK__ventas__459533BF1E99A8D0",
                table: "ventas");

            migrationBuilder.DropPrimaryKey(
                name: "PK__tamanos__073FB91C101651A2",
                table: "tamanos");

            migrationBuilder.DropPrimaryKey(
                name: "PK__recetas__11DB53AB771D52E7",
                table: "recetas");

            migrationBuilder.DropPrimaryKey(
                name: "PK__proveedo__8D3DFE281562B3BA",
                table: "proveedores");

            migrationBuilder.DropPrimaryKey(
                name: "PK__producto__FF341C0DF0AEECF5",
                table: "productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK__insumos__D4F202B1297E3169",
                table: "insumos");

            migrationBuilder.DropPrimaryKey(
                name: "PK__empleado__88B5139483CC8B74",
                table: "empleados");

            migrationBuilder.DropPrimaryKey(
                name: "PK__detalleV__3C2E445C48FCB97E",
                table: "detalleVentas");

            migrationBuilder.DropPrimaryKey(
                name: "PK__detalles__905DB0ED54001F5C",
                table: "detalles_Compras");

            migrationBuilder.DropPrimaryKey(
                name: "PK__compras__C4BAA6043CD63BFB",
                table: "compras");

            migrationBuilder.DropPrimaryKey(
                name: "PK__categori__CD54BC5AC013A8AC",
                table: "categorias");

            migrationBuilder.RenameColumn(
                name: "telefono",
                table: "proveedores",
                newName: "Telefono");

            migrationBuilder.RenameColumn(
                name: "direccion",
                table: "proveedores",
                newName: "Direccion");

            migrationBuilder.RenameColumn(
                name: "correo",
                table: "proveedores",
                newName: "Correo");

            migrationBuilder.RenameColumn(
                name: "medida",
                table: "detalles_Compras",
                newName: "Medida");

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "proveedores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "proveedores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Correo",
                table: "proveedores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "estado",
                table: "empleados",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "precio_insumo",
                table: "detalles_Compras",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Medida",
                table: "detalles_Compras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "cantidad",
                table: "detalles_Compras",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__ventas__459533BF24E19040",
                table: "ventas",
                column: "id_venta");

            migrationBuilder.AddPrimaryKey(
                name: "PK__tamanos__073FB91C364ED960",
                table: "tamanos",
                column: "id_tamano");

            migrationBuilder.AddPrimaryKey(
                name: "PK__recetas__11DB53ABAE38FAD1",
                table: "recetas",
                column: "id_receta");

            migrationBuilder.AddPrimaryKey(
                name: "PK__proveedo__8D3DFE288859DED1",
                table: "proveedores",
                column: "id_proveedor");

            migrationBuilder.AddPrimaryKey(
                name: "PK__producto__FF341C0DAD2F40EB",
                table: "productos",
                column: "id_producto");

            migrationBuilder.AddPrimaryKey(
                name: "PK__insumos__D4F202B1AE226FBC",
                table: "insumos",
                column: "id_insumo");

            migrationBuilder.AddPrimaryKey(
                name: "PK__empleado__88B513941CE0EF7C",
                table: "empleados",
                column: "id_empleado");

            migrationBuilder.AddPrimaryKey(
                name: "PK__detalleV__3C2E445CAC618977",
                table: "detalleVentas",
                column: "id_detalleVenta");

            migrationBuilder.AddPrimaryKey(
                name: "PK__detalles__905DB0ED175A41C9",
                table: "detalles_Compras",
                column: "id_detalles_compra");

            migrationBuilder.AddPrimaryKey(
                name: "PK__compras__C4BAA604A7DD9276",
                table: "compras",
                column: "id_compra");

            migrationBuilder.AddPrimaryKey(
                name: "PK__categori__CD54BC5AB649B0E9",
                table: "categorias",
                column: "id_categoria");

            migrationBuilder.AddForeignKey(
                name: "FK__compras__id_prov__4CA06362",
                table: "compras",
                column: "id_proveedor",
                principalTable: "proveedores",
                principalColumn: "id_proveedor");

            migrationBuilder.AddForeignKey(
                name: "FK__detalles___id_co__4D94879B",
                table: "detalles_Compras",
                column: "id_compra",
                principalTable: "compras",
                principalColumn: "id_compra");

            migrationBuilder.AddForeignKey(
                name: "FK__detalles___id_in__4E88ABD4",
                table: "detalles_Compras",
                column: "id_insumo",
                principalTable: "insumos",
                principalColumn: "id_insumo");

            migrationBuilder.AddForeignKey(
                name: "FK__detalleVe__id_pr__4F7CD00D",
                table: "detalleVentas",
                column: "id_producto",
                principalTable: "productos",
                principalColumn: "id_producto");

            migrationBuilder.AddForeignKey(
                name: "FK__detalleVe__id_ve__5070F446",
                table: "detalleVentas",
                column: "id_venta",
                principalTable: "ventas",
                principalColumn: "id_venta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__productos__id_ca__5165187F",
                table: "productos",
                column: "id_categoria",
                principalTable: "categorias",
                principalColumn: "id_categoria");

            migrationBuilder.AddForeignKey(
                name: "FK__productos__id_ta__52593CB8",
                table: "productos",
                column: "id_tamano",
                principalTable: "tamanos",
                principalColumn: "id_tamano");

            migrationBuilder.AddForeignKey(
                name: "FK__recetas__id_insu__534D60F1",
                table: "recetas",
                column: "id_insumo",
                principalTable: "insumos",
                principalColumn: "id_insumo");

            migrationBuilder.AddForeignKey(
                name: "FK__recetas__id_prod__5441852A",
                table: "recetas",
                column: "id_producto",
                principalTable: "productos",
                principalColumn: "id_producto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__compras__id_prov__4CA06362",
                table: "compras");

            migrationBuilder.DropForeignKey(
                name: "FK__detalles___id_co__4D94879B",
                table: "detalles_Compras");

            migrationBuilder.DropForeignKey(
                name: "FK__detalles___id_in__4E88ABD4",
                table: "detalles_Compras");

            migrationBuilder.DropForeignKey(
                name: "FK__detalleVe__id_pr__4F7CD00D",
                table: "detalleVentas");

            migrationBuilder.DropForeignKey(
                name: "FK__detalleVe__id_ve__5070F446",
                table: "detalleVentas");

            migrationBuilder.DropForeignKey(
                name: "FK__productos__id_ca__5165187F",
                table: "productos");

            migrationBuilder.DropForeignKey(
                name: "FK__productos__id_ta__52593CB8",
                table: "productos");

            migrationBuilder.DropForeignKey(
                name: "FK__recetas__id_insu__534D60F1",
                table: "recetas");

            migrationBuilder.DropForeignKey(
                name: "FK__recetas__id_prod__5441852A",
                table: "recetas");

            migrationBuilder.DropPrimaryKey(
                name: "PK__ventas__459533BF24E19040",
                table: "ventas");

            migrationBuilder.DropPrimaryKey(
                name: "PK__tamanos__073FB91C364ED960",
                table: "tamanos");

            migrationBuilder.DropPrimaryKey(
                name: "PK__recetas__11DB53ABAE38FAD1",
                table: "recetas");

            migrationBuilder.DropPrimaryKey(
                name: "PK__proveedo__8D3DFE288859DED1",
                table: "proveedores");

            migrationBuilder.DropPrimaryKey(
                name: "PK__producto__FF341C0DAD2F40EB",
                table: "productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK__insumos__D4F202B1AE226FBC",
                table: "insumos");

            migrationBuilder.DropPrimaryKey(
                name: "PK__empleado__88B513941CE0EF7C",
                table: "empleados");

            migrationBuilder.DropPrimaryKey(
                name: "PK__detalleV__3C2E445CAC618977",
                table: "detalleVentas");

            migrationBuilder.DropPrimaryKey(
                name: "PK__detalles__905DB0ED175A41C9",
                table: "detalles_Compras");

            migrationBuilder.DropPrimaryKey(
                name: "PK__compras__C4BAA604A7DD9276",
                table: "compras");

            migrationBuilder.DropPrimaryKey(
                name: "PK__categori__CD54BC5AB649B0E9",
                table: "categorias");

            migrationBuilder.RenameColumn(
                name: "Telefono",
                table: "proveedores",
                newName: "telefono");

            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "proveedores",
                newName: "direccion");

            migrationBuilder.RenameColumn(
                name: "Correo",
                table: "proveedores",
                newName: "correo");

            migrationBuilder.RenameColumn(
                name: "Medida",
                table: "detalles_Compras",
                newName: "medida");

            migrationBuilder.AlterColumn<string>(
                name: "telefono",
                table: "proveedores",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "direccion",
                table: "proveedores",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "correo",
                table: "proveedores",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "estado",
                table: "empleados",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "precio_insumo",
                table: "detalles_Compras",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "cantidad",
                table: "detalles_Compras",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "medida",
                table: "detalles_Compras",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK__ventas__459533BF1E99A8D0",
                table: "ventas",
                column: "id_venta");

            migrationBuilder.AddPrimaryKey(
                name: "PK__tamanos__073FB91C101651A2",
                table: "tamanos",
                column: "id_tamano");

            migrationBuilder.AddPrimaryKey(
                name: "PK__recetas__11DB53AB771D52E7",
                table: "recetas",
                column: "id_receta");

            migrationBuilder.AddPrimaryKey(
                name: "PK__proveedo__8D3DFE281562B3BA",
                table: "proveedores",
                column: "id_proveedor");

            migrationBuilder.AddPrimaryKey(
                name: "PK__producto__FF341C0DF0AEECF5",
                table: "productos",
                column: "id_producto");

            migrationBuilder.AddPrimaryKey(
                name: "PK__insumos__D4F202B1297E3169",
                table: "insumos",
                column: "id_insumo");

            migrationBuilder.AddPrimaryKey(
                name: "PK__empleado__88B5139483CC8B74",
                table: "empleados",
                column: "id_empleado");

            migrationBuilder.AddPrimaryKey(
                name: "PK__detalleV__3C2E445C48FCB97E",
                table: "detalleVentas",
                column: "id_detalleVenta");

            migrationBuilder.AddPrimaryKey(
                name: "PK__detalles__905DB0ED54001F5C",
                table: "detalles_Compras",
                column: "id_detalles_compra");

            migrationBuilder.AddPrimaryKey(
                name: "PK__compras__C4BAA6043CD63BFB",
                table: "compras",
                column: "id_compra");

            migrationBuilder.AddPrimaryKey(
                name: "PK__categori__CD54BC5AC013A8AC",
                table: "categorias",
                column: "id_categoria");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__compras__id_prov__6D0D32F4",
                table: "compras",
                column: "id_proveedor",
                principalTable: "proveedores",
                principalColumn: "id_proveedor");

            migrationBuilder.AddForeignKey(
                name: "FK__detalles___id_co__72C60C4A",
                table: "detalles_Compras",
                column: "id_compra",
                principalTable: "compras",
                principalColumn: "id_compra");

            migrationBuilder.AddForeignKey(
                name: "FK__detalles___id_in__71D1E811",
                table: "detalles_Compras",
                column: "id_insumo",
                principalTable: "insumos",
                principalColumn: "id_insumo");

            migrationBuilder.AddForeignKey(
                name: "FK__detalleVe__id_pr__04E4BC85",
                table: "detalleVentas",
                column: "id_producto",
                principalTable: "productos",
                principalColumn: "id_producto");

            migrationBuilder.AddForeignKey(
                name: "FK__detalleVe__id_ve__05D8E0BE",
                table: "detalleVentas",
                column: "id_venta",
                principalTable: "ventas",
                principalColumn: "id_venta");

            migrationBuilder.AddForeignKey(
                name: "FK__productos__id_ca__7A672E12",
                table: "productos",
                column: "id_categoria",
                principalTable: "categorias",
                principalColumn: "id_categoria");

            migrationBuilder.AddForeignKey(
                name: "FK__productos__id_ta__797309D9",
                table: "productos",
                column: "id_tamano",
                principalTable: "tamanos",
                principalColumn: "id_tamano");

            migrationBuilder.AddForeignKey(
                name: "FK__recetas__id_insu__7E37BEF6",
                table: "recetas",
                column: "id_insumo",
                principalTable: "insumos",
                principalColumn: "id_insumo");

            migrationBuilder.AddForeignKey(
                name: "FK__recetas__id_prod__7D439ABD",
                table: "recetas",
                column: "id_producto",
                principalTable: "productos",
                principalColumn: "id_producto");

        }
    }
}
