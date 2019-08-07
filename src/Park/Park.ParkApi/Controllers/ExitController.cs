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
        ExitApp _exitApp = new ExitApp();

        [HttpGet]
        public void Get()
        {

        }

        [HttpGet("{plateNo}")]
        public void Get(string plateNo)
        {

        }

        [HttpPost]
        public Task<ExitRespDto> Post([FromBody] ExitReqDto dto)
        {
            return _exitApp.Exit(dto);
        }

        [HttpPost]
        public void Record([FromBody] ExitReqDto dto)
        {
        }
    }
}
