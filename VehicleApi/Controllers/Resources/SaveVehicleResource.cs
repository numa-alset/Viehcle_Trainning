﻿using System.Collections.ObjectModel;
using VehicleApi.Core.Models;

namespace VehicleApi.Controllers.Resources
{
    public class SaveVehicleResource
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        public Contact Contact { get; set; }
        public ICollection<int> Features { get; set; }

        public SaveVehicleResource() { 
        Features= new Collection<int>();
        }
    }
}
