using Domain.Entities;

namespace Application.Abstractions
{
    public interface IJwtProvider
    {
        string Genereate(ApplicationUser user);
    }
}
