using System.Collections.ObjectModel;
using VehicleApi.Core.Models;

namespace VehicleApi.Controllers.Resources
{
    public class VehicleResource
    {
        public int Id { get; set; }
        public KeyValuePairResource Model { get; set; }
        public KeyValuePairResource Make { get; set; }
        public bool IsRegistered { get; set; }
        public Contact Contact { get; set; }
        public DateTime LastUpdate { get; set; }
        public ICollection<KeyValuePairResource> FeatureVehicles { get; set; }
        public VehicleResource()
        {
            FeatureVehicles = new Collection<KeyValuePairResource>();
        }
    }
}
