using Newtonsoft.Json;

namespace Microsoft.eShopOnContainers.Services.Identity.API.Models.UserViewModel
{
    public class LoginRensponse
    {
        public int code { get; set; }

        public string result { get; set; }
        public Meta meta { get; set; }
    }

    public class NewUserResponse
    {
        public int code { get; set; }
        public User result { get; set; }
    }

    public partial class UserResponse
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("result")]
        public User Result { get; set; }
    }
}