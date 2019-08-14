using Microsoft.AspNetCore.Mvc;
using Park.App;
using Park.Entity.DTO;
using System.Threading.Tasks;

namespace Park.ParkApi.Controllers
{
    [Route("park/[controller]")]
    [ApiController]
    public class EntryController : ControllerBase
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
        public Task Post([FromBody] EntryInDTO dto)
        {
            return new EntryApp().Enter(dto);
        }

        [HttpPost]
        public void Record([FromBody] EntryInDTO dto)
        {
        }
    }
}
