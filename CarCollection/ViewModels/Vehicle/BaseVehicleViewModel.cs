using System.ComponentModel.DataAnnotations;

namespace CarCollection.ViewModels.Vehicle
    {
    public class BaseVehicleViewModel
        {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Engine { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        }
    }
