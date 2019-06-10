using System;
using Newtonsoft.Json;

namespace Ordering.API.Application.Models
{
    public partial class UserResponse
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("result")]
        public User Result { get; set; }
    }

    public partial class User
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("group_id")]
        public long GroupId { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("created_in")]
        public string CreatedIn { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("store_id")]
        public long StoreId { get; set; }

        [JsonProperty("website_id")]
        public long WebsiteId { get; set; }

        [JsonProperty("addresses")]
        public object[] Addresses { get; set; }

        [JsonProperty("disable_auto_group_change")]
        public long DisableAutoGroupChange { get; set; }
    }
}