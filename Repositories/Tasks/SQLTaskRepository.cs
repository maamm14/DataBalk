using Microsoft.EntityFrameworkCore;
using UserTaskApi.Data;
using UserTaskApi.Models.Domain;
using UserTaskApi.Repositories.TaskRepo;

namespace UserTaskApi.Repositories.Task
{
    public class SQLTaskRepository : ITaskRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLTaskRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all tasks including the user they are assigned to
        public async Task<List<TaskDomain>> GetAllAsync()
        {
            return await _dbContext.Tasks.Include(t => t.AssignedUser).ToListAsync();
        }

        // Get a task by its Id including the user it is assigned to
        public async Task<TaskDomain?> GetByIdAsync(int Id)
        {
            return await _dbContext.Tasks.Include(t => t.AssignedUser).FirstOrDefaultAsync(x => x.Id == Id);
        }

        // Create a new task and add it to the database
        public async Task<TaskDomain> CreateAsync(TaskDomain tasks)
        {
            // Adding a new task to the database
            await _dbContext.Tasks.AddAsync(tasks);
            // Saving changes to the database
            await _dbContext.SaveChangesAsync();
            // Returning the created task response
            return tasks;
        }

        // Update an existing task in the database
        public async Task<TaskDomain?> UpdateAsync(int id, TaskDomain tasks)
        {
            // Find the existing task by its Id
            TaskDomain? existingTask = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            // Check if the task exists; if not, return null
            if (existingTask == null)
            {
                return null;
            }
            // Update the existing task's properties
            existingTask.AssigneeId = tasks.AssigneeId;
            existingTask.Title = tasks.Title;
            existingTask.Description = tasks.Description;
            existingTask.DueDate = tasks.DueDate;
            // Save changes to the database
            await _dbContext.SaveChangesAsync();
            // Return the updated task response
            return existingTask;
        }

        // Delete a task from the database
        public async Task<TaskDomain?> DeleteAsync(int id)
        {
            // Find the task to delete by its Id
            TaskDomain? existingTask = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            // Check if the task exists; if not, return null
            if (existingTask == null)
            {
                return null;
            }
            // Remove the task from the database
            _dbContext.Tasks.Remove(existingTask);
            // Save changes to the database
            await _dbContext.SaveChangesAsync();
            // Return the deleted task response
            return existingTask;
        }

        public async Task<List<TaskDomain>> GetTasksByUserIdAsync(string? userId)
        {
            return await _dbContext.Tasks
                            .Include(t => t.AssignedUser)
                            .Where(x => x.AssigneeId == userId)
                            .ToListAsync();
        }
    }
}
