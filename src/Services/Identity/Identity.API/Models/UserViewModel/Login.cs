using System.ComponentModel.DataAnnotations;

namespace Microsoft.eShopOnContainers.Services.Identity.API.Models.UserViewModel
{
    public class Login
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}