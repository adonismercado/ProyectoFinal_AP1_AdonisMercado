using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoFinal_AP1_AdonisMercado.Migrations.ContextoMigrations
{
    /// <inheritdoc />
    public partial class AddIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Distribuidores",
                columns: table => new
                {
                    DistribuidorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distribuidores", x => x.DistribuidorId);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    VehiculoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarcaVehiculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModeloVehiculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorVehiculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroChasis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnioFabricacion = table.Column<int>(type: "int", nullable: false),
                    Motor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transmision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Traccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroPuertas = table.Column<int>(type: "int", nullable: false),
                    Kilometraje = table.Column<int>(type: "int", nullable: false),
                    EstadoVehiculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoCombustible = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.VehiculoId);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombrePedido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    DistribuidorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.PedidoId);
                    table.ForeignKey(
                        name: "FK_Pedidos_Distribuidores_DistribuidorId",
                        column: x => x.DistribuidorId,
                        principalTable: "Distribuidores",
                        principalColumn: "DistribuidorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    DocumentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreOriginal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreAlmacenado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RutaDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.DocumentoId);
                    table.ForeignKey(
                        name: "FK_Documentos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoDetalles",
                columns: table => new
                {
                    PedidoDetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    VehiculoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoDetalles", x => x.PedidoDetalleId);
                    table.ForeignKey(
                        name: "FK_PedidoDetalles_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoDetalles_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "VehiculoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Vehiculos",
                columns: new[] { "VehiculoId", "AnioFabricacion", "ColorVehiculo", "EstadoVehiculo", "Kilometraje", "MarcaVehiculo", "ModeloVehiculo", "Motor", "NumeroChasis", "NumeroPuertas", "Precio", "TipoCombustible", "Traccion", "Transmision" },
                values: new object[,]
                {
                    { 1, 2020, "Blanco", "Nuevo", 0, "Toyota", "Corolla", "1.8L", "JTDBR32E720045671", 4, 20000.00m, "Gasolina", "FWD", "Automática" },
                    { 2, 2019, "Negro", "Usado", 45000, "Honda", "Civic", "2.0L", "2HGFA16598H345612", 4, 18000.00m, "Gasolina", "FWD", "Automática" },
                    { 3, 2018, "Azul", "Usado", 60000, "Ford", "Focus", "2.0L TDCi", "1FAHP3F26CL123589", 4, 15000.00m, "Diésel", "FWD", "Manual" },
                    { 4, 2025, "Rojo", "Nuevo", 0, "Chevrolet", "Tahoe", "5.3L V8", "1GNSKCKC4FR367812", 5, 115000.00m, "Gasolina", "4x4", "Automática" },
                    { 5, 2024, "Gris", "Nuevo", 0, "Toyota", "Hilux", "2.8L Turbo Diesel", "MR0HA3CD205123678", 4, 52000.00m, "Diesel", "4x4", "Automática" },
                    { 6, 2023, "Blanco", "Nuevo", 0, "Nissan", "Sentra", "2.0L", "3N1AB8CV4PY245781", 4, 22000.00m, "Gasolina", "FWD", "CVT" },
                    { 7, 2025, "Azul", "Nuevo", 0, "Honda", "CR-V", "1.5L Turbo", "5J6RM4H77GL046821", 5, 55000.00m, "Gasolina", "AWD", "Automática" },
                    { 8, 2024, "Rojo", "Nuevo", 0, "Mazda", "CX-5", "2.5L SkyActiv", "JM3KFBDM4M0557812", 5, 48000.00m, "Gasolina", "AWD", "Automática" },
                    { 9, 2023, "Blanco", "Usado", 20000, "Toyota", "RAV4 Hybrid", "2.5L Hybrid", "JTMGB3FV2MD062345", 5, 35000.00m, "Gasolina", "AWD", "Automática" },
                    { 10, 2022, "Negro", "Usado", 35000, "Chevrolet", "Silverado", "3.0L Duramax", "1GCUYDED2MZ234567", 4, 60000.00m, "Diesel", "4x4", "Automática" },
                    { 11, 2026, "Gris", "Nuevo", 0, "Toyota", "RAV4", "2.5L", "2T3J1RFV7RW345612", 5, 80000m, "Gasolina", "AWD", "Automática" },
                    { 12, 2026, "Gris", "Nuevo", 0, "Kia", "K5", "1.6L Turbo", "5XXG64J20MG152487", 4, 59000m, "Gasolina", "FWD", "Automática" },
                    { 13, 2024, "Sky Blue", "Usado", 70000, "Honda", "Pilot", "3.5L V6", "5FNYF6H56LB123789", 5, 6000m, "Gasolina", "AWD", "Automática" },
                    { 14, 2026, "Rojo", "Nuevo", 0, "Toyota", "Tacoma TRD Pro", "3.5L V6", "3TMCZ5AN8PM234981", 4, 120000m, "Gasolina", "4x4", "Automática" },
                    { 15, 2026, "Storm Cloud", "Nuevo", 0, "Toyota", "Highlander", "3.5L V6", "5TDKDRBH4PS145678", 5, 110000m, "Gasolina", "AWD", "Automática" },
                    { 16, 2025, "Blanco", "Usado", 15000, "JETOUR", "Dashing", "1.5L Turbo", "L6T743DF5RA012345", 5, 38000m, "Gasolina", "FWD", "Automática" },
                    { 17, 2025, "Blanco", "Nuevo", 0, "Hyundai", "Sonata", "2.5L", "5NPEJ4J27MH045612", 4, 40000m, "Gasolina", "FWD", "Automática" },
                    { 18, 2026, "Blanco", "Nuevo", 0, "Kia", "Sonet", "1.5L", "MZK123F47RN654321", 4, 35000m, "Gasolina", "FWD", "Automática" },
                    { 19, 2025, "Blanco", "Nuevo", 0, "Mercedes-Benz", "GLE 63 S Coupe", "4.0L V8 Biturbo", "4JGED7FB7MA123456", 5, 165000.00m, "Gasolina", "AWD 4MATIC", "Automática AMG" },
                    { 20, 2025, "Gris", "Nuevo", 0, "Nissan", "Frontier Pro-4X", "2.5L Turbo Diesel", "1N6ED1EK9RN345678", 4, 55000m, "Diesel", "4x4", "Automática" },
                    { 21, 2025, "Blanco", "Usado", 10000, "Toyota", "Land Cruiser Prado Premium", "4.0L V6", "JTEBR3FJ3RK128945", 5, 90000m, "Gasolina", "4x4", "Automática" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_PedidoId",
                table: "Documentos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoDetalles_PedidoId",
                table: "PedidoDetalles",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoDetalles_VehiculoId",
                table: "PedidoDetalles",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_DistribuidorId",
                table: "Pedidos",
                column: "DistribuidorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "PedidoDetalles");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Distribuidores");
        }
    }
}
