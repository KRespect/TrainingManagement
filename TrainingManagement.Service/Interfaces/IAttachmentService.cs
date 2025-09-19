using System.Threading.Tasks;
using TrainingManagement.Model;

namespace TrainingManagement.Service
{
    public interface IAttachmentService
    {
        Task<Attachment> UploadAsync(AttachmentUploadDto dto);
        Task<FileDownloadDto> DownloadAsync(int id);
        Task DeleteAsync(int id);
        Task<Attachment[]> GetListAsync(int? trainingId, int? questionnaireId);
        Task LinkAttachmentAsync(AttachmentLinkDto dto);
    }
}