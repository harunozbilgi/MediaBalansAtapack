using MediaBalans.Domain.Entities;

namespace MediaBalans.Atapack.WebApp.Models
{
    public class ServicesViewModel
    {
        public Service Service { get; set; }
        public List<ServiceProperty> ServiceProperties { get; set; }
    }
}
