using Microsoft.EntityFrameworkCore;
using ProyectoFinal_AP1_AdonisMercado.DAL;

namespace ProyectoFinal_AP1_AdonisMercado.Tests;

public class TestDbFactory : IDbContextFactory<Contexto>
{
    private readonly DbContextOptions<Contexto> _options;

    public TestDbFactory(DbContextOptions<Contexto> options)
    {
        _options = options;
    }

    public Contexto CreateDbContext()
        => new Contexto(_options);

    public ValueTask<Contexto> CreateDbContextAsync(
        CancellationToken cancellationToken = default)
        => new ValueTask<Contexto>(new Contexto(_options));
}
