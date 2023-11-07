using Microsoft.EntityFrameworkCore.Migrations;

namespace PF_Pach_OS.Migrations
{
    public partial class ValidacionesBaseDeDatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Insumos ADD CONSTRAINT CK_Longitud CHECK (LEN(nom_insumo) >= 3 AND LEN(nom_insumo) <= 30);");
            migrationBuilder.Sql("ALTER TABLE Insumos ALTER COLUMN Nom_insumo varchar(30) NOT NULL;");
            migrationBuilder.Sql("ALTER TABLE Insumos ALTER COLUMN cant_insumo int NOT NULL;");
            migrationBuilder.Sql("ALTER TABLE Insumos ALTER COLUMN medida varchar(20) NOT NULL;");
            migrationBuilder.Sql("ALTER TABLE Insumos ALTER COLUMN Estado tinyint NOT NULL;");
            migrationBuilder.Sql("ALTER TABLE detalles_Compras ADD CONSTRAINT CK_Mayor100 CHECK (precio_insumo >= 100);");
            migrationBuilder.Sql("ALTER TABLE detalles_Compras ADD CONSTRAINT CK_Mayor0 CHECK (cantidad > 0);");

            //Constraints de Ventas

            migrationBuilder.Sql("ALTER TABLE detalleVentas ALTER COLUMN cant_vendida int NOT NULL;");
            migrationBuilder.Sql("ALTER TABLE detalleVentas ALTER COLUMN precio int NOT NULL;");

            migrationBuilder.Sql("ALTER TABLE detalleVentas ADD CONSTRAINT CK_Mayor0detallesVentas CHECK (cant_vendida > 0);");
            migrationBuilder.Sql("ALTER TABLE ventas ADD CONSTRAINT CK_Mayor0Ventas CHECK (total_venta > 0);");
            migrationBuilder.Sql("ALTER TABLE ventas ADD CONSTRAINT CK_Mayor100Ventas CHECK (pago > 100);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Insumos DROP CONSTRAINT CK_Longitud;");
            migrationBuilder.Sql("ALTER TABLE Insumos ALTER COLUMN Nom_insumo DROP NOT NULL;");
            migrationBuilder.Sql("ALTER TABLE Insumos ALTER COLUMN cant_insumo DROP NOT NULL;");
            migrationBuilder.Sql("ALTER TABLE Insumos ALTER COLUMN medida DROP NOT NULL;");
            migrationBuilder.Sql("ALTER TABLE Insumos ALTER COLUMN Estado DROP NOT NULL;");
            migrationBuilder.Sql("ALTER TABLE detalles_Compras DROP CONSTRAINT CK_Mayor100;");
            migrationBuilder.Sql("ALTER TABLE detalles_Compras DROP CONSTRAINT CK_Mayor0;");

            migrationBuilder.Sql("ALTER TABLE detalleVentas ALTER COLUMN cant_vendida DROP NOT NULL;");
            migrationBuilder.Sql("ALTER TABLE detalleVentas ALTER COLUMN precio DROP NOT NULL;");

            migrationBuilder.Sql("ALTER TABLE detalleVentas DROP CONSTRAINT CK_Mayor0detallesVentas;");
            migrationBuilder.Sql("ALTER TABLE ventas DROP CONSTRAINT CK_Mayor0Ventas;");
            migrationBuilder.Sql("ALTER TABLE ventas DROP CONSTRAINT CK_Mayor100Ventas;");
        }
    }
}