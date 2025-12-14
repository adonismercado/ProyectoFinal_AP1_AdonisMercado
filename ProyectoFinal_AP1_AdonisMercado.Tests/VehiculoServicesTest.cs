using Microsoft.EntityFrameworkCore;
using ProyectoFinal_AP1_AdonisMercado.DAL;
using ProyectoFinal_AP1_AdonisMercado.Models;
using ProyectoFinal_AP1_AdonisMercado.Services;
using Xunit;

namespace ProyectoFinal_AP1_AdonisMercado.Tests;

public class VehiculoServiceTests
{
    private IDbContextFactory<Contexto> GetFactory(string dbName)
    {
        var options = new DbContextOptionsBuilder<Contexto>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new TestDbFactory(options);
    }

    [Fact]
    public async Task InsertarConId_Inserta_Y_DevuelveId()
    {
        var factory = GetFactory("InsertarVehiculoDB");
        var service = new VehiculoService(factory);

        var vehiculo = new Vehiculo
        {
            MarcaVehiculo = "Toyota",
            ModeloVehiculo = "Corolla",
            AnioFabricacion = 2023,
            Precio = 25000,
            Kilometraje = 0,
            NumeroPuertas = 4,
            StockVehiculo = 10,
            isActive = true
        };

        var id = await service.InsertarConId(vehiculo);
        var guardado = await service.Buscar(id);

        Assert.True(id > 0);
        Assert.NotNull(guardado);
        Assert.Equal("Toyota", guardado!.MarcaVehiculo);
        Assert.Equal("Corolla", guardado.ModeloVehiculo);
        Assert.Equal(2023, guardado.AnioFabricacion);
    }

    [Fact]
    public async Task Modificar_Actualiza_VehiculoExistente()
    {
        var factory = GetFactory("ModificarVehiculoDB");
        var service = new VehiculoService(factory);

        var vehiculo = new Vehiculo
        {
            MarcaVehiculo = "Honda",
            ModeloVehiculo = "Civic",
            AnioFabricacion = 2022,
            Precio = 28000,
            Kilometraje = 5000,
            NumeroPuertas = 4,
            StockVehiculo = 5,
            isActive = true
        };

        var id = await service.InsertarConId(vehiculo);

        // Modificar
        vehiculo.Precio = 26000;
        vehiculo.StockVehiculo = 8;
        var resultado = await service.Modificar(vehiculo);
        var modificado = await service.Buscar(id);

        Assert.True(resultado);
        Assert.Equal(26000, modificado!.Precio);
        Assert.Equal(8, modificado.StockVehiculo);
    }

    [Fact]
    public async Task ActualizarImagen_Actualiza_ImagenUrl()
    {
        var factory = GetFactory("ActualizarImagenDB");
        var service = new VehiculoService(factory);

        var vehiculo = new Vehiculo
        {
            MarcaVehiculo = "Mazda",
            ModeloVehiculo = "CX-5",
            AnioFabricacion = 2024,
            Precio = 35000,
            Kilometraje = 0,
            NumeroPuertas = 4,
            StockVehiculo = 3,
            isActive = true
        };

        var id = await service.InsertarConId(vehiculo);
        var nuevaUrl = "https://example.com/mazda-cx5.jpg";

        var resultado = await service.ActualizarImagen(id, nuevaUrl);
        var modificada = await service.Buscar(id);

        Assert.True(resultado);
        Assert.Equal(nuevaUrl, modificada!.ImagenUrl);
    }

    [Fact]
    public async Task ActualizarImagen_RetornaFalse_CuandoVehiculoNoExiste()
    {
        var factory = GetFactory("ActualizarImagenNoExisteDB");
        var service = new VehiculoService(factory);

        var resultado = await service.ActualizarImagen(999, "https://example.com/imagen.jpg");

        Assert.False(resultado);
    }

    [Fact]
    public async Task Existe_DevuelveTrue_CuandoVehiculoExiste()
    {
        var factory = GetFactory("ExisteVehiculoDB");
        var service = new VehiculoService(factory);

        var vehiculo = new Vehiculo
        {
            MarcaVehiculo = "Ford",
            ModeloVehiculo = "Focus",
            AnioFabricacion = 2021,
            Precio = 22000,
            Kilometraje = 15000,
            NumeroPuertas = 4,
            StockVehiculo = 2,
            isActive = true
        };

        var id = await service.InsertarConId(vehiculo);

        var existe = await service.Existe(id);
        var noExiste = await service.Existe(999);

        Assert.True(existe);
        Assert.False(noExiste);
    }

    [Fact]
    public async Task Buscar_DevuelveVehiculo_ConPedidoDetalles()
    {
        var factory = GetFactory("BuscarVehiculoDB");
        var contexto = await factory.CreateDbContextAsync();

        var vehiculo = new Vehiculo
        {
            VehiculoId = 1,
            MarcaVehiculo = "Nissan",
            ModeloVehiculo = "Sentra",
            AnioFabricacion = 2023,
            Precio = 24000,
            Kilometraje = 1000,
            NumeroPuertas = 4,
            StockVehiculo = 7,
            isActive = true
        };

        contexto.Vehiculos.Add(vehiculo);
        await contexto.SaveChangesAsync();

        var service = new VehiculoService(factory);
        var encontrado = await service.Buscar(1);

        Assert.NotNull(encontrado);
        Assert.Equal("Nissan", encontrado!.MarcaVehiculo);
        Assert.Equal("Sentra", encontrado.ModeloVehiculo);
        Assert.NotNull(encontrado.PedidoDetalles);
    }

    [Fact]
    public async Task Buscar_DevuelveNull_CuandoNoExiste()
    {
        var factory = GetFactory("BuscarNoExisteDB");
        var service = new VehiculoService(factory);

        var resultado = await service.Buscar(999);

        Assert.Null(resultado);
    }

    [Fact]
    public async Task ListarVehiculos_DevuelveSegunCriterio()
    {
        var factory = GetFactory("ListarVehiculosDB");
        var service = new VehiculoService(factory);

        await service.InsertarConId(new Vehiculo
        {
            MarcaVehiculo = "Chevrolet",
            ModeloVehiculo = "Spark",
            AnioFabricacion = 2024,
            Precio = 18000,
            Kilometraje = 0,
            NumeroPuertas = 4,
            StockVehiculo = 12,
            isActive = true
        });

        await service.InsertarConId(new Vehiculo
        {
            MarcaVehiculo = "Chevrolet",
            ModeloVehiculo = "Cruze",
            AnioFabricacion = 2023,
            Precio = 28000,
            Kilometraje = 2000,
            NumeroPuertas = 4,
            StockVehiculo = 5,
            isActive = false
        });

        await service.InsertarConId(new Vehiculo
        {
            MarcaVehiculo = "Hyundai",
            ModeloVehiculo = "Elantra",
            AnioFabricacion = 2024,
            Precio = 26000,
            Kilometraje = 0,
            NumeroPuertas = 4,
            StockVehiculo = 8,
            isActive = true
        });

        var vehiculosActivos = await service.ListarVehiculos(v => v.isActive == true);
        var vehiculosChevrolet = await service.ListarVehiculos(v => v.MarcaVehiculo == "Chevrolet");

        Assert.Equal(2, vehiculosActivos.Count);
        Assert.All(vehiculosActivos, v => Assert.True(v.isActive));

        Assert.Equal(2, vehiculosChevrolet.Count);
        Assert.All(vehiculosChevrolet, v => Assert.Equal("Chevrolet", v.MarcaVehiculo));
    }

    [Fact]
    public async Task Deshabilitar_Cambia_isActive_A_False()
    {
        var factory = GetFactory("DeshabilitarVehiculoDB");
        var service = new VehiculoService(factory);

        var vehiculo = new Vehiculo
        {
            MarcaVehiculo = "Kia",
            ModeloVehiculo = "Rio",
            AnioFabricacion = 2022,
            Precio = 20000,
            Kilometraje = 8000,
            NumeroPuertas = 4,
            StockVehiculo = 4,
            isActive = true
        };

        var id = await service.InsertarConId(vehiculo);
        var resultado = await service.Deshabilitar(id);
        var deshabilitado = await service.Buscar(id);

        Assert.True(resultado);
        Assert.False(deshabilitado!.isActive);
    }

    [Fact]
    public async Task Deshabilitar_RetornaFalse_CuandoNoExiste()
    {
        var factory = GetFactory("DeshabilitarNoExisteDB");
        var service = new VehiculoService(factory);

        var resultado = await service.Deshabilitar(999);

        Assert.False(resultado);
    }

    [Fact]
    public async Task Habilitar_Cambia_isActive_A_True()
    {
        var factory = GetFactory("HabilitarVehiculoDB");
        var service = new VehiculoService(factory);

        var vehiculo = new Vehiculo
        {
            MarcaVehiculo = "Volkswagen",
            ModeloVehiculo = "Jetta",
            AnioFabricacion = 2023,
            Precio = 27000,
            Kilometraje = 3000,
            NumeroPuertas = 4,
            StockVehiculo = 6,
            isActive = false
        };

        var id = await service.InsertarConId(vehiculo);
        var resultado = await service.Habilitar(id);
        var habilitado = await service.Buscar(id);

        Assert.True(resultado);
        Assert.True(habilitado!.isActive);
    }

    [Fact]
    public async Task Habilitar_RetornaFalse_CuandoNoExiste()
    {
        var factory = GetFactory("HabilitarNoExisteDB");
        var service = new VehiculoService(factory);

        var resultado = await service.Habilitar(999);

        Assert.False(resultado);
    }

    [Fact]
    public async Task Eliminar_BorraVehiculo()
    {
        var factory = GetFactory("EliminarVehiculoDB");
        var service = new VehiculoService(factory);

        var vehiculo = new Vehiculo
        {
            MarcaVehiculo = "Subaru",
            ModeloVehiculo = "Impreza",
            AnioFabricacion = 2022,
            Precio = 29000,
            Kilometraje = 10000,
            NumeroPuertas = 4,
            StockVehiculo = 3,
            isActive = true
        };

        var id = await service.InsertarConId(vehiculo);
        var resultado = await service.Eliminar(id);
        var eliminado = await service.Buscar(id);

        Assert.True(resultado);
        Assert.Null(eliminado);
    }

    [Fact]
    public async Task Eliminar_RetornaFalse_CuandoNoExiste()
    {
        var factory = GetFactory("EliminarNoExisteDB");
        var service = new VehiculoService(factory);

        var resultado = await service.Eliminar(999);

        Assert.False(resultado);
    }

    [Fact]
    public async Task ListarVehiculos_DevuelveVacioSinCoincidencias()
    {
        var factory = GetFactory("ListarVaciooDB");
        var service = new VehiculoService(factory);

        await service.InsertarConId(new Vehiculo
        {
            MarcaVehiculo = "Tesla",
            ModeloVehiculo = "Model 3",
            AnioFabricacion = 2024,
            Precio = 45000,
            Kilometraje = 0,
            NumeroPuertas = 4,
            StockVehiculo = 2,
            isActive = true
        });

        var resultado = await service.ListarVehiculos(v => v.MarcaVehiculo == "BMW");

        Assert.Empty(resultado);
    }
}