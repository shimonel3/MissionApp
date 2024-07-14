using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace MissionApp.Entities
{
    [BsonIgnoreExtraElements]
    public class Mission
    {
        public string CodeName { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }
        [BsonElement("location")]
        public GeoJson2DCoordinates Location { get; set; }
    }
}
