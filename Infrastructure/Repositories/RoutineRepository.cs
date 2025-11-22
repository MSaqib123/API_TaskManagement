// src/TaskRoutine.Infrastructure/Repositories/RoutineRepository.cs (Similar Inheritance)
using Dapper;
using Domain.Entities;
using Infrastructure.Data;
using System.Data;

namespace TaskRoutine.Infrastructure.Repositories;

public class RoutineRepository(ConnectionOptions options) : BaseRepository(new SqlConnectionFactory(options))
{
    public async Task<Guid> CreateRoutineAsync(RoutineItem routine)
    {
        using var conn = GetConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@Title", routine.Title);
        parameters.Add("@Notes", routine.Notes);
        parameters.Add("@StartTime", routine.StartTime);
        parameters.Add("@EndTime", routine.EndTime);
        parameters.Add("@Id", dbType: DbType.Guid, direction: ParameterDirection.Output);

        await conn.ExecuteAsync("sp_CreateRoutine", parameters, commandType: CommandType.StoredProcedure);
        return parameters.Get<Guid>("@Id");
    }

    public async Task DeleteRoutineAsync(Guid id)
    {
        using var conn = GetConnection();
        await conn.ExecuteAsync("sp_DeleteRoutine", new { Id = id }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RoutineItem>> GetAllRoutinesAsync()
    {
        using var conn = GetConnection();
        return await conn.QueryAsync<RoutineItem>("sp_GetAllRoutines", commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RoutineItem>> GetDueRoutinesAsync(string currentTime)
    {
        using var conn = GetConnection();
        return await conn.QueryAsync<RoutineItem>("sp_GetDueRoutines", new { CurrentTime = currentTime }, commandType: CommandType.StoredProcedure);
    }
}