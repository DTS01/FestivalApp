using MongoDB.Driver;
using FestivalApp.Shared.Models;
using FestivalApp.Shared.Storage;
using FestivalApp.Server.Repository;

namespace FestivalApp.Server.Repository
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly IMongoCollection<Shift> _shifts;

        // Constructor initializes the connection to "schifts" collection in MongoDB
        public ShiftRepository(DataStorage dbstorage)
        {
            var database = dbstorage.GetDatabase();
            _shifts = database.GetCollection<Shift>("schifts");
        }

        // Method to asynchronously retrieve all shifts
        public async Task<IEnumerable<Shift>> GetAllShifts()
        {
            return await _shifts.Find(_ => true).ToListAsync();
        }
    }
}
