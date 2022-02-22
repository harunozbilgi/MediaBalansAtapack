using MediaBalans.Domain.Entities;

namespace MediaBalans.Atapack.WebApp.Models
{
    public class ProductsViewModel
    {
        public List<Product> Products { get; set; } 
        public Product Product { get; set; }    
        public List<Category> Categories { get; set; }  
    }
}
