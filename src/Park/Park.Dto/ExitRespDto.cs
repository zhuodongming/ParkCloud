using System;
using System.Collections.Generic;
using System.Text;

namespace Park.Dto
{
    public class ExitRespDto
    {
        public bool IsPassPort { get; set; }//是否是通行证
        public DateTime EffectiveDate { get; set; }//生效日期
        public DateTime ExpiryDate { get; set; }//失效日期
    }
}
