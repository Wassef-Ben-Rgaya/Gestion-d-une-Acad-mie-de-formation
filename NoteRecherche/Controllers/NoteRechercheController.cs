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
    [Route("Note")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class NotRechercheController : ControllerBase
    {
        private readonly INoteService _service;
        private readonly Serilog.ILogger _logger;

        public NotRechercheController(INoteService service, Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetNote/{NoteID}")]
        public async Task<ActionResult<NoteDto>> GetArticle(int MatiereID)
        {
            try
            {
                var Note = await _service.GetNote(MatiereID).ConfigureAwait(false);
                if (Note == null)
                {
                    return NotFound();
                }
                return Ok(Note);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de Note : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la récupération de Note.");
            }
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetAll()
        {
            try
            {
                var Note = await _service.GetAllNotes().ConfigureAwait(false);
                if (Note == null || !Note.Any())
                {
                    return NotFound("Aucun Note trouvé.");
                }
                return Ok(Note);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de tous les Notes : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la récupération de tous les Notes.");
            }
        }






    }
}
