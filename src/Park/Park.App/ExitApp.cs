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
        EnterRep _enterRep = IocManager.GetRequiredService<EnterRep>();
        ExitRep _exitRep = IocManager.GetRequiredService<ExitRep>();

        public async Task<ExitRespDto> Exit(ExitReqDto dto)
        {
            /* 1.是否在场
             * 2.是否在保通行证
             * 3.算费,是否免费期内
             * 4.是否已经缴费
             */

            EnterEntity enterEntity = await _enterRep.FirstOrDefaultAsync($"where plate_no='{dto.PlateNo}'");
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
