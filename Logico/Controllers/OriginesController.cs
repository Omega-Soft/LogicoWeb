using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OriginesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public OriginesController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/<OriginesController>
        [HttpGet]
        public IActionResult GetAllOrigines()
        {
            try
            {
                var origines = _repository.Origine.GetAllOrigines();
                _logger.LogInfo($"Returned all origines from database.");
                return Ok(origines);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllOrigines action: {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        // GET api/<OriginesController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var origine = _repository.Origine.GetOrigineById(id);
                if (origine is null)
                {
                    _logger.LogError($"Origine with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned origine with id: {id}");

                    return Ok(origine);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOrigineById action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // POST api/<OriginesController>
        [HttpPost]
        public IActionResult CreateOrigine(_0300Origine origine)
        {
            try
            {
                if (origine is null)
                {
                    _logger.LogError("Origine object sent from client is null.");
                    return BadRequest("Origine object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Origine object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Origine.Create(origine);
                _repository.Save();

                return Ok(origine);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Creation action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<OriginesController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateOrigine(int id, [FromBody] _0300Origine origine)
        {
            try
            {
                if (!_repository.Origine.Exists(x => x.IdOrigine == id))
                {
                    _logger.LogError($"Origine with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (origine is null)
                {
                    _logger.LogError("Origine object sent from client is null.");
                    return BadRequest("Origine object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid origine object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Origine.Update(origine);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOrigine action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<OriginesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrigine(int id)
        {
            try
            {
                var origine = _repository.Origine.GetOrigineById(id);
                if (origine == null)
                {
                    _logger.LogError($"Origine with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Origine.Delete(origine);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteOrigine action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
