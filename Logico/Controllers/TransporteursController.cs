using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransporteursController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public TransporteursController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/<TransporteursController>
        [HttpGet]
        public IActionResult GetAllTransporteurs()
        {
            try
            {
                var transporteurs = _repository.Transporteur.GetAllTransporteurs();
                _logger.LogInfo($"Returned all transporteurs from database.");
                return Ok(transporteurs);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllTransporteurs action: {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        // GET api/<TransporteurssController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var transporteur = _repository.Transporteur.GetTransporteurById(id);
                if (transporteur is null)
                {
                    _logger.LogError($"Transporteur with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Transporteur with id: {id}");

                    return Ok(transporteur);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTransporteurById action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // POST api/<TransporteursController>
        [HttpPost]
        public IActionResult CreateTransporteur(_0700Transporteur transporteur)
        {
            try
            {
                if (transporteur is null)
                {
                    _logger.LogError("Transporteur object sent from client is null.");
                    return BadRequest("Transporteur object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Transporteur object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Transporteur.Create(transporteur);
                _repository.Save();

                return Ok(transporteur);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Creation action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<TransporteursController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateTransporteur(int id, [FromBody] _0700Transporteur transporteur)
        {
            try
            {
                if (!_repository.Transporteur.Exists(x => x.IdTransporteur == id))
                {
                    _logger.LogError($"Transporteur with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (transporteur is null)
                {
                    _logger.LogError("Transporteur object sent from client is null.");
                    return BadRequest("v object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid transporteur object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Transporteur.Update(transporteur);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateTransporteur action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<TransporteursController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTransporteur(int id)
        {
            try
            {
                var transporteur = _repository.Transporteur.GetTransporteurById(id);
                if (transporteur == null)
                {
                    _logger.LogError($"Transporteur with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Transporteur.Delete(transporteur);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteTransporteur action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}

