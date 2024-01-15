using FestivalApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController] // Indicates that this class is an API controller
[Route("api/bookings")] // Defines the route for this controller
public class BookingsController : ControllerBase
{
    private readonly IBookingRepository _repository; // Dependency injection for database manipulation

    // Constructor that injects the dependency of IBookingRepository
    public BookingsController(IBookingRepository repository)
    {
        _repository = repository;
    }

    // Get all bookings - HTTP GET method
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
    {
        try
        {
            var bookings = await _repository.GetAllBookings();
            return Ok(bookings); // Returns a 200 OK response with the list of bookings
        }
        catch (System.Exception ex)
        {
            // Handles any exceptions and returns a 500 Internal Server Error response
            return StatusCode(500, $"An error occurred while retrieving booking data. Error: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("getby/{navn:alpha}")]
    public async Task<ActionResult<IEnumerable<Booking>>> GetBookings(string alpha)
    {
        try
        {
            var bookings = await _repository.GetBookingsByShiftName(alpha);
            return Ok(bookings);
        }
        catch (System.Exception ex)
        {
            // Handles any exceptions and returns a 500 Internal Server Error response
            return StatusCode(500, $"An error occurred while retrieving booking data. Error: {ex.Message}");
        }
    }

    // Create a new booking - HTTP POST method
    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
    {
        try
        {
            await _repository.AddBooking(booking);
            return Ok(); // Returns a 200 OK response if creation is successful
        }
        catch (System.Exception ex)
        {
            // Handles any exceptions and returns a 500 Internal Server Error response
            return StatusCode(500, $"An error occurred while creating a booking. Error: {ex.Message}");
        }
    }

    // Delete a booking - HTTP DELETE method
    [HttpDelete("{bookingId}")]
    public async Task<IActionResult> DeleteBooking(string bookingId)
    {
        try
        {
            await _repository.DeleteBooking(bookingId);
            return Ok(); // Returns a 200 OK response if deletion is successful
        }
        catch (Exception ex)
        {
            // Handles any exceptions and returns a 500 Internal Server Error response
            return StatusCode(500, $"An error occurred while deleting a booking. Error: {ex.Message}");
        }
    }

    // Additional methods for updates, etc. can be added here as needed
}
