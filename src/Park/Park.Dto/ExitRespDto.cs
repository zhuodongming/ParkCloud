using System;
using System.Collections.Generic;
using System.Text;

namespace Park.Dto
{
    public class ExitRespDto
    {
        public bool IsPassPort { get; set; }//是否是通行证
        public bool IsBlacklist { get; set; }//是否黑名单
        public DateTime EffectiveTime { get; set; }//生效时间
        public DateTime ExpiryTime { get; set; }//失效时间
        public string remark { get; set; }//备注
    }
}
