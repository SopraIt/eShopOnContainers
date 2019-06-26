using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class AddressInformation
    {
        [JsonProperty("billingAddress")]
        public Address BillingAddress { get; set; }

        [JsonProperty("shipping_method_code")]
        public string ShippingMethodCode { get; set; }

        private string _shipping_carrier_code;
        [JsonProperty("shippingCarrierCode")]
        public string shippingCarrierCode { 
            get {
                return _shipping_carrier_code;
            } 
            set {
                _shipping_carrier_code = value;
            } 
        }

        [JsonProperty("shipping_carrier_code")]
        public string shipping_carrier_code { 
            get {
                return _shipping_carrier_code;
            } 
            set {
                _shipping_carrier_code = value;
            } 
        }

        [JsonProperty("payment_method_code")]
        public string PaymentMethodCode { get; set; }

        // [JsonProperty("payment_method_additional")]
        // public PaymentMethodAdditional PaymentMethodAdditional { get; set; }

        [JsonProperty("shippingAddress")]
        public Address ShippingAddress { get; set; }
    }
}
