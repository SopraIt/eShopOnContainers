using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Catalog.Nosql.Model
{

    public partial class MediaGallery
    {
        public string image { get; set; }
        public int pos { get; set; }
        public string typ { get; set; }
        public string lab { get; set; }
        public object vid { get; set; }
    }
}