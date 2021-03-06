﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Park.Dto
{
    public class ExitReqDto
    {
        public string ReqID { get; set; }//请求ID
        public string PlateNo { get; set; }//车牌号码
        public string PlateColor { get; set; }//车牌颜色
        public string ExitNo { get; set; }//出口编号
        public int VehicleType { get; set; }//车辆类型（小车=0, 大车=1, 超大车=2）
        public int PassMode { get; set; }//通行方式（0:临时车,1:通行证）
        public string PicUrl { get; set; }//图片地址
    }
}
