using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Park.Entity
{
    [TableName("blacklist")]
    [PrimaryKey("id", AutoIncrement = false)]
    public class BlacklistEntity
    {
        public long id { get; set; }
        public string park_id { get; set; }//车场id
        public string park_name { get; set; }//车场名称
        public string plate_no { get; set; }//车牌号码
        public string plate_color { get; set; }//车牌颜色
        public DateTime effective_time { get; set; }//生效时间
        public DateTime expiry_time { get; set; }//失效时间
        public string creater { get; set; }//创建者
        public int status { get; set; }//状态 0:禁用，1:启用
        public string remark { get; set; }//备注
        public DateTime create_time { get; set; }
        public string create_ip { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_ip { get; set; }
    }
}
