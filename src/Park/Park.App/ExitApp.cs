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
    public class ExitApp
    {
        BlacklistRep _blacklistRep = IocManager.GetRequiredService<BlacklistRep>();
        PassportRep _passportRep = IocManager.GetRequiredService<PassportRep>();
        EnterRep _enterRep = IocManager.GetRequiredService<EnterRep>();
        ExitRep _exitRep = IocManager.GetRequiredService<ExitRep>();

        public async Task<ExitRespDto> Exit(ExitReqDto dto)
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

            var respDto = new ExitRespDto();
            ParkEntity parkEntity = (ParkEntity)HttpContextEx.Current.Items["ParkUser"];

            //通行证检查
            PassportEntity passportEntity = await _passportRep.FirstOrDefaultAsync($"where park_id='{parkEntity.park_id}' and plate_no='{dto.PlateNo}'");
            if (passportEntity != null)
            {
                DateTime now = DateTime.Now;
                if (passportEntity.effective_time < now && now < passportEntity.expiry_time)
                {
                    respDto.IsPassport = true;
                    respDto.EffectiveTime = passportEntity.effective_time;
                    respDto.ExpiryTime = passportEntity.expiry_time;
                    return respDto;
                }
            }

            //黑名单检查
            BlacklistEntity blacklistEntity = await _blacklistRep.FirstOrDefaultAsync($"where park_id='{parkEntity.park_id}' and plate_no='{dto.PlateNo}'");
            if (blacklistEntity != null)
            {
                DateTime now = DateTime.Now;
                if (blacklistEntity.effective_time < now && now < blacklistEntity.expiry_time)
                {
                    respDto.IsBlacklist = true;
                    respDto.EffectiveTime = blacklistEntity.effective_time;
                    respDto.ExpiryTime = blacklistEntity.expiry_time;
                    return respDto;
                }
            }

            EnterEntity enterEntity = await _enterRep.FirstOrDefaultAsync($"where park_id='{parkEntity.park_id}' and plate_no='{dto.PlateNo}'");
            if (enterEntity != null)
            {
                //算费服务
                var exitEntity = new ExitEntity
                {
                    id = new IdWorker(1, 1).NextId(),
                    req_id = dto.ReqID,
                    park_id = enterEntity.park_id,
                    park_name = enterEntity.park_name,
                    plate_no = dto.PlateNo,
                    plate_color = dto.PlateColor,
                    enter_time = enterEntity.enter_time,
                    enter_no = enterEntity.enter_no,
                    exit_time = DateTime.Now,
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
                await _exitRep.InsertAsync(exitEntity);
            }

            return null;
        }
    }
}
