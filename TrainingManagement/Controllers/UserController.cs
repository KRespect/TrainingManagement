using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingManagement.Model;
using TrainingManagement.Service;
using TrainingManagement.Service;

namespace TrainingManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>创建成功的用户对象</returns>
        [HttpPost("create")]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            var result = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        /// <summary>
        /// 根据ID获取用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>用户对象</returns>
        [HttpGet("get")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user != null ? Ok(user) : NotFound();
        }

        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        [HttpGet("get_all")]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="user">更新后的用户信息</param>
        /// <returns>操作结果</returns>
        [HttpPost("update")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            return await _userService.UpdateUserAsync(user) ? NoContent() : NotFound();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>操作结果</returns>
        [HttpGet("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _userService.DeleteUserAsync(id) ? NoContent() : NotFound();
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>操作结果</returns>
        [HttpPost("{id}/password")]
        public async Task<IActionResult> ResetPassword(int id, [FromBody] string newPassword)
        {
            return await _userService.ResetPasswordAsync(id, newPassword) ? NoContent() : NotFound();
        }

        /// <summary>
        /// 分配用户到部门
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="departmentId">部门ID</param>
        /// <returns>操作结果</returns>
        [HttpGet("assign_department")]
        public async Task<IActionResult> AssignDepartment(int id, int departmentId)
        {
            return await _userService.AssignDepartmentAsync(id, departmentId) ? NoContent() : NotFound();
        }

        /// <summary>
        /// 为用户分配角色
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns>操作结果</returns>
        [HttpGet("assign_role")]
        public async Task<IActionResult> AssignRole(int id, int roleId)
        {
            return await _userService.AssignRoleAsync(id, roleId) ? NoContent() : NotFound();
        }
    }
}