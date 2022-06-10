using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotMPsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public LotMPsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }
       
        // GET: api/<LotMPsController>
        [HttpGet]
        public IActionResult GetAllLotMPs()
        {
            try
            {
                var lotMPs = _repository.LotMp.GetAllLotMPs();
                _logger.LogInfo($"Returned all lotMPs from database.");
                return Ok(lotMPs);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllLotMps action: {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        // GET api/<LotMPsController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var lotMPs = _repository.LotMp.GetLotMPById(id);
                if (lotMPs is null)
                {
                    _logger.LogError($"LotMp with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned lotMPs with id: {id}");

                    return Ok(lotMPs);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetLotMpById action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // POST api/<LotMPsController>
        [HttpPost]
        public IActionResult CreateLotMp(_0301LotMp lotMPs)
        {
            try
            {
                if (lotMPs is null)
                {
                    _logger.LogError("LotMp object sent from client is null.");
                    return BadRequest("LotMp object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid LotMp object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.LotMp.Create(lotMPs);
                _repository.Save();

                return Ok(lotMPs);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Creation action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<LotMPsController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateLotMp(int id, [FromBody] _0301LotMp lotMPs)
        {
            try
            {
                if (!_repository.LotMp.Exists(x => x.IdLot == id))
                {
                    _logger.LogError($"LotMp with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (lotMPs is null)
                {
                    _logger.LogError("LotMp object sent from client is null.");
                    return BadRequest("LotMp object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid lotMPs object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.LotMp.Update(lotMPs);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateLotMp action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<LotMPsController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteLotMp(int id)
        {
            try
            {
                var lotMPs = _repository.LotMp.GetLotMPById(id);
                if (lotMPs == null)
                {
                    _logger.LogError($"LotMp with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.LotMp.Delete(lotMPs);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteLotMp action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        // GET api/<LotMPsController>/5
        [HttpGet("GenerateCodeLot/{date}")]
        public IActionResult GenerateCodeLot(string date)
        {
            var dateLot = DateTime.ParseExact(date, "yyyy-MM-dd", null);
            var prefix = "LMP-";
            var sufix = "/" + dateLot.Month + "-" + dateLot.Year.ToString().Substring(2);
            var num = "";
            var code = "";

            try
            {
                var lot = _repository.LotMp.GetAll().Where(x => x.CodeLot.StartsWith("LMP")).ToList().LastOrDefault();
                if (lot is null)
                {
                    num = "0001";
                }
                else
                {
                    var n = (int.Parse(lot.CodeLot.Substring(4, 4))+1);
                    if (n > 0 && n < 10) num = "000" + n;
                    else if (n > 9 && n < 100) num = "00" + n;
                    else if (n > 99 && n < 1000) num = "0" + n;
                    else num = "" + n;
                }
                code = prefix + num + sufix;
                return Ok(code);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GenerateCodeLot action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}

