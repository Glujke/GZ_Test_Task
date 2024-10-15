using Microsoft.AspNetCore.Mvc;
using GZ_Test_Repo.Entity;
using GZ_Test_Repo.Repository;

namespace GZ_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IRepository<Patient> _repository;

        public PatientController(IRepository<Patient> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            try
            {
                var patients = await _repository.GetAllAsync();
                if (patients == null)
                {
                    return NotFound();
                }
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            try
            {
                var patient = await _repository.GetByIdAsync(id);
                if (patient == null)
                {
                    return NotFound();
                }
                return patient;
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient([FromBody] Patient patient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _repository.CreateAsync(patient);
                return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, [FromBody] Patient patient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (id != patient.Id)
                {
                    return BadRequest();
                }
                await _repository.UpdateAsync(patient);
                return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                var patient = await _repository.GetByIdAsync(id);
                if (patient == null)
                {
                    return NotFound();
                }
                await _repository.DeleteAsync(patient);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}