using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BateauxController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public BateauxController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/<BateauxController>
        [HttpGet]
        public IActionResult GetAllBateaux()
        {
            try
            {
                var bateaux = _repository.Bateau.GetAllBateaux();
                _logger.LogInfo($"Returned all bateaux from database.");
                return Ok(bateaux);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllBateaux action: {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        // GET api/<BateauxController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var bateau = _repository.Bateau.GetBateauById(id);
                if (bateau is null)
                {
                    _logger.LogError($"Bateau with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned bateau with id: {id}");

                    return Ok(bateau);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetBateauById action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // POST api/<BateauxController>
        [HttpPost]
        public IActionResult CreatBateau(_0300Bateau bateau)
        {
            try
            {
                if (bateau is null)
                {
                    _logger.LogError("Bateau  object sent from client is null.");
                    return BadRequest("Bateau  object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid bateau object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Bateau.Create(bateau);
                _repository.Save();

                return Ok(bateau);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Creation action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BateauxController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateBateau(int id, [FromBody] _0300Bateau bateau)
        {
            try
            {
                if (!_repository.Bateau.Exists(x => x.IdBateau == id))
                {
                    _logger.LogError($"Bateau  with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (bateau is null)
                {
                    _logger.LogError("Bateau  object sent from client is null.");
                    return BadRequest("Bateau  object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid bateau  object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Bateau.Update(bateau);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateBateau action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BateauxController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBateau(int id)
        {
            try
            {
                var bateau = _repository.Bateau.GetBateauById(id);
                if (bateau == null)
                {
                    _logger.LogError($"Bateau with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Bateau.Delete(bateau);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteBateau action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}

