using Application.Abstractions;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context { get; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var rs = await _context.SaveChangesAsync();
            return rs > 0;
        }
    }
}
