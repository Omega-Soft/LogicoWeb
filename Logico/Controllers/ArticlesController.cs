using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public ArticlesController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/<ArticlesController>
        [HttpGet]
        public IActionResult GetAllArticles()
        {
            try
            {
                var articles = _repository.Article.GetAllArticles();
                _logger.LogInfo($"Returned all articles from database.");
                return Ok(articles);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllArticles action: {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        // GET api/<ArticlesController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var article = _repository.Article.GetArticleById(id);
                if (article is null)
                {
                    _logger.LogError($"Article with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned article with id: {id}");

                    return Ok(article);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetArticleById action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ArticlesController>
        [HttpPost]
        public IActionResult CreateArticle(_0400Article article)
        {
            try
            {
                if (article is null)
                {
                    _logger.LogError("Article object sent from client is null.");
                    return BadRequest("Article object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid article object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Article.Create(article);
                _repository.Save();

                return Ok(article);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Creation action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ArticlesController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateArticle(int id, [FromBody] _0400Article article)
        {
            try
            {
                if (!_repository.Article.Exists(x => x.IdArticle == id))
                {
                    _logger.LogError($"Article with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (article is null)
                {
                    _logger.LogError("Article object sent from client is null.");
                    return BadRequest("Article object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid article object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Article.Update(article);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateArticle action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ArticlesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteArticle(int id)
        {
            try
            {
                var article = _repository.Article.GetArticleById(id);
                if (article == null)
                {
                    _logger.LogError($"Article with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Article.Delete(article);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteArticle action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
