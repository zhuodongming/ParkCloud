using Infrastructure.DI;
using Microsoft.AspNetCore.Mvc;
using Park.App;
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
        ExitApp _exitApp = IocManager.GetRequiredService<ExitApp>();

        [HttpGet]
        public void Get()
        {

        }

        [HttpGet("{plateNo}")]
        public void Get(string plateNo)
        {

        }

        [HttpPost]
        public Task Post([FromBody] ExitDto dto)
        {
            return _exitApp.Exit(dto);
        }

        [HttpPost]
        public void Record([FromBody] ExitDto dto)
        {
        }
    }
}
