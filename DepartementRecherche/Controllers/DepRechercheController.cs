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
    [Route("Departement")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class ArtRechercheController : ControllerBase
    {
        private readonly IDepartementService _service;
        private readonly Serilog.ILogger _logger;

        public ArtRechercheController(IDepartementService service, Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetDepartement/{DepartementID}")]
        public async Task<ActionResult<DepartementDto>> GetDepartement(int DepartementID)
        {
            try
            {
                var Departement = await _service.GetDepartement(DepartementID).ConfigureAwait(false);
                if (Departement == null)
                {
                    return NotFound();
                }
                return Ok(Departement);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de Departement : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la récupération de Departement.");
            }
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<DepartementDto>>> GetAll()
        {
            try
            {
                var Departement = await _service.GetAllDepartements().ConfigureAwait(false);
                if (Departement == null || !Departement.Any())
                {
                    return NotFound("Aucun Departement trouvé.");
                }
                return Ok(Departement);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de tous les Departements : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la récupération de tous les Departements.");
            }
        }






    }
}
