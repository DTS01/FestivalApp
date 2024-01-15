using FestivalApp.Shared.Models;
using MongoDB.Driver;
using FestivalApp.Shared.Storage;

public class BookingRepository : IBookingRepository
{
    private readonly IMongoCollection<Booking> _bookings;

    // Constructor initializes the connection to the 'bookings' collection in MongoDB
    public BookingRepository(DataStorage dbstorage)
    {
        var database = dbstorage.GetDatabase();
        _bookings = database.GetCollection<Booking>("bookings");
    }

    // Method to asynchronously retrieve all bookings with a specific shift name
    public async Task<IEnumerable<Booking>> GetBookingsByShiftName(string name)
    {
        return await _bookings.Find(b => b.ShiftName == name).ToListAsync();
    }

    // Method to asynchronously retrieve all bookings
    public async Task<IEnumerable<Booking>> GetAllBookings()
    {
        return await _bookings.Find(_ => true).ToListAsync();
    }

    // Method to asynchronously add a new booking
    public async Task AddBooking(Booking booking)
    {
        await _bookings.InsertOneAsync(booking);
    }

    // Method to asynchronously delete a booking based on its ID
    public async Task DeleteBooking(string bookingId)
    {
        await _bookings.DeleteOneAsync(b => b.BookingID == bookingId);
    }

    // Additional CRUD operations like Update can be implemented here
}
