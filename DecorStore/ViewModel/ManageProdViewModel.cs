using DecorStore.Models;

namespace DecorStore.ViewModel
{
    public class ManageProdViewModel
    {
        public IEnumerable<DecorStore.Models.Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}
