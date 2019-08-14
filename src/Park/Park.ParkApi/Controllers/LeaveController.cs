using Microsoft.AspNetCore.Mvc;
using Park.App;
using Park.Entity.DTO;
using System.Threading.Tasks;

namespace Park.ParkApi.Controllers
{
    [Route("park/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        [HttpGet]
        public void Get()
        {

        }

        [HttpGet("{plateNo}")]
        public void Get(string plateNo)
        {

        }

        [HttpPost]
        public Task<LeaveOutDto> Post([FromBody] LeaveInDTO dto)
        {
            return new LeaveApp().Exit(dto);
        }

        [HttpPost]
        public void Record([FromBody] LeaveInDTO dto)
        {
        }
    }
}
