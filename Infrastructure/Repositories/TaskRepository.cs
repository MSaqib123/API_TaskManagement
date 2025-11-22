// src/TaskRoutine.Infrastructure/Repositories/TaskRepository.cs (Inherited: TaskRepository(ConnectionOptions options) : BaseRepository(new SqlConnectionFactory(options)))
using Dapper;
using Domain.Entities;
using Infrastructure.Data;
using System.Data;

namespace TaskRoutine.Infrastructure.Repositories;

public class TaskRepository(ConnectionOptions options) : BaseRepository(new SqlConnectionFactory(options))
{
    public async Task<Guid> CreateTaskAsync(TaskItem task)
    {
        using var conn = GetConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@Title", task.Title);
        parameters.Add("@Description", task.Description);
        parameters.Add("@Priority", (int)task.Priority);
        parameters.Add("@Category", task.Category);
        parameters.Add("@Status", task.Status);
        parameters.Add("@Recurrence", task.Recurrence);
        parameters.Add("@DueDate", task.DueDate);
        parameters.Add("@OrderIndex", task.OrderIndex);
        parameters.Add("@Id", dbType: DbType.Guid, direction: ParameterDirection.Output);

        await conn.ExecuteAsync("sp_CreateTask", parameters, commandType: CommandType.StoredProcedure);
        return parameters.Get<Guid>("@Id");
    }

    public async Task UpdateTaskAsync(TaskItem task)
    {
        using var conn = GetConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@Id", task.Id);
        parameters.Add("@Title", task.Title);
        parameters.Add("@Description", task.Description);
        parameters.Add("@Priority", (int)task.Priority);
        parameters.Add("@Category", task.Category);
        parameters.Add("@Status", task.Status);
        parameters.Add("@Recurrence", task.Recurrence);
        parameters.Add("@DueDate", task.DueDate);

        await conn.ExecuteAsync("sp_UpdateTask", parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task ToggleCompleteAsync(Guid id)
    {
        using var conn = GetConnection();
        await conn.ExecuteAsync("sp_ToggleTaskComplete", new { Id = id }, commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteTaskAsync(Guid id)
    {
        using var conn = GetConnection();
        await conn.ExecuteAsync("sp_DeleteTask", new { Id = id }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync(string? search = null, string? category = null, string? status = null, int? priority = null, string? recurrence = null)
    {
        using var conn = GetConnection();
        return await conn.QueryAsync<TaskItem>("sp_GetAllTasks", new
        {
            SearchQuery = search,
            Category = category,
            Status = status,
            Priority = priority,
            Recurrence = recurrence
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateOrderAsync(Guid id, int newIndex)
    {
        using var conn = GetConnection();
        await conn.ExecuteAsync("sp_UpdateTaskOrder", new { Id = id, NewOrderIndex = newIndex }, commandType: CommandType.StoredProcedure);
    }

    public async Task ClearCompletedAsync()
    {
        using var conn = GetConnection();
        await conn.ExecuteAsync("sp_ClearCompletedTasks", commandType: CommandType.StoredProcedure);
    }
}