using MagicVilla.API.Data;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MagicVilla.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDBContext _dbContext;

        public VillaController(ILogger<VillaController> logger, ApplicationDBContext dBContext)
        {
            _logger = logger;
            _dbContext = dBContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("Get All Villages");
            return Ok(_dbContext.Villas.ToList());
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVillas(int id)
        {
            if (id == 0) 
            {
                _logger.LogError($"Error to get village id: {id}");
                return BadRequest();
            }

            var villa = _dbContext.Villas.FirstOrDefault(x => x.Id.Equals(id));

            if (villa == null) 
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> AddVilla([FromBody] VillaDto villaDto)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            if (_dbContext.Villas.FirstOrDefault(x => x.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Equal Name", $"The name {villaDto.Name} already exist");
                return BadRequest(ModelState);
            }

            if(villaDto == null) 
            {
                return BadRequest(villaDto);
            }

            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            DateTime date = DateTime.Now;

            Villa model = new ()
            {
                Name = villaDto.Name,
                Amenity = villaDto.Amenity,
                Cost = villaDto.Cost,
                CreationDate = date,
                Details = villaDto.Details,
                ImageUrl = villaDto.ImageUrl,
                Ocupance = villaDto.Ocupance,
                SquareMeter = villaDto.SquareMeter,
                UpdateDate = date,
            };

            _dbContext.Villas.Add(model);
            _dbContext.SaveChanges();

            return CreatedAtRoute("GetVilla", new { id = model.Id }, villaDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = _dbContext.Villas.FirstOrDefault(x => x.Id == id);
            
            if (villa == null)
            {
                return NotFound();
            }

            _dbContext.Villas.Remove(villa);
            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto) 
        {
            if (villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }

            var villa = _dbContext.Villas.FirstOrDefault(x => x.Id.Equals(id));

            if (villa == null) 
            {
                return NotFound();
            }

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Amenity = villaDto.Amenity,
                Cost = villaDto.Cost,
                Details = villaDto.Details,
                ImageUrl = villaDto.ImageUrl,
                Ocupance = villaDto.Ocupance,
                SquareMeter = villaDto.SquareMeter,
                UpdateDate = DateTime.Now,
            };

            _dbContext.Villas.Update(model);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> pacthDto)
        {
            if (pacthDto == null || id == 0)
            {
                return BadRequest();
            }

            var villa = _dbContext.Villas.AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));

            if (villa == null)
            {
                return NotFound();
            }

            VillaDto villaDto = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Amenity = villa.Amenity,
                Cost = villa.Cost,
                Details = villa.Details,
                ImageUrl = villa.ImageUrl,
                Ocupance = villa.Ocupance,
                SquareMeter = villa.SquareMeter
            };

            pacthDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Amenity = villaDto.Amenity,
                Cost = villaDto.Cost,
                Details = villaDto.Details,
                ImageUrl = villaDto.ImageUrl,
                Ocupance = villaDto.Ocupance,
                SquareMeter = villaDto.SquareMeter,
                UpdateDate = DateTime.Now,
            };

            _dbContext.Villas.Update(model);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
