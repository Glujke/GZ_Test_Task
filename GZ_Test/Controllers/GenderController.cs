using Microsoft.AspNetCore.Mvc;
using GZ_Test_Repo.Entity;
using GZ_Test_Repo.Repository;

namespace GZ_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenderController : ControllerBase
    {
        private readonly IRepository<Gender> _repository;

        public GenderController(IRepository<Gender> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gender>>> GetGenders()
        {
            try
            {
                var genders = await _repository.GetAllAsync();
                if (genders == null)
                {
                    return NotFound();
                }
                return Ok(genders);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Gender>> GetGender(int id)
        {
            try
            {
                var gender = await _repository.GetByIdAsync(id);
                if (gender == null)
                {
                    return NotFound();
                }
                return gender;
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Gender>> PostGender(Gender gender)
        {
            try
            {
                await _repository.CreateAsync(gender);
                return CreatedAtAction(nameof(GetGender), new { id = gender.Id }, gender);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGender(int id, Gender gender)
        {
            try
            {
                if (id != gender.Id)
                {
                    return BadRequest();
                }
                await _repository.UpdateAsync(gender);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGender(int id)
        {
            try
            {
                var gender = await _repository.GetByIdAsync(id);
                if (gender == null)
                {
                    return NotFound();
                }
                await _repository.DeleteAsync(gender);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}