using System.IO;
using System.Threading.Tasks;
using SqlSugar;
using TrainingManagement.Model;

namespace TrainingManagement.Service
{
    public class AttachmentService : IAttachmentService
    {
        private readonly ISqlSugarClient _db;
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        public AttachmentService(ISqlSugarClient db)
        {
            _db = db;
        }

        public async Task<Attachment> UploadAsync(AttachmentUploadDto dto)
        {
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.File.FileName)}";
            var filePath = Path.Combine(_uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            return new Attachment
            {
                Name = dto.File.FileName,
                Url = $"/uploads/{fileName}",
                FileType = Path.GetExtension(dto.File.FileName),
                FileSize = dto.File.Length,
                UploadTime = DateTime.Now
            };
        }

        public async Task<FileDownloadDto> DownloadAsync(int id)
        {
            var attachment = await _db.Queryable<Attachment>()
                .Where(a => a.Id == id)
                .FirstAsync();

            if (attachment == null)
            {
                throw new FileNotFoundException("Attachment not found");
            }

            var filePath = Path.Combine(_uploadPath, attachment.Url.Replace("/uploads/", ""));
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found");
            }

            return new FileDownloadDto
            {
                Content = await System.IO.File.ReadAllBytesAsync(filePath),
                ContentType = GetContentType(attachment.FileType),
                FileName = attachment.Name
            };
        }

        private string GetContentType(string fileType)
        {
            return fileType switch
            {
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".ppt" => "application/vnd.ms-powerpoint",
                ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".txt" => "text/plain",
                _ => "application/octet-stream"
            };
        }

        public async Task DeleteAsync(int id)
        {
            var attachment = await _db.Queryable<Attachment>()
                .Where(a => a.Id == id)
                .FirstAsync();

            if (attachment == null)
            {
                throw new FileNotFoundException("Attachment not found");
            }

            var filePath = Path.Combine(_uploadPath, attachment.Url.Replace("/uploads/", ""));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            await _db.Deleteable<Attachment>()
                .Where(a => a.Id == id)
                .ExecuteCommandAsync();
        }

        public async Task<Attachment[]> GetListAsync(int? trainingId, int? questionnaireId)
        {
            var query = _db.Queryable<Attachment>();
            
            if (trainingId.HasValue)
            {
                query = query.Where(a => a.TrainingId == trainingId.Value);
            }

            if (questionnaireId.HasValue)
            {
                query = query.Where(a => a.QuestionnaireId == questionnaireId.Value);
            }

            return await query.ToArrayAsync();
        }

        public async Task LinkAttachmentAsync(AttachmentLinkDto dto)
        {
            var attachment = await _db.Queryable<Attachment>()
                .Where(a => a.Id == dto.AttachmentId)
                .FirstAsync();

            if (attachment == null)
            {
                throw new FileNotFoundException("Attachment not found");
            }

            if (dto.TrainingId.HasValue)
            {
                attachment.TrainingId = dto.TrainingId.Value;
            }

            if (dto.QuestionnaireId.HasValue)
            {
                attachment.QuestionnaireId = dto.QuestionnaireId.Value;
            }

            await _db.Updateable(attachment)
                .ExecuteCommandAsync();
        }
    }
}