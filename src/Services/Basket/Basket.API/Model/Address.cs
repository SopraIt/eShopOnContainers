using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class Address
    {
        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("region_id")]
        public long RegionId { get; set; }

        private string _country_id;
        [JsonProperty("CountryId")]
        public string CountryId { 
            get {
                return _country_id;
            } 
            set {
                _country_id = value;
            } 
        }

        [JsonProperty("country_id")]
        public string country_id { 
            get {
                return _country_id;
            } 
            set {
                _country_id = value;
            } 
        }

        [JsonProperty("street")]
        public List<string> Street { get; set; }

        [JsonProperty("telephone")]
        public string Telephone { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("region_code")]
        public string RegionCode { get; set; }

        [JsonProperty("company", NullValueHandling = NullValueHandling.Ignore)]
        public string Company { get; set; }
    }
}