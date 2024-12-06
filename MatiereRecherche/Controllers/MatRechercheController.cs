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
    [Route("Matiere")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class MatRechercheController : ControllerBase
    {
        private readonly IMatiereService _service;
        private readonly Serilog.ILogger _logger;

        public MatRechercheController(IMatiereService service, Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetMatiere/{MatiereID}")]
        public async Task<ActionResult<MatiereDto>> GetMatiere(int MatiereID)
        {
            try
            {
                var Matiere = await _service.GetMatiere(MatiereID).ConfigureAwait(false);
                if (Matiere == null)
                {
                    return NotFound();
                }
                return Ok(Matiere);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de Matiere : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la récupération de Matiere.");
            }
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<MatiereDto>>> GetAll()
        {
            try
            {
                var Matiere = await _service.GetAllMatieres().ConfigureAwait(false);
                if (Matiere == null || !Matiere.Any())
                {
                    return NotFound("Aucun Departement trouvé.");
                }
                return Ok(Matiere);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de tous les Matieres : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la récupération de tous les Matieres.");
            }
        }






    }
}
