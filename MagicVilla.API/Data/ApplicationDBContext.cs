using MagicVilla.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.API.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(new Villa 
            {
                Id = 1,
                Name = "Vista a la Piscina",
                Amenity = "Muy amena",
                Cost = 100,
                Details = "Detalle de la piscina",
                SquareMeter = 300,
                ImageUrl = "",
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Ocupance = 5
            }, new Villa
            {
                Id = 2,
                Name = "Vista a al Mar",
                Amenity = "Muy amena",
                Cost = 400,
                Details = "Detalle del mar",
                SquareMeter = 300,
                ImageUrl = "",
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Ocupance = 10
            });
        }
    }
}
