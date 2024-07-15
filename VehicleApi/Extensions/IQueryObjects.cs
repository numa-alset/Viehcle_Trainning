namespace VehicleApi.Extensions
{
    public interface IQueryObjects
    {
        int? MakeId { get; set; }
        int? ModelId { get; set; }
         String SortBy { get; set; }
         bool IsSortAscending { get; set; }
        int Page { get; set; }
        int PageSize { get; set; }
       
    }
}
