using Microsoft.AspNetCore.Mvc;
using Park.App;
using Park.Entity.DTO;
using System.Threading.Tasks;

namespace Park.ParkApi.Controllers
{
    [Route("park/[controller]")]
    [ApiController]
    public class EnterController : ControllerBase
    {
        EnterApp _enterApp = new EnterApp();

        [HttpGet]
        public void Get()
        {

        }

        [HttpGet("{plateNo}")]
        public void Get(string plateNo)
        {

        }

        [HttpPost]
        public Task Post([FromBody] EnterInDTO dto)
        {
            return _enterApp.Enter(dto);
        }

        [HttpPost]
        public void Record([FromBody] EnterInDTO dto)
        {
        }
    }
}
