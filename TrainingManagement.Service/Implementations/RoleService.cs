using System.Collections.Generic;
using System.Threading.Tasks;
using SqlSugar;
using TrainingManagement.Model;

namespace TrainingManagement.Service
{
    public class RoleService : IRoleService
    {
        private readonly ISqlSugarClient _db;

        public RoleService(ISqlSugarClient db)
        {
            _db = db;
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            return await _db.Insertable(role).ExecuteReturnEntityAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _db.Queryable<Role>()
                .FirstAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _db.Queryable<Role>().ToListAsync();
        }

        public async Task<(IEnumerable<Role> data, int total)> SearchRolesAsync(string name, bool? isActive, int pageIndex, int pageSize)
        {
            var query = _db.Queryable<Role>();
            
            if (!string.IsNullOrEmpty(name))
                query = query.Where(r => r.Name.Contains(name));
            
            if (isActive.HasValue)
                query = query.Where(r => r.IsActive == isActive);

            var count = await query.CountAsync();
            var data = await query.ToPageListAsync(pageIndex, pageSize);

            return (data, count);
        }

        public async Task<bool> UpdateRoleAsync(Role role)
        {
            return await _db.Updateable(role).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> ToggleRoleStatusAsync(int id)
        {
            return await _db.Updateable<Role>()
                .SetColumns(r => r.IsActive == !r.IsActive)
                .Where(r => r.Id == id)
                .ExecuteCommandAsync() > 0;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            return await _db.Deleteable<Role>(id).ExecuteCommandAsync() > 0;
        }
    }
}