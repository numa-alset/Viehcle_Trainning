using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleApi.Core.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public bool IsRegistered { get; set; }
        public Contact Contact { get; set; }
        public DateTime LastUpdate { get; set; }
        public ICollection<Feature> Features { get; set; }
        public ICollection<FeatureVehicle> FeatureVehicles { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public Vehicle()
        {
            Features = new Collection<Feature>();
            FeatureVehicles = new Collection<FeatureVehicle>();
            Photos = new Collection<Photo>();
        }

    }

}
