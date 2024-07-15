using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleApi.Core.Models
{
    public class Feature
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<FeatureVehicle> FeatureVehicles { get; set; }

        public Feature()
        {
            Vehicles = new Collection<Vehicle>();
            FeatureVehicles = new Collection<FeatureVehicle>();

        }
    }
}
