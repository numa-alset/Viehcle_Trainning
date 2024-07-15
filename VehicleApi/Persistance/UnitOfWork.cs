using VehicleApi.Core;

namespace VehicleApi.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VehicleApiDbContext context;

        public UnitOfWork(VehicleApiDbContext context) 
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
         await  context.SaveChangesAsync();
        }
    }
}
