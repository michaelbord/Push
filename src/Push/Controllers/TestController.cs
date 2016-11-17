using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebPush.Controllers
{
    /// <summary>
    /// Outils de test.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private static int Count = 0;

        /// <summary>
        /// Incrémente et retourne la valeur du compteur.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            Count++;
            return Count.ToString();
        }

        /// <summary>
        /// Retourne la valeur du compteur.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("display")]
        public string Display()
        {
            return Count.ToString();
        }
    }
}