using System.Collections.Generic;
using System.Threading.Tasks;
using SqlSugar;
using TrainingManagement.Model;
using System.Linq;
using BCrypt.Net;

namespace TrainingManagement.Service
{
    public class UserService : IUserService
    {
        private readonly ISqlSugarClient _db;

        public UserService(ISqlSugarClient db)
        {
            _db = db;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.Password = HashPassword(user.Password);
            return await _db.Insertable(user).ExecuteReturnEntityAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _db.Queryable<User>()
                .Includes(u => u.Department)
                .Includes(u => u.Roles)
                .FirstAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _db.Queryable<User>()
                .Includes(u => u.Department)
                .Includes(u => u.Roles)
                .ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            return await _db.Updateable(user).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _db.Deleteable<User>(id).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> ResetPasswordAsync(int id, string newPassword)
        {
            return await _db.Updateable<User>()
                .SetColumns(u => u.Password == HashPassword(newPassword))
                .Where(u => u.Id == id)
                .ExecuteCommandAsync() > 0;
        }

        public async Task<bool> AssignDepartmentAsync(int userId, int departmentId)
        {
            return await _db.Updateable<User>()
                .SetColumns(u => u.DepartmentId == departmentId)
                .Where(u => u.Id == userId)
                .ExecuteCommandAsync() > 0;
        }

        public async Task<bool> AssignRoleAsync(int userId, int roleId)
        {
            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };
            return await _db.Insertable(userRole).ExecuteCommandAsync() > 0;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _db.Queryable<User>()
                .Where(u => u.Account == username)
                .FirstAsync();

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            return user;
        }
    }
}