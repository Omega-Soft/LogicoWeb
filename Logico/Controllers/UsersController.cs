using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public UsersController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _repository.User.GetAllUsers();
                _logger.LogInfo($"Returned all users from database.");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUsers action: {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        // GET api/Users/5
        [HttpGet("{idUser}")]
        public IActionResult GetUserById(int idUser)
        {
            try
            {
                var user = _repository.User.GetUserById(idUser);
                if (user is null)
                {
                    _logger.LogError($"User with id: {idUser}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user with id: {idUser}");

                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserById action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // POST api/Users
        [HttpPost]
        public IActionResult CreateUser(WebUser user)
        {
            try
            {
                if (user is null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from client.");
                    return BadRequest("Invalid model object");
                }
                user.Password = LoginController.CalculateMD5Hash(user.Password);
                _repository.User.Create(user);
                _repository.Save();

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Creation action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] WebUser user)
        {
            try
            {
                if (!_repository.User.Exists(x => x.IdUser == id))
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (user is null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from client.");
                    return BadRequest("Invalid model object");
                }
                user.Password = LoginController.CalculateMD5Hash(user.Password);
                _repository.User.Update(user);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateUser action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _repository.User.GetUserById(id);
                if (user == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.User.Delete(user);
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
