using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleApi.Core.Models
{
    [Table("FeatureVehicles")]
    public class FeatureVehicle
    {

        public int FeatureId { get; set; }
        public int VehicleId { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
