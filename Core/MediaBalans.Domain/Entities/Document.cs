
using MediaBalans.Domain.Common;

namespace MediaBalans.Domain.Entities
{
    public class Document : BaseEntity
    {
        public string DocumentUnique { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentSize { get; set; }
        public string DocumentFolderName { get; set; }
        public string Storage_Url { get; set; }
        public string Image_Url { get; set; }
        public string Video_Url { get; set; }
    }
    
}
