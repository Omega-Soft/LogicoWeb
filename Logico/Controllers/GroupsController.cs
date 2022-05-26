using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public GroupsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/Groups
        [HttpGet]
        public IActionResult GetAllGroups()
        {
            try
            {
                var groups = _repository.Group.GetAllGroups();
                _logger.LogInfo($"Returned all Groups from database.");
                return Ok(groups);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllGroups action: {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        // POST api/Groups
        [HttpPost]
        public IActionResult CreateGroup(WebGroupUser group)
        {
            try
            {
                if (group is null)
                {
                    _logger.LogError("Group object sent from client is null.");
                    return BadRequest("Group object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Group object sent from client.");
                    return BadRequest("Invalid model object");
                }
                _repository.Group.Create(group);
                _repository.Save();

                List<WebPage> pages = _repository.Page.GetAll().ToList();
                foreach (var page in pages)
                {
                    WebRole role = new WebRole();
                    role.IdGroup = group.IdGroup;
                    role.IdPage = page.IdPage;
                    role.Read = false;
                    role.Add = false;
                    role.Update = false;
                    role.Delete = false;
                    _repository.Role.Create(role);
                    _repository.Save();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Creation action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Groups/5
        [HttpPut("{id}")]
        public IActionResult UpdateGroup(int id, [FromBody] WebGroupUser group)
        {
            try
            {
                if (!_repository.Group.Exists(x => x.IdGroup == id))
                {
                    _logger.LogError($"Group with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (group is null)
                {
                    _logger.LogError("Group object sent from client is null.");
                    return BadRequest("Group object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Group object sent from client.");
                    return BadRequest("Invalid model object");
                }
                _repository.Group.Update(group);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateGroup action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Groups/5
        [HttpDelete("{id}")]
        public IActionResult DeleteGroup(int id)
        {
            try
            {
                var group = _repository.Group.GetGroupById(id);
                if (group == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                //_repository.Group.Delete(group);
                group.Deleted = true;
                _repository.Group.Update(group);
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
