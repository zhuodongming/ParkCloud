using Microsoft.AspNetCore.Mvc;
using Park.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Park.ParkApi.Controllers
{
    [Route("park/[controller]")]
    public class ExitController : ControllerBase
    {
        [HttpGet]
        public void Get()
        {

        }

        [HttpGet("{plateNo}")]
        public void Get(string plateNo)
        {

        }

        [HttpPost]
        public void Post([FromBody] ExitDto dto)
        {

        }

        [HttpPost]
        public void Record([FromBody] ExitDto dto)
        {
        }
    }
}
