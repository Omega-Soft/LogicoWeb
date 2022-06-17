using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonReceptionsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public BonReceptionsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }
        // GET: api/LotMPsController/NextBR
        [HttpGet("GenerateCodeBR")]
        public async Task<ActionResult> GenerateCodeBR()
        {
            var prefix = "BR-";
            var num = "";
            var code = "";

            try
            {
                var br = _repository.BonReceptionMp.GetAll().Where(x => x.CodeBr.StartsWith("BR")).ToList().LastOrDefault();
                if (br is null)
                {
                    num = "0001";
                }
                else
                {
                    var n = (int.Parse(br.CodeBr.Substring(3, 4)) + 1);
                    if (n > 0 && n < 10) num = "000" + n;
                    else if (n > 9 && n < 100) num = "00" + n;
                    else if (n > 99 && n < 1000) num = "0" + n;
                    else num = "" + n;
                }
                code = prefix + num;
                return Ok(code);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GenerateCodeBR action: {ex.Message}");
                return BadRequest(ex.Message);
            }


            //try
            //{
            //    int num = 1;
            //    var count = _repository.BonReceptionMp.GetAllBonReceptions().Count();
            //    var prefix = "BR-";

            //    if (_repository.BonReceptionMp.GetAllBonReceptions().Any())
            //    {
            //        var lastBr = _repository.BonReceptionMp.GetAll().OrderBy(x => x.IdBr).LastOrDefault().CodeBr;
            //        num = int.Parse(lastBr, 0) + 1;

            //        if (num > 0 && num < 10) prefix += "000";
            //        else if (num > 9 && num < 100) prefix += "00";
            //        else if (num > 99 && num < 1000) prefix += "0";
            //    }

            //    return Ok(prefix + num);
            //}
            //catch (Exception e)
            //{
            //    return BadRequest(e);
            //}
        }


        // GET: api/<BonReceptionsController>
        [HttpGet]
        public IActionResult GetAllBonReceptions()
        {
            try
            {
                var bonReceptions = _repository.BonReceptionMp.GetAllBonReceptions();
                _logger.LogInfo($"Returned all bonReceptions from database.");
                return Ok(bonReceptions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllBonReceptions action: {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        // GET: api/<BonReceptionsController>
        [HttpGet("Lot/{idLot}")]
        public IActionResult GetAllBonReceptionLot(int idLot)
        {
            try
            {
                var bonReceptions = _repository.BonReceptionMp.GetAllBonReceptionLot(idLot);
                _logger.LogInfo($"Returned all bonReceptions from database.");
                return Ok(bonReceptions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllBonReceptions action: {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        // GET api/<BonReceptionsController>/5
        [HttpGet("{id}")]
        public ActionResult<Object> GetById(int id)
        {
            try
            {
                var bonReception = _repository.BonReceptionMp.GetBonReceptionById(id);
                if (bonReception is null)
                {
                    _logger.LogError($"BonReception with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned BonReception with id: {id}");

                    return bonReception;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetBRById action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // POST api/<GetBonReceptionsController>
        [HttpPost]
        public IActionResult CreatebonReception(_0301BonReceptionMp bonReception)
        {
            try
            {
                if (bonReception is null)
                {
                    _logger.LogError("BonReception object sent from client is null.");
                    return BadRequest("BonReception object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid BonReception object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.BonReceptionMp.Create(bonReception);
                _repository.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Creation action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BonReceptionsController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateBonReception(int id, [FromBody] _0301BonReceptionMp bonReception)
        {
            try
            {
                if (!_repository.BonReceptionMp.Exists(x => x.IdBr == id))
                {
                    _logger.LogError($"BonReception with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (bonReception is null)
                {
                    _logger.LogError("BonReception object sent from client is null.");
                    return BadRequest("BonReception object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid bonReception object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.BonReceptionMp.Update(bonReception);
                _repository.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateBonReception action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BonReceptionsController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBonReception(int id)
        {
            try
            {
                var bonReception = _repository.BonReceptionMp.GetByCondition(x=>x.IdBr == id).FirstOrDefault();
                if (bonReception == null)
                {
                    _logger.LogError($"BonReception with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                //_repository.BonReceptionMp.Delete(bonReception);
                bonReception.Deleted = true;
                _repository.BonReceptionMp.Update(bonReception);
                _repository.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteBonReception action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
