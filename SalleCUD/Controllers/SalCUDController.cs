using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Serilog;
using Service;

namespace ArticlesCUD.Controllers
{
    [Produces("application/json")]
    [Route("SalleCUD")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class SalCUDController : ControllerBase
    {
        private readonly ISalleService _service;
        private readonly Serilog.ILogger _logger;

        public SalCUDController(ISalleService service, Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }


        [Route("AddSalle")]
        [HttpPost]
        public async Task<ActionResult> AjoutSalle(SalleDto Salle)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {

                var ajt = await _service.AddSalle(Salle).ConfigureAwait(false);
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

                _logger.Error("Erreur AjoutSalle <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


        /// <summary>
        /// Modifs the Article.
        /// </summary>
        /// <param name="Salle">The Article.</param>
        /// <returns></returns>
        [Route("UpdSalle")]
        [HttpPut]
        public async Task<ActionResult> ModifSalle(SalleDto Salle)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var upd = await _service.UpdSalle(Salle).ConfigureAwait(false);
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

                _logger.Error("Erreur ModifSalle <==> " + ex.ToString());
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
        [Route("DelSalle/{SalleID}")]
        [HttpDelete]
        public async Task<ActionResult> DeletSalle(int SalleID)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var del = await _service.delSalle(SalleID).ConfigureAwait(false);

                if (del)
                {
                    var showmessage = "Salle supprime avec succes";
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

                _logger.Error("Erreur DeletSalle <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }
    }
}
