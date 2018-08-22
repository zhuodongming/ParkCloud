using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Park.Entity
{
    [TableName("park")]
    [PrimaryKey("park_id", AutoIncrement = false)]
    public class ParkEntity
    {
        public string park_id { get; set; }//车场id
        public string park_key { get; set; }//车场key
        public string park_name { get; set; }//车场名称
        public int status { get; set; }//状态 0:禁用，1:启用
        public string remark { get; set; }//备注
        public DateTime create_time { get; set; }
        public string create_ip { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_ip { get; set; }
    }
}
