namespace Mosahem.Domain.Entities
{
    public class TemporaryFileUpload : BaseEntity
    {
        public string FileKey { get; set; }
        public string FolderName { get; set; }
    }
}
