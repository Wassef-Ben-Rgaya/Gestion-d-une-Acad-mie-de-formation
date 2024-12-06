using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Serilog;
using Service;

namespace ArticlesCUD.Controllers
{
    [Produces("application/json")]
    [Route("EnseignantCUD")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class EnsCUDController : ControllerBase
    {
        private readonly IEnseignantService _service;
        private readonly Serilog.ILogger _logger;

        public EnsCUDController(IEnseignantService service, Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }


        [Route("AddEnseignant")]
        [HttpPost]
        public async Task<ActionResult> AjoutEnseignant(EnseignantDto Enseignant)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var ajt = await _service.AddEnseignant(Enseignant).ConfigureAwait(false);
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

                _logger.Error("Erreur AjoutCollege <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


        /// <summary>
        /// Modifs the Article.
        /// </summary>
        /// <param name="Enseignant">The Article.</param>
        /// <returns></returns>
        [Route("UpdEnseignant")]
        [HttpPut]
        public async Task<ActionResult> ModifEnseignant(EnseignantDto Enseignant)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var upd = await _service.UpdEnseignant(Enseignant).ConfigureAwait(false);
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

                _logger.Error("Erreur ModifEnseignant <==> " + ex.ToString());
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
        [Route("DelEnseignant/{EnseignantID}")]
        [HttpDelete]
        public async Task<ActionResult> DeletEnseignant(int EnseignantID)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var del = await _service.delEnseignant(EnseignantID).ConfigureAwait(false);

                if (del)
                {
                    var showmessage = "Enseignant supprime avec succes";
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

                _logger.Error("Erreur DeletEnseignant <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }
    }
}
