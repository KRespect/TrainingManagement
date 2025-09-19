using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
using TrainingManagement.Model;
using TrainingManagement.Service.Interfaces;

namespace TrainingManagement.Service.Implementations
{
    public class TrainingService : ITrainingService
    {
        private readonly ISqlSugarClient _db;

        public TrainingService(ISqlSugarClient db)
        {
            _db = db;
        }

        public async Task<Training> CreateTrainingAsync(Training training)
        {
            training.CreateTime = DateTime.Now;
            return await _db.Insertable(training).ExecuteReturnEntityAsync();
        }

        public async Task<bool> DeleteTrainingAsync(int id)
        {
            return await _db.Deleteable<Training>(id).ExecuteCommandAsync() > 0;
        }

        public async Task<IEnumerable<Training>> GetAllTrainingsAsync()
        {
            return await _db.Queryable<Training>()
                .Includes(t => t.Department)
                .Includes(t => t.Creator)
                .ToListAsync();
        }

        public async Task<Training> GetTrainingByIdAsync(int id)
        {
            return await _db.Queryable<Training>()
                .Includes(t => t.Department)
                .Includes(t => t.Creator)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateTrainingAsync(Training training)
        {
            return await _db.Updateable(training).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> PublishTrainingAsync(int id)
        {
            var training = await _db.Queryable<Training>()
                .Where(t => t.Id == id)
                .FirstAsync();

            if (training == null)
            {
                throw new Exception("Training not found");
            }

            if (training.Status == Model.TrainingStatus.Published)
            {
                return true;
            }

            training.Status = Model.TrainingStatus.Published;
            return await _db.Updateable(training).ExecuteCommandAsync() > 0;
        }

        public async Task<IEnumerable<Training>> GetTrainingsByDepartmentAsync(int departmentId)
        {
            return await _db.Queryable<Training>()
                .Where(t => t.DepartmentId == departmentId)
                .ToListAsync();
        }

        public async Task<TrainingSearchResult<Training>> SearchTrainingsAsync(TrainingSearchRequest request)
        {
            var query = _db.Queryable<Training>()
                .Includes(t => t.Department)
                .Includes(t => t.Creator);
            
            if (!string.IsNullOrEmpty(request.Name))
                query = query.Where(t => t.Name.Contains(request.Name));
            
            if (request.IsPublished.HasValue)
                query = query.Where(t => t.IsPublished == request.IsPublished);

            var count = await query.CountAsync();
            var data = await query.ToPageListAsync(request.PageIndex, request.PageSize);

            return new TrainingSearchResult<Training>
            {
                TotalCount = count,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }
    }
}