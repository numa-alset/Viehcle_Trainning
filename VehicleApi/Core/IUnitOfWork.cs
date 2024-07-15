namespace VehicleApi.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
