using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Park.Entity.DTO.Auth;

namespace Park.AuthApi.Controllers
{
    /// <summary>
    /// 登录及与登录相关的接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        //登录
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<LoginOutDTO> Login([FromBody]LoginInDTO inDTO)
        {
            //登录操作
            return new LoginOutDTO() { Token = Guid.NewGuid().ToString() };
        }

        //注销
        [HttpGet]
        public void Logout([FromQuery]string token)
        {
            //注销操作
        }

        //查询用户
        [HttpGet]
        public ActionResult<QueryUserOutDTO> QueryUser([FromQuery]string token)
        {
            //查询用户操作
            return new QueryUserOutDTO() { };
        }
    }
}