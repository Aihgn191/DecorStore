using DecorStore.Areas.Identity.Pages.Account;
using DecorStore.Models;

namespace DecorStore.ViewModel
{
    public class UserViewModel
    {
        public List<Category> Categories { get; set; }
        public UserCustom User { get; set; }
        
    }
}
