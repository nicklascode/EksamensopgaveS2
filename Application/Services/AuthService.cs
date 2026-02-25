using DataAccess.Repositories;
using Domain.Entities;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class AuthService(IGenericRepository<User> userRepo) : IAuth
    {
        public async Task<int?> LoginAsync(UserDto req)
        {
            var users = await userRepo.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Name == req.Name);

            // User not found
            if (user == null)
            {
                return null;
            }

            return user.Id;
        }

        public async Task<User?> RegisterAsync(UserDto req)
        {
            if (userRepo.GetAllAsync().Result.Any(u => u.Name == req.Name))
                return null;

            var user = new User();

            user.Name = req.Name;
            user.Email = req.Email;

            await userRepo.AddAsync(user);

            return user;
        }
    }
}
