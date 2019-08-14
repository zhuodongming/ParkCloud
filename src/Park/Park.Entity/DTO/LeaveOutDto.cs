using System;
using System.Collections.Generic;
using System.Text;

namespace Park.Entity.DTO
{
    public class LeaveOutDto
    {
        public string PlateNo { get; set; }//车牌号码
        public string PlateColor { get; set; }//车牌颜色
        public DateTime EnterTime { get; set; }//入场时间
        public string EnterNo { get; set; }//入口编号
        public DateTime ExitTime { get; set; }//出场时间
        public string ExitNo { get; set; }//出口编号
        public string ParkingTime { get; set; }//停车时长 单位:分钟
        public long Receivable { get; set; }//应收 单位：分
        public long Paid { get; set; }//实收 单位：分
        public long Discount { get; set; }//折扣 单位：分
        public string Remark { get; set; }//备注

        public bool IsPassport { get; set; }//是否通行证
        public bool IsBlacklist { get; set; }//是否黑名单
        public DateTime EffectiveTime { get; set; }//生效时间
        public DateTime ExpiryTime { get; set; }//失效时间
    }
}
