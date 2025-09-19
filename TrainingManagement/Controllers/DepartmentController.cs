using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingManagement.Model;
using TrainingManagement.Model.Dtos;
using TrainingManagement.Service.Interfaces;

namespace TrainingManagement.Controllers
{
    /// <summary>
    /// 部门管理API控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        /// 获取部门树形结构
        /// </summary>
        /// <returns>部门树形结构</returns>
        [HttpGet("tree")]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetTree()
        {
            var departments = await _departmentService.GetTreeAsync();
            return Ok(departments);
        }

        /// <summary>
        /// 创建新部门
        /// </summary>
        /// <param name="dto">部门创建DTO</param>
        /// <returns>创建成功的部门信息</returns>
        [HttpPost("create")]
        public async Task<ActionResult<DepartmentDto>> Create([FromBody] DepartmentCreateDto dto)
        {
            var result = await _departmentService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        /// <summary>
        /// 获取指定ID的部门信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>部门详细信息</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> Get(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            return department != null ? Ok(department) : NotFound();
        }

        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="dto">部门更新DTO</param>
        /// <returns>操作结果</returns>
        [HttpPost("update/{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] DepartmentDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("id error");
            }

            var res=await _departmentService.UpdateAsync(dto) ;
            return res;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>操作结果</returns>
        [HttpGet("delete")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _departmentService.DeleteAsync(id);
        }
    }
}