
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebPush.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using WebPush.Models;
    using WebPush.Core;
    using Newtonsoft.Json;

    [Route("api/[controller]")]
    public class PushController : Controller
    {
        // http://www.restapitutorial.com/lessons/httpmethods.html

        /// <summary>
        /// Retourne la liste des tous les boutons
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            return Json(PushRepository.AllPushButtons, settings);
        }

        /// <summary>
        /// Retourne la liste des boutons d'un client
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
        /// Enregistre un push
        /// </summary>
        /// <param name="pushButton">The value.</param>
        [HttpPost]
        public void Post([FromBody]Guid pushButton)
        {
            PushRepository.SavePush(pushButton);
        }

        /// <summary>
        /// Ajout un nouveau bouton à un client
        /// </summary>
        /// <param name="idCustomer">The identifier customer.</param>
        /// <param name="pushButton">The push button.</param>
        [HttpPut("{idCustomer}")]
        public void Put(int idCustomer, [FromBody]Guid pushButton)
        {
            PushRepository.Pair(idCustomer, pushButton);
        }

        /// <summary>
        /// Supprime un nouveau bouton à un client
        /// </summary>
        /// <param name="idCustomer">The identifier customer.</param>
        /// <param name="pushButton">The push button.</param>
        [HttpDelete("{idCustomer}")]
        public void Delete(int idCustomer, [FromBody]Guid pushButton)
        {
            PushRepository.Pair(idCustomer, pushButton);
        }
    }
}
