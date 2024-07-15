using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleApi.Core.Models
{
    public class Make
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }
        public Make()
        {
            Models = new Collection<Model>();
        }
    }
}
