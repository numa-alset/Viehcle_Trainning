﻿using VehicleApi.Extensions;

namespace VehicleApi.Core.Models
{
    public class VehicleQuery : IQueryObjects
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get ; set; }
        public int Page { get;set; }
        public int PageSize { get; set; }
    }
}
