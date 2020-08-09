using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elekta.Equipment.RemoteService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
