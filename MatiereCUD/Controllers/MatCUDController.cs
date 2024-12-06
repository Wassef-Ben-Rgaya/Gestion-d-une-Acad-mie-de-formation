using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Serilog;
using Service;

namespace ArticlesCUD.Controllers
{
    [Produces("application/json")]
    [Route("MatiereCUD")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class MatCUDController : ControllerBase
    {
        private readonly IMatiereService _service;
        private readonly Serilog.ILogger _logger;

        public MatCUDController(IMatiereService service, Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }


        [Route("AddMatiere")]
        [HttpPost]
        public async Task<ActionResult> AjoutMatiere(MatiereDto Matiere)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var ajt = await _service.AddMatiere(Matiere).ConfigureAwait(false);
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

                _logger.Error("Erreur AjoutMatiere <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


        /// <summary>
        /// Modifs the Article.
        /// </summary>
        /// <param name="Matiere">The Article.</param>
        /// <returns></returns>
        [Route("UpdMatiere")]
        [HttpPut]
        public async Task<ActionResult> ModifMatiere(MatiereDto Matiere)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var upd = await _service.UpdMatiere(Matiere).ConfigureAwait(false);
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

                _logger.Error("Erreur ModifMatiere <==> " + ex.ToString());
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
        [Route("DelMatiere/{MatiereID}")]
        [HttpDelete]
        public async Task<ActionResult> DeletMatiere(int MatiereID)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var del = await _service.delMatiere(MatiereID).ConfigureAwait(false);

                if (del)
                {
                    var showmessage = "Matiere supprime avec succes";
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

                _logger.Error("Erreur DeletMatiere <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }
    }
}
