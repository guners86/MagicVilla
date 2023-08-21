using System.ComponentModel.DataAnnotations;

namespace MagicVilla.API.Models
{
    public class Villa
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Ocupance { get; set; }
        public int SquareMeter { get; set; }
        public string Details { get; set; }
        [Required]
        public double Cost { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
