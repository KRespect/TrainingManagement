using System.Collections.Generic;
using System.Threading.Tasks;
using SqlSugar;
using TrainingManagement.Model;
using System.Linq;

namespace TrainingManagement.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly ISqlSugarClient _db;

        public QuestionService(ISqlSugarClient db)
        {
            _db = db;
        }

        public async Task<Question> CreateQuestionAsync(Question question)
        {
            return await _db.Insertable(question).ExecuteReturnEntityAsync();
        }

        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            return await _db.Queryable<Question>()
                .FirstAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await _db.Queryable<Question>().ToListAsync();
        }

        public async Task<IEnumerable<Question>> SearchQuestionsAsync(string title, string? difficulty, string? type)
        {
            var query = _db.Queryable<Question>();
            
            if (!string.IsNullOrEmpty(title))
                query = query.Where(q => q.Title.Contains(title));
            
            if (!string.IsNullOrEmpty(difficulty))
                query = query.Where(q => q.Difficulty.ToString() == difficulty);
            
            if (!string.IsNullOrEmpty(type))
                query = query.Where(q => q.Type.ToString() == type);

            return await query.ToListAsync();
        }

        public async Task<bool> UpdateQuestionAsync(Question question)
        {
            return await _db.Updateable(question).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            return await _db.Deleteable<Question>(id).ExecuteCommandAsync() > 0;
        }
    }
}