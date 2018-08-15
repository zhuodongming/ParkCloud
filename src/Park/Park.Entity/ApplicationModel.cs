using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Park.Entity
{
    [TableName("application")]
    [PrimaryKey("app_id", AutoIncrement = false)]
    public class ApplicationModel
    {
        public string app_id { get; set; }//id
        public string app_key { get; set; }//key
        public string app_name { get; set; }//应用名称
        public string description { get; set; }//描述
        public DateTime expiry_date { get; set; }//过期日期
        public int status { get; set; }//状态
        public string createip { get; set; }//
        public DateTime createtime { get; set; }//
        public string modifyip { get; set; }//
        public DateTime? modifytime { get; set; }//
    }
}
