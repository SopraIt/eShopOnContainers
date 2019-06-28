using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class Address
    {
        public string region { get; set; }
        public long region_id { get; set; }

        private string _country_id;
        public string CountryId { 
            get {
                return _country_id;
            } 
            set {
                _country_id = value;
            } 
        }
        public string country_id { 
            get {
                return _country_id;
            } 
            set {
                _country_id = value;
            } 
        }
        public List<string> street { get; set; }
        public string telephone { get; set; }
        public string postcode { get; set; }
        public string city { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string region_code { get; set; }
        public string company { get; set; }
    }
}