using MediaBalans.Domain.Common;


namespace MediaBalans.Domain.Entities.Languages
{
    public class NewsLanguage : BaseEntity
    {
        public Guid NewsId { get; set; }    
        public string Lang_Code { get; set; }
        public string Title { get; set; }   
        public string Description { get; set; } 
        public virtual News News { get; set; }  
    }
}
