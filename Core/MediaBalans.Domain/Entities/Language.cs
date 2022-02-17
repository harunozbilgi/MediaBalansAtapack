using MediaBalans.Domain.Common;

namespace MediaBalans.Domain.Entities
{
    public class Language : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string LangCode { get; set; }

    }
}
