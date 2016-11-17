using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebPush.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private static int Count = 0;

        // GET: api/values
        [HttpGet]
        public string Get()
        {
            Count++;
            return Count.ToString();
        }

        [HttpGet]
        [Route("display")]
        public string Display()
        {
            return Count.ToString();
        }

    }
}
