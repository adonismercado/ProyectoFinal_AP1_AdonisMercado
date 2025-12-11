using Microsoft.EntityFrameworkCore;
using ProyectoFinal_AP1_AdonisMercado.DAL;
using ProyectoFinal_AP1_AdonisMercado.Models;
using ProyectoFinal_AP1_AdonisMercado.Services;
using Xunit;

namespace ProyectoFinal_AP1_AdonisMercado.Test;

public class PedidoServicesTests
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
        var factory = GetFactory("InsertPedidoDB");
        var service = new PedidoServices(factory);
        var contexto = await factory.CreateDbContextAsync();

        // Crear distribuidor
        contexto.Distribuidores.Add(new Distribuidor
        {
            DistribuidorId = 1,
            Nombre = "Distribuidor Test",
            isActive = true
        });

        // Crear vehículo para el detalle
        contexto.Vehiculos.Add(new Vehiculo
        {
            VehiculoId = 1,
            MarcaVehiculo = "Toyota",
            ModeloVehiculo = "Corolla",
            AnioFabricacion = 2023,
            Precio = 25000,
            Kilometraje = 0,
            NumeroPuertas = 4,
            StockVehiculo = 10,
            isActive = true
        });
        await contexto.SaveChangesAsync();

        var pedido = new Pedido
        {
            NombrePedido = "Pedido A",
            DistribuidorId = 1,
            FechaPedido = DateTime.Now,
            Estado = "Pendiente",
            isActive = true,
            PedidoDetalles = new List<PedidoDetalle>
            {
                new PedidoDetalle
                {
                    VehiculoId = 1,
                    Cantidad = 5,
                    PrecioUnitario = 25000
                }
            }
        };

        var result = await service.Guardar(pedido);
        var saved = await service.Buscar(pedido.PedidoId);

        Assert.True(result);
        Assert.NotNull(saved);
        Assert.Equal("Pedido A", saved!.NombrePedido);
        Assert.Single(saved.PedidoDetalles);

        // Verificar que el stock aumentó
        var contexto2 = await factory.CreateDbContextAsync();
        var vehiculo = await contexto2.Vehiculos.FindAsync(1);
        Assert.Equal(15, vehiculo!.StockVehiculo);
    }

    [Fact]
    public async Task Guardar_Modifica_CuandoExiste()
    {
        var factory = GetFactory("ModifyPedidoDB");
        var service = new PedidoServices(factory);
        var contexto = await factory.CreateDbContextAsync();

        contexto.Distribuidores.Add(new Distribuidor
        {
            DistribuidorId = 1,
            Nombre = "Distribuidor Test",
            isActive = true
        });

        contexto.Vehiculos.Add(new Vehiculo
        {
            VehiculoId = 1,
            MarcaVehiculo = "Honda",
            ModeloVehiculo = "Civic",
            AnioFabricacion = 2022,
            Precio = 28000,
            Kilometraje = 5000,
            NumeroPuertas = 4,
            StockVehiculo = 20,
            isActive = true
        });
        await contexto.SaveChangesAsync();

        var pedido = new Pedido
        {
            NombrePedido = "Original",
            DistribuidorId = 1,
            FechaPedido = DateTime.Now,
            Estado = "Pendiente",
            isActive = true,
            PedidoDetalles = new List<PedidoDetalle>
            {
                new PedidoDetalle { VehiculoId = 1, Cantidad = 3, PrecioUnitario = 28000 }
            }
        };

        await service.Guardar(pedido);
        var pedidoId = pedido.PedidoId;

        // Modificar
        var pedidoModificado = await service.Buscar(pedidoId);
        pedidoModificado!.NombrePedido = "Modificado";
        pedidoModificado.PedidoDetalles = new List<PedidoDetalle>
        {
            new PedidoDetalle { PedidoId = pedidoId, VehiculoId = 1, Cantidad = 7, PrecioUnitario = 28000 }
        };

        var result = await service.Guardar(pedidoModificado);
        var updated = await service.Buscar(pedidoId);

        Assert.True(result);
        Assert.Equal("Modificado", updated!.NombrePedido);
        Assert.Equal(7, updated.PedidoDetalles.First().Cantidad);
    }

    [Fact]
    public async Task Deshabilitar_Cambia_isActive_A_False_Y_Reduce_Stock()
    {
        var factory = GetFactory("DisablePedidoDB");
        var service = new PedidoServices(factory);
        var contexto = await factory.CreateDbContextAsync();

        contexto.Distribuidores.Add(new Distribuidor
        {
            DistribuidorId = 1,
            Nombre = "Distribuidor Test",
            isActive = true
        });

        contexto.Vehiculos.Add(new Vehiculo
        {
            VehiculoId = 1,
            MarcaVehiculo = "Mazda",
            ModeloVehiculo = "CX-5",
            AnioFabricacion = 2024,
            Precio = 35000,
            Kilometraje = 100,
            NumeroPuertas = 4,
            StockVehiculo = 30,
            isActive = true
        });
        await contexto.SaveChangesAsync();

        var pedido = new Pedido
        {
            NombrePedido = "Test D",
            DistribuidorId = 1,
            FechaPedido = DateTime.Now,
            Estado = "Pendiente",
            isActive = true,
            PedidoDetalles = new List<PedidoDetalle>
            {
                new PedidoDetalle { VehiculoId = 1, Cantidad = 5, PrecioUnitario = 35000 }
            }
        };

        await service.Guardar(pedido);
        var pedidoId = pedido.PedidoId;

        var result = await service.Deshabilitar(pedidoId);
        var disabled = await service.Buscar(pedidoId);

        var contexto2 = await factory.CreateDbContextAsync();
        var vehiculo = await contexto2.Vehiculos.FindAsync(1);

        Assert.True(result);
        Assert.False(disabled!.isActive);
        Assert.Equal(30, vehiculo!.StockVehiculo); // 30 + 5 - 5 = 30
    }

    [Fact]
    public async Task Habilitar_Cambia_isActive_A_True_Y_Aumenta_Stock()
    {
        var factory = GetFactory("EnablePedidoDB");
        var service = new PedidoServices(factory);
        var contexto = await factory.CreateDbContextAsync();

        contexto.Distribuidores.Add(new Distribuidor
        {
            DistribuidorId = 1,
            Nombre = "Distribuidor Test",
            isActive = true
        });

        contexto.Vehiculos.Add(new Vehiculo
        {
            VehiculoId = 1,
            MarcaVehiculo = "Ford",
            ModeloVehiculo = "Focus",
            AnioFabricacion = 2021,
            Precio = 22000,
            Kilometraje = 15000,
            NumeroPuertas = 4,
            StockVehiculo = 15,
            isActive = true
        });
        await contexto.SaveChangesAsync();

        var pedido = new Pedido
        {
            NombrePedido = "Test",
            DistribuidorId = 1,
            FechaPedido = DateTime.Now,
            Estado = "Pendiente",
            isActive = false,
            PedidoDetalles = new List<PedidoDetalle>
            {
                new PedidoDetalle { VehiculoId = 1, Cantidad = 4, PrecioUnitario = 22000 }
            }
        };

        await service.Guardar(pedido);
        var pedidoId = pedido.PedidoId;

        var result = await service.Habilitar(pedidoId);
        var enabled = await service.Buscar(pedidoId);

        var contexto2 = await factory.CreateDbContextAsync();
        var vehiculo = await contexto2.Vehiculos.FindAsync(1);

        Assert.True(result);
        Assert.True(enabled!.isActive);
        Assert.Equal(23, vehiculo!.StockVehiculo); // 15 + 4 + 4 = 23
    }

    [Fact]
    public async Task Eliminar_Borra_ElPedido_Y_Reduce_Stock()
    {
        var factory = GetFactory("DeletePedidoDB");
        var service = new PedidoServices(factory);
        var contexto = await factory.CreateDbContextAsync();

        contexto.Distribuidores.Add(new Distribuidor
        {
            DistribuidorId = 1,
            Nombre = "Distribuidor Test",
            isActive = true
        });

        contexto.Vehiculos.Add(new Vehiculo
        {
            VehiculoId = 1,
            MarcaVehiculo = "Nissan",
            ModeloVehiculo = "Sentra",
            AnioFabricacion = 2023,
            Precio = 24000,
            Kilometraje = 1000,
            NumeroPuertas = 4,
            StockVehiculo = 25,
            isActive = true
        });
        await contexto.SaveChangesAsync();

        var pedido = new Pedido
        {
            NombrePedido = "Eliminar",
            DistribuidorId = 1,
            FechaPedido = DateTime.Now,
            Estado = "Pendiente",
            isActive = true,
            PedidoDetalles = new List<PedidoDetalle>
            {
                new PedidoDetalle { VehiculoId = 1, Cantidad = 8, PrecioUnitario = 24000 }
            }
        };

        await service.Guardar(pedido);
        var pedidoId = pedido.PedidoId;

        var deleted = await service.Eliminar(pedidoId);
        var result = await service.Buscar(pedidoId);

        var contexto2 = await factory.CreateDbContextAsync();
        var vehiculo = await contexto2.Vehiculos.FindAsync(1);

        Assert.True(deleted);
        Assert.Null(result);
        Assert.Equal(25, vehiculo!.StockVehiculo); // 25 + 8 - 8 = 25
    }

    [Fact]
    public async Task EnviarPedido_Cambia_Estado_A_Enviado()
    {
        var factory = GetFactory("EnviarPedidoDB");
        var service = new PedidoServices(factory);
        var contexto = await factory.CreateDbContextAsync();

        contexto.Distribuidores.Add(new Distribuidor
        {
            DistribuidorId = 1,
            Nombre = "Distribuidor Test",
            isActive = true
        });

        contexto.Vehiculos.Add(new Vehiculo
        {
            VehiculoId = 1,
            MarcaVehiculo = "Chevrolet",
            ModeloVehiculo = "Spark",
            AnioFabricacion = 2024,
            Precio = 18000,
            Kilometraje = 0,
            NumeroPuertas = 4,
            StockVehiculo = 20,
            isActive = true
        });
        await contexto.SaveChangesAsync();

        var pedido = new Pedido
        {
            NombrePedido = "Pedido Envío",
            DistribuidorId = 1,
            FechaPedido = DateTime.Now,
            Estado = "Pendiente",
            isActive = true,
            PedidoDetalles = new List<PedidoDetalle>
            {
                new PedidoDetalle { VehiculoId = 1, Cantidad = 2, PrecioUnitario = 18000 }
            }
        };

        await service.Guardar(pedido);
        var pedidoId = pedido.PedidoId;

        var result = await service.EnviarPedido(pedidoId);
        var enviado = await service.Buscar(pedidoId);

        Assert.True(result);
        Assert.Equal("Enviado", enviado!.Estado);
    }

    [Fact]
    public async Task ListarPedidos_Devuelve_Pedidos_Segun_Criterio()
    {
        var factory = GetFactory("ListarPedidosDB");
        var service = new PedidoServices(factory);
        var contexto = await factory.CreateDbContextAsync();

        contexto.Distribuidores.Add(new Distribuidor
        {
            DistribuidorId = 1,
            Nombre = "Distribuidor Test",
            isActive = true
        });

        contexto.Vehiculos.Add(new Vehiculo
        {
            VehiculoId = 1,
            MarcaVehiculo = "Hyundai",
            ModeloVehiculo = "Elantra",
            AnioFabricacion = 2023,
            Precio = 26000,
            Kilometraje = 500,
            NumeroPuertas = 4,
            StockVehiculo = 30,
            isActive = true
        });
        await contexto.SaveChangesAsync();

        await service.Guardar(new Pedido
        {
            NombrePedido = "Pedido Activo 1",
            DistribuidorId = 1,
            FechaPedido = DateTime.Now,
            Estado = "Pendiente",
            isActive = true,
            PedidoDetalles = new List<PedidoDetalle>
            {
                new PedidoDetalle { VehiculoId = 1, Cantidad = 3, PrecioUnitario = 26000 }
            }
        });

        await service.Guardar(new Pedido
        {
            NombrePedido = "Pedido Inactivo",
            DistribuidorId = 1,
            FechaPedido = DateTime.Now,
            Estado = "Pendiente",
            isActive = false,
            PedidoDetalles = new List<PedidoDetalle>
            {
                new PedidoDetalle { VehiculoId = 1, Cantidad = 2, PrecioUnitario = 26000 }
            }
        });

        await service.Guardar(new Pedido
        {
            NombrePedido = "Pedido Activo 2",
            DistribuidorId = 1,
            FechaPedido = DateTime.Now,
            Estado = "Pendiente",
            isActive = true,
            PedidoDetalles = new List<PedidoDetalle>
            {
                new PedidoDetalle { VehiculoId = 1, Cantidad = 5, PrecioUnitario = 26000 }
            }
        });

        var pedidosActivos = await service.ListarPedidos(p => p.isActive == true);

        Assert.Equal(2, pedidosActivos.Count);
        Assert.All(pedidosActivos, p => Assert.True(p.isActive));
    }

    [Fact]
    public async Task Existe_Devuelve_True_Cuando_Pedido_Existe()
    {
        var factory = GetFactory("ExistePedidoDB");
        var service = new PedidoServices(factory);
        var contexto = await factory.CreateDbContextAsync();

        contexto.Distribuidores.Add(new Distribuidor
        {
            DistribuidorId = 1,
            Nombre = "Distribuidor Test",
            isActive = true
        });

        contexto.Vehiculos.Add(new Vehiculo
        {
            VehiculoId = 1,
            MarcaVehiculo = "Kia",
            ModeloVehiculo = "Rio",
            AnioFabricacion = 2022,
            Precio = 20000,
            Kilometraje = 8000,
            NumeroPuertas = 4,
            StockVehiculo = 15,
            isActive = true
        });
        await contexto.SaveChangesAsync();

        var pedido = new Pedido
        {
            NombrePedido = "Pedido Existe",
            DistribuidorId = 1,
            FechaPedido = DateTime.Now,
            Estado = "Pendiente",
            isActive = true,
            PedidoDetalles = new List<PedidoDetalle>
            {
                new PedidoDetalle { VehiculoId = 1, Cantidad = 1, PrecioUnitario = 20000 }
            }
        };

        await service.Guardar(pedido);
        var pedidoId = pedido.PedidoId;

        var existe = await service.Existe(pedidoId);
        var noExiste = await service.Existe(999);

        Assert.True(existe);
        Assert.False(noExiste);
    }
}