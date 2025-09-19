using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingManagement.Model;
using TrainingManagement.Model.Dtos;
using TrainingManagement.Service;

namespace TrainingManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireController : ControllerBase
    {
        private readonly IQuestionnaireService _questionnaireService;

        public QuestionnaireController(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }

        /// <summary>
        /// 创建新的问卷
        /// </summary>
        /// <param name="questionnaire">问卷信息</param>
        /// <returns>创建成功的问卷对象</returns>
        [HttpPost("create")]
        public async Task<ActionResult<QuestionnaireDto>> Create([FromBody] QuestionnaireCreateDto dto)
        {
            var result = await _questionnaireService.CreateQuestionnaireAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        /// <summary>
        /// 根据ID获取问卷信息
        /// </summary>
        /// <param name="id">问卷ID</param>
        /// <returns>问卷对象</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionnaireDto>> Get(int id)
        {
            var questionnaire = await _questionnaireService.GetQuestionnaireByIdAsync(id);
            return questionnaire != null ? Ok(questionnaire) : NotFound();
        }

        /// <summary>
        /// 获取所有问卷列表
        /// </summary>
        /// <returns>问卷列表</returns>
        [HttpGet("get_all")]
        public async Task<ActionResult<IEnumerable<QuestionnaireDto>>> GetAll()
        {
            return Ok(await _questionnaireService.GetAllQuestionnairesAsync());
        }

        /// <summary>
        /// 根据部门ID获取问卷列表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>问卷列表</returns>
        [HttpGet("get_by_department")]
        public async Task<ActionResult<IEnumerable<QuestionnaireDto>>> GetByDepartment(int departmentId)
        {
            return Ok(await _questionnaireService.GetQuestionnairesByDepartmentAsync(departmentId));
        }

        /// <summary>
        /// 更新问卷信息
        /// </summary>
        /// <param name="id">问卷ID</param>
        /// <param name="questionnaire">更新后的问卷信息</param>
        /// <returns>操作结果</returns>
        [HttpPost("update")]
        public async Task<IActionResult> Update( [FromBody] QuestionnaireDto dto)
        {
            return await _questionnaireService.UpdateQuestionnaireAsync(dto) ? NoContent() : NotFound();
        }

        /// <summary>
        /// 发布问卷
        /// </summary>
        /// <param name="id">问卷ID</param>
        /// <returns>操作结果</returns>
        [HttpGet("publish")]
        public async Task<IActionResult> Publish(int id)
        {
            return await _questionnaireService.PublishQuestionnaireAsync(id) ? NoContent() : NotFound();
        }

        /// <summary>
        /// 向问卷中添加题目
        /// </summary>
        /// <param name="questionnaireId">问卷ID</param>
        /// <param name="questionId">题目ID</param>
        /// <returns>操作结果</returns>
        [HttpGet("add_question")]
        public async Task<IActionResult> AddQuestion(int questionnaireId, int questionId)
        {
            return await _questionnaireService.AddQuestionToQuestionnaireAsync(questionnaireId, questionId) 
                ? NoContent() 
                : BadRequest("添加题目失败");
        }

        /// <summary>
        /// 删除问卷
        /// </summary>
        /// <param name="id">问卷ID</param>
        /// <returns>操作结果</returns>
        [HttpGet("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _questionnaireService.DeleteQuestionnaireAsync(id) ? NoContent() : NotFound();
        }
    }
}