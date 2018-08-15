using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Park.Dto;

namespace Park.ParkApi.Controllers
{
    [Route("park/[controller]")]
    public class EnterController : ControllerBase
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
        public void Post([FromBody] EnterDto dto)
        {

        }

        [HttpPost]
        public void Record([FromBody] ExitDto dto)
        {
        }
    }
}
