using Infrastructure.Helper;
using Park.Entity;
using Park.Entity.DTO;
using Park.Repository;
using System;
using System.Threading.Tasks;

namespace Park.App
{
    public class EnterApp
    {
        public Task Enter(EnterInDTO dto)
        {
            var parkPO = (parkPO)HttpContextEx.Current.Items["ParkUser"];

            var entity = new enterPO
            {
                id = new IdWorker(1, 1).NextId(),
                req_id = dto.ReqID,
                park_id = parkPO.park_id,
                park_name = parkPO.park_name,
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
            return new ParkRep<enterPO>().InsertAsync(entity);
        }
    }
}
