using Microsoft.AspNetCore.Mvc;
using VroomAPI.Services.RentersService;
using VroomAPI.Services.RentersService.RentersDTO;

namespace VroomAPI.Controllers
{
    [ApiController]
    [Route("Vroom/[controller]")]
    public class RenterController : Controller
    {
        private readonly IRenterService _renterService;
        private readonly ILogger<RenterController> _renterControllerLogger;
        public RenterController(IRenterService renterService, ILogger<RenterController> renterControllerLogger)
        {
            _renterService = renterService;
            _renterControllerLogger = renterControllerLogger;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<RenterDTO>>? Get(int id)
        {
           var renter = await _renterService.GetRenterAsync(id);
            if (renter == null) 
            {
                return BadRequest();
            }
            return Ok(renter);
        }

        [HttpGet("Gets")]
        public async Task<ActionResult<IEnumerable<RenterDTO>>> Gets()
        {
            var renters = await _renterService.GetRentersAsync();
            if(renters == null)
            {
                return BadRequest();
            }
            return Ok(renters);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(RenterCreateDTO renterCreateDTO)
        {
           var result = await _renterService.AddRenterAsync(renterCreateDTO);
            if(result == Results.Created())
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _renterService.RemoveRenterAsync(id);
            if (result == Results.Ok())
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id,RenterUpdateDTO renterUpdateDTO)
        {
            IResult result = await _renterService.UpdateRenterAsync(id, renterUpdateDTO);
            return Ok(result);
        }
    }
}
