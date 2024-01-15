using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FestivalApp.Shared.Models
{
    public class Booking
    {
        [BsonId] // Marks this field as the primary key in MongoDB
        [BsonRepresentation(BsonType.ObjectId)] // Allows automatic conversion between string and MongoDB's ObjectId
        public string? Id { get; set; } // Id field used by MongoDB as the primary key
        public string BookingID { get; set; } = string.Empty; // Unique booking Id
        public string Name { get; set; } = string.Empty; // Name of the person who made the booking
        public string Phonenr { get; set; } = string.Empty; // Contact person's phone number
        public string ShiftName { get; set; } = string.Empty; // Name of the booked shift
        public string StartDate { get; set; } = DateTime.Today.ToString("yyyy-MM-dd"); // Start date of the booking
        public string EndDate { get; set; } = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"); // End date of the booking

        // A parameterless constructor is required for MongoDB driver to deserialize objects
        public Booking() { }

        // Constructor to create a new booking with specific values
        public Booking(string bookingID, string name, string phonenr, string shiftName, string startDate, string endDate)
        {
            Id = bookingID;
            BookingID = bookingID;
            Name = name;
            Phonenr = phonenr;
            ShiftName = shiftName;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
