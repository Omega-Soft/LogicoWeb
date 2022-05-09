using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvenancesController : ControllerBase
    {

        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public ProvenancesController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/<ProvenanceController>
        [HttpGet]
            public IActionResult GetAllProvenances()
            {
                try
                {
                    var provenances = _repository.Provenance.GetAllProvenances();
                    _logger.LogInfo($"Returned all provenances from database.");
                    return Ok(provenances);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Something went wrong inside GetAllProvenances action: {ex.Message}");

                    return BadRequest(ex.Message);
                }
            }

        // GET api/<ProvenanceController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var provenance = _repository.Provenance.GetProvenanceById(id);
                if (provenance is null)
                {
                    _logger.LogError($"Provenance with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Provenance with id: {id}");

                    return Ok(provenance);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetProvenanceById action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ProvenanceController>
        [HttpPost]
        public IActionResult CreateProvenance(_0300Provenance provenance)
        {
            try
            {
                if (provenance is null)
                {
                    _logger.LogError("Provenance object sent from client is null.");
                    return BadRequest("Provenance object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Provenance object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Provenance.Create(provenance);
                _repository.Save();

                return Ok(provenance);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Creation action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ProvenanceController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateProvenance(int id, [FromBody] _0300Provenance provenance)
        {
            try
            {
                if (!_repository.Provenance.Exists(x => x.IdProvenance == id))
                {
                    _logger.LogError($"Provenance with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (provenance is null)
                {
                    _logger.LogError("Provenance object sent from client is null.");
                    return BadRequest("Provenance object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid provenance object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Provenance.Update(provenance);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateProvenance action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        // DELETE api/<ProvenanceController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProvenance(int id)
        {
            try
            {
                var provenance = _repository.Provenance.GetProvenanceById(id);
                if (provenance == null)
                {
                    _logger.LogError($"Provenance with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Provenance.Delete(provenance);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteProvenance action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
