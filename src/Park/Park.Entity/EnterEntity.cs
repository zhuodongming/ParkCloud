using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Park.Entity
{
    [TableName("enter")]
    [PrimaryKey("id", AutoIncrement = false)]
    public class EnterEntity
    {
        public long id { get; set; }//id
        public string req_id { get; set; }//请求id
        public string park_id { get; set; }//车场id
        public string park_name { get; set; }//车场名称
        public string plate_no { get; set; }//车牌号码
        public string plate_color { get; set; }//车牌颜色
        public DateTime enter_time { get; set; }//入场时间
        public string enter_no { get; set; }//入口编号
        public int vehicle_type { get; set; }//车辆类型
        public int pass_mode { get; set; }//通行方式
        public string pic_url { get; set; }//图片url
        public DateTime create_time { get; set; }
        public string create_ip { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_ip { get; set; }
    }
}
