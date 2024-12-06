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
    [Route("Enseignant")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class EnsRechercheController : ControllerBase
    {
        private readonly IEnseignantService _service;
        private readonly Serilog.ILogger _logger;

        public EnsRechercheController(IEnseignantService service, Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetEnseignant/{EnseignantID}")]
        public async Task<ActionResult<EnseignantDto>> GetEnseignant(int EnseignantID)
        {
            try
            {
                var Enseignant = await _service.GetEnseignant(EnseignantID).ConfigureAwait(false);
                if (Enseignant == null)
                {
                    return NotFound();
                }
                return Ok(Enseignant);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de Enseignant : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la récupération de Enseignant.");
            }
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<EnseignantDto>>> GetAll()
        {
            try
            {
                var Enseignant = await _service.GetAllEnseignants().ConfigureAwait(false);
                if (Enseignant == null || !Enseignant.Any())
                {
                    return NotFound("Aucun Enseignant trouvé.");
                }
                return Ok(Enseignant);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de tous les Enseignants : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la récupération de tous les Enseignants.");
            }
        }






    }
}
