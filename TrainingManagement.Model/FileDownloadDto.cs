namespace TrainingManagement.Model
{
    public class FileDownloadDto
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}