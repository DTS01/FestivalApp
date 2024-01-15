using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.InteropServices;

namespace FestivalApp.Shared.Models
{
    public class Shift
    {
        [BsonId] // MongoDB primary key
        [BsonRepresentation(BsonType.ObjectId)] // Allows MongoDB to automatically convert to and from ObjectId
        public string? Id { get; set; } // MongoDB requires a property named Id

        // Name of the shift
        [BsonElement("navn")]
        public string Navn { get; set; } = " ";

        //Long description of the shift
        [BsonElement("lang_beskr")]
        public string Lang_beskr { get; set; } = " ";

        // Short description of the shift
        [BsonElement("beskrivelse")]
        public string Beskrivelse { get; set; } = " ";

        // Status of the shift
        [BsonElement("status")]
        public string Status { get; set; } = " ";
    }
}
