﻿using Microsoft.AspNetCore.Mvc;
using Park.App;
using Park.Entity.Dto;
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
        public Task<ExitOutDto> Post([FromBody] ExitInDto dto)
        {
            return _exitApp.Exit(dto);
        }

        [HttpPost]
        public void Record([FromBody] ExitInDto dto)
        {
        }
    }
}
