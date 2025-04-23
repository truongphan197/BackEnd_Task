
namespace Application.Abstractions
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
    }
}
