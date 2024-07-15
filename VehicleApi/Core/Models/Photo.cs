using System.ComponentModel.DataAnnotations;

namespace VehicleApi.Core.Models
{
    public class Photo
    {
        public int Id { get; set; }
        [StringLength(255)]
        public required string  FileName { get; set; }

        public int VehicleId { get; set; }
    }
}
