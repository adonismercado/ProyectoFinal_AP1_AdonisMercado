using Microsoft.EntityFrameworkCore;
using ProyectoFinal_AP1_AdonisMercado.DAL;
using ProyectoFinal_AP1_AdonisMercado.Models;
using ProyectoFinal_AP1_AdonisMercado.Services;
using Xunit;

namespace ProyectoFinal_AP1_AdonisMercado.Test;

public class DistribuidorServicesTests
{
    private IDbContextFactory<Contexto> GetFactory(string dbName)
    {
        var options = new DbContextOptionsBuilder<Contexto>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new TestDbFactory(options);
    }

    [Fact]
    public async Task Guardar_Inserta_CuandoNoExiste()
    {
        // Arrange
        var factory = GetFactory("InsertDistribuidorDB");
        var service = new DistribuidorServices(factory);

        var distribuidor = new Distribuidor
        {
            DistribuidorId = 1,
            Nombre = "Distribuidor A",
            isActive = true
        };

        // Act
        var result = await service.Guardar(distribuidor);
        var saved = await service.Buscar(1);

        // Assert
        Assert.True(result);
        Assert.NotNull(saved);
        Assert.Equal("Distribuidor A", saved!.Nombre);
        Assert.True(saved.isActive);
    }

    [Fact]
    public async Task Guardar_Modifica_CuandoExiste()
    {
        // Arrange
        var factory = GetFactory("ModifyDistribuidorDB");
        var service = new DistribuidorServices(factory);

        var distribuidor = new Distribuidor
        {
            DistribuidorId = 5,
            Nombre = "Original",
            isActive = true
        };

        await service.Guardar(distribuidor);

        // Act
        distribuidor.Nombre = "Modificado";
        var result = await service.Guardar(distribuidor);
        var updated = await service.Buscar(5);

        // Assert
        Assert.True(result);
        Assert.Equal("Modificado", updated!.Nombre);
    }

    [Fact]
    public async Task Deshabilitar_Cambia_isActive_A_False()
    {
        // Arrange
        var factory = GetFactory("DisableDistribuidorDB");
        var service = new DistribuidorServices(factory);

        await service.Guardar(new Distribuidor
        {
            DistribuidorId = 3,
            Nombre = "Test D",
            isActive = true
        });

        // Act
        var result = await service.Deshabilitar(3);
        var disabled = await service.Buscar(3);

        // Assert
        Assert.True(result);
        Assert.False(disabled!.isActive);
    }

    [Fact]
    public async Task Habilitar_Cambia_isActive_A_True()
    {
        // Arrange
        var factory = GetFactory("EnableDistribuidorDB");
        var service = new DistribuidorServices(factory);

        await service.Guardar(new Distribuidor
        {
            DistribuidorId = 2,
            Nombre = "Test",
            isActive = false
        });

        // Act
        var result = await service.Habilitar(2);
        var enabled = await service.Buscar(2);

        // Assert
        Assert.True(result);
        Assert.True(enabled!.isActive);
    }

    [Fact]
    public async Task Eliminar_Borra_ElDistribuidor()
    {
        // Arrange
        var factory = GetFactory("DeleteDistribuidorDB");
        var service = new DistribuidorServices(factory);

        await service.Guardar(new Distribuidor
        {
            DistribuidorId = 10,
            Nombre = "Eliminar"
        });

        // Act
        var deleted = await service.Eliminar(10);
        var result = await service.Buscar(10);

        // Assert
        Assert.True(deleted);
        Assert.Null(result);
    }

    [Fact]
    public async Task ObtenerPedidosDelDistribuidor_DevuelveSoloActivos()
    {
        // Arrange
        var factory = GetFactory("PedidosDistribuidorDB");
        var contexto = await factory.CreateDbContextAsync();

        contexto.Distribuidores.Add(new Distribuidor
        {
            DistribuidorId = 1,
            Nombre = "D1"
        });

        contexto.Pedidos.Add(new Pedido
        {
            PedidoId = 1,
            DistribuidorId = 1,
            NombrePedido = "Pedido A",
            isActive = true
        });

        contexto.Pedidos.Add(new Pedido
        {
            PedidoId = 2,
            DistribuidorId = 1,
            NombrePedido = "Pedido B",
            isActive = false
        });

        await contexto.SaveChangesAsync();

        var service = new DistribuidorServices(factory);

        // Act
        var pedidos = await service.ObtenerPedidosDelDistribuidor(1);

        // Assert
        Assert.Single(pedidos);
        Assert.Equal("Pedido A", pedidos[0].NombrePedido);
    }
}
