using Microsoft.AspNetCore.Mvc;
using GZ_Test_Repo.Entity;
using GZ_Test_Repo.Repository;
using Microsoft.Extensions.Logging;

namespace GZ_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecializationController : ControllerBase
    {
        private readonly IRepository<Specialization> _repository;

        public SpecializationController(IRepository<Specialization> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialization>>> GetSpecializations()
        {
            try
            {
                var specializations = await _repository.GetAllAsync();
                if (specializations == null)
                {
                    return NotFound();
                }
                return Ok(specializations);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Specialization>> GetSpezialization(int id)
        {
            try
            {
                var specialization = await _repository.GetByIdAsync(id);
                if (specialization == null)
                {
                    return NotFound();
                }
                return specialization;
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Specialization>> PostSpecialization(Specialization specialization)
        {
            try
            {
                await _repository.CreateAsync(specialization);
                return CreatedAtAction(nameof(GetSpezialization), new { id = specialization.Id }, specialization);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpezialization(int id, Specialization specialization)
        {
            try
            {
                if (id != specialization.Id)
                {
                    return BadRequest();
                }
                await _repository.UpdateAsync(specialization);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpezialization(int id)
        {
            try
            {
                var specialization = await _repository.GetByIdAsync(id);
                if (specialization == null)
                {
                    return NotFound();
                }
                await _repository.DeleteAsync(specialization);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}