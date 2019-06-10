namespace Microsoft.eShopOnContainers.Services.Identity.API.Models.UserViewModel
{

    public class Customer
    {
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }

    public class NewUser
    {
        public Customer customer { get; set; }
        public string password { get; set; }
    }
}