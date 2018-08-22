using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Park.Entity
{
    [TableName("application")]
    [PrimaryKey("app_id", AutoIncrement = false)]
    public class ApplicationEntity
    {
        public string app_id { get; set; }//id
        public string app_key { get; set; }//key
        public string app_name { get; set; }//应用名称
        public DateTime effective_time { get; set; }//生效时间
        public DateTime expiry_time { get; set; }//失效时间
        public int status { get; set; }//状态
        public string remark { get; set; }//备注
        public string createip { get; set; }//
        public DateTime createtime { get; set; }//
        public string modifyip { get; set; }//
        public DateTime? modifytime { get; set; }//
    }
}
