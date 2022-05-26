using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public PagesController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/Pages
        [HttpGet]
        public IActionResult GetAllPages()
        {
            try
            {
                var roles = _repository.Page.GetAllPages();
                _logger.LogInfo($"Returned all Pages from database.");
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllPages action: {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        // POST api/Pages
        [HttpPost]
        public IActionResult CreatePage(WebPage page)
        {
            try
            {
                if (page is null)
                {
                    _logger.LogError("Page object sent from client is null.");
                    return BadRequest("Page object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Page object sent from client.");
                    return BadRequest("Invalid model object");
                }
                _repository.Page.Create(page);
                _repository.Save();

                return Ok(page);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Creation action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Pages/5
        [HttpPut("{id}")]
        public IActionResult UpdatePage(int id, [FromBody] WebPage role)
        {
            try
            {
                if (!_repository.Page.Exists(x => x.IdPage == id))
                {
                    _logger.LogError($"Page with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (role is null)
                {
                    _logger.LogError("Page object sent from client is null.");
                    return BadRequest("Page object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Page object sent from client.");
                    return BadRequest("Invalid model object");
                }
                _repository.Page.Update(role);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatePage action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Pages/5
        [HttpDelete("{id}")]
        public IActionResult DeletePage(int id)
        {
            try
            {
                var page = _repository.Page.GetPageById(id);
                if (page == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                //_repository.Page.Delete(role);
                page.Deleted = true;
                _repository.Page.Update(page);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteUser action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

    }
}
