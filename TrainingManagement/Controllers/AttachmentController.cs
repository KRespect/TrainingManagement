using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrainingManagement.Model;
using TrainingManagement.Service;
using TrainingManagement.Model;

namespace TrainingManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="dto">附件上传信息</param>
        /// <returns>上传结果</returns>
        [HttpPost(template: "upload")]
        public async Task<IActionResult> Upload([FromForm] AttachmentUploadDto dto)
        {
            var result = await _attachmentService.UploadAsync(dto);
            return Ok(result);
        }

        /// <summary>
        /// 下载附件
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <returns>文件流</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var file = await _attachmentService.DownloadAsync(id);
            return File(file.Content, file.ContentType, file.FileName);
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <returns>操作结果</returns>
        [HttpGet("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _attachmentService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <param name="trainingId">培训ID（可选）</param>
        /// <param name="questionnaireId">问卷ID（可选）</param>
        /// <returns>附件列表</returns>
        [HttpGet("get_list")]
        public async Task<IActionResult> GetList([FromQuery] int? trainingId, [FromQuery] int? questionnaireId)
        {
            var list = await _attachmentService.GetListAsync(trainingId, questionnaireId);
            return Ok(list);
        }

        /// <summary>
        /// 关联附件到培训或问卷
        /// </summary>
        /// <param name="dto">附件关联信息</param>
        /// <returns>操作结果</returns>
        [HttpPost("link")]
        public async Task<IActionResult> LinkAttachment([FromBody] AttachmentLinkDto dto)
        {
            await _attachmentService.LinkAttachmentAsync(dto);
            return Ok();
        }
    }
}