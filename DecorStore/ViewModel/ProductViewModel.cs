using DecorStore.Models;

namespace DecorStore.ViewModel
{
    public class ProductViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Prods { get; set; }
        public List<Product> Prods_Recom { get; set; }
        public List<Producer> Producers { get; set; }
        public string Catename { get; set; }
        public PaginatedList<Product> PageProducts { get; set;}
    }
}
