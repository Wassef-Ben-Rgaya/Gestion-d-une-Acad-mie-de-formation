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
    [Route("Etudiant")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class EtudRechercheController : ControllerBase
    {
        private readonly IEtudiantService _service;
        private readonly Serilog.ILogger _logger;

        public EtudRechercheController(IEtudiantService service, Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetEtudiant/{EtudiantID}")]
        public async Task<ActionResult<EtudiantDto>> GetEtudiant(int EtudiantID)
        {
            try
            {
                var Etudiant = await _service.GetEtudiant(EtudiantID).ConfigureAwait(false);
                if (Etudiant == null)
                {
                    return NotFound();
                }
                return Ok(Etudiant);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de Etudiant : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la récupération de Etudiant.");
            }
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<EtudiantDto>>> GetAll()
        {
            try
            {
                var Etudiant = await _service.GetAllEtudiants().ConfigureAwait(false);
                if (Etudiant == null || !Etudiant.Any())
                {
                    return NotFound("Aucun Departement trouvé.");
                }
                return Ok(Etudiant);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de tous les Etudiants : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la récupération de tous les Etudiants.");
            }
        }






    }
}
