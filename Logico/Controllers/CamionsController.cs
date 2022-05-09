using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CamionsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public CamionsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/<CamionsController>
        [HttpGet]
        public IActionResult GetAllCamions()
        {
            try
            {
                var camion = _repository.Camion.GetAllCamions();
                _logger.LogInfo($"Returned all camion from database.");
                return Ok(camion);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllCamionss action: {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        // GET api/<CamionsController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var camion = _repository.Camion.GetCamionById(id);
                if (camion is null)
                {
                    _logger.LogError($"a with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Camion with id: {id}");

                    return Ok(camion);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetCamionById action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // POST api/<CamionsController>
        [HttpPost]
        public IActionResult CreateCamion(_0700Camion camion)
        {
            try
            {
                if (camion is null)
                {
                    _logger.LogError("Camion object sent from client is null.");
                    return BadRequest("Camion object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Camion object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Camion.Create(camion);
                _repository.Save();

                return Ok(camion);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Creation action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CamionsController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateCamion(int id, [FromBody] _0700Camion camion)
        {
            try
            {
                if (!_repository.Camion.Exists(x => x.IdCamion == id))
                {
                    _logger.LogError($"Camion with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (camion is null)
                {
                    _logger.LogError("Camion object sent from client is null.");
                    return BadRequest("Camion object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Camion object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Camion.Update(camion);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateCamion action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CamionsController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCamion(int id)
        {
            try
            {
                var camion = _repository.Camion.GetCamionById(id);
                if (camion == null)
                {
                    _logger.LogError($"Camion with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Camion.Delete(camion);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteCamion action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}

