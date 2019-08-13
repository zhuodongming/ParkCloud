using Infrastructure.Helper;
using Park.Entity;
using Park.Entity.DTO;
using Park.Repository;
using System;
using System.Threading.Tasks;

namespace Park.App
{
    public class ExitApp
    {
        ParkRep<blacklistPO> _blacklistRep = new ParkRep<blacklistPO>();
        ParkRep<passportPO> _passportRep = new ParkRep<passportPO>();
        ParkRep<enterPO> _enterRep = new ParkRep<enterPO>();
        ParkRep<leavePO> _exitRep = new ParkRep<leavePO>();

        public async Task<ExitOutDTO> Exit(ExitInDTO dto)
        {
            /* 通行证检查
             * 黑名单检查
             * 
             * 算费
             * 1.是否在场
             * 2.是否在保通行证
             * 3.算费,是否免费期内
             * 4.是否已经缴费
             */

            var respDTO = new ExitOutDTO();
            parkPO parkEntity = (parkPO)HttpContextEx.Current.Items["ParkUser"];

            //通行证检查
            passportPO passportEntity = await _passportRep.FirstOrDefaultAsync($"where park_id='{parkEntity.park_id}' and plate_no='{dto.PlateNo}'");
            if (passportEntity != null)
            {
                DateTime now = DateTime.Now;
                if (passportEntity.effective_time < now && now < passportEntity.expiry_time)
                {
                    respDTO.IsPassport = true;
                    respDTO.EffectiveTime = passportEntity.effective_time;
                    respDTO.ExpiryTime = passportEntity.expiry_time;
                    return respDTO;
                }
            }

            //黑名单检查
            var blacklistPO = await _blacklistRep.FirstOrDefaultAsync($"where park_id='{parkEntity.park_id}' and plate_no='{dto.PlateNo}'");
            if (blacklistPO != null)
            {
                DateTime now = DateTime.Now;
                if (blacklistPO.effective_time < now && now < blacklistPO.expiry_time)
                {
                    respDTO.IsBlacklist = true;
                    respDTO.EffectiveTime = blacklistPO.effective_time;
                    respDTO.ExpiryTime = blacklistPO.expiry_time;
                    return respDTO;
                }
            }

            var enterPO = await _enterRep.FirstOrDefaultAsync($"where park_id='{parkEntity.park_id}' and plate_no='{dto.PlateNo}'");
            if (enterPO != null)
            {
                //算费服务
                var leavePO = new leavePO
                {
                    id = new IdWorker(1, 1).NextId(),
                    req_id = dto.ReqID,
                    park_id = enterPO.park_id,
                    park_name = enterPO.park_name,
                    plate_no = dto.PlateNo,
                    plate_color = dto.PlateColor,
                    enter_time = enterPO.enter_time,
                    enter_no = enterPO.enter_no,
                    leave_time = DateTime.Now,
                    exit_no = dto.ExitNo,
                    parking_time = 10,
                    receivable = 0,
                    paid = 0,
                    discount = 0,
                    vehicle_type = dto.VehicleType,
                    pass_mode = dto.PassMode,
                    pic_url = dto.PicUrl,
                    create_ip = "",
                    create_time = DateTime.Now,
                };
                await _exitRep.InsertAsync(leavePO);
            }

            return null;
        }
    }
}
