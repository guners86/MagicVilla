using MagicVilla.API.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDto> GetVillas()
        {
            return new List<VillaDto>
            {
                new VillaDto { Id = 1, Name = "Vista a la Piscina" },
                new VillaDto { Id = 2, Name = "Vista a la Playa" }
            };
        }
    }
}
