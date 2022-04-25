using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FournisseursController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public FournisseursController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/<FournisseursController>
        [HttpGet]
        public IActionResult GetAllFournisseurs()
        {
            try
            {
                var fournisseurs = _repository.Fournisseur.GetAllFournisseurs();
                _logger.LogInfo($"Returned all fournisseurs from database.");
                return Ok(fournisseurs);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllFournisseurs action: {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        // GET api/<FournisseursController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var fournisseur = _repository.Fournisseur.GetFournisseurById(id);
                if (fournisseur is null)
                {
                    _logger.LogError($"Fournisseur with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Fournisseur with id: {id}");

                    return Ok(fournisseur);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetfournisseurById action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // POST api/<fournisseursController>
        [HttpPost]
        public IActionResult Createfournisseur(_0400Fournisseur fournisseur)
        {
            try
            {
                if (fournisseur is null)
                {
                    _logger.LogError("Fournisseur object sent from client is null.");
                    return BadRequest("Fournisseur object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Fournisseur object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Fournisseur.Create(fournisseur);
                _repository.Save();

                return Ok(fournisseur);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Creation action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<FournisseursController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateFournisseur(int id, [FromBody] _0400Fournisseur fournisseur)
        {
            try
            {
                if (!_repository.Fournisseur.Exists(x => x.IdFournisseur == id))
                {
                    _logger.LogError($"Fournisseur with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (fournisseur is null)
                {
                    _logger.LogError("Fournisseur object sent from client is null.");
                    return BadRequest("Fournisseur object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid fournisseur object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Fournisseur.Update(fournisseur);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateFournisseur action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<FournisseursController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteFournisseur(int id)
        {
            try
            {
                var fournisseur = _repository.Fournisseur.GetFournisseurById(id);
                if (fournisseur == null)
                {
                    _logger.LogError($"Fournisseur with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Fournisseur.Delete(fournisseur);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteFournisseur action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
