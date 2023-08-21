using MagicVilla.API.Models.DataTransferObjects;

namespace MagicVilla.API.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto { Id = 1, Name = "Vista a la Piscina", Ocupance = 3, SquareMeter = 30 },
            new VillaDto { Id = 2, Name = "Vista a la Playa", Ocupance = 100, SquareMeter = 10000 }
        };
    }
}
