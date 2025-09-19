using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingManagement.Model;

namespace TrainingManagement.Service
{
    public interface IQuestionService
    {
        Task<Question> CreateQuestionAsync(Question question);
        Task<Question> GetQuestionByIdAsync(int id);
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<IEnumerable<Question>> SearchQuestionsAsync(string title, string? difficulty, string? type);
        Task<bool> UpdateQuestionAsync(Question question);
        Task<bool> DeleteQuestionAsync(int id);
    }
}