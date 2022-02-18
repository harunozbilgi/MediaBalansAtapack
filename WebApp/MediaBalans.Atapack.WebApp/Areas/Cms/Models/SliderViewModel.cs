using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Atapack.WebApp.Areas.Cms.Models
{
    public class SliderViewModel
    {
        public Slider Slider { get; set; }
        public List<SliderLanguage> SliderLanguages { get; set; }
    }
}
