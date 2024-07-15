using VehicleApi.Core.Models;

namespace VehicleApi.Core
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}