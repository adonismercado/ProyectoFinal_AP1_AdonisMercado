using Microsoft.EntityFrameworkCore;
using ProyectoFinal_AP1_AdonisMercado.Models;

namespace ProyectoFinal_AP1_AdonisMercado.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Distribuidor> Distribuidores { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoDetalle> PedidoDetalles { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; }
    public DbSet<Documento> Documentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vehiculo>().HasData(
            new Vehiculo
            {
                VehiculoId = 1,
                MarcaVehiculo = "Toyota",
                ModeloVehiculo = "Corolla",
                ColorVehiculo = "Blanco",
                NumeroChasis = "ABC123XYZ456",
                AnioFabricacion = 2020,
                EstadoVehiculo = "Nuevo",
                TipoCombustible = "Gasolina",
                Precio = 20000.00m
            },
            new Vehiculo
            {
                VehiculoId = 2,
                MarcaVehiculo = "Honda",
                ModeloVehiculo = "Civic",
                ColorVehiculo = "Negro",
                NumeroChasis = "DEF456UVW789",
                AnioFabricacion = 2019,
                EstadoVehiculo = "Usado",
                TipoCombustible = "Gasolina",
                Precio = 18000.00m
            },
            new Vehiculo
            {
                VehiculoId = 3,
                MarcaVehiculo = "Ford",
                ModeloVehiculo = "Focus",
                ColorVehiculo = "Azul",
                NumeroChasis = "GHI789RST012",
                AnioFabricacion = 2018,
                EstadoVehiculo = "Usado",
                TipoCombustible = "Diésel",
                Precio = 15000.00m
            },
            new Vehiculo
            {
                VehiculoId = 4,
                MarcaVehiculo = "Chevrolet",
                ModeloVehiculo = "Tahoe",
                ColorVehiculo = "Rojo",
                NumeroChasis = "JKL423123444",
                AnioFabricacion = 2025,
                EstadoVehiculo = "Nuevo",
                TipoCombustible = "Gasolina",
                Precio = 115000.00m
            },
            new Vehiculo
            {
                VehiculoId = 5,
                MarcaVehiculo = "Toyota",
                ModeloVehiculo = "Hilux",
                ColorVehiculo = "Gris",
                NumeroChasis = "MNO321XYZ654",
                AnioFabricacion = 2024,
                EstadoVehiculo = "Nuevo",
                TipoCombustible = "Diesel",
                Precio = 52000.00m
            },
            new Vehiculo
            {
                VehiculoId = 6,
                MarcaVehiculo = "Nissan",
                ModeloVehiculo = "Sentra",
                ColorVehiculo = "Blanco",
                NumeroChasis = "PQR654UVW321",
                AnioFabricacion = 2023,
                EstadoVehiculo = "Nuevo",
                TipoCombustible = "Gasolina",
                Precio = 22000.00m
            },
            new Vehiculo
            {
                VehiculoId = 7,
                MarcaVehiculo = "Honda",
                ModeloVehiculo = "CR-V",
                ColorVehiculo = "Azul",
                NumeroChasis = "STU987RST654",
                AnioFabricacion = 2025,
                EstadoVehiculo = "Nuevo",
                TipoCombustible = "Gasolina",
                Precio = 55000.00m
            },
            new Vehiculo
            {
                VehiculoId = 8,
                MarcaVehiculo = "Mazda",
                ModeloVehiculo = "CX-5",
                ColorVehiculo = "Rojo",
                NumeroChasis = "VWX123LMN789",
                AnioFabricacion = 2024,
                EstadoVehiculo = "Nuevo",
                TipoCombustible = "Gasolina",
                Precio = 48000.00m
            },
            new Vehiculo
            {
                VehiculoId = 9,
                MarcaVehiculo = "Toyota",
                ModeloVehiculo = "RAV4 Hybrid",
                ColorVehiculo = "Blanco",
                NumeroChasis = "YZA456OPQ123",
                AnioFabricacion = 2023,
                EstadoVehiculo = "Usado",
                TipoCombustible = "Gasolina",
                Precio = 35000.00m
            },
            new Vehiculo
            {
                VehiculoId = 10,
                MarcaVehiculo = "Chevrolet",
                ModeloVehiculo = "Silverado",
                ColorVehiculo = "Negro",
                NumeroChasis = "BCD789EFG456",
                AnioFabricacion = 2022,
                EstadoVehiculo = "Usado",
                TipoCombustible = "Diesel",
                Precio = 60000.00m
            }
        );
    }
}
