using FestivalApp.Server.Repository;
using FestivalApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController] // Indicates that this class is an API controller
[Route("api/schifts")] // Defines the route for this controller
public class ShiftsController : ControllerBase
{
    private readonly IShiftRepository _repository; // Dependency injection for database manipulation

    // Constructor that injects the dependency of IShiftRepository
    public ShiftsController(IShiftRepository repository)
    {
        _repository = repository;
    }

    // Get all shifts - HTTP GET method
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Shift>>> GetShifts()
    {
        try
        {
            var shifts = await _repository.GetAllShifts();
            return Ok(shifts); // Returns a 200 OK response with the list of shifts
        }
        catch (Exception ex)
        {
            // Handles any exceptions and returns a 500 Internal Server Error response
            return StatusCode(500, $"An error occurred while retrieving shelter data. Error: {ex.Message}");
        }
    }

    // Additional methods for creating, updating, or deleting shelters can be added here
}
