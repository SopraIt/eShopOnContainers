using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Basket.API.Model
{
    public partial class TotalSegment
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public decimal Value { get; set; }
        public string Area { get; set; }
        //public ExtensionAttributes ExtensionAttributes { get; set; }
    }
}