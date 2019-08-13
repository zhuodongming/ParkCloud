using System;
using System.Collections.Generic;
using System.Text;

namespace Park.Entity.DTO.Auth
{
    public class LoginInDTO
    {
        public string Account { get; set; }//账号名称
        public string EMail { get; set; }//电子邮箱
        public string PhoneNumber { get; set; }//手机号码

        public string Password { get; set; }//密码
    }
}
