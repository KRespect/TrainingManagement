using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingManagement.Model;

namespace TrainingManagement.Service
{
    public interface IRoleService
    {
        Task<Role> CreateRoleAsync(Role role);
        Task<Role> GetRoleByIdAsync(int id);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<(IEnumerable<Role> data, int total)> SearchRolesAsync(string name, bool? isActive, int pageIndex, int pageSize);
        Task<bool> UpdateRoleAsync(Role role);
        Task<bool> ToggleRoleStatusAsync(int id);
        Task<bool> DeleteRoleAsync(int id);
    }
}