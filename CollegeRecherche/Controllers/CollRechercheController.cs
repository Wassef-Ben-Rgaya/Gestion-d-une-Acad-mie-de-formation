using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace ArticlesRecherche.Controllers
{
    [Produces("application/json")]
    [Route("College")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class ArtRechercheController : ControllerBase
    {
        private readonly ICollegeService _service;
        private readonly Serilog.ILogger _logger;

        public ArtRechercheController(ICollegeService service, Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetCollege/{CollegeID}")]
        public async Task<ActionResult<CollegeDto>> GetArticle(int CollegeID)
        {
            try
            {
                var College = await _service.GetCollege(CollegeID).ConfigureAwait(false);
                if (College == null)
                {
                    return NotFound();
                }
                return Ok(College);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de College : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la récupération de College.");
            }
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CollegeDto>>> GetAll()
        {
            try
            {
                var College = await _service.GetAllColleges().ConfigureAwait(false);
                if (College == null || !College.Any())
                {
                    return NotFound("Aucun College trouvé.");
                }
                return Ok(College);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de tous les College : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la récupération de tous les College.");
            }
        }






    }
}
