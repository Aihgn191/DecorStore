using Microsoft.AspNetCore.Identity;

namespace DecorStore.Models
{
    public class UserCustom: IdentityUser
    {
        public string? Address { get; set; }

        public string? FullName { get; set; }
    }
}
