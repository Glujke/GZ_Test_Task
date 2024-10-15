using Microsoft.AspNetCore.Mvc;
using GZ_Test_Repo.Entity;
using GZ_Test_Repo.Repository;

namespace GZ_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IRepository<Doctor> _repository;

        public DoctorController(IRepository<Doctor> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            try
            {
                var doctors = await _repository.GetAllAsync();
                if (doctors == null)
                {
                    return NotFound();
                }
                return Ok(doctors);
            } catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            try
            {
                var doctor = await _repository.GetByIdAsync(id);
                if (doctor == null)
                {
                    return NotFound();
                }
                return doctor;
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor([FromBody] Doctor doctor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _repository.CreateAsync(doctor);
                return CreatedAtAction(nameof(GetDoctor), new { id = doctor.Id }, doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, [FromBody] Doctor doctor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (id != doctor.Id)
                {
                    return BadRequest();
                }
                await _repository.UpdateAsync(doctor);
                return CreatedAtAction(nameof(GetDoctor), new { id = doctor.Id }, doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                var doctor = await _repository.GetByIdAsync(id);
                if (doctor == null)
                {
                    return NotFound();
                }
                await _repository.DeleteAsync(doctor);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}