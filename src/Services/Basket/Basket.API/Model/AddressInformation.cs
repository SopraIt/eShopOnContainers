using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class AddressInformation
    {
        public Address billingAddress { get; set; }
        public string shipping_method_code { get; set; }

        private string _shipping_carrier_code;
        public string shippingCarrierCode { 
            get {
                return _shipping_carrier_code;
            } 
            set {
                _shipping_carrier_code = value;
            } 
        }
        public string shipping_carrier_code { 
            get {
                return _shipping_carrier_code;
            } 
            set {
                _shipping_carrier_code = value;
            } 
        }
        public string payment_method_code { get; set; }

        // [JsonProperty("payment_method_additional")]
        // public PaymentMethodAdditional PaymentMethodAdditional { get; set; }
        public Address shippingAddress { get; set; }
    }
}
