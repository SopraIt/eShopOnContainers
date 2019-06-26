using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{

    public partial class Tax
    {
        public int TaxId { get; set; }
        public string TaxDescription { get; set; }
        public int TaxRate { get; set; }
    }
}