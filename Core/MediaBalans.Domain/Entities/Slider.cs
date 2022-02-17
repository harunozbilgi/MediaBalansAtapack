using MediaBalans.Domain.Common;
using MediaBalans.Domain.Entities.Languages;


namespace MediaBalans.Domain.Entities
{
    public class Slider : BaseEntity
    {
        public Slider()
        {
            SliderLanguages = new HashSet<SliderLanguage>();
        }
        public string FileCode { get; set; }    
        public string Url { get; set; } 
        public virtual ICollection<SliderLanguage> SliderLanguages { get; set; }

    }
}
