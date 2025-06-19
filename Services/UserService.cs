using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users;
        private int _nextId = 1;

        public UserService()
        {
            _users = new List<User>
            {
                new User { Id = _nextId++, Name = "張三", Email = "zhang.san@example.com", Phone = "0912345678", CreatedAt = DateTime.UtcNow.AddDays(-10) },
                new User { Id = _nextId++, Name = "李四", Email = "li.si@example.com", Phone = "0923456789", CreatedAt = DateTime.UtcNow.AddDays(-5) },
                new User { Id = _nextId++, Name = "王五", Email = "wang.wu@example.com", Phone = "0934567890", CreatedAt = DateTime.UtcNow.AddDays(-2) }
            };
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await Task.FromResult(_users.Where(u => u.IsActive));
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id && u.IsActive);
            return await Task.FromResult(user);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.Id = _nextId++;
            user.CreatedAt = DateTime.UtcNow;
            user.IsActive = true;
            
            _users.Add(user);
            return await Task.FromResult(user);
        }

        public async Task<User> UpdateUserAsync(int id, User userUpdate)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id && u.IsActive);
            if (existingUser == null)
                return null;

            existingUser.Name = userUpdate.Name;
            existingUser.Email = userUpdate.Email;
            existingUser.Phone = userUpdate.Phone;

            return await Task.FromResult(existingUser);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id && u.IsActive);
            if (user == null)
                return false;

            user.IsActive = false;
            return await Task.FromResult(true);
        }
    }
} 