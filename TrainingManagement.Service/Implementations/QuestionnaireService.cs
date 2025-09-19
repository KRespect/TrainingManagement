using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
using TrainingManagement.Model;
using TrainingManagement.Model.Dtos;

namespace TrainingManagement.Service
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly ISqlSugarClient _db;

        public QuestionnaireService(ISqlSugarClient db)
        {
            _db = db;
        }

        public async Task<QuestionnaireDto> CreateQuestionnaireAsync(QuestionnaireCreateDto dto)
        {
            var questionnaire = new Questionnaire
            {
                Title = dto.Title,
                Description = dto.Description,
                DepartmentId = dto.DepartmentId,
                CreateTime = DateTime.Now
            };
            var result = await _db.Insertable(questionnaire).ExecuteReturnEntityAsync();
            return ToDto(result);
        }

        public async Task<bool> DeleteQuestionnaireAsync(int id)
        {
            return await _db.Deleteable<Questionnaire>(id).ExecuteCommandAsync() > 0;
        }

        public async Task<IEnumerable<QuestionnaireDto>> GetAllQuestionnairesAsync()
        {
            var questionnaires = await _db.Queryable<Questionnaire>()
                .Includes(q => q.Questions, q => q.Question)
                .Includes(q => q.Department)
                .ToListAsync();
            return questionnaires.Select(ToDto);
        }

        public async Task<IEnumerable<QuestionnaireDto>> GetQuestionnairesByDepartmentAsync(int departmentId)
        {
            var questionnaires = await _db.Queryable<Questionnaire>()
                .Where(q => q.DepartmentId == departmentId)
                .Includes(q => q.Questions, q => q.Question)
                .ToListAsync();
            return questionnaires.Select(ToDto);
        }

        public async Task<QuestionnaireDto> GetQuestionnaireByIdAsync(int id)
        {
            var questionnaire = await _db.Queryable<Questionnaire>()
                .Includes(q => q.Questions, q => q.Question)
                .Includes(q => q.Department)
                .FirstAsync(x => x.Id == id);
            return questionnaire != null ? ToDto(questionnaire) : null;
        }

        public async Task<bool> UpdateQuestionnaireAsync(QuestionnaireDto dto)
        {
            var questionnaire = ToModel(dto);
            return await _db.Updateable(questionnaire).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> PublishQuestionnaireAsync(int id)
        {
            var questionnaire = await _db.Queryable<Questionnaire>()
                .Where(q => q.Id == id)
                .FirstAsync();

            if (questionnaire == null)
            {
                throw new Exception("Questionnaire not found");
            }

            if (questionnaire.Status == Model.QuestionnaireStatus.Published)
            {
                return true;
            }

            questionnaire.Status = Model.QuestionnaireStatus.Published;
            return await _db.Updateable(questionnaire).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> AddQuestionToQuestionnaireAsync(int questionnaireId, int questionId)
        {
            var item = new QuestionnaireQuestion
            {
                QuestionnaireId = questionnaireId,
                QuestionId = questionId,
                SortOrder = await GetNextSortOrder(questionnaireId)
            };
            return await _db.Insertable(item).ExecuteCommandAsync() > 0;
        }

        private async Task<int> GetNextSortOrder(int questionnaireId)
        {
            var maxOrder = await _db.Queryable<QuestionnaireQuestion>()
                .Where(q => q.QuestionnaireId == questionnaireId)
                .MaxAsync(q => q.SortOrder);
            return maxOrder + 1;
        }

        private QuestionnaireDto ToDto(Questionnaire questionnaire)
        {
            return new QuestionnaireDto
            {
                Id = questionnaire.Id,
                Title = questionnaire.Title,
                Description = questionnaire.Description,
                DepartmentId = questionnaire.DepartmentId,
                CreateTime = questionnaire.CreateTime
            };
        }

        private Questionnaire ToModel(QuestionnaireDto dto)
        {
            return new Questionnaire
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                DepartmentId = dto.DepartmentId,
                CreateTime = dto.CreateTime
            };
        }

        public async Task<(IEnumerable<QuestionnaireDto> data, int total)> SearchQuestionnairesAsync(string name, int pageIndex, int pageSize)
        {
            var query = _db.Queryable<Questionnaire>()
                .Includes(q => q.Questions, q => q.Question)
                .Includes(q => q.Department);
            
            if (!string.IsNullOrEmpty(name))
                query = query.Where(q => q.Title.Contains(name));

            var count = await query.CountAsync();
            var data = await query.ToPageListAsync(pageIndex, pageSize);

            return (data.Select(ToDto), count);
        }
    }
}