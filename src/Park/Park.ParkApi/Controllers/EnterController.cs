using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DI;
using Microsoft.AspNetCore.Mvc;
using Park.App;
using Park.Dto;

namespace Park.ParkApi.Controllers
{
    [Route("park/[controller]")]
    public class EnterController : ControllerBase
    {
        EnterApp _enterApp = IocManager.GetRequiredService<EnterApp>();

        [HttpGet]
        public void Get()
        {

        }

        [HttpGet("{plateNo}")]
        public void Get(string plateNo)
        {

        }

        [HttpPost]
        public Task Post([FromBody] EnterReqDto dto)
        {
            return _enterApp.Enter(dto);
        }

        [HttpPost]
        public void Record([FromBody] EnterReqDto dto)
        {
        }
    }
}
