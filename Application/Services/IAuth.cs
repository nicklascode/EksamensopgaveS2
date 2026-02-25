using Domain.Entities;
using Domain.Models;

namespace Application.Services
{
    public interface IAuth
    {
        Task<User?> RegisterAsync(UserDto req);
        Task<int?> LoginAsync(UserDto req);
    }
}
