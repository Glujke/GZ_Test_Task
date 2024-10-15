using Microsoft.AspNetCore.Mvc;
using GZ_Test_Repo.Entity;
using GZ_Test_Repo.Repository;

namespace GZ_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AreaController : ControllerBase
    {
        private readonly IRepository<Area> _repository;

        public AreaController(IRepository<Area> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetAreas()
        {
            try
            {
                var res = await _repository.GetAllAsync();
                if (res == null)
                {
                    return NotFound();
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetArea(int id)
        {
            try
            {
                var area = await _repository.GetByIdAsync(id);
                if (area == null)
                {
                    return NotFound();
                }
                return area;
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Area>> PostArea(Area area)
        {
            try
            {
                await _repository.CreateAsync(area);
                return CreatedAtAction(nameof(GetArea), new { id = area.Id }, area);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea(int id, Area area)
        {
            try
            {
                if (id != area.Id)
                {
                    return BadRequest();
                }
                await _repository.UpdateAsync(area);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            try
            {
                var area = await _repository.GetByIdAsync(id);
                if (area == null)
                {
                    return NotFound();
                }
                await _repository.DeleteAsync(area);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}