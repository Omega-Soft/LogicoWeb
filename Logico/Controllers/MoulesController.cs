using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoulesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public MoulesController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/<MoulesController>
        [HttpGet]
        public IActionResult GetAllMoules()
        {
            try
            {
                var moules = _repository.Moule.GetAllMoules();
                _logger.LogInfo($"Returned all moules from database.");
                return Ok(moules);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllMoules action: {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        // GET api/<MoulesController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var moule = _repository.Moule.GetMouleById(id);
                if (moule is null)
                {
                    _logger.LogError($"Moule with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned moule with id: {id}");

                    return Ok(moule);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetMouleById action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // POST api/<MoulesController>
        [HttpPost]
        public IActionResult CreateMoule(_0600Moule moule)
        {
            try
            {
                if (moule is null)
                {
                    _logger.LogError("Moule object sent from client is null.");
                    return BadRequest("Moule object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid moule object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Moule.Create(moule);
                _repository.Save();

                return Ok(moule);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Creation action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<MoulesController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateMoule(int id, [FromBody] _0600Moule moule)
        {
            try
            {
                if (!_repository.Moule.Exists(x => x.IdMoule == id))
                {
                    _logger.LogError($"Moule with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (moule is null)
                {
                    _logger.LogError("Moule object sent from client is null.");
                    return BadRequest("Moule object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid moule object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Moule.Update(moule);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateMoule action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<MoulesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMoule(int id)
        {
            try
            {
                var moule = _repository.Moule.GetMouleById(id);
                if (moule == null)
                {
                    _logger.LogError($"Moule with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Moule.Delete(moule);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteMoule action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
