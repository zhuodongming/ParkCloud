using Infrastructure.DI;
using Infrastructure.Helper;
using Park.Dto;
using Park.Entity;
using Park.Rep;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Park.App
{
    [Scoped]
    public class EnterApp
    {
        EnterRep _enterRep = IocManager.GetRequiredService<EnterRep>();

        public Task Enter(EnterReqDto dto)
        {
            ParkEntity parkEntity = (ParkEntity)HttpContextEx.Current.Items["ParkUser"];
            var entity = new EnterEntity
            {
                id = new IdWorker(1, 1).NextId(),
                req_id = dto.ReqID,
                park_id = parkEntity.park_id,
                park_name = parkEntity.park_name,
                plate_no = dto.PlateNo,
                plate_color = dto.PlateColor,
                enter_time = DateTime.Now,
                enter_no = dto.EnterNo,
                vehicle_type = dto.VehicleType,
                pass_mode = dto.PassMode,
                pic_url = dto.PicUrl,
                create_ip = "",
                create_time = DateTime.Now,
            };
            return _enterRep.InsertAsync(entity);
        }
    }
}
