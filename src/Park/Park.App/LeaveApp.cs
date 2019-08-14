using Infrastructure.Helper;
using Park.Entity;
using Park.Entity.DTO;
using Park.Repository;
using System;
using System.Threading.Tasks;

namespace Park.App
{
    public class LeaveApp
    {
        ParkRep<blacklistPO> _blacklistRep = new ParkRep<blacklistPO>();
        ParkRep<passportPO> _passportRep = new ParkRep<passportPO>();
        ParkRep<entryPO> _entryRep = new ParkRep<entryPO>();
        ParkRep<leavePO> _leaveRep = new ParkRep<leavePO>();

        public async Task<LeaveOutDto> Exit(LeaveInDTO dto)
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

            var leaveOutDTO = new LeaveOutDto();
            parkPO parkPO = (parkPO)HttpContextEx.Current.Items["ParkUser"];

            //通行证检查
            passportPO passportPO = await _passportRep.FirstOrDefaultAsync($"where park_id='{parkPO.park_id}' and plate_no='{dto.PlateNo}'");
            if (passportPO != null)
            {
                DateTime now = DateTime.Now;
                if (passportPO.effective_time < now && now < passportPO.expiry_time)
                {
                    leaveOutDTO.IsPassport = true;
                    leaveOutDTO.EffectiveTime = passportPO.effective_time;
                    leaveOutDTO.ExpiryTime = passportPO.expiry_time;
                    return leaveOutDTO;
                }
            }

            //黑名单检查
            var blacklistPO = await _blacklistRep.FirstOrDefaultAsync($"where park_id='{parkPO.park_id}' and plate_no='{dto.PlateNo}'");
            if (blacklistPO != null)
            {
                DateTime now = DateTime.Now;
                if (blacklistPO.effective_time < now && now < blacklistPO.expiry_time)
                {
                    leaveOutDTO.IsBlacklist = true;
                    leaveOutDTO.EffectiveTime = blacklistPO.effective_time;
                    leaveOutDTO.ExpiryTime = blacklistPO.expiry_time;
                    return leaveOutDTO;
                }
            }

            var enterPO = await _entryRep.FirstOrDefaultAsync($"where park_id='{parkPO.park_id}' and plate_no='{dto.PlateNo}'");
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
                    entry_time = enterPO.entry_time,
                    entry_no = enterPO.entry_no,
                    leave_time = DateTime.Now,
                    exit_no = dto.ExitNo,
                    parking_time = 10,
                    receivable = 0,
                    paid = 0,
                    discount = 0,
                    vehicle_type = dto.VehicleType,
                    pass_mode = dto.PassMode,
                    pic_url = dto.PicUrl,
                    create_time = DateTime.Now,
                };
                await _leaveRep.InsertAsync(leavePO);
            }

            return null;
        }
    }
}
