using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualitesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public QualitesController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/<QualitesController>
        [HttpGet]
        public IActionResult GetAllQualites()
        {
            try
            {
                var qualite = _repository.Qualite.GetAllQualites();
                _logger.LogInfo($"Returned all qualities from database.");
                return Ok(qualite);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllQualites action: {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        // GET api/<QualitesController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var qualite = _repository.Qualite.GetQualiteById(id);
                if (qualite is null)
                {
                    _logger.LogError($"Qualite with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned qualite with id: {id}");

                    return Ok(qualite);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetQualiteById action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // POST api/<QualitesController>
        [HttpPost]
        public IActionResult CreateQualite(_1100Qualite qualite)
        {
            try
            {
                if (qualite is null)
                {
                    _logger.LogError("Qualite object sent from client is null.");
                    return BadRequest("Qualite object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid qualite object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Qualite.Create(qualite);
                _repository.Save();

                return Ok(qualite);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Creation action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<QualitesController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateQualite(int id, [FromBody] _1100Qualite qualite)
        {
            try
            {
                if (!_repository.Qualite.Exists(x => x.IdQualite == id))
                {
                    _logger.LogError($"Qualite with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (qualite is null)
                {
                    _logger.LogError("Qualite object sent from client is null.");
                    return BadRequest("Qualite object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid qualite object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Qualite.Update(qualite);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateQualite action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<QualitesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteQualite(int id)
        {
            try
            {
                var qualite = _repository.Qualite.GetQualiteById(id);
                if (qualite == null)
                {
                    _logger.LogError($"Qualite with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Qualite.Delete(qualite);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteQualite action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
