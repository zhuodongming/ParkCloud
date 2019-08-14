using Infrastructure.Helper;
using Park.Entity;
using Park.Entity.DTO;
using Park.Repository;
using System;
using System.Threading.Tasks;

namespace Park.App
{
    public class EntryApp
    {
        public Task Enter(EntryInDTO dto)
        {
            var parkPO = (parkPO)HttpContextEx.Current.Items["ParkUser"];

            var entryPO = new entryPO
            {
                id = new IdWorker(1, 1).NextId(),
                req_id = dto.ReqID,
                park_id = parkPO.park_id,
                park_name = parkPO.park_name,
                plate_no = dto.PlateNo,
                plate_color = dto.PlateColor,
                entry_time = DateTime.Now,
                entry_no = dto.EnterNo,
                vehicle_type = dto.VehicleType,
                pass_mode = dto.PassMode,
                pic_url = dto.PicUrl,
                create_time = DateTime.Now,
            };
            return new ParkRep<entryPO>().InsertAsync(entryPO);
        }
    }
}
