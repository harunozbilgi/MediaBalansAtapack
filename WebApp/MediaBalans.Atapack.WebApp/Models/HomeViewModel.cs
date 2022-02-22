using MediaBalans.Domain.Entities;

namespace MediaBalans.Atapack.WebApp.Models
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public Page Page { get; set; }
        public List<Product> Products { get; set; }
        public List<Service> Services { get; set; } 
    }
}
