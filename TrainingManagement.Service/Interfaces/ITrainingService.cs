using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingManagement.Model;

namespace TrainingManagement.Service.Interfaces
{
    public interface ITrainingService
    {
        Task<Training> CreateTrainingAsync(Training training);
        Task<Training> GetTrainingByIdAsync(int id);
        Task<IEnumerable<Training>> GetAllTrainingsAsync();
        Task<TrainingSearchResult<Training>> SearchTrainingsAsync(TrainingSearchRequest request);
        Task<IEnumerable<Training>> GetTrainingsByDepartmentAsync(int departmentId);
        Task<bool> UpdateTrainingAsync(Training training);
        Task<bool> PublishTrainingAsync(int id);
        Task<bool> DeleteTrainingAsync(int id);
    }
}
