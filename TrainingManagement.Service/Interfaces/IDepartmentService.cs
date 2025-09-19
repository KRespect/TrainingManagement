using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingManagement.Model;
using TrainingManagement.Model.Dtos;

namespace TrainingManagement.Service.Interfaces
{
    public interface IDepartmentService
    {
        Task<DepartmentDto> CreateAsync(DepartmentCreateDto dto);
        Task<DepartmentDto> GetByIdAsync(int id);
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task<IEnumerable<DepartmentDto>> GetTreeAsync();
        Task<bool> UpdateAsync(DepartmentDto department);
        Task<bool> DeleteAsync(int id);
    }
}