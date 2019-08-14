using System;
using NPoco;

namespace Park.Repository
{
    [TableName("application")]
    [PrimaryKey("app_id", AutoIncrement = false)]
    public class applicationPO
    {
        public string app_id { get; set; }//app_id
        public string app_key { get; set; }//app_key
        public string app_name { get; set; }//应用名称
        public DateTime effective_time { get; set; }//生效时间
        public DateTime expiry_time { get; set; }//失效时间
        public bool status { get; set; }//状态 0:禁用，1:启用
        public string remark { get; set; }//备注
        public DateTime create_time { get; set; }//
        public DateTime? modify_time { get; set; }//
    }

    [TableName("blacklist")]
    [PrimaryKey("id", AutoIncrement = false)]
    public class blacklistPO
    {
        public long id { get; set; }//id
        public string park_id { get; set; }//车场id
        public string park_name { get; set; }//车场名称
        public string plate_no { get; set; }//车牌号码
        public string plate_color { get; set; }//车牌颜色
        public DateTime effective_time { get; set; }//生效时间
        public DateTime expiry_time { get; set; }//失效时间
        public string creater { get; set; }//创建者
        public bool status { get; set; }//状态 0:禁用，1:启用
        public string remark { get; set; }//备注
        public DateTime create_time { get; set; }//
        public DateTime? modify_time { get; set; }//
    }

    [TableName("entry")]
    [PrimaryKey("id", AutoIncrement = false)]
    public class entryPO
    {
        public long id { get; set; }//id
        public string req_id { get; set; }//请求id
        public string park_id { get; set; }//车场id
        public string park_name { get; set; }//车场名称
        public string plate_no { get; set; }//车牌号码
        public string plate_color { get; set; }//车牌颜色
        public DateTime entry_time { get; set; }//入场时间
        public string entry_no { get; set; }//入口编号
        public sbyte vehicle_type { get; set; }//车辆类型
        public sbyte pass_mode { get; set; }//通行方式
        public string pic_url { get; set; }//图片url
        public DateTime create_time { get; set; }//
        public DateTime? modify_time { get; set; }//
    }

    [TableName("leave")]
    [PrimaryKey("id", AutoIncrement = false)]
    public class leavePO
    {
        public long id { get; set; }//id
        public string req_id { get; set; }//请求id
        public string park_id { get; set; }//车场id
        public string park_name { get; set; }//车场名称
        public string plate_no { get; set; }//车牌号码
        public string plate_color { get; set; }//车牌颜色
        public DateTime entry_time { get; set; }//入场时间
        public string entry_no { get; set; }//入口编号
        public DateTime? leave_time { get; set; }//离场时间
        public string exit_no { get; set; }//出口编号
        public int? parking_time { get; set; }//停车时长 单位:分钟
        public long? receivable { get; set; }//应收 单位：分
        public long? paid { get; set; }//实收 单位：分
        public long? discount { get; set; }//折扣 单位：分
        public sbyte vehicle_type { get; set; }//车辆类型
        public sbyte pass_mode { get; set; }//通行方式
        public string pic_url { get; set; }//图片url
        public DateTime create_time { get; set; }//
        public DateTime? modify_time { get; set; }//
    }

    [TableName("park")]
    [PrimaryKey("park_id", AutoIncrement = false)]
    public class parkPO
    {
        public string park_id { get; set; }//车场id
        public string park_key { get; set; }//车场key
        public string park_name { get; set; }//车场名称
        public bool status { get; set; }//状态 0:禁用，1:启用
        public string remark { get; set; }//备注
        public DateTime create_time { get; set; }//
        public DateTime? modify_time { get; set; }//
    }

    [TableName("passport")]
    [PrimaryKey("id", AutoIncrement = false)]
    public class passportPO
    {
        public long id { get; set; }//id
        public string park_id { get; set; }//车场id
        public string park_name { get; set; }//车场名称
        public string plate_no { get; set; }//车牌号码
        public string plate_color { get; set; }//车牌颜色
        public DateTime effective_time { get; set; }//生效时间
        public DateTime expiry_time { get; set; }//失效时间
        public string creater { get; set; }//创建者
        public bool status { get; set; }//状态 0:禁用，1:启用
        public string remark { get; set; }//备注
        public DateTime create_time { get; set; }//
        public DateTime? modify_time { get; set; }//
    }

}
