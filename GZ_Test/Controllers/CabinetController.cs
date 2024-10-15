using Microsoft.AspNetCore.Mvc;
using GZ_Test_Repo.Entity;
using GZ_Test_Repo.Repository;

namespace GZ_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CabinetController : ControllerBase
    {
        private readonly IRepository<Cabinet> _repository;
        private readonly ILogger<CabinetController> _logger;

        public CabinetController(IRepository<Cabinet> repository, ILogger<CabinetController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cabinet>>> GetCabinetes()
        {
            try
            {
                var cabinetes = await _repository.GetAllAsync();
                if (cabinetes == null)
                {
                    return NotFound();
                }
                return Ok(cabinetes);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                _logger?.LogError(ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cabinet>> GetCabinet(int id)
        {
            try
            {
                var cabinet = await _repository.GetByIdAsync(id);
                if (cabinet == null)
                {
                    return NotFound();
                }
                return cabinet;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                _logger?.LogError(ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Cabinet>> PostCabinet(Cabinet cabinet)
        {
            try
            {
                await _repository.CreateAsync(cabinet);
                return CreatedAtAction(nameof(GetCabinet), new { id = cabinet.Id }, cabinet);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                _logger?.LogError(ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCabinet(int id, Cabinet cabinet)
        {
            try
            {
                if (id != cabinet.Id)
                {
                    return BadRequest();
                }
                await _repository.UpdateAsync(cabinet);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                _logger?.LogError(ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCabinet(int id)
        {
            try
            {
                var cabinet = await _repository.GetByIdAsync(id);
                if (cabinet == null)
                {
                    return NotFound();
                }
                await _repository.DeleteAsync(cabinet);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                _logger?.LogError(ex.StackTrace);
                return StatusCode(500);
            }
        }
    }
}