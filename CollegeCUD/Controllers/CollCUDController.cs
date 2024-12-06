using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Serilog;
using Service;

namespace ArticlesCUD.Controllers
{
    [Produces("application/json")]
    [Route("CollegeCUD")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class CollCUDController : ControllerBase
    {
        private readonly ICollegeService _service;
        private readonly Serilog.ILogger _logger;

        public CollCUDController(ICollegeService service, Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }


        [Route("AddCollege")]
        [HttpPost]
        public async Task<ActionResult> AjoutCollege(CollegeDto College)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var ajt = await _service.AddCollege(College).ConfigureAwait(false);
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
        /// <param name="College">The Article.</param>
        /// <returns></returns>
        [Route("UpdCollege")]
        [HttpPut]
        public async Task<ActionResult> ModifCollege(CollegeDto College)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var upd = await _service.UpdCollege(College).ConfigureAwait(false);
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

                _logger.Error("Erreur ModifCollege <==> " + ex.ToString());
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
        [Route("DelCollege/{CollegeID}")]
        [HttpDelete]
        public async Task<ActionResult> DeletCollege(int CollegeID)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var del = await _service.delCollege(CollegeID).ConfigureAwait(false);

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

                _logger.Error("Erreur DeletCollege <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }
    }
}
