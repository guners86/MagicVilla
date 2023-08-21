using System.ComponentModel.DataAnnotations;

namespace MagicVilla.API.Models.DataTransferObjects
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public int Ocupance { get; set; }
        [Required]
        public string Details { get; set; }

        [Required]
        public double Cost { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public string Amenity { get; set; }

        public int SquareMeter { get; set; }
    }
}
