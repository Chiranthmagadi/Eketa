using Microsoft.AspNetCore.Mvc;
using System;

namespace Elekta.Equipment.RemoteService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentAvailabilityController : ControllerBase
    {
        private readonly IService _service;
        public EquipmentAvailabilityController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetEquipmentAvailability(DateTime date)
        {
            var result = _service.IsEquipmentAvailable(date);
            return Ok(result);
        }
    }
}
