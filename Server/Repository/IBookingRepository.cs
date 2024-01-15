using FestivalApp.Shared.Models;

// Interface that defines the contract for the booking repository
public interface IBookingRepository
{
    // Declares a method to retrieve all bookings
    Task<IEnumerable<Booking>> GetAllBookings();

    // Declares a method to add a booking
    Task AddBooking(Booking booking);

    // Declares a method to delete a booking based on ID
    Task DeleteBooking(string bookingId);

    // Declares a method to retrieve bookings by shift name
    Task<IEnumerable<Booking>> GetBookingsByShiftName(string name);
}
