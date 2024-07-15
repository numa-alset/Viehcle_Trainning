using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VehicleApi.Core;
using VehicleApi.Core.Models;
using VehicleApi.Extensions;

namespace VehicleApi.Persistance
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleApiDbContext context;

        public VehicleRepository(VehicleApiDbContext context)
        {
            this.context = context;
        }
        public async Task<Vehicle> GetVehicle(int id,bool icludeRelated=true)
        {
            if(!icludeRelated) { return await context.Vehicles.FindAsync(id); }
            return await context.Vehicles.Include(v => v.FeatureVehicles)
                 .Include(v => v.Features)
                 .Include(v => v.Model)
                 .ThenInclude(m => m.Make)
                 .SingleOrDefaultAsync(f => f.Id == id);

        }

        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Remove(vehicle);
        }

        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj)
        {
            var result = new QueryResult<Vehicle>();
           var query= context.Vehicles.Include(v => v.FeatureVehicles)
                 .Include(v => v.Features)
                 .Include(v => v.Model)
                 .ThenInclude(m => m.Make).AsQueryable();
            if (queryObj.MakeId.HasValue)
                query = query.Where(v=>v.Model.MakeId==queryObj.MakeId);
            if (queryObj.ModelId.HasValue)
                query = query.Where(v =>  v.ModelId == queryObj.ModelId);

            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"]=v =>v.Model.Make.Name,
                ["model"]= v =>v.Model.Name,
                ["contactName"]=v =>v.Contact.Name,
            };

            query =query.ApplyOrdering(queryObj,columnsMap);
            result.TotalItems=await query.CountAsync();
            query = query.ApllyPaging(queryObj);
            result.Items = await query.ToListAsync();
            return result;
        }
       
        
    }
}
