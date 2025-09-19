﻿﻿﻿﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingManagement.Model;
using TrainingManagement.Model.Dtos;
using TrainingManagement.Service.Interfaces;

namespace TrainingManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        /// <summary>
        /// 创建新的培训
        /// </summary>
        /// <param name="training">培训信息</param>
        /// <returns>创建成功的培训对象</returns>
        [HttpPost("create")]
        public async Task<ActionResult<TrainingDto>> Create([FromBody] TrainingCreateDto dto)
        {
            var training = new Training
            {
                Name = dto.Name,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                DepartmentId = dto.DepartmentId
            };

            var result = await _trainingService.CreateTrainingAsync(training);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, 
                new TrainingDto 
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    StartDate = result.StartDate,
                    EndDate = result.EndDate,
                    DepartmentId = result.DepartmentId,
                    IsPublished = result.IsPublished
                });
        }

        /// <summary>
        /// 根据ID获取培训信息
        /// </summary>
        /// <param name="id">培训ID</param>
        /// <returns>培训对象</returns>
        [HttpGet("get")]
        public async Task<ActionResult<TrainingDto>> Get(int id)
        {
            var training = await _trainingService.GetTrainingByIdAsync(id);
            return training != null ? Ok(new TrainingDto 
            {
                Id = training.Id,
                Name = training.Name,
                Description = training.Description,
                StartDate = training.StartDate,
                EndDate = training.EndDate,
                DepartmentId = training.DepartmentId,
                IsPublished = training.IsPublished
            }) : NotFound();
        }

        /// <summary>
        /// 获取所有培训列表
        /// </summary>
        /// <returns>培训列表</returns>
        [HttpGet("get_all")]
        public async Task<ActionResult<IEnumerable<TrainingDto>>> GetAll()
        {
            var trainings = await _trainingService.GetAllTrainingsAsync();
            return Ok(trainings.Select(t => new TrainingDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                DepartmentId = t.DepartmentId,
                IsPublished = t.IsPublished
            }));
        }

        /// <summary>
        /// 根据部门ID获取培训列表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>培训列表</returns>
        [HttpGet("get_by_department")]
        public async Task<ActionResult<IEnumerable<TrainingDto>>> GetByDepartment(int departmentId)
        {
            var trainings = await _trainingService.GetTrainingsByDepartmentAsync(departmentId);
            return Ok(trainings.Select(t => new TrainingDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                DepartmentId = t.DepartmentId,
                IsPublished = t.IsPublished
            }));
        }

        /// <summary>
        /// 更新培训信息
        /// </summary>
        /// <param name="id">培训ID</param>
        /// <param name="training">更新后的培训信息</param>
        /// <returns>操作结果</returns>
        [HttpPost("upload")]
        public async Task<IActionResult> Update( [FromBody] TrainingDto dto)
        {
          
            var training = new Training
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                DepartmentId = dto.DepartmentId,
                IsPublished = dto.IsPublished
            };

            return await _trainingService.UpdateTrainingAsync(training) ? NoContent() : NotFound();
        }

        /// <summary>
        /// 发布培训
        /// </summary>
        /// <param name="id">培训ID</param>
        /// <returns>操作结果</returns>
        [HttpGet("publish")]
        public async Task<IActionResult> Publish(int id)
        {
            return await _trainingService.PublishTrainingAsync(id) ? NoContent() : NotFound();
        }

        /// <summary>
        /// 删除培训
        /// </summary>
        /// <param name="id">培训ID</param>
        /// <returns>操作结果</returns>
        [HttpGet("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _trainingService.DeleteTrainingAsync(id) ? NoContent() : NotFound();
        }
    }
}
