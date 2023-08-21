using AutoMapper;
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
        private readonly IMapper _mapper;

        public VillaController(ILogger<VillaController> logger, ApplicationDBContext dBContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dBContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Get All Villages");
            IEnumerable<Villa> villaList = await _dbContext.Villas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villaList));
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVillas(int id)
        {
            if (id == 0) 
            {
                _logger.LogError($"Error to get village id: {id}");
                return BadRequest();
            }

            var villa = await _dbContext.Villas.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (villa == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> AddVilla([FromBody] VillaCreateDto villaDto)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            if (await _dbContext.Villas.FirstOrDefaultAsync(x => x.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Equal Name", $"The name {villaDto.Name} already exist");
                return BadRequest(ModelState);
            }

            if(villaDto == null) 
            {
                return BadRequest(villaDto);
            }

            DateTime date = DateTime.Now;

            Villa model = _mapper.Map<Villa>(villaDto);
            model.CreationDate = date;
            model.UpdateDate = date;

            _dbContext.Villas.Add(model);
            await _dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = await _dbContext.Villas.FirstOrDefaultAsync(x => x.Id == id);
            
            if (villa == null)
            {
                return NotFound();
            }

            _dbContext.Villas.Remove(villa);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaDto) 
        {
            if (villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }

            var villa = await _dbContext.Villas.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (villa == null) 
            {
                return NotFound();
            }

            Villa model = _mapper.Map<Villa>(villaDto);
            model.UpdateDate = DateTime.Now;

            _dbContext.Villas.Update(model);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> pacthDto)
        {
            if (pacthDto == null || id == 0)
            {
                return BadRequest();
            }

            var villa = await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (villa == null)
            {
                return NotFound();
            }

            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);

            pacthDto.ApplyTo(villaDto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa model = _mapper.Map<Villa>(villaDto);
            model.UpdateDate = DateTime.Now;

            _dbContext.Villas.Update(model);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
