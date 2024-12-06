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
    [Route("Salle")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class SalRechercheController : ControllerBase
    {
        private readonly ISalleService _service;
        private readonly Serilog.ILogger _logger;

        public SalRechercheController(ISalleService service, Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetSalle/{SalleID}")]
        public async Task<ActionResult<DepartementDto>> GetSalle(int SalleID)
        {
            try
            {
                var Salle = await _service.GetSalle(SalleID).ConfigureAwait(false);
                if (Salle == null)
                {
                    return NotFound();
                }
                return Ok(Salle);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de Salle : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la récupération de Salle.");
            }
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<SalleDto>>> GetAll()
        {
            try
            {
                var Salle = await _service.GetAllSalles().ConfigureAwait(false);
                if (Salle == null || !Salle.Any())
                {
                    return NotFound("Aucun Salle trouvé.");
                }
                return Ok(Salle);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de tous les Salles : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la récupération de tous les Salles.");
            }
        }






    }
}
