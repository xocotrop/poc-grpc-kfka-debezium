using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PockGRPC.Models;

namespace PockGRPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public static List<Modelo> _modelo = new List<Modelo>();

        public ValuesController()
        {

        }

        [HttpGet("clear")]
        public void Clear()
        {
            _modelo.Clear();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_modelo);
        }
    }
}
