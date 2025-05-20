using System.ComponentModel.DataAnnotations;

namespace CrudVehiclesApp.Api.Domain.Entities
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }
        
        public DateTime Year { get; set; }

        [Required]
        [MaxLength(50)]
        public string LicensePlateNumber { get; set; }
    }
}
