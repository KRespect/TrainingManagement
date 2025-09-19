using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingManagement.Model;
using TrainingManagement.Model.Dtos;

namespace TrainingManagement.Service
{
    public interface IQuestionnaireService
    {
        Task<QuestionnaireDto> CreateQuestionnaireAsync(QuestionnaireCreateDto dto);
        Task<QuestionnaireDto> GetQuestionnaireByIdAsync(int id);
        Task<IEnumerable<QuestionnaireDto>> GetAllQuestionnairesAsync();
        Task<(IEnumerable<QuestionnaireDto> data, int total)> SearchQuestionnairesAsync(string name, int pageIndex, int pageSize);
        Task<IEnumerable<QuestionnaireDto>> GetQuestionnairesByDepartmentAsync(int departmentId);
        Task<bool> UpdateQuestionnaireAsync(QuestionnaireDto questionnaire);
        Task<bool> PublishQuestionnaireAsync(int id);
        Task<bool> AddQuestionToQuestionnaireAsync(int questionnaireId, int questionId);
        Task<bool> DeleteQuestionnaireAsync(int id);
    }
}