using Microsoft.AspNetCore.Mvc;
using Park.App;
using Park.Entity.DTO;
using System.Threading.Tasks;

namespace Park.ParkApi.Controllers
{
    [Route("park/[controller]")]
    [ApiController]
    public class ExitController : ControllerBase
    {
        ExitApp _exitApp = new ExitApp();

        [HttpGet]
        public void Get()
        {

        }

        [HttpGet("{plateNo}")]
        public void Get(string plateNo)
        {

        }

        [HttpPost]
        public Task<ExitOutDTO> Post([FromBody] ExitInDTO dto)
        {
            return _exitApp.Exit(dto);
        }

        [HttpPost]
        public void Record([FromBody] ExitInDTO dto)
        {
        }
    }
}
