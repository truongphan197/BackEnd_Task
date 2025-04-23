using Domain.Entities;
using Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public TasksRepository(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Tasks>> GetAsync()
        {
            var rs = await _context.Tasks.ToListAsync();
            return rs;
        }

        public async Task CreateAsync(Tasks task)
        {
            var rs = await _context.Tasks.AddAsync(task);
        }

        public async Task<Tasks?> GetByIdAsync(Guid? Id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == Id);
            if (task == null)
                return null;

            return task;
        }

        public async Task UpdateAsync(Tasks task)
        {
            _context.Tasks.Update(task);
        }

        public async Task DeleteAsync(Tasks task)
        {            
            _context.Tasks.Remove(task);
        }
    }
}
