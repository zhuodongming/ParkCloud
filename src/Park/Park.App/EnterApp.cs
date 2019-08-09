using Infrastructure.Helper;
using Park.Entity;
using Park.Entity.Dto;
using Park.Repository;
using System;
using System.Threading.Tasks;

namespace Park.App
{
    public class EnterApp
    {
        EnterRep _enterRep = new EnterRep();

        public Task Enter(EnterInDto dto)
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
