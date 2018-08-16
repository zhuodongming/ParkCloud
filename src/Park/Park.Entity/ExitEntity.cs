using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Park.Entity
{
    [TableName("exit")]
    [PrimaryKey("id", AutoIncrement = false)]
    public class ExitEntity
    {
        public long id { get; set; }//id
        public string req_id { get; set; }//请求id
        public string park_id { get; set; }//车场id
        public string park_name { get; set; }//车场名称
        public string plate_no { get; set; }//车牌号码
        public string plate_color { get; set; }//车牌颜色
        public DateTime enter_time { get; set; }//入场时间
        public string enter_no { get; set; }//入口编号
        public DateTime exit_time { get; set; }//出场时间
        public string exit_no { get; set; }//出口编号
        public int parking_time { get; set; }//停车时长 单位:分钟
        public long receivable { get; set; }//应收 单位：分
        public long paid { get; set; }//实收 单位：分
        public long discount { get; set; }//折扣 单位：分
        public int vehicle_type { get; set; }//车辆类型
        public int pass_mode { get; set; }//通行方式
        public string pic_url { get; set; }//图片url
        public DateTime create_time { get; set; }
        public string create_ip { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_ip { get; set; }
    }
}
