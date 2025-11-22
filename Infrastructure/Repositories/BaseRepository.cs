using Infrastructure.Data;
using System.Data;

namespace TaskRoutine.Infrastructure.Repositories;

public abstract class BaseRepository(IDbConnectionFactory factory)
{
    protected readonly IDbConnectionFactory _factory = factory;

    protected IDbConnection GetConnection() => _factory.CreateConnection();
}