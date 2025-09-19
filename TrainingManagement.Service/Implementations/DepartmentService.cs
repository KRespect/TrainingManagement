using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SqlSugar;
using TrainingManagement.Model;
using TrainingManagement.Model.Dtos;
using TrainingManagement.Service.Interfaces;

namespace TrainingManagement.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ISqlSugarClient _db;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(ISqlSugarClient db, ILogger<DepartmentService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<DepartmentDto> CreateAsync(DepartmentCreateDto dto)
        {
            var department = new Department
            {
                Name = dto.Name,
                Code = dto.Code,
                ParentId = dto.ParentId,
                CreateTime = DateTime.Now
            };
            var result = await _db.Insertable(department).ExecuteReturnEntityAsync();
            return ToDto(result);
        }

        public async Task<DepartmentDto> GetByIdAsync(int id)
        {
            var department = await _db.Queryable<Department>()
                .Where(d => d.Id == id)
                .FirstAsync();
            return department != null ? ToDto(department) : null;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            return await _db.Queryable<Department>()
                .Select(t=>new DepartmentDto{
                    Id = t.Id,
                    Name = t.Name,
                    Code = t.Code,
                    ParentId = t.ParentId,
                    CreateTime = t.CreateTime
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<DepartmentDto>> GetTreeAsync()
        {
            var allDepartments = await _db.Queryable<Department>() .Select(t=>new DepartmentDto{
                    Id = t.Id,
                    Name = t.Name,
                    Code = t.Code,
                    ParentId = t.ParentId,
                   
                }).ToListAsync();
            return BuildDepartmentTree(allDepartments, 0);
        }

        private IEnumerable<DepartmentDto> BuildDepartmentTree(List<DepartmentDto> departments, int? parentId)
        {
            return departments
                .Where(d => d.ParentId == parentId)
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Code = d.Code,
                    ParentId = d.ParentId,
                    Children = BuildDepartmentTree(departments, d.Id).ToList()
                });
        }

        public async Task<bool> UpdateAsync(DepartmentDto department)
        {
            var model = ToModel(department);
            return await _db.Updateable(model)
                .ExecuteCommandAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _db.Deleteable<Department>()
                .Where(d => d.Id == id)
                .ExecuteCommandAsync() > 0;
        }

        private DepartmentDto ToDto(Department department)
        {
            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                ParentId = department.ParentId,
                CreateTime = department.CreateTime
            };
        }
         private Department ToModel(DepartmentDto department)
        {
            return new Department
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                ParentId = department.ParentId,
                CreateTime = department.CreateTime
            };
        }
    }
}
