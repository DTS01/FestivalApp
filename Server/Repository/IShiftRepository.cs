using FestivalApp.Shared.Models;


namespace FestivalApp.Server.Repository
{
    // Interface that defines the contract for shift repository
    public interface IShiftRepository
    {
        // Declares a method to retrieve all shifts
        Task<IEnumerable<Shift>> GetAllShifts();
    }
}

