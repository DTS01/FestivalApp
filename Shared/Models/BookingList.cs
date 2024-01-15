using FestivalApp.Shared.Models;
using MongoDB.Driver;

namespace FestivalBookingApp.Server.Repository
{
    public class BookingList
    {
        private IMongoCollection<Booking> _bookings; // MongoDB collection of booking objects

        // Constructor initializes the connection to the database and sets the collection
        public BookingList(IMongoClient client)
        {
            var database = client.GetDatabase("MusikDB");
            _bookings = database.GetCollection<Booking>("bookings");
        }

        // Asynchronous method to retrieve all bookings from the database
        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _bookings.Find(_ => true).ToListAsync();
        }

        // Asynchronous method to add a new booking to the database
        public async Task AddBookingAsync(Booking booking)
        {
            await _bookings.InsertOneAsync(booking);
        }
    }
}
