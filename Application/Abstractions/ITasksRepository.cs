using Domain.Entities;

namespace Application.Abstractions
{
    public interface ITasksRepository
    {
        Task<IEnumerable<Tasks>> GetAsync();
        Task<Tasks?> GetByIdAsync(Guid? Id);
        Task CreateAsync(Tasks task);
        Task UpdateAsync(Tasks task);
        Task DeleteAsync(Tasks task);
    }
}
