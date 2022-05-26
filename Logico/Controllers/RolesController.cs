using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public RolesController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/Roles
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            try
            {
                var roles = _repository.Role.GetAllRoles();
                _logger.LogInfo($"Returned all Roles from database.");
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllRoles action: {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        // POST api/Roles
        [HttpPost]
        public IActionResult CreateRole(WebRole role)
        {
            try
            {
                if (role is null)
                {
                    _logger.LogError("Role object sent from client is null.");
                    return BadRequest("Role object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Role object sent from client.");
                    return BadRequest("Invalid model object");
                }
                _repository.Role.Create(role);
                _repository.Save();

                return Ok(role);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Creation action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Roles/5
        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, [FromBody] WebRole role)
        {
            try
            {
                if (!_repository.Role.Exists(x => x.IdRole == id))
                {
                    _logger.LogError($"Role with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (role is null)
                {
                    _logger.LogError("Role object sent from client is null.");
                    return BadRequest("Role object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Role object sent from client.");
                    return BadRequest("Invalid model object");
                }
                _repository.Role.Update(role);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateRole action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Roles/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            try
            {
                var role = _repository.Role.GetRoleById(id);
                if (role == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Role.Delete(role);
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
