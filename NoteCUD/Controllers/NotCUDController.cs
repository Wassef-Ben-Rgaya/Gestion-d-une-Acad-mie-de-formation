using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Serilog;
using Service;

namespace ArticlesCUD.Controllers
{
    [Produces("application/json")]
    [Route("NoteCUD")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class NotCUDController : ControllerBase
    {
        private readonly INoteService _service;
        private readonly Serilog.ILogger _logger;

        public NotCUDController(INoteService service, Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }


        [Route("AddNote")]
        [HttpPost]
        public async Task<ActionResult> AjoutNote(NoteDto Note)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var ajt = await _service.AddNote(Note).ConfigureAwait(false);
                if (ajt)
                {
                    var showmessage = "Insertion effectuee avec succes";
                    dict.Add("Message", showmessage);
                    return Ok(dict);

                }
                else
                {
                    var showmessage = "Echec d'insertion... Reprendre l'operation";
                    dict.Add("Message", showmessage);
                    return BadRequest(dict);
                }

            }
            catch (Exception ex)
            {

                _logger.Error("Erreur AjoutNote <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


        /// <summary>
        /// Modifs the Article.
        /// </summary>
        /// <param name="Note">The Article.</param>
        /// <returns></returns>
        [Route("UpdNote")]
        [HttpPut]
        public async Task<ActionResult> ModifNote(NoteDto Note)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var upd = await _service.UpdNote(Note).ConfigureAwait(false);
                if (upd)
                {
                    var showmessage = "Modification effectuee avec succes";
                    dict.Add("Message", showmessage);
                    return Ok(dict);

                }
                else
                {
                    var showmessage = "Echec de modification... Reprendre l'operation";
                    dict.Add("Message", showmessage);
                    return BadRequest(dict);

                }

            }
            catch (Exception ex)
            {

                _logger.Error("Erreur ModifNote <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }
        /// <summary>
        /// Suppression du Article.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        [Route("DelNote/{NoteID}")]
        [HttpDelete]
        public async Task<ActionResult> DeletNote(int MatiereID)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var del = await _service.delNote(MatiereID).ConfigureAwait(false);

                if (del)
                {
                    var showmessage = "College supprime avec succes";
                    dict.Add("Message", showmessage);
                    return Ok(dict);
                }


                else
                {
                    var showmessage = "Erreur lors de la suppression...Svp reprendre l'operation";
                    dict.Add("Message", showmessage);
                    return BadRequest(dict);
                }

            }
            catch (Exception ex)
            {

                _logger.Error("Erreur DeletNote <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }
    }
}
