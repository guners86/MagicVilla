using System.ComponentModel.DataAnnotations;

namespace MagicVilla.API.Models.DataTransferObjects
{
    public class VillaDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int Ocupance { get; set; }

        public string Details { get; set; }

        [Required]
        public double Cost { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }

        public int SquareMeter { get; set; }
    }
}
