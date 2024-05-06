using UserTaskApi.Models.Domain;

namespace UserTaskApi.Repositories.TaskRepo
{
    public interface ITaskRepository
    {
        Task<List<TaskDomain>> GetAllAsync();

        Task<TaskDomain?> GetByIdAsync(int id);

        Task<TaskDomain> CreateAsync(TaskDomain tasks);

        Task<TaskDomain?> UpdateAsync(int id, TaskDomain tasks);

        Task<TaskDomain?> DeleteAsync(int id);
        Task<List<TaskDomain>> GetTasksByUserIdAsync(string? userId);
    }
}