using Microsoft.EntityFrameworkCore;
using VehicleApi.Core;
using VehicleApi.Core.Models;

namespace VehicleApi.Persistance
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly VehicleApiDbContext context;

        public PhotoRepository(VehicleApiDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await context.Photos.Where(p => p.VehicleId == vehicleId).ToListAsync();
        }
    }
}
