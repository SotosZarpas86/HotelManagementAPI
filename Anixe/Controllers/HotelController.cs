using Anixe.Core.Models;
using AnixeService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Anixe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : Controller
    {

        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<HotelModel>>> GetAll()
        {
            return Ok(await _hotelService.GetAll());
        }

        [HttpGet]
        [Route("{input}")]
        public async Task<ActionResult<IList<HotelModel>>> GetByInput(string input)
        {
            return Ok(await _hotelService.Get(input));
        }

        [HttpPost]
        public async Task<ActionResult<HotelModel>> Create([FromBody] HotelCreateModel hotel)
        {
            return Ok(await _hotelService.Create(hotel));
        }
    }
}
