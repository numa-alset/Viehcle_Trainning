using VehicleApi.Core.Models;

namespace VehicleApi.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool icludeRelated = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
        Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery filter);
    }
}