using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingManagement.Model;

namespace TrainingManagement.Service
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ResetPasswordAsync(int id, string newPassword);
        Task<bool> AssignDepartmentAsync(int userId, int departmentId);
        Task<bool> AssignRoleAsync(int userId, int roleId);
        Task<User> Authenticate(string username, string password);
    }
}