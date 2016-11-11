// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
namespace WebPush.Controllers
{
    using System;
    using Core;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    /// <summary>
    /// Web API de gestion des boutons Push.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class PushController : Controller
    {
        // http://www.restapitutorial.com/lessons/httpmethods.html

        /// <summary>
        /// Retourne la liste de tous les boutons Push existants ainsi que la liste des évènements reçus.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            return Json(
                new
                {
                    PushButtons = PushRepository.AllPushButtons,
                    Pushings = PushRepository.AllPushings,
                    Customers = PushRepository.Customers
                },
                settings);
        }

        /// <summary>
        /// Retourne la liste des boutons Push d'un client.
        /// </summary>
        /// <param name="idCustomer">The identifier customer.</param>
        /// <returns></returns>
        [HttpGet("{idCustomer}")]
        public JsonResult Get(int idCustomer)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            return Json(PushRepository.GetCustomerPushButton(idCustomer), settings);
        }

        /// <summary>
        /// Enregistre un évènement de Push.
        /// </summary>
        /// <param name="pushButtonId">The value.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]Guid pushButtonId)
        {
            if (PushRepository.SavePush(pushButtonId))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Associe un bouton Push à un client.
        /// </summary>
        /// <param name="idCustomer">L'identifiant unique d'un client.</param>
        /// <param name="pushButtonId">L'identifiant unique d'un bouton Push.</param>
        [HttpPut("{idCustomer}")]
        public IActionResult Put(int idCustomer, [FromBody]Guid pushButtonId)
        {
            if (PushRepository.Pair(idCustomer, pushButtonId))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Supprime l'association d'un bouton Push avec un client
        /// </summary>
        /// <param name="idCustomer">L'identifiant unique d'un client.</param>
        /// <param name="pushButtonId">L'identifiant unique d'un bouton Push.</param>
        [HttpDelete("{idCustomer}")]
        public IActionResult Delete(int idCustomer, [FromBody]Guid pushButtonId)
        {
            if (PushRepository.Unpair(idCustomer, pushButtonId))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Réinitialise la base de données.
        /// </summary>
        [HttpDelete]
        [Route("reset")]
        public void Reset()
        {
            PushRepository.Reset();
        }
    }
}