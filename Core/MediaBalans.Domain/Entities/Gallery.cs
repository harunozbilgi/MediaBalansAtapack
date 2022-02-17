using MediaBalans.Domain.Common;

namespace MediaBalans.Domain.Entities
{
    public class Gallery : BaseEntity
    {
        public string Name { get; set; }
        public string FileCode { get; set; }

    }
}
